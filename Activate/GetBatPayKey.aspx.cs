using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IMSite.Data;
using Decryption;
using System.Collections.Generic;
using BackupCowWeb.Order;

namespace BackupCowWeb.Activate
{
    public partial class GetBatPayKey : System.Web.UI.Page
    {

        const string HttpReq_HardCode = "HardCode";
        const string HttpReq_OrderID = "OrderID";
        const char Split_HardCode = '|';
        const char Split_PcInfo = '}';

        const string Split_Result = "]";
        const string Split_Computer = "{";
        const string Split_ComputerInfo = "[";

        const string Result_OK = "OK";
        const string Result_Error = "Error";

        public class TActivePcInfo
        { 
            public string hardCode;
            public string pcName;
            public string pcID;
            public TActivePcInfo( string _hardCode, string _pcName )
            {
                hardCode = _hardCode;
                pcName = _pcName;
            }
            public void setPcID(string _pcID)
            {
                pcID = _pcID;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string hardCodeStr = HttpContext.Current.Request[ HttpReq_HardCode ];
            string orderID = HttpContext.Current.Request[ HttpReq_OrderID ];
            string ip = GetRealIP();

            if (string.IsNullOrEmpty(hardCodeStr) || string.IsNullOrEmpty(orderID))
            {
                Response.Clear();
                Response.Write("");
                Response.End();
                return;
            }

            // ��� Order �����ڣ� �� ��� ����
            if (!Order.Order.OrderExist(orderID))
                OrderMail.CheckOrderExist(orderID);

            char[] splitPc = new char[] { Split_HardCode };
            char[] splitPcInfo = new char[] { Split_PcInfo };
            string[] hardCodeList = hardCodeStr.Split(splitPc);
            List<TActivePcInfo> UnRegisterList = new List<TActivePcInfo>();

                // Ѱ��δ����� HardCode
            for (int i = 0; i < hardCodeList.Length; i++)
            {
                string pcStr = hardCodeList[i];
                string[] pcInfoList = pcStr.Split(splitPcInfo);
                string hardCode = pcInfoList[0];
                string pcName = pcInfoList[1];
                string pcID = "";
                if (pcInfoList.Length > 2)  // �汾����
                    pcID = pcInfoList[2];
                else
                if (hardCode.Length >= 20)
                    pcID = pcName;
                else
                    pcID = hardCode;
                

                if (!Activation.checkHardcodeExist(orderID, hardCode))  // δ����
                {
                    TActivePcInfo PcInfo = new TActivePcInfo(hardCode, pcName);
                    PcInfo.setPcID(pcID);
                    UnRegisterList.Add(PcInfo);
                }
                    
            }

            string ResultStr1 = "";
            string ResultStr2 = "";

            TOrderInfo OrderInfo = Order.Order.getOrderInfo(orderID); // ��ȡ Order ��Ϣ

                // ������ HardCode �ɹ�
            if (Order.Order.addActivateCount(orderID,UnRegisterList.Count))
            {
                for (int i = 0; i < UnRegisterList.Count; i++)
                {
                    TActivateInfo activateInfo = new TActivateInfo(orderID, UnRegisterList[i].hardCode);
                    activateInfo.setPcInfo(UnRegisterList[i].pcName, ip, UnRegisterList[i].pcID);
                    activateInfo.setActivateTime(DateTime.UtcNow);
                    Activation.addActivate(activateInfo); // ��¼�Ѽ����hardcode
                }

                string keyAll = "";
                for (int i = 0; i < hardCodeList.Length; i++)  // Ϊÿ�� hardcode ���� license
                {
                    string pcStr = hardCodeList[i];
                    string[] pcInfoList = pcStr.Split(splitPcInfo);
                    string hardCode = pcInfoList[0];
                    string pcID = "";
                    if (pcInfoList.Length > 2) // �汾����
                        pcID = pcInfoList[2];
                    else
                        pcID = hardCode;
                    DateTime oldActivateTime = Activation.getActivateTime(OrderInfo.orderID, hardCode);
                    string key = BigInteger.GetKey(hardCode, OrderInfo.edition, oldActivateTime, OrderInfo.days);
                    keyAll = keyAll + pcID + Split_ComputerInfo + key + Split_Computer;
                }

                ResultStr1 = Result_OK;
                ResultStr2 = keyAll;
            }
            else
            {
                ResultStr1 = Result_Error;
                if (OrderInfo == null)
                    ResultStr2 = GetPayKey.ErrorResult_NotExist;
                else
                {
                    int remainCount = OrderInfo.userCount + OrderInfo.activateCount - OrderInfo.activateCount;
                    ResultStr2 = String.Format(GetPayKey.ErrorResult_UserLack, remainCount);
                }

            }

            string ResultStr = ResultStr1 + Split_Result + ResultStr2;

            Response.Clear();
            Response.Write(ResultStr);
            Response.End();
            
                
        }

        #region ��ȡ��ʵIP

        static public string GetRealIP()
        {
            string userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIP) || userIP.IndexOf("unknown", StringComparison.OrdinalIgnoreCase) > -1)
            {
                userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                if (string.IsNullOrEmpty(userIP) || userIP.IndexOf("unknown", StringComparison.OrdinalIgnoreCase) > -1)
                    userIP = System.Web.HttpContext.Current.Request.UserHostAddress;
                else if (userIP.IndexOf(",") > -1)
                    userIP = userIP.Substring(0, userIP.IndexOf(","));
            }
            else if (userIP.IndexOf(",") > -1)
                userIP = userIP.Substring(0, userIP.IndexOf(","));

            return userIP.Trim();
        }

        #endregion
    }    
}

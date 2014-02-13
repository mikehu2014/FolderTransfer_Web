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
using BackupCowWeb.Order;
using Decryption;

namespace BackupCowWeb.Activate
{
    public partial class GetLicenseKey : System.Web.UI.Page
    {
        const char Split_hardCode = ':';  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string orderID = Request.QueryString["OrderID"];
                string hardCode = Request.QueryString["HardCode"];
                if (!string.IsNullOrEmpty(orderID) && !string.IsNullOrEmpty(hardCode))
                {
                    txtOrderID.Text = orderID;
                    txtHardCode.Text = hardCode;
                    btnGetLicense_Click(sender, e);
                }
            }
        }

        protected void btnGetLicense_Click(object sender, EventArgs e)
        {
            string orderID = txtOrderID.Text.Trim();
            string hardCode = txtHardCode.Text.Trim();

            TOrderInfo o = Order.Order.getOrderInfo(orderID);

            if (o == null)
            {
                lbResult.Text = GetPayKey.ErrorResult_NotExist;
                return;
            }

            char[] hardCodeSplit = new char[] { Split_hardCode };
            string[] hardCodeList = hardCode.Split(hardCodeSplit);
            if( hardCodeList.Length != 2 )
            {
                lbResult.Text = GetPayKey.ErrorResult_NotExist;
                return;
            }

            string MacAdressStr = hardCodeList[0];
            string PcID = hardCodeList[1];

            TActivateInfo ActivateInfo = new TActivateInfo(orderID, MacAdressStr);
            ActivateInfo.setActivateTime(DateTime.UtcNow);
            ActivateInfo.setPcInfo("", "", PcID);

            if (!Activation.activatePc(ActivateInfo))  // 没有足够的剩余激活数量
            {
                lbResult.Text = String.Format(GetPayKey.ErrorResult_UserLack, 0);
                return;
            }

            lbResult.Text = "";
            DateTime oldActivateTime = Activation.getActivateTime(orderID, MacAdressStr);
            string key = BigInteger.GetKey(MacAdressStr, o.edition, oldActivateTime, o.days);
            txtLicense.Text = key;
        }
    }
}

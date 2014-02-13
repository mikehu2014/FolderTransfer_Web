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

namespace BackupCowWeb.Activate
{
    public partial class GetTrialKey : System.Web.UI.Page
    {
        const int trialDay = 5;
        const string HttpReq_HardCode = "HardCode";
        const string HttpReq_PcName = "PcName";
        const string HttpReq_PcID = "PcID";
        const string HttpReq_WinOS = "WinOS";

        const string Split_Result = "|";
        const string Result_True = "True";
        const string Result_False = "False";

        protected void Page_Load(object sender, EventArgs e)
        {
            string hardcode = HttpContext.Current.Request[ HttpReq_HardCode ];
            string pcName = HttpContext.Current.Request[HttpReq_PcName];
            string pcID = HttpContext.Current.Request[HttpReq_PcID];
            string winOS = HttpContext.Current.Request[HttpReq_WinOS];
            string ip = GetRealIP();

            if (string.IsNullOrEmpty(hardcode))
            {
                Response.Clear();
                Response.Write("");
                Response.End();
                return;
            }

                // 版本兼容
            if (string.IsNullOrEmpty(pcID))
                if (hardcode.Length >= 20)
                    pcID = pcName;
                else
                    pcID = hardcode;
            
                
                // 记录试用信息
            if (!TrialHandle.checkExist(hardcode, pcName))
            {
                    // 试用的详细信息
                TrialInfo t = new TrialInfo(hardcode);
                t.setPcInfo(pcName, ip);
                t.setTrialTime(DateTime.UtcNow);
                t.setPcID(pcID);
                t.setWinOS(winOS);
                
                TrialHandle.addTrial(t);

                    // 试用的统计信息
                TrialDaily td = new TrialDaily();
                td.Upgrade();
            };

            DateTime trialTime = TrialHandle.getTrialTime(hardcode);
            TimeSpan ts = DateTime.Now - trialTime;
            string key = BigInteger.GetKey(hardcode, 0, trialTime, trialDay);
            string Result = Result_True;
            if (ts.TotalDays > trialDay)
                Result = Result_False;

            string ResultStr = Result + Split_Result + key;

            Response.Clear();
            Response.Write(ResultStr);
            Response.End();
        }

        #region 获取真实IP

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

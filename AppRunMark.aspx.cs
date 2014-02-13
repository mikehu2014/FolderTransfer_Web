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

public partial class AppRunMark : System.Web.UI.Page
{   
    static string HttpMarkRun_PcID = "PcID";
    static string HttpMarkRun_PcName = "PcName";
    static string HttpMarkRun_NetworkMode = "NetworkMode";
    static string HttpMarkRun_NetworkPc = "NetworkPc";
    static string HttpMarkRun_SendFile = "SendFile";
    static string HttpMarkRun_ShareDown = "ShareDown";
    static string HttpMarkRun_AdsShowCount = "AdsShowCount";
    static string HttpMarkRun_WinOS = "WinOS";
    static string HttpMarkRun_Status = "Status";


    protected void Page_Load(object sender, EventArgs e)
    {
        string PcID = HttpContext.Current.Request[HttpMarkRun_PcID];
        string PcName = HttpContext.Current.Request[HttpMarkRun_PcName];
        string IP = GetRealIP();
        string NetworkMode = HttpContext.Current.Request[HttpMarkRun_NetworkMode];
        string NetworkPcStr = HttpContext.Current.Request[HttpMarkRun_NetworkPc];
        string SendFileStr = HttpContext.Current.Request[HttpMarkRun_SendFile];
        string ShareDownFileStr = HttpContext.Current.Request[HttpMarkRun_ShareDown];
        string AdsShowStr = HttpContext.Current.Request[HttpMarkRun_AdsShowCount];
        string WinOS = HttpContext.Current.Request[HttpMarkRun_WinOS];
        string Status = HttpContext.Current.Request[HttpMarkRun_Status];

        if (( string.IsNullOrEmpty(PcID) || string.IsNullOrEmpty(PcName))||
           (  string.IsNullOrEmpty(NetworkMode) || string.IsNullOrEmpty(NetworkPcStr) || 
              string.IsNullOrEmpty(SendFileStr) || string.IsNullOrEmpty(ShareDownFileStr)))
        {
            Response.Clear();
            Response.Write("");
            Response.End();
            return;
        }

        int NetworkPcCount = 0;
        int.TryParse(NetworkPcStr, out NetworkPcCount);
        int SendFileCount = 0;
        int.TryParse(SendFileStr, out SendFileCount);
        int ShareDownCount = 0;
        int.TryParse(ShareDownFileStr, out ShareDownCount);
        int AdsShowCount = 0;
        int.TryParse(AdsShowStr, out AdsShowCount);

        AppUserInfo au = new AppUserInfo(PcID, PcName, WinOS);
        au.SetNetworkInfo(IP, NetworkMode, NetworkPcCount);
        au.SetTransferInfo(SendFileCount, ShareDownCount, AdsShowCount);
        au.setStatus(Status);
        au.MarkApp();

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

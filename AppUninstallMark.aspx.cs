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

public partial class AppUninstallMark : System.Web.UI.Page
{
    static string HttpUninstallApp_PcID = "PcID";
    static string HttpUninstallApp_Reasons = "Reasons";
    static string HttpUninstallApp_Suggestions = "Suggestions";
    static string HttpUninstallApp_Email = "Email";

    string PcID;
    string Reasons, Suggestions, Email;

    protected void Page_Load(object sender, EventArgs e)
    {
        PcID = HttpContext.Current.Request[HttpUninstallApp_PcID];
        Reasons = HttpContext.Current.Request[HttpUninstallApp_Reasons];
        Suggestions = HttpContext.Current.Request[HttpUninstallApp_Suggestions];
        Email = HttpContext.Current.Request[HttpUninstallApp_Email];
        
        // PcID 是否已存在
        if ( string.IsNullOrEmpty(PcID))
        {
            Response.Clear();
            Response.Write("");
            Response.End();
            return;
        }

        // 添加卸载列表
        if (ReadIsExist())
            UpgradePc();
        else
            AddPc();
    }

    // 是否存在
    private Boolean ReadIsExist()
    {
        string sql = "SELECT * FROM UserUninstall Where ( PcID = '" + PcID + "' )";
        Conn con = new Conn();
        DataTable dt = con.ExecuteDataTable(sql, false);
        return (dt.Rows.Count > 0);
    }

    // 添加
    private void AddPc()
    {
        string sql = "Insert Into UserUninstall ( PcID, Reason, Suggestion, Email, UninstallTime ) Values ";
        sql = sql + "('" + PcID + "', '" + Reasons + "', '" + Suggestions + "', '" + Email + "', '" + DateTime.Now + "')";
        Conn con = new Conn();
        con.ExecuteNonQueryD(sql);
    }

    private void UpgradePc()
    {
        string sql = "UPDATE UserUninstall SET Reason = '" + Reasons + "', Email = '" + Email + "', Suggestion = '" + Suggestions;
        sql = sql + "' where PcID = '" + PcID + "'";
        Conn con = new Conn();
        con.ExecuteNonQueryD(sql);
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

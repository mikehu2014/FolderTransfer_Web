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
using System.Data.SqlClient;

public partial class AdsMark : System.Web.UI.Page
{
    static string HttpMarkRun_ProductName = "ProductName";
    static string HttpMarkRun_PcID = "PcID";
    static string HttpMarkRun_PcName = "PcName";

    protected void Page_Load(object sender, EventArgs e)
    {
        string ProductName = HttpContext.Current.Request[HttpMarkRun_ProductName];
        string PcID = HttpContext.Current.Request[HttpMarkRun_PcID];
        string PcName = HttpContext.Current.Request[HttpMarkRun_PcName];

        if ((string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(PcID) || string.IsNullOrEmpty(PcName)))
        {
            Response.Clear();
            Response.Write("");
            Response.End();
            return;
        }

        string IP = GetRealIP();

            // 是否之前已点击
        if (getIsExist(ProductName, PcID)) 
            UpdateAds(ProductName, PcID, IP);  // 更新点击时间
        else
            AddAds(ProductName, PcID, PcName, IP);  // 添加点击
    }

    private Boolean getIsExist(string ProductName, string PcID)
    {
        Conn con = new Conn();
        string sql = "SELECT count(*) FROM AdsClick Where ( PcID = '" + PcID + "' and ProductName = '" + ProductName + "' )";
        int Count = 0;
        try
        {
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                int.TryParse(dt.Rows[0][0].ToString(), out Count);
        }
        catch { }
        return Count > 0;
    }

    private void AddAds(string ProductName, string PcID, string PcName, string Ip)
    {
        Conn con = new Conn();
        string sql = "insert into AdsClick ( ProductName, PcID, PcName, IP, ClickDate ) values ";
        sql = sql + "('" + ProductName + "', '" + PcID + "', '" + PcName + "', '" + Ip + "', '" + DateTime.Now + "')";
        try
        {
            con.ExecuteNonQueryD(sql);
        }
        catch { }
    }

    private void UpdateAds(string ProductName, string PcID, string Ip)
    {
        string sql = "UPDATE AdsClick SET ClickDate = '" + DateTime.Now + "', IP = '" + Ip + "' ";
        sql = sql + "where ( PcID = '" + PcID + "' and ProductName = '" + ProductName + "' )";

        Conn con = new Conn();

        try
        {
            con.ExecuteNonQueryD(sql);
        }
        catch { };  

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

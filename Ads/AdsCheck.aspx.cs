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

public partial class Ads_AdsCheck : System.Web.UI.Page
{
    static string HttpStr_ProductName = "productname";
    static string HttpStr_Ip = "ip";

    static string HttpResult_Yes = "yes";
    static string HttpResult_No = "no";

    protected void Page_Load(object sender, EventArgs e)
    {
        string ProcductName = Request.QueryString[HttpStr_ProductName];
        string Ip = Request.QueryString[HttpStr_Ip];
        string ResultStr = "";

        if (getIsExist(ProcductName, Ip))
            ResultStr = HttpResult_Yes;
        else
            ResultStr = HttpResult_No;

        Response.Clear();
        Response.Write(ResultStr);
        Response.End();
    }

    private Boolean getIsExist(string ProductName, string Ip)
    {
        Conn con = new Conn();
        string sql = "SELECT ClickDate FROM AdsClick Where ( ProductName = '" + ProductName + "' and Ip = '" + Ip + "' )";
        DateTime LastRun = DateTime.MinValue;
        try
        {
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                DateTime.TryParse(dt.Rows[0][0].ToString(), out LastRun);
        }
        catch { LastRun = DateTime.MinValue; }
        TimeSpan ts = DateTime.Now - LastRun;
        return ts.TotalHours <= 5;
    }

}

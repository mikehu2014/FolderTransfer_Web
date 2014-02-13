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
//using BackupCowWeb.Mail;
using System.Net.Mail;
using System.Net;
using IMSite.Data;
using System.Data.SqlClient;


public partial class TestApp : System.Web.UI.Page
{
    static string HttpStr_cmd = "cmd";
    static string HttpStr_startdate = "startdate";
    static string HttpStr_stopdate = "stopdate";

    static string Cmd_getip = "getip";

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = HttpContext.Current.Request[HttpStr_cmd];
        string StartDate = HttpContext.Current.Request[HttpStr_startdate];
        string StopDate = HttpContext.Current.Request[HttpStr_stopdate];

        if (string.IsNullOrEmpty(Cmd) || string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(StartDate)||(Cmd != Cmd_getip)) 
        {
            Response.Clear();
            Response.Write("");
            Response.End();
            return;
        }

        string sql = "select distinct IP from Trial where TrialDate between '" + StartDate + "' and '" + StopDate + "'";
        Conn con = new Conn();
        SqlDataReader sdr = con.ExecuteReaderD(sql);
        string ResultStr = "";
        while (sdr.Read())
        {
            if (ResultStr != "")
                ResultStr = ResultStr + ",";
            ResultStr = ResultStr + sdr[0].ToString();
        }
        sdr.Close();

        Response.Clear();
        Response.Write(ResultStr);
        Response.End();
    }
}

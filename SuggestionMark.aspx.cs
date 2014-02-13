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

public partial class SuggestionMark : System.Web.UI.Page
{
    static string HttpUninstallApp_PcID = "PcID";
    static string HttpUninstallApp_Suggestions = "Suggestions";
    static string HttpUninstallApp_Email = "Email";

    string PcID;
    string Suggestions, Email;

    protected void Page_Load(object sender, EventArgs e)
    {
        PcID = HttpContext.Current.Request[HttpUninstallApp_PcID];
        Suggestions = HttpContext.Current.Request[HttpUninstallApp_Suggestions];
        Email = HttpContext.Current.Request[HttpUninstallApp_Email];

        // PcID 是否已存在
        if (string.IsNullOrEmpty(PcID)||ReadIsExist())
        {
            Response.Clear();
            Response.Write("");
            Response.End();
            return;
        }

        // 添加
        AddSuggestion();
    }

    // 是否存在
    private Boolean ReadIsExist()
    {
        string sql = "SELECT * FROM UserSuggestion Where ( PcID = '" + PcID + "' ) and ( Suggestion = '" + Suggestions;
        sql = sql + "' ) and ( Email = '" + Email + "' )";
        Conn con = new Conn();
        DataTable dt = con.ExecuteDataTable(sql, false);
        return (dt.Rows.Count > 0);
    }

    // 添加
    private void AddSuggestion()
    {
        string sql = "Insert Into UserSuggestion ( PcID, Suggestion, Email ) Values ";
        sql = sql + "('" + PcID + "', '" + Suggestions + "', '" + Email + "')";
        Conn con = new Conn();
        con.ExecuteNonQueryD(sql);
    }
}

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

public partial class AppUninstallList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbLastDayUninistall.Text = getDayUninstall(DateTime.Now.AddDays(-1).Date);
            lbTodayUninistall.Text = getDayUninstall(DateTime.Now.Date);
            lbLastDayInistall.Text = getDayTrial(DateTime.Now.AddDays(-1).Date);
            lbTodayInistall.Text = getDayTrial(DateTime.Now.Date);
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int IpNum = 12;
        int LocationNum = 13;

        try
        {
            if (e.Row.RowIndex != -1)
            {
                string city, country;
                Conn conn = new Conn();
                if (string.Compare(e.Row.Cells[IpNum].Text, "unknown", true) != 0
                    && !string.IsNullOrEmpty(e.Row.Cells[IpNum].Text) && string.Compare(e.Row.Cells[IpNum].Text, "&nbsp;", true) != 0)
                {
                    conn.AddParamWithValue("@IP", e.Row.Cells[IpNum].Text);
                    SqlParameter pCountry = conn.AddParamWithValue("@country", String.Empty);
                    SqlParameter pCity = conn.AddParamWithValue("@city", String.Empty);
                    pCountry.Size = 100;
                    pCity.Size = 100;
                    pCountry.Direction = ParameterDirection.Output;
                    pCity.Direction = ParameterDirection.Output;
                    conn.CommandType = CommandType.StoredProcedure;
                    conn.ExecuteNonQueryD("GetIpInfo");
                    if (pCity.Value == null)
                        city = string.Empty;
                    else
                        city = pCity.Value.ToString();
                    if (pCountry.Value == null)
                        country = string.Empty;
                    else
                        country = pCountry.Value.ToString();

                    if (string.IsNullOrEmpty(city))
                        e.Row.Cells[LocationNum].Text = country;
                    else
                        e.Row.Cells[LocationNum].Text = city + ", " + country;
                }

            }
        }
        catch {
            e.Row.Cells[LocationNum].Text = "unknown";    
        }
    }

    private string getDayUninstall(DateTime dt)
    {
        string sql = "select count(*) from UserUninstall where UninstallTime between '" + dt + "' and ";
        sql = sql + " '" + dt.AddDays(1) + "'";

        int trialCount = 0;

        try
        {
            Conn con = new Conn();
            DataTable dtb = con.ExecuteDataTable(sql, false);
            if (dtb.Rows.Count > 0)
                int.TryParse(dtb.Rows[0][0].ToString(), out trialCount);
        }
        catch { };

        return "" + trialCount;
    }

    private string getDayTrial(DateTime dt)
    {
        string sql = "select count(*) from Trial where trialdate between '" + dt + "' and ";
        sql = sql + " '" + dt.AddDays(1) + "'";

        int trialCount = 0;

        try
        {
            Conn con = new Conn();
            DataTable dtb = con.ExecuteDataTable(sql, false);
            if (dtb.Rows.Count > 0)
                int.TryParse(dtb.Rows[0][0].ToString(), out trialCount);
        }
        catch { };

        return "" + trialCount;
    }
}

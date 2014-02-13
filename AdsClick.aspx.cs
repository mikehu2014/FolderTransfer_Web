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

public partial class AdsDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int IpNum = 2;
        int LocationNum = 3;

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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

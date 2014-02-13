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

namespace WebApplication1
{
    public partial class _Default : IMSite.Admin.SupperAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                lbLastDay.Text = getDayTrial(DateTime.Now.AddDays(-1).Date);
                lbToday.Text = getDayTrial(DateTime.Now.Date);
                lbLastSeven.Text = getTwoDateUser(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1));
                lbLastMonth.Text = getTwoDateUser(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(-1));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string hardCode = txtHardCode.Text.Trim();
            string pcName = txtPcName.Text.Trim();

            Boolean isEmptyHardCode = string.IsNullOrEmpty(hardCode);
            Boolean isEmptyPcName = string.IsNullOrEmpty(pcName);
            Boolean notSearch = isEmptyHardCode && isEmptyPcName;

            if (notSearch)
                return;

            string sql = "SELECT * FROM Trial Where ";
            if (!isEmptyHardCode)
            {
                sql = sql + "( HardCode = '" + hardCode + "' )";
            }

            if (!isEmptyPcName)
            {
                if (!isEmptyHardCode)
                    sql = sql + " and ";
                sql = sql + "( PcName = '" + pcName + "' ) ";
            }
            sql = sql + "ORDER BY trialdate DESC";
        
            SqlDataSource1.SelectCommand = sql;
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }

        protected void btnViewall_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM Trial ORDER BY trialdate DESC";
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView1.SelectedIndex = e.RowIndex;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int IpNum = 4;
            int LocationNum = 5;

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

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable("select count(*) from Trial", false);
            if (dt.Rows.Count > 0)
                Label2.Text = "Trial Count: " + dt.Rows[0][0].ToString();
            //dt = con.ExecuteDataTable("select count(*) from Trial where Isvalid = 1", false);
            //if (dt.Rows.Count > 0)
            //    Label3.Text = "Verified Count: " + dt.Rows[0][0].ToString();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private string getTwoDateUser(DateTime dt1, DateTime dt2)
        {
            int UseCount = 0;
            int DayCount = 0;

            string sql = "select sum(UserCount), count(*) from TrialDaily where Time between '" + dt1 + "' and '" + dt2 + "'";
            try
            {
                Conn con = new Conn();
                SqlDataReader sdr = con.ExecuteReaderD(sql);
                if (sdr.Read())
                {
                    int.TryParse(sdr[0].ToString(), out UseCount);
                    int.TryParse(sdr[1].ToString(), out DayCount);
                }
                sdr.Close();
            }
            catch { };

            if (DayCount > 0)
                UseCount = UseCount / DayCount;
            return "" + UseCount;
        }

        protected void btnDailyAnaly_Click(object sender, EventArgs e)
        {

        }
        protected void btnCountryAnaylze_Click(object sender, EventArgs e)
        {

        }
        protected void btnTrialOne_Click(object sender, EventArgs e)
        {

        }
}
}

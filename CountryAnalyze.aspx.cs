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
using System.Collections.Generic;

public partial class CountryAnalyze : IMSite.Admin.SupperAdminPage
{
    const string Type_Month = "Month";
    const string Type_TenDay = "TenDay";

    const string Cmd_Refresh = "refresh";
    static Boolean IsMonthShow = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];

        if (Cmd == Cmd_Refresh) // 刷新
        {
            int YearNow = 2013;
            int.TryParse(ddlYear.SelectedValue, out YearNow);
            refreshMonth( YearNow );
            refreshTenDay( YearNow );
        }

        if (!IsPostBack)
        {
            selectDdlYear();
            ShowMonth(DateTime.Now.Month);
        }
    }

    private void selectDdlYear()
    {
        string YearNow = "" + DateTime.Now.Year;
        for (int i = 0; i < ddlYear.Items.Count - 1; i++)
        {
            if (ddlYear.Items[i].Value == YearNow)
            {
                ddlYear.SelectedIndex = i;
                return;
            }
        }
    }

    private string getLocationStr(string ip)
    {
        string country = "";
        Conn conn = new Conn();
        if (string.Compare(ip, "unknown", true) != 0
            && !string.IsNullOrEmpty(ip) && string.Compare(ip, "&nbsp;", true) != 0)
        {
            conn.AddParamWithValue("@IP", ip);
            SqlParameter pCountry = conn.AddParamWithValue("@country", String.Empty);
            SqlParameter pCity = conn.AddParamWithValue("@city", String.Empty);
            pCountry.Size = 100;
            pCity.Size = 100;
            pCountry.Direction = ParameterDirection.Output;
            pCity.Direction = ParameterDirection.Output;
            conn.CommandType = CommandType.StoredProcedure;
            conn.ExecuteNonQueryD("GetIpInfo");
            if (pCountry.Value == null)
                country = string.Empty;
            else
                country = pCountry.Value.ToString();
        }

        return country;
    }

    public class CountryInfo
    {
        public string Location;
        public int Count;
        public CountryInfo( string _Location )
        {
            Location = _Location;
            Count = 1;
        }
        public void AddCount()
        {
            Count++;
        }
    }

    private void refreshMonth(int Year_Now)
    {
        for (int i = 1; i <= 12; i++)
        {
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > i));
            if (!IsOnTime)
                continue;

            // 查询是否存在月份数据
            string sql = "select * from CountryAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
            sql = sql + " and ( Number = " + i + " )";
            Conn con = new Conn();
            try
            {
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    continue;
            }
            catch { }

            // 获取月份数据
            sql = "select IP from Trial where ( year(TrialDate) = " + Year_Now + " )  and ( month(TrialDate) = " + i + " )";
            Dictionary<string,CountryInfo> CountryList = new Dictionary<string,CountryInfo>();
            try
            {
                SqlDataReader sdr = con.ExecuteReaderD(sql);
                while (sdr.Read())
                {
                    string ip = sdr[0].ToString();
                    string country = getLocationStr(ip);
                    if (country == string.Empty)
                        continue;
                    if (CountryList.ContainsKey(country))
                        CountryList[country].AddCount();
                    else
                    {
                        CountryInfo NewCountry = new CountryInfo(country);
                        CountryList.Add(country, NewCountry);
                    }
                }
                sdr.Close();
            }
            catch { }

            // 添加月份数据
            foreach( KeyValuePair<string,CountryInfo> c in CountryList )
            {
                sql = "insert into CountryAnalyze ( Year, Type, Number, Country, Value ) values ( ";
                sql = sql + Year_Now + ", '" + Type_Month + "', " + i + ", '" + c.Value.Location + "', " + c.Value.Count + " )";
                try
                {
                    con.ExecuteNonQueryD(sql);
                }
                catch { }

            }

        }
    
    }

    private void refreshTenDay(int Year_Now)
    {
        for (int i = 1; i <= 12; i++)
        {

            for (int j = 1; j <= 3; j++)
            {
                int DayOnTime = 100;
                if (j < 3)
                    DayOnTime = j * 10;

                Boolean IsOnTime = DateTime.Now.Year > Year_Now;
                IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > i));
                IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month == i) && (DateTime.Now.Day > DayOnTime));
                if (!IsOnTime)
                    continue;

                int DayNumber = (i - 1) * 3 + j;

                // 查询是否存在月份数据
                string sql = "select * from CountryAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
                sql = sql + " and ( Number = " + DayNumber + " )";
                Conn con = new Conn();
                try
                {
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        continue;
                }
                catch { }

                int StartDay = (j - 1) * 10 + 1;
                int EndDay = (j * 10);
                if (j == 3)
                    EndDay = EndDay + 1;

                // 获取月份数据
                sql = "select IP from Trial where ( year(TrialDate) = " + Year_Now + " ) and ";
                sql = sql +"( month(TrialDate) = " + i + " ) and ( day(TrialDate) between " + StartDay + " and " + EndDay + " )";
                Dictionary<string, CountryInfo> CountryList = new Dictionary<string, CountryInfo>();
                try
                {
                    SqlDataReader sdr = con.ExecuteReaderD(sql);
                    while (sdr.Read())
                    {
                        string ip = sdr[0].ToString();
                        string country = getLocationStr(ip);
                        if (country == string.Empty)
                            continue;
                        if (CountryList.ContainsKey(country))
                            CountryList[country].AddCount();
                        else
                        {
                            CountryInfo NewCountry = new CountryInfo(country);
                            CountryList.Add(country, NewCountry);
                        }
                    }
                    sdr.Close();
                }
                catch { }


                // 添加月份数据
                foreach (KeyValuePair<string, CountryInfo> c in CountryList)
                {
                    sql = "insert into CountryAnalyze ( Year, Type, Number, Country, Value ) values ( ";
                    sql = sql + Year_Now + ", '" + Type_TenDay + "', " + DayNumber + ", '" + c.Value.Location + "', " + c.Value.Count + " )";
                    try
                    {
                        con.ExecuteNonQueryD(sql);
                    }
                    catch { }

                }

            }
        }
    }

    private void ShowMonth(int MonthNumber)
    {
        IsMonthShow = true;

        string YearShowStr = ddlYear.Text;

        string sql1 = "SELECT * FROM CountryAnalyze WHERE ( ( Year = " + YearShowStr + " ) And ( Type = '" + Type_Month + "' ) AND ( Number = ";
        string sql2 = " ) ) ORDER BY Value DESC, Country";

        int MonthNum = MonthNumber;

        MonthNum = MonthNum - 1;
        if (MonthNum < 1)
            MonthNum = 1;
        if (MonthNum + 2 > 12)
            MonthNum = 10;

        SqlDataSource1.SelectCommand = sql1 + MonthNum + sql2;
        SqlDataSource1.DataBind();
        GridView1.DataBind();

        SqlDataSource2.SelectCommand = sql1 + (MonthNum + 1) + sql2;
        SqlDataSource2.DataBind();
        GridView2.DataBind();

        SqlDataSource3.SelectCommand = sql1 + (MonthNum + 2) + sql2;
        SqlDataSource3.DataBind();
        GridView3.DataBind();
    }

    protected void btnMonth_Click(object sender, EventArgs e)
    {
        int MonthNum = 1;
        int.TryParse(ddlMonth.Text, out MonthNum);
        ShowMonth(MonthNum);
    }

    protected void btnTenDays_Click(object sender, EventArgs e)
    {
        IsMonthShow = false;

        string YearShowStr = ddlYear.Text;

        string sql1 = "SELECT * FROM CountryAnalyze WHERE ( ( Year = " + YearShowStr + " ) And ( Type = '" + Type_TenDay + "' ) AND ( Number = ";
        string sql2 = " ) ) ORDER BY Value DESC, Country";

        int MonthNum = 1;
        int.TryParse(ddlMonth.Text, out MonthNum);

        int DayNum = (MonthNum - 1) * 3 + 1;

        SqlDataSource1.SelectCommand = sql1 + DayNum + sql2;
        SqlDataSource1.DataBind();
        GridView1.DataBind();

        SqlDataSource2.SelectCommand = sql1 + (DayNum + 1) + sql2;
        SqlDataSource2.DataBind();
        GridView2.DataBind();

        SqlDataSource3.SelectCommand = sql1 + (DayNum + 2) + sql2;
        SqlDataSource3.DataBind();
        GridView3.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

        if (!IsMonthShow)
        {
            int DayCount = 0;
            int.TryParse(e.Row.Cells[1].Text, out DayCount);
            int Num = ((DayCount - 1) % 3) + 1;
            DayCount = ((DayCount - 1) / 3) + 1;
            e.Row.Cells[1].Text = DayCount + "-" + Num;

        }
    }
}

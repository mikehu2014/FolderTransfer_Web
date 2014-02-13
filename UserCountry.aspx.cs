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

public partial class UserCountry : System.Web.UI.Page
{
    static private int Year_Now = 2013;
    const string Type_Month = "Month";
    const string Type_TenDay = "TenDay";

    const string Cmd_Refresh = "refresh";

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];

        if (Cmd == Cmd_Refresh) // 刷新
        {
            RefreshMonthUser();
            RefreshTenDayUser();
        }
    }

    public class CountryInfo
    {
        public string Location;
        public int Count;
        public CountryInfo(string _Location)
        {
            Location = _Location;
            Count = 1;
        }
        public void AddCount()
        {
            Count++;
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

    private void RefreshMonthUser()
    {

        for (int i = 1; i <= 12; i++)
        {
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > i));
            if (!IsOnTime)
                continue;

            Boolean IsExistMonth = false;

            // 查询是否存在月份数据
            string sql = "select * from UserCountry Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
            sql = sql + " and ( Number = " + i + " )";
            Conn con = new Conn();
            try
            {
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    IsExistMonth = true;
            }
            catch { }

            // 插入月份数据
            if (!IsExistMonth)
            {
                // 添加月份数据
                sql = "insert into UserCountry ( Year, Type, Number ) values ( " + Year_Now + ", '" + Type_Month + "', " + i + " )";
                try
                {
                    con.ExecuteNonQueryD(sql);
                }
                catch { }
            }
            else
                return; // 已存在则不刷新

            string FirstUseStr = "'" + Year_Now + "-" + (i + 1) + "-1'";
            string LastUseStr = "'" + Year_Now + "-" + i + "-1'";

            // 获取月份数据
            sql = "select IP from AppUser where ( FirstUse < " + FirstUseStr + " ) and ( LastUse >= " + LastUseStr + " )";
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
            catch { };


            // 添加月份数据
            foreach( KeyValuePair<string,CountryInfo> c in CountryList )
            {
                sql = "insert into UserCountry ( Year, Type, Number, Country, Value ) values ( ";
                sql = sql + Year_Now + ", '" + Type_Month + "', " + i + ", '" + c.Value.Location + "', " + c.Value.Count + " )";
                try
                {
                    con.ExecuteNonQueryD(sql);
                }
                catch { }

            }
        }
    }

    private void RefreshTenDayUser()
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

                Boolean IsExistTenDay = false;

                // 查询是否存在月份数据
                string sql = "select * from UserCountry Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
                sql = sql + " and ( Number = " + DayNumber + " )";
                Conn con = new Conn();
                try
                {
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        IsExistTenDay = true;
                }
                catch { }


                if (!IsExistTenDay)
                {
                    // 添加月份数据
                    sql = "insert into UserCountry ( Year, Type, Number ) values ( " + Year_Now + ", '" + Type_TenDay + "', " + DayNumber + " )";
                    try
                    {
                        con.ExecuteNonQueryD(sql);
                    }
                    catch { }
                }
                else
                    return; // 已存在则不刷新


                int StartDay = (j - 1) * 10 + 1;
                int EndDay = (j * 10) + 1;
                int m = i;
                if (j == 3)
                {
                    EndDay = 1;
                    m = m + 1;
                }


                string FirstUseStr = "'" + Year_Now + "-" + m + "-" + EndDay + "'";
                string LastUseStr = "'" + Year_Now + "-" + i + "-" + StartDay + "'";

                // 获取月份数据
                sql = "select IP from AppUser where ( FirstUse < " + FirstUseStr + " ) and ( LastUse >= " + LastUseStr + " )";
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
                catch { };


                // 添加月份数据
                foreach (KeyValuePair<string, CountryInfo> c in CountryList)
                {
                    sql = "insert into UserCountry ( Year, Type, Number, Country, Value ) values ( ";
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

    protected void btnMonth_Click(object sender, EventArgs e)
    {

    }
    protected void btnTenDays_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}

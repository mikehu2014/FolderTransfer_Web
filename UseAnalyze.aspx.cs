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
using System.Drawing;

public partial class UseAnalyze : IMSite.Admin.SupperAdminPage
{
    private Boolean IsShowTenDay = false;
    const string Type_Month = "Month";
    const string Type_TenDay = "TenDay";

    const string Cmd_Refresh = "refresh";

    static private string SqlSelect = "";

    const int Col_Month = 1;
    const int Col_UseCount = 2;
    const int Col_UseChange = 3;
    const int Col_OrderUse = 4;
    const int Col_LostCount = 5;
    const int Col_OrderLost = 6;
    const int Col_TrialLost = 7;

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];

        if (Cmd == Cmd_Refresh) // 刷新
        {
            int YearNow = 2013;
            int.TryParse(ddlYear.SelectedValue, out YearNow);
            RefreshMonthUser(YearNow);
            RefreshMonthLostUse(YearNow);
            RefreshTenDayUser(YearNow);
            RefreshTenDayLostUse(YearNow);
        }

        if (!IsPostBack)
        {
            selectDdlYear();
            string YearStr = ddlYear.Text;
            IsShowTenDay = false;
            SqlSelect = "SELECT * FROM UseAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_Month + "' ) )";
        }

        SqlDataSource1.SelectCommand = SqlSelect;
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
    private void RefreshMonthUser(int Year_Now)
    {

        for (int i = 1; i <= 12; i++)
        {
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > i));
            if (!IsOnTime)
                continue;

            Boolean IsExistMonth = false;

            // 查询是否存在月份数据
            string sql = "select * from UseAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
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
                sql = "insert into UseAnalyze ( Year, Type, Number ) values ( " + Year_Now + ", '" + Type_Month + "', " + i + " )";
                try
                {
                    con.ExecuteNonQueryD(sql);
                }
                catch { }           
            }

            string FirstUseStr = "'" + Year_Now + "-" + ( i + 1 ) + "-1'";
            if(i==12)
                FirstUseStr = "'" + (Year_Now+1) + "-1-1'";
            string LastUseStr = "'" + Year_Now + "-" + i + "-1'";

            // 获取月份数据
            sql = "select count(*) from AppUser where ( FirstUse < " + FirstUseStr + " ) and ( LastUse >= " + LastUseStr + " )";
            int Count = 0;
            try
            {
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out Count);
            }
            catch { }

            // 获取月份数据
            sql = "select count(*) from AppUser inner join Activate ON AppUser.PcID = Activate.PcID where ( AppUser.FirstUse < " + FirstUseStr + " ) and ( AppUser.LastUse >= " + LastUseStr + " )";
            int OrderCount = 0;
            try
            {
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out OrderCount);
            }
            catch { }

            // 添加月份数据
            sql = "update UseAnalyze set UseCount = " + Count + ", OrderUse = " + OrderCount + " where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
            sql = sql + " and ( Number = " + i + " )";  
            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { }

        }  
    }

    private void RefreshMonthLostUse(int Year_Now)
    {
        for (int i = 1; i <= 12; i++)
        {
            // 判断时间是否适合统计
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > ( i + 1 )));
            if (!IsOnTime)
                continue;

            // 获取用户离开数人数
            string sql = "select count(*) from AppUser where ( year(LastUse) = " + Year_Now + " )  and ( month(LastUse) = " + i + " )";
            int LostCount = 0;
            try
            {
                Conn con = new Conn();
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out LostCount);
            }
            catch { }


            // 获取当月用户离开数数据
            sql = "select count(*) from AppUser where ( year(LastUse) = " + Year_Now + " )  and ( month(LastUse) = " + i + " ) and ( year(FirstUse) = " + Year_Now + " ) and ( month(FirstUse) = " + i + " )";
            int Count = 0; 
            try
            {
                Conn con = new Conn();
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out Count);
            }
            catch { }

            // 获取购买用户离开数数据
            sql = "select count(*) from AppUser inner join Activate ON AppUser.PcID = Activate.PcID where ( year(AppUser.LastUse) = " + Year_Now + " )  and ( month(AppUser.LastUse) = " + i + " )";
            int OrderLost = 0;
            try
            {
                Conn con = new Conn();
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out OrderLost);
            }
            catch { }


            // 添加月份数据
            sql = "update UseAnalyze set LostCount = " + LostCount + ", TrialLost = " + Count + ", OrderLost = " + OrderLost + " where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
            sql = sql + " and ( Number = " + i + " )";
            try
            {
                Conn con = new Conn();
                con.ExecuteNonQueryD(sql);
            }
            catch { }

        }  
    }

    private void RefreshTenDayUser(int Year_Now)
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
                string sql = "select * from UseAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
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
                    sql = "insert into UseAnalyze ( Year, Type, Number ) values ( " + Year_Now + ", '" + Type_TenDay + "', " + DayNumber + " )";
                    try
                    {
                        con.ExecuteNonQueryD(sql);
                    }
                    catch { }    
                }


                int StartDay = (j - 1) * 10 + 1;
                int EndDay = (j * 10) + 1;
                int m = i;
                if (j == 3)
                {
                    EndDay = 1;
                    m = m + 1;
                }


                string FirstUseStr = "'" + Year_Now + "-" + m + "-" + EndDay + "'";
                if(m>12)
                    FirstUseStr = "'" + (Year_Now+1) + "-1-1'";
                string LastUseStr = "'" + Year_Now + "-" + i + "-" + StartDay + "'";

                    // 获取月份数据
                sql = "select count(*) from AppUser where ( FirstUse < " + FirstUseStr + " ) and ( LastUse >= " + LastUseStr + " )";
                int Count = 0;
                try
                {
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out Count);
                }
                catch { }


                // 获取月份数据
                sql = "select count(*) from AppUser inner join Activate ON AppUser.PcID = Activate.PcID  where ( AppUser.FirstUse < " + FirstUseStr + " ) and ( AppUser.LastUse >= " + LastUseStr + " )";
                int OrderCount = 0;
                try
                {
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out OrderCount);
                }
                catch { }


                // 添加月份数据
                sql = "update UseAnalyze set UseCount = " + Count + ", OrderUse = " + OrderCount + " where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
                sql = sql + " and ( Number = " + DayNumber + " )";
                try
                {
                    con.ExecuteNonQueryD(sql);
                }
                catch { }

            }
        }
    }

    private void RefreshTenDayLostUse(int Year_Now)
    {
        for (int i = 1; i <= 12; i++)
        {
            // 判断时间是否适合统计
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > (i + 1)));
            if (!IsOnTime)
                continue;

            for (int j = 1; j <= 3; j++)
            {
                int DayNumber = (i - 1) * 3 + j;

                int DayOnTime = 100;
                if (j < 3)
                    DayOnTime = j * 10;

                int StartDay = (j - 1) * 10 + 1;
                int EndDay = (j * 10);
                if (j == 3)
                    EndDay = EndDay + 1;
        

                // 获取用户离开数人数
                string sql = "select count(*) from AppUser where ( year(LastUse) = " + Year_Now + " ) and ( month(LastUse) = " + i + " ) and ( day(LastUse) between " + StartDay + " and " + EndDay + " )";
                int LostCount = 0;
                try
                {
                    Conn con = new Conn();
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out LostCount);
                }
                catch { }


                // 获取当月用户离开数数据
                sql = "select count(*) from AppUser where ( year(LastUse) = " + Year_Now + " ) and ( month(LastUse) = " + i + " ) and ( day(LastUse) between " + StartDay + " and " + EndDay + " )";
                sql = sql + " and ( year(FirstUse) = " + Year_Now + " ) and ( month(FirstUse) = " + i + " ) and ( day(FirstUse) between " + StartDay + " and " + EndDay + " )";
                int Count = 0;
                try
                {
                    Conn con = new Conn();
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out Count);
                }
                catch { }


                // 获取用户离开数人数
                sql = "select count(*) from AppUser inner join Activate ON AppUser.PcID = Activate.PcID  where ( year(AppUser.LastUse) = " + Year_Now + " ) and ( month(AppUser.LastUse) = " + i + " ) and ( day(AppUser.LastUse) between " + StartDay + " and " + EndDay + " )";
                int OrderLost = 0;
                try
                {
                    Conn con = new Conn();
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out OrderLost);
                }
                catch { }


                // 添加月份数据
                sql = "update UseAnalyze set LostCount = " + LostCount + ", TrialLost = " + Count + ", OrderLost = " + OrderLost + " where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
                sql = sql + " and ( Number = " + DayNumber + " )";
                try
                {
                    Conn con = new Conn();
                    con.ExecuteNonQueryD(sql);
                }
                catch { }
            }

        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        int YearNow = 2013;
        int.TryParse(ddlYear.SelectedValue, out YearNow);
        RefreshMonthUser(YearNow);
        RefreshMonthLostUse(YearNow);
        RefreshTenDayUser(YearNow);
        RefreshTenDayLostUse(YearNow);
        GridView1.DataBind();
    }

    private void RefreshRowData(int CountValue, int CountChange, GridViewRowEventArgs e)
    {
        string UseCountStr = e.Row.Cells[CountValue].Text;
        int UseCount = 0;
        int.TryParse(UseCountStr, out UseCount);

        int LastRow = e.Row.RowIndex - 1;
        int LastUseCount = UseCount;
        if (LastRow >= 0)
            int.TryParse(GridView1.Rows[LastRow].Cells[CountValue].Text, out LastUseCount);

        int UseChangeCount = UseCount - LastUseCount;           
        string SignStr = "";
        if (UseChangeCount > 0)
        {
            e.Row.Cells[CountChange].ForeColor = Color.Red;
            SignStr = "+";
        }
        else if (UseChangeCount < 0)
        {
            e.Row.Cells[CountChange].ForeColor = Color.Green;
            SignStr = "-";
        }

        UseChangeCount = Math.Abs(UseChangeCount);
        string TextStr = SignStr + UseChangeCount; 
        if (LastUseCount == 0)
            LastUseCount = 1;
        int Precentage = (UseChangeCount * 100) / LastUseCount;
        TextStr = TextStr + " ( " + SignStr + Precentage + "% )";

        e.Row.Cells[CountChange].Text = TextStr;
    }

    private void RefreshPrecange(int ColParent, int ColChild, GridViewRowEventArgs e)
    {
        string PrentStr = e.Row.Cells[ColParent].Text;
        string ChildStr = e.Row.Cells[ColChild].Text;

        int PrentCount = 0;
        int.TryParse(PrentStr, out PrentCount);

        int ChildCount = 0;
        int.TryParse(ChildStr, out ChildCount);

        if (PrentCount == 0)
            PrentCount = 1;
        int Percentege = (ChildCount * 100) / PrentCount;

        string ShowStr = ChildStr + " ( " + Percentege + "% )";
        e.Row.Cells[ColChild].Text = ShowStr;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

        try
        {
            if (IsShowTenDay)
            {
                int DayCount = 0;
                int.TryParse(e.Row.Cells[Col_Month].Text, out DayCount);
                int Num = ((DayCount - 1) % 3) + 1;
                DayCount = ((DayCount - 1) / 3) + 1;
                e.Row.Cells[Col_Month].Text = DayCount + "-" + Num;

            }

            RefreshRowData(Col_UseCount, Col_UseChange, e);
            //RefreshPrecange(Col_UseCount, Col_OrderUse, e);
            //RefreshPrecange(Col_LostCount, Col_OrderLost, e);
            //RefreshPrecange(Col_LostCount, Col_TrialLost, e);

        }
        catch { }
    }
    
    
    protected void btnTenDay_Click(object sender, EventArgs e)
    {
        string YearStr = ddlYear.Text;
        IsShowTenDay = true;

        string sql = "SELECT * FROM UseAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_TenDay + "' ) )"; 
        SqlDataSource1.SelectCommand = sql;
        SqlSelect = sql;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }

    protected void btnMonth_Click(object sender, EventArgs e)
    {
        string YearStr = ddlYear.Text;
        IsShowTenDay = false;

        string sql = "SELECT * FROM UseAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_Month + "' ) )";
        SqlDataSource1.SelectCommand = sql;
        SqlSelect = sql;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }
    protected void btnRefresh_Click1(object sender, EventArgs e)
    {

    }
}

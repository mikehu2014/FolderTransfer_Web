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


public partial class TrialAnalyze : IMSite.Admin.SupperAdminPage
{
    private Boolean IsShowTenDay = false;
    const string Type_Month = "Month";
    const string Type_TenDay = "TenDay";

    const string Cmd_Refresh = "refresh";

    static private string SqlSelect = "";

    const int Col_Month = 1;
    const int Col_Value = 2;
    const int Col_Change = 3;
    const int Col_Percentage = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];

        if (Cmd == Cmd_Refresh) // 刷新
        {
            int YearNow = 2013;
            int.TryParse(ddlYear.SelectedValue, out YearNow);
            refreshMonth(YearNow);
            refreshTenDay(YearNow);
        }

        if (!IsPostBack)
        {
            string YearStr = ddlYear.Text;
            IsShowTenDay = false;
            SqlSelect = "SELECT * FROM TrialAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_Month + "' ) )";
        }

        SqlDataSource1.SelectCommand = SqlSelect;

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
    }

    private void refreshMonth(int Year_Now)
    {

        for (int i = 1; i <= 12; i++)
        {
            Boolean IsOnTime = DateTime.Now.Year > Year_Now;
            IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month > i));
            if(!IsOnTime)
                continue;

                // 查询是否存在月份数据
            string sql = "select * from TrialAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_Month + "' )";
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
            sql = "select count(*) from Trial where ( year(TrialDate) = " + Year_Now + " )  and ( month(TrialDate) = " + i + " )";
            int Count = 0;
            try
            {
                DataTable dt = con.ExecuteDataTable(sql, false);
                if (dt.Rows.Count > 0)
                    int.TryParse(dt.Rows[0][0].ToString(), out Count);
            }
            catch { }


            // 添加月份数据
            sql = "insert into TrialAnalyze ( Year, Type, Number, Value ) values ( " + Year_Now + ", '" + Type_Month + "', " + i + ", " + Count + " )";
            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { }

        }
    }

    private void refreshTenDay(int Year_Now)
    {

        for (int i = 1; i <= 12; i++)
        {

            for (int j = 1; j <= 3; j++)
            {
                int DayOnTime = 100;
                if( j < 3 )  
                    DayOnTime = j * 10;

                Boolean IsOnTime = DateTime.Now.Year > Year_Now;
                IsOnTime = IsOnTime || ( ( DateTime.Now.Year == Year_Now ) && (DateTime.Now.Month > i));
                IsOnTime = IsOnTime || ((DateTime.Now.Year == Year_Now) && (DateTime.Now.Month == i) && (DateTime.Now.Day > DayOnTime));
                if ( !IsOnTime )
                    continue;

                int DayNumber = (i - 1) * 3 + j;

                // 查询是否存在月份数据
                string sql = "select * from TrialAnalyze Where ( Year = " + Year_Now + " ) and ( Type = '" + Type_TenDay + "' )";
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
                sql = "select count(*) from Trial where ( year(TrialDate) = " + Year_Now + " ) and ( month(TrialDate) = " + i + " ) and ( day(TrialDate) between " + StartDay + " and " + EndDay + " )";
                int Count = 0;
                try
                {
                    DataTable dt = con.ExecuteDataTable(sql, false);
                    if (dt.Rows.Count > 0)
                        int.TryParse(dt.Rows[0][0].ToString(), out Count);
                }
                catch { }


                // 添加月份数据
                try
                {
                    sql = "insert into TrialAnalyze ( Year, Type, Number, Value ) values ( " + Year_Now + ", '" + Type_TenDay + "', " + DayNumber + ", " + Count + " )";
                    con.ExecuteNonQueryD(sql);
                }
                catch { }
            
            }
        }

    }
    
    protected void btnTenDay_Click(object sender, EventArgs e)
    {
        string YearStr = ddlYear.Text;
        IsShowTenDay = true;

        string sql = "SELECT * FROM TrialAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_TenDay + "' ) )";
        SqlDataSource1.SelectCommand = sql;
        SqlSelect = sql;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }

    protected void btnMonth_Click(object sender, EventArgs e)
    {
        string YearStr = ddlYear.Text;
        IsShowTenDay = false;

        string sql = "SELECT * FROM TrialAnalyze WHERE ( ( Year = " + YearStr + " ) and ( Type = '" + Type_Month + "' ) )";
        SqlDataSource1.SelectCommand = sql;
        SqlSelect = sql;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
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

            string ValueStr = e.Row.Cells[Col_Value].Text;
            int TrialCount = 0;
            int.TryParse(ValueStr, out TrialCount);

            int LastRow = e.Row.RowIndex - 1;
            int LastTrialCount = TrialCount;
            if (LastRow >= 0)
                int.TryParse(GridView1.Rows[LastRow].Cells[Col_Value].Text, out LastTrialCount);

            int ChangeCount = TrialCount - LastTrialCount;
            string TextStr = "";
            if (ChangeCount > 0)
            {
                e.Row.Cells[Col_Change].ForeColor = Color.Red;
                e.Row.Cells[Col_Percentage].ForeColor = Color.Red;
                TextStr = "+";
            }
            else
                if (ChangeCount < 0)
                {
                    e.Row.Cells[Col_Change].ForeColor = Color.Green;
                    e.Row.Cells[Col_Percentage].ForeColor = Color.Green;
                    TextStr = "-";
                }

            ChangeCount = Math.Abs(ChangeCount);
            e.Row.Cells[Col_Change].Text = TextStr + ChangeCount;
            if (LastTrialCount == 0)
                LastTrialCount = 1;
            int Precentage = (ChangeCount * 100) / LastTrialCount;
            e.Row.Cells[Col_Percentage].Text = TextStr + Precentage + "%";
        }
        catch { }
 
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

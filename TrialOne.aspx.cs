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

public partial class TrialOne : System.Web.UI.Page
{
    const string Cmd_Refresh = "refresh";

    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];
        if (Cmd == Cmd_Refresh) // 刷新
        {
            string Year = Request.QueryString["year"];
            int YearCount = -1;
            if(int.TryParse(Year, out YearCount))
                refreshTenDay(YearCount);
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
                string sql = "select * from TrialOne Where ( Year = " + Year_Now + " ) and ( Number = " + DayNumber + " )";
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
                sql = "select count(*) from ";
                sql = sql + "( select count(*) as People, IP from Trial where ( year(TrialDate) = " + Year_Now + " ) and ( month(TrialDate) = " + i + " ) ";
                sql = sql + "and ( day(TrialDate) between " + StartDay + " and " + EndDay + " ) group by IP ) t where ";

                int OneCount = 0, TweCount = 0, ThreeCount = 0, FourCount = 0, FiveCount = 0;
                for (int k = 1; k <= 5; k++)
                {
                    string tempSql = sql;

                    if (k < 5)
                        tempSql = tempSql + "People = " + k;
                    else
                        tempSql = tempSql + "People >= " + k;

                    int Count = 0;
                    try
                    {
                        DataTable dt = con.ExecuteDataTable(tempSql, false);
                        if (dt.Rows.Count > 0)
                            int.TryParse(dt.Rows[0][0].ToString(), out Count);
                    }
                    catch { }

                    if(k ==1)
                        OneCount = Count;
                    else
                    if(k==2)
                        TweCount = Count;
                    else
                    if(k==3)
                        ThreeCount = Count;
                    else
                    if (k == 4) 
                        FourCount = Count;
                    else
                    if (k == 5)
                        FiveCount = Count;
                }

                // 添加月份数据
                try
                {
                    sql = "insert into TrialOne ( Year, Number, OnePc, TwoPc, ThreePc, FourPc, FivePlusPc ) values ";
                    sql = sql + "( " + Year_Now + ", " + DayNumber + ", " + OneCount + ", " + TweCount ;
                    sql = sql + ", " + ThreeCount + ", " + FourCount + ", " + FiveCount + " )";
                    con.ExecuteNonQueryD(sql);
                }
                catch { }
            }
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

        int DayCount = 0;
        int.TryParse(e.Row.Cells[1].Text, out DayCount);
        int Num = ((DayCount - 1) % 3) + 1;
        DayCount = ((DayCount - 1) / 3) + 1;
        e.Row.Cells[1].Text = DayCount + "-" + Num;
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IMSite.Data;
using System.Data.SqlClient;

/// <summary>
/// DailyUserInfo 的摘要说明
/// </summary>
public class DailyUserInfo
{
    int SendCountChange;
    int ShareDownCountChange;
    int AdsCountChange;
    int ActionPerson;

    public DailyUserInfo(int _SendCountChange, int _ShareDownCountChange, int _AdsCountChange)
    {
        SendCountChange = _SendCountChange;
        ShareDownCountChange = _ShareDownCountChange;
        AdsCountChange = _AdsCountChange;
        if ((SendCountChange > 0) || (ShareDownCountChange > 0))
            ActionPerson = 1;
        else
            ActionPerson = 0;
    }

    public void Upgrade()
    {
        if (getIsExistToday())
            upgradeToday();
        else
            addToday();
    }

    private Boolean getIsExistToday()
    {
        string sql = "select count(*) from DailyAnalyze where Time = '" + DateTime.Now.Date + "'";

        int Count = 0;
        try
        {
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                int.TryParse(dt.Rows[0][0].ToString(), out Count);
        }
        catch { };

        return Count > 0;
    }

    private void addToday()
    {
        string sql = "insert into DailyAnalyze ( Time, UserCount, SendCount, ShareDownCount, AdsCount, ActionPerson ) values ";
        sql = sql + "( '" + DateTime.Now.Date + "', " + 1 + ", " + SendCountChange + ", " + ShareDownCountChange;
        sql = sql + ", " + AdsCountChange + ", " + ActionPerson + " )";

        try
        {
            Conn con = new Conn();
            con.ExecuteNonQueryD(sql);
        }
        catch { };
    }

    private void upgradeToday()
    {
        int LastUseCount = 0;
        int LastSendCount = 0;
        int LastShareDownCount = 0;
        int LastAdsCount = 0;
        int LastActionPerson = 0;

            // 读取数据
        string sql = "select * from DailyAnalyze where Time = '" + DateTime.Now.Date + "'";
        try
        {
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            if (sdr.Read())
            {
                int.TryParse(sdr["UserCount"].ToString(), out LastUseCount);
                int.TryParse(sdr["SendCount"].ToString(), out LastSendCount);
                int.TryParse(sdr["ShareDownCount"].ToString(), out LastShareDownCount);
                int.TryParse(sdr["AdsCount"].ToString(), out LastAdsCount);
                int.TryParse(sdr["ActionPerson"].ToString(), out LastActionPerson);
            }
            sdr.Close();
        }
        catch { };

            // 更新数据
        LastUseCount = LastUseCount + 1;
        LastSendCount = LastSendCount + SendCountChange;
        LastShareDownCount = LastShareDownCount + ShareDownCountChange;
        LastAdsCount = LastAdsCount + AdsCountChange;
        LastActionPerson = LastActionPerson + ActionPerson;

            // 写入数据
        sql = "UPDATE DailyAnalyze SET UserCount = " + LastUseCount + ", SendCount = " + LastSendCount;
        sql = sql + ", ShareDownCount = " + LastShareDownCount + ", AdsCount = " + LastAdsCount;
        sql = sql + ", ActionPerson = " + LastActionPerson;
        sql = sql + " where Time = '" + DateTime.Now.Date + "'";
        try
        {
            Conn con = new Conn();
            con.ExecuteNonQueryD(sql);
        }
        catch { };
    }
}

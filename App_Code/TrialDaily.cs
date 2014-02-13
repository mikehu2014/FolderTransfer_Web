using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using IMSite.Data;

/// <summary>
/// TrialDaily 的摘要说明
/// </summary>
public class TrialDaily
{
    public TrialDaily()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
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
        string sql = "select count(*) from TrialDaily where Time = '" + DateTime.Now.Date + "'";

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
        string sql = "insert into TrialDaily ( Time, UserCount ) values ";
        sql = sql + "( '" + DateTime.Now.Date + "', " + 1 + " )";

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

        // 读取数据
        string sql = "select * from TrialDaily where Time = '" + DateTime.Now.Date + "'";
        try
        {
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            if (sdr.Read())
            {
                int.TryParse(sdr["UserCount"].ToString(), out LastUseCount);
            }
            sdr.Close();
        }
        catch { };

        // 更新数据
        LastUseCount = LastUseCount + 1;

        // 写入数据
        sql = "UPDATE TrialDaily SET UserCount = " + LastUseCount + " where Time = '" + DateTime.Now.Date + "'";
        try
        {
            Conn con = new Conn();
            con.ExecuteNonQueryD(sql);
        }
        catch { };
    }
}

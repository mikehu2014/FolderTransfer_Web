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
/// AppUserInfo 的摘要说明
/// </summary>
public class AppUserInfo
{
    private string PcID;
    private string PcName;
    private string Ip;
    private string NetworkMode;
    private int NetworkPc;
    private int FileSend;
    private int ShareDown;
    private int AdsShowCount;
    private string WinOS;
    private string Status;


    public AppUserInfo(string _PcID, string _PcName, string _WinOS)
	{
        PcID = _PcID;
        PcName = _PcName;
        WinOS = _WinOS;
	}

    public void SetNetworkInfo(string _Ip, string _NetworkMode, int _NetworkPc)
    {
        Ip = _Ip;
        NetworkMode = _NetworkMode;
        NetworkPc = _NetworkPc;
    }

    public void SetTransferInfo(int _FileSend, int _ShareDown, int _AdsShowCount)
    {
        FileSend = _FileSend;
        ShareDown = _ShareDown;
        AdsShowCount = _AdsShowCount;
    }

    public void setStatus(string _Status) 
    {
        Status = _Status;
    }

    public void MarkApp()
    {
        DateTime LastRun = getAppLastRunTime();
        if( LastRun == DateTime.MinValue )  // 是否第一次运行
            FirstUse();
        else   // 是否同一天运行
        if ((LastRun.Year == DateTime.Now.Year) && (LastRun.Month == DateTime.Now.Month) && (LastRun.Day == DateTime.Now.Day))
            return;
        else   // 刷新运行信息
        {
            refreshDailyUser();

            int UseCont = getUseCount();
            UseCont++;
            UpdateLastUse(UseCont);
        }
    }

        // 获取 Pc 最后运行时间
    private DateTime getAppLastRunTime()
    {
        DateTime LastRun = DateTime.MinValue; 

        Conn con = new Conn();
        string sql = "SELECT LastUse FROM AppUser Where PcID = '" + PcID + "'";
        SqlDataReader sdr = con.ExecuteReaderD(sql);
        if (sdr.Read())
        {
            LastRun = DateTime.Parse(sdr["LastUse"].ToString()); 
        }
        sdr.Close();

        return LastRun;
    }

        // 获取使用次数
    private int getUseCount()
    {
        int UseCount = 0;

        Conn con = new Conn();
        string sql = "SELECT UseCount FROM AppUser Where PcID = '" + PcID + "'";
        SqlDataReader sdr = con.ExecuteReaderD(sql);
        if (sdr.Read())
        {
            UseCount = int.Parse(sdr["UseCount"].ToString());
        }
        sdr.Close();

        return UseCount;    
    }

        // 第一次运行时创建表
    private void FirstUse()
    {
        Conn con = new Conn();
        string sql = "insert into AppUser ( PcID, PcName, FirstUse, LastUse, RealUseDate, UseCount, IP, FileSendCount, ShareDownCount";
        sql = sql + ", NetworkMode, NetworkPc, AdsShowCount, WinOS, Status ) values ";
        sql = sql + "('" + PcID + "', '" + PcName + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + DateTime.Now + "', " + 1 + ", '" + Ip + "'";
        sql = sql + ", " + FileSend + ", " + ShareDown + ", '" + NetworkMode + "', " + NetworkPc + ", " + AdsShowCount + ", '" + WinOS + ", '" + Status + "' )";
        con.ExecuteNonQueryD(sql);
    }

        // 第 N + 1 次运行时更新表
    private void UpdateLastUse(int UseCount)
    {
        int LastSend = 0;
        int LastShareDown = 0;
        int LastAdsCount = 0;
        readLastInfo(out LastSend, out LastShareDown, out LastAdsCount);
        if ((FileSend > LastSend) || (ShareDown > LastShareDown))  // 更新最后一次使用时间
        {
            string sql1 = "UPDATE AppUser SET RealUseDate = '" + DateTime.Now + "' where PcID = '" + PcID + "'";
            Conn con1 = new Conn();
            try
            {
                con1.ExecuteNonQueryD(sql1);
            }
            catch { }; 
        }


            // 更新使用信息
        string sql = "UPDATE AppUser SET LastUse = '" + DateTime.Now + "', UseCount = " + UseCount + ", IP = '" + Ip + "'";
        sql = sql + ", FileSendCount = " + FileSend + ", ShareDownCount = " + ShareDown + ", NetworkMode = '" + NetworkMode + "'";
        sql = sql + ", NetworkPc = " + NetworkPc + ", AdsShowCount = " + AdsShowCount + ", WinOS = '" + WinOS + ", Status = '" + Status + "'";
        sql = sql + " where PcID = '" + PcID + "'";
        Conn con = new Conn();
        try
        {
            con.ExecuteNonQueryD(sql);
        }
        catch { };         
    }
        
        // 读取上次信息
    private void readLastInfo(out int LastSend, out int LastShareDown, out int LastAdsCount)
    {
        LastSend = 0;
        LastShareDown = 0;
        LastAdsCount = 0;

        string sql = "select FileSendCount, ShareDownCount, AdsShowCount from AppUser where PcID = '" + PcID + "'";
        try
        {
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            if (sdr.Read())
            {
                int.TryParse(sdr["FileSendCount"].ToString(), out LastSend);
                int.TryParse(sdr["ShareDownCount"].ToString(), out LastShareDown);
                int.TryParse(sdr["AdsShowCount"].ToString(), out LastAdsCount);
            }
            sdr.Close();
        }
        catch { };
    }

        // 刷新当前用户变化记录
    private void refreshDailyUser()
    {   
            // 读取 上次运行信息
        int LastSend = 0;
        int LastShareDown = 0;
        int LastAdsShow = 0;
        readLastInfo(out LastSend, out LastShareDown, out LastAdsShow);
            
            // 获取变化信息
        int SendChange = 0;
        if (FileSend > LastSend)
            SendChange = FileSend - LastSend;

        int ShareDownChange = 0;
        if (ShareDown > LastShareDown)
            ShareDownChange = ShareDown - LastShareDown;

        int AdsChange = 0;
        if (AdsShowCount > LastAdsShow)
            AdsChange = AdsShowCount - LastAdsShow;

        DailyUserInfo du = new DailyUserInfo(SendChange, ShareDownChange, AdsChange);
        du.Upgrade();
    }
}

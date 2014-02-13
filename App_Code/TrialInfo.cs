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

namespace BackupCowWeb.Activate
{
    public class TrialInfo
    {
        public string hardCode;
        public string pcName, ip, pcID;
        public DateTime trialTime;
        public string WinOS;

        public TrialInfo(string _hardCode)
        {
            hardCode = _hardCode;
        }

        public void setPcInfo(string _pcName, string _ip)
        {
            pcName = _pcName;
            ip = _ip;
        }

        public void setPcID(string _pcID)
        {
            pcID = _pcID;
        }

        public void setTrialTime(DateTime _trialTime)
        {
            trialTime = _trialTime;
        }

        public void setWinOS(string _WinOS)
        {
            WinOS = _WinOS;
        }
    }

    public class TrialHandle
    {
        public static void addTrial(TrialInfo t)
        {
            string sql = "Insert Into Trial (HardCode, PcName, IP, TrialDate, PcID, WinOS) Values ";
            sql = sql + "('" + t.hardCode + "', '" + t.pcName + "', '" + t.ip + "', '" + t.trialTime + "', '" + t.pcID + "', '" + t.WinOS + "')";
            Conn con = new Conn();
            con.ExecuteNonQueryD(sql);
        }

        public static Boolean checkExist(string hardCode, string pcName)
        {
            string sql = "SELECT * FROM Trial Where ( HardCode = '" + hardCode + "' ) and ( PcName = '" + pcName + "' )";
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable(sql, false);
            return (dt.Rows.Count > 0);
        }

        public static DateTime getTrialTime(string hardCode)
        {
            string sql = "SELECT * FROM Trial Where HardCode = '" + hardCode + "'";
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                return (DateTime)dt.Rows[0]["TrialDate"];
            else
                return DateTime.UtcNow;
        }

        public static void setPcID(string hardCode, string pcID)
        {
            string sql = "UPDATE Trial SET PcID = '" + pcID + "' ";
            sql = sql + "where HardCode = '" + hardCode + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };   
        }

        public static void refreshTrial()
        {
            Conn con = new Conn();
            string sql = "SELECT * FROM Trial";
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            while (sdr.Read())
            {
                if( sdr["PcID"].ToString() == "" )
                {
                    string HardCode = sdr["HardCode"].ToString();
                    setPcID(HardCode, HardCode); 
                }
            }
            sdr.Close();

        }
    }
}

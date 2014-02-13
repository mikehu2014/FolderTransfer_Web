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
using BackupCowWeb.Order;

namespace BackupCowWeb.Activate
{
    public class TActivateInfo
    {
        public string orderID, hardCode;
        public string pcName, ip, pcID;
        public DateTime activateTime; 

        public TActivateInfo(string _orderID, string _hardCode)
        {
            orderID = _orderID;
            hardCode = _hardCode;
        }
        public void setPcInfo(string _pcName, string _ip, string _pcID )
        {
            pcName = _pcName;
            ip = _ip;
            pcID = _pcID;
        }
        public void setActivateTime(DateTime _activateTime)
        {
            activateTime = _activateTime;
        }
    }

    public class Activation
    {
        static public Boolean activatePc(TActivateInfo ActivateInfo)
        {
                // 已激活
            if (checkHardcodeExist(ActivateInfo.orderID, ActivateInfo.hardCode))
                return true;

                // 激活成功
            if (Order.Order.addActivateCount(ActivateInfo.orderID, 1))
            {
                addActivate(ActivateInfo);
                return true;
            }
            else
            {
                return false;
            }

        }

        static private Boolean IsHardCodeMatch(string HardCode1, string HardCode2)
        {
            char[] splitHardcode = new char[] { '-' };

            string[] s1l = HardCode1.Split(splitHardcode);
            string[] s2l = HardCode2.Split(splitHardcode);

            string[] MaxList, MinList;

            if (s1l.Length > s2l.Length)
            {
                MaxList = s1l;
                MinList = s2l;
            }
            else
            {
                MinList = s1l;
                MaxList = s2l;
            }

            Boolean IsMatch = true;
            for (int i = 0; i <= MinList.Length - 1; i++)
            {
                string smin = MinList[i];
                Boolean IsFind = false;
                for (int j = 0; j <= MaxList.Length - 1; j++)
                {
                    if (MaxList[j] == smin)
                    {
                        IsFind = true;
                        break;
                    }
                }
                if (!IsFind)
                {
                    IsMatch = false;
                    break;
                }
            }

            return IsMatch;
        }

        static public Boolean checkHardcodeExist(string orderID, string hardCode)
        {
            Conn con = new Conn();
            string sql = "SELECT HardCode FROM Activate Where OrderID = '" + orderID + "'";
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            Boolean IsExist = false;
            while (sdr.Read())
            {
                string ExistHardCode = sdr["HardCode"].ToString();
                if (IsHardCodeMatch(ExistHardCode, hardCode))
                {
                    IsExist = true;
                    break;
                }
            }
            sdr.Close();

            return IsExist;
        }

        static public void addActivate(TActivateInfo ActivateInfo)
        {
            string status = "";
            if (checkAddition(ActivateInfo.orderID))
                status = "Addition";

            Conn con = new Conn();
            string sql = "insert into Activate ( OrderID, HardCode, PcName, IP, ActivateTime, Status, PcID ) values";
            sql = sql + " ('" + ActivateInfo.orderID + "', '" + ActivateInfo.hardCode + "'";
            sql = sql + ", '" + ActivateInfo.pcName + "', '" + ActivateInfo.ip + "'";
            sql = sql + ", '" + ActivateInfo.activateTime + "', '" + status + "', '" + ActivateInfo.pcID + "')";
            con.ExecuteNonQueryD(sql);
        }

        static public void deleteOrder(string orderID)
        {
            Conn con = new Conn();
            string sql = "Delete From Activate where OrderID = '" + orderID + "'";
            con.ExecuteNonQueryD(sql);
        }

        static public Boolean checkAddition(string orderID)
        {
            string sql = "Select count(*) from Activate where OrderID = '" + orderID + "'";
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable(sql, false);
            int activateCount = 0;
            if (dt.Rows.Count > 0)
                int.TryParse(dt.Rows[0][0].ToString(), out activateCount);

            TOrderInfo o = Order.Order.getOrderInfo(orderID);
            return activateCount >= o.userCount;
        }

        static public DateTime getActivateTime(string orderID, string hardCode)
        {
            Conn con = new Conn();
            string sql = "SELECT ActivateTime FROM Activate Where OrderID = '" + orderID + "' and HardCode = '" + hardCode + "'";
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                return (DateTime)dt.Rows[0]["ActivateTime"];
            else
                return DateTime.UtcNow;
        }


        public static void setPcID(string hardCode, string pcID)
        {
            string sql = "UPDATE Activate SET PcID = '" + pcID + "' ";
            sql = sql + "where HardCode = '" + hardCode + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };
        }

        public static void refreshActivate()
        {
            Conn con = new Conn();
            string sql = "SELECT * FROM Activate";
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            while (sdr.Read())
            {
                if (sdr["PcID"].ToString() == "")
                {
                    string HardCode = sdr["HardCode"].ToString();
                    setPcID(HardCode, HardCode);
                }
            }
            sdr.Close();

        }
    }
}

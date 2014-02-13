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
using System.Collections.Generic;

namespace BackupCowWeb
{

    /// <summary>
    /// ForgetPasswordInfo 的摘要说明
    /// </summary>
    public class ForgetPasswordInfo
    {
        public ForgetPasswordInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        static public void addForgetPassword( string AccountName, string Uid )
        {
                // 存在则先删除
            removeForgetPassword(AccountName);

                // 添加
            DateTime LastTime = DateTime.Now.AddMinutes(10);
            //LastTime.AddMinutes(10);

            Conn con = new Conn();
            string sql = "insert into ForgetPassword ( AccountName, UID, LastTime ) values ";
            sql = sql + "('" + AccountName + "', '" + Uid + "', '" + LastTime + "')";
            con.ExecuteNonQueryD(sql);
        }

        static public Boolean checkForgetPassword(string AccountName, string Uid)
        {
            Conn con = new Conn();
            string sql = "select * from ForgetPassword Where ( AccountName = '" + AccountName + "' ";
            sql = sql + " and UID = '" + Uid + "' )";
            DataTable dt = con.ExecuteDataTable(sql, false);
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            Boolean isVaild = false;
            if (sdr.Read())
            {
                DateTime LastTime;
                if( DateTime.TryParse(sdr["LastTime"].ToString(), out LastTime ) )
                    isVaild = DateTime.Compare(DateTime.Now, LastTime) <= 0;
            }
            sdr.Close();
            return isVaild;
        }

        static public void removeForgetPassword(string AccountName)
        {
            Conn con = new Conn();
            string sql = "Delete From ForgetPassword where AccountName = '" + AccountName + "'";
            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { }
        }

        static public void ClearOutDateForgetPassword()
        {
            List<string> DeleteList = new List<string>();

            Conn con = new Conn();
            string sql = "select * from ForgetPassword";
            DataTable dt = con.ExecuteDataTable(sql, false);
            SqlDataReader sdr = con.ExecuteReaderD(sql);           
            while (sdr.Read())
            {
                Boolean isVaild = false;
                DateTime LastTime;
                if (DateTime.TryParse(sdr["LastTime"].ToString(), out LastTime))
                    isVaild = DateTime.Compare(DateTime.Now, LastTime) <= 0;
                if (!isVaild)
                    DeleteList.Add(sdr["AccountName"].ToString());
            }
            sdr.Close();

            foreach (string AccountName in DeleteList)
                removeForgetPassword(AccountName);
        }
    }
}

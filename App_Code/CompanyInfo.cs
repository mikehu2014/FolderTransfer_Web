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

namespace BackupCowWeb
{

    /// <summary>
    /// CompanyInfo 的摘要说明
    /// </summary>
    public class CompanyInfo
    {
        public CompanyInfo()
        {

        }

        static public Boolean checkExist(string accountName)
        {
            Conn con = new Conn();
            string sql = "SELECT Password FROM CompanyNetwork Where CompanyName = '" + accountName + "'";
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        static public void addCompany(string accountName, string email, string password)
        {
            Conn con = new Conn();
            string sql = "insert into CompanyNetwork ( CompanyName, Email, Password, SignupTime, GroupUser ) values ";
            sql = sql + "('" + accountName + "', '" + email + "', '" + password + "', '" + DateTime.Now + "', " + 0 + ")";
            con.ExecuteNonQueryD(sql);
        }

        static public int getCompanyUser(string accountName)
        {         
            string sql = "SELECT * FROM CompanyNetwork Where CompanyName = '" + accountName + "'";
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            string userCountStr = "";
            int userCount = 0;
            if (sdr.Read())
                userCountStr = sdr["GroupUser"].ToString();
            if( ! int.TryParse( userCountStr, out userCount ))
                userCount = 0;
            sdr.Close();
            return userCount;
                                   
        }

        static public void addCompanyUser(string accountName, int DelCount)
        {
            int OldUserCount = getCompanyUser(accountName);
            int NewUserCount = OldUserCount + DelCount;
            changeGroupCount(accountName, NewUserCount);       
        }

        static public void changePassword(string accountName, string password)
        {
            string sql = "UPDATE CompanyNetwork SET Password = '" + password + "' ";
            sql = sql + "where CompanyName = '" + accountName + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };   
        }

        static public string getEmail(string accountName)
        {
            string sql = "SELECT * FROM CompanyNetwork Where CompanyName = '" + accountName + "'";
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            string Email = "";
            if (sdr.Read())
                Email = sdr["Email"].ToString();
            sdr.Close();
            return Email;
                
        }

        static public void changeSignupTime(string accountName)
        {
            string sql = "UPDATE CompanyNetwork SET SignupTime = '" + DateTime.Now +"' ";
            sql = sql + "where CompanyName = '" + accountName + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };           
        }

        static public void changeGroupCount(string accountName, int groupCount)
        {
            string sql = "UPDATE CompanyNetwork SET GroupUser = " + groupCount + " ";
            sql = sql + "where CompanyName = '" + accountName + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };     
        }
    }
}

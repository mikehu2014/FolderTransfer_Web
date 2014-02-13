using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using IMSite.Data;


namespace BackupCowWeb.Company
{

    public class TCompanyPcInfo {
        public string PcID, PcName;
        public string LanIp, LanPort;
        public string InternetIp, InternetPort;
        public string CloudIDNumber;
        public DateTime OnlineTime;

        public TCompanyPcInfo(string _PcID, string _PcName) {
            PcID = _PcID;
            PcName = _PcName;
        }

        public void setLanInfo( string _LanIp, string _LanPort ){
            LanIp = _LanIp;
            LanPort = _LanPort;
        }

        public void setInterInfo(string _InternetIp, string _InternetPort){
            InternetIp = _InternetIp;
            InternetPort = _InternetPort;
        }

        public void setCloudIDNumber(string _CloudIDNumber){
            CloudIDNumber = _CloudIDNumber;
        }

        public void setOnlineTime(DateTime _OnlineTime) {
            OnlineTime = _OnlineTime;
        }

    }

    public class TCompanyInfo {
        public string CompanyName;
        public Dictionary<string, TCompanyPcInfo> CompanyPcInfoHash;

        public TCompanyInfo(string _CompanyName) {
            CompanyName = _CompanyName;
            CompanyPcInfoHash = new Dictionary<string, TCompanyPcInfo>();
        }
    }

    public partial class WebForm1 : System.Web.UI.Page
    {
        const string HttpReq_CompanyName = "CompanyName";
        const string HttpReq_Password = "Password";
        const string HttpReq_PcID = "PcID";
        const string HttpReq_PcName = "PcName";
        const string HttpReq_LanIp = "LanIp";
        const string HttpReq_LanPort = "LanPort";
        const string HttpReq_InternetIp = "InternetIp";
        const string HttpReq_InternetPort = "InternetPort";
        const string HttpReq_CloudIDNumber = "CloudIDNumber";

        const string LoginResult_CompanyNotFind = "CompanyNotFind";
        const string LoginResult_PasswordError = "PasswordError";
        const string LoginResult_OK = "OK";

        const string Split_Result = "<Result/>";
        const string Split_Pc = "<Pc/>";
        const string Split_PcPro = "<PcPro/>";

        const string Cmd_Refresh = "refresh";

        const string Cmd_Login = "login";
        const string Cmd_HeartBeat = "heartbeat";
        const string Cmd_AddServerNumber = "addservernumber";
        const string Cmd_ReadServerNumber = "readservernumber";
        const string Cmd_Logout = "logout";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string Cmd = Request.QueryString["cmd"];

            if (Cmd == Cmd_Refresh) // 刷新
            {
                refreshCompanyList();

                Response.Clear();
                Response.Write("Refresh Completed");
                Response.End();
                return;
            }

                // Login 或 LogOut 才更新 Company Pc 列表
            if ((Cmd != Cmd_Login) && (Cmd != Cmd_HeartBeat) && (Cmd != Cmd_Logout) && (Cmd != Cmd_AddServerNumber) && (Cmd != Cmd_ReadServerNumber))
                return;

            string CompanyName = HttpContext.Current.Request[ HttpReq_CompanyName ];
            string Password = HttpContext.Current.Request[HttpReq_Password];

            string PcID = HttpContext.Current.Request[HttpReq_PcID];
            string PcName = HttpContext.Current.Request[HttpReq_PcName];
            string LanIp = HttpContext.Current.Request[HttpReq_LanIp];
            string LanPort = HttpContext.Current.Request[HttpReq_LanPort];
            string InternetIp = HttpContext.Current.Request[HttpReq_InternetIp];
            string InternetPort = HttpContext.Current.Request[HttpReq_InternetPort];
            string CloudIDNumber = HttpContext.Current.Request[HttpReq_CloudIDNumber];

            string Result  = check(CompanyName, Password); // 检查数据库

            if ( Result == LoginResult_OK )
            {                   
                    // Login 则创建 并 返回 列表
                if ( Cmd == Cmd_Login )
                {
                    TCompanyPcInfo NewComputerInfo = new TCompanyPcInfo(PcID, PcName);
                    NewComputerInfo.setLanInfo(LanIp, LanPort);
                    NewComputerInfo.setInterInfo(InternetIp, InternetPort);
                    NewComputerInfo.setCloudIDNumber(CloudIDNumber);
                    NewComputerInfo.setOnlineTime(DateTime.Now);

                    login(CompanyName, NewComputerInfo);

                    Result = Result + Split_Result + getCompanyPcList( CompanyName, CloudIDNumber );
                }

                    // HeartBeat  更新在线时间  
                if (Cmd == Cmd_HeartBeat)
                    heartBeat(CompanyName, PcID);
                  
                    // 添加 Server Number
                if (Cmd == Cmd_AddServerNumber)
                    Result = "" + AddServerNumber(CompanyName);

                    // 读取 Server Number
                if (Cmd == Cmd_ReadServerNumber)
                    Result = "" + getServerNumber(CompanyName);

                    // LogOut 则删除
                if (Cmd == Cmd_Logout)
                    logout(CompanyName, PcID);
            }

            Response.Clear();
            Response.Write( Result );
            Response.End();


        }
            
            // Company Login
        private string check( string company, string password ){
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            Conn con = new Conn();
            string sql = "SELECT Password FROM CompanyNetwork Where CompanyName = '" + company + "'";
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count == 0)
                return LoginResult_CompanyNotFind;
            else
            if ( dt.Rows[0]["Password"].ToString() != password )
                return LoginResult_PasswordError;
            else
                return LoginResult_OK;
                
        }

            // Pc Login
        private void login(string CompanyName, TCompanyPcInfo CompanyPcInfo)
        {
                // 如果 已上线则先下线
            logout(CompanyName, CompanyPcInfo.PcID);

            string sql = "insert into CompanyComputer ( CompanyName, PcID, PCName, LanIp, LanPort, ";
            sql = sql + "InternetIp, InternetPort, CloudIDNumber, OnlineTime ) values ";

            sql = sql + "('" + CompanyName + "', '" + CompanyPcInfo.PcID + "', '" + CompanyPcInfo.PcName + "', '";
            sql = sql + CompanyPcInfo.LanIp + "', '" + CompanyPcInfo.LanPort + "', '" + CompanyPcInfo.InternetIp + "', '";
            sql = sql + CompanyPcInfo.InternetPort + "', '" + CompanyPcInfo.CloudIDNumber + "', '";
            sql = sql + CompanyPcInfo.OnlineTime + "')";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
                CompanyInfo.addCompanyUser(CompanyName, 1); // 添加成功
            }
            catch { };
        }

            // Pc Logout
        private void logout(string CompanyName, string PcID)
        {
            string sql = "Delete From CompanyComputer where ( CompanyName = '" + CompanyName + "' and ";
            sql = sql + "PcID = '" + PcID + "')";

            Conn con = new Conn();

            try
            {
               int Delecount = con.ExecuteNonQueryD(sql);
               if (Delecount > 0)  // 删除成功
                   CompanyInfo.addCompanyUser(CompanyName, -1);
            }
            catch { };

        }

            // Pc Login HeartBeat
        private void heartBeat(string CompanyName, string PcID)
        {
            string sql = "UPDATE CompanyComputer SET OnlineTime = '" + DateTime.Now + "' "; 
            sql = sql + "where ( CompanyName = '" + CompanyName + "' and PcID = '" + PcID + "')";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };
        }

        private int getServerNumber(string CompanyName)
        {
            string ServerNumberStr = "";

            string sql = "select ServerNumber from CompanyNetwork where CompanyName = '" + CompanyName + "'";
            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            if (sdr.Read())
                ServerNumberStr = sdr["ServerNumber"].ToString();
            sdr.Close();

            int ServerNumber = 0;
            if (ServerNumberStr == "")
                ServerNumber = 0;
            else
                if (!int.TryParse(ServerNumberStr, out ServerNumber))
                    ServerNumber = 0;

            return ServerNumber;
        }

        private void setServerNumber(string CompanyName, int ServerNumber)
        {
            string sql = "UPDATE CompanyNetwork SET ServerNumber = " + ServerNumber + " ";
            sql = sql + "where CompanyName = '" + CompanyName + "'";

            Conn con = new Conn();

            try
            {
                con.ExecuteNonQueryD(sql);
            }
            catch { };
        }

        private int AddServerNumber(string CompanyName)
        {
            int ServerNumber = getServerNumber(CompanyName);
            ServerNumber = ServerNumber + 1;
            if (ServerNumber >= 100)
                ServerNumber = 0;
            setServerNumber(CompanyName, ServerNumber);
            return ServerNumber;
        }


        private Dictionary<string, TCompanyPcInfo> getCompanyComputerList(string CompanyName, string CloudIDNumber)
        {
            Dictionary<string, TCompanyPcInfo> CompanyComputerList = new Dictionary<string, TCompanyPcInfo>(); 

            string sql = "select * from CompanyComputer where ( CompanyName = '" + CompanyName + "' and ";
            sql = sql + "CloudIDNumber = '" + CloudIDNumber + "')";

            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            while (sdr.Read())
            {
               string PcID = sdr["PcID"].ToString();
               string PcName = sdr["PCName"].ToString();
               string LanIp = sdr["LanIp"].ToString();
               string LanPort = sdr["LanPort"].ToString();
               string InternetIp = sdr["InternetIp"].ToString();
               string InternetPort = sdr["InternetPort"].ToString();
               DateTime OnlineTime =  DateTime.Parse( sdr["OnlineTime"].ToString() );

               TCompanyPcInfo PcInfo = new TCompanyPcInfo(PcID, PcName);
               PcInfo.setLanInfo(LanIp, LanPort);
               PcInfo.setInterInfo(InternetIp, InternetPort);
               PcInfo.setCloudIDNumber(CloudIDNumber);
               PcInfo.setOnlineTime(OnlineTime);

               CompanyComputerList.Add(PcID, PcInfo);
            }
            sdr.Close();

            return CompanyComputerList;
        }

            // get Login Pc List
        private string getCompanyPcList( string CompanyName, string CloudIDNumber ) 
        {
            //Dictionary<string, TCompanyPcInfo> CompanyPcInfoHash = CompanyInfoHash[CompanyName].CompanyPcInfoHash;

            Dictionary<string, TCompanyPcInfo> CompanyPcInfoHash = getCompanyComputerList( CompanyName, CloudIDNumber );

            string s = "";
            List<string> DeleteList = new List<string>();
            foreach( KeyValuePair< String, TCompanyPcInfo > p in CompanyPcInfoHash )
            {
                TimeSpan ts =  DateTime.Now - p.Value.OnlineTime;
                if (ts.TotalMinutes > 20)
                {
                    DeleteList.Add(p.Value.PcID);
                    continue;
                }

                if( p.Value.CloudIDNumber != CloudIDNumber )
                    continue;

                if ( s != "") 
                    s = s + Split_Pc;

                s = s + p.Value.PcID;
                s = s + Split_PcPro + p.Value.PcName;
                s = s + Split_PcPro + p.Value.LanIp;
                s = s + Split_PcPro + p.Value.LanPort;
                s = s + Split_PcPro + p.Value.InternetIp;
                s = s + Split_PcPro + p.Value.InternetPort;

            }

            foreach (string PcID in DeleteList)
                logout( CompanyName, PcID);
                //CompanyPcInfoHash.Remove(PcID);
          

            return s;
        }

        private void refreshCompanyList()
        {

                // 刷新 GroupPc数
            Dictionary<string, TCompanyPcInfo> CompanyComputerList = new Dictionary<string, TCompanyPcInfo>();

            string sql = "select * from CompanyNetwork";

            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            while (sdr.Read())
            {
                string CompanyName = sdr["CompanyName"].ToString();
                string SignupTimeStr = sdr["SignupTime"].ToString();
                string UserCountStr = sdr["GroupUser"].ToString();
                    // 版本兼容, 重设注册时间
                if (SignupTimeStr == "")
                    CompanyInfo.changeSignupTime(CompanyName);
                    // 版本兼容, 重设用户数
                refreshCompanyPc( CompanyName ); // Logout 超时 Pc
                int NewUserCount = getCompanyPcCount(CompanyName);
                if (UserCountStr == "")
                    CompanyInfo.changeGroupCount(CompanyName, NewUserCount);
                else
                {
                    int OldUserCount = 0;
                    int.TryParse(UserCountStr, out OldUserCount);
                    if (NewUserCount != OldUserCount)
                        CompanyInfo.changeGroupCount(CompanyName, NewUserCount);
                }

                
            }
            sdr.Close();   
        }

        private int getCompanyPcCount( string accountName )
        {
            string sql = "SELECT Count(*) FROM CompanyComputer Where CompanyName = '" + accountName + "'";
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable( sql, false );
            int userCount = 0;
            if (dt.Rows.Count > 0)
            {
                string UserCountStr = dt.Rows[0][0].ToString();
                int.TryParse(UserCountStr, out userCount); 
            }
            return userCount;
        }

        private void refreshCompanyPc(string accountName)
        {

            List<string> DeleteList = new List<string>();

            string sql = "select * from CompanyComputer Where CompanyName = '" + accountName + "'";

            Conn con = new Conn();
            SqlDataReader sdr = con.ExecuteReaderD(sql);
            while (sdr.Read())
            {
                string PcID = sdr["PcID"].ToString();
                DateTime OnlineTime = DateTime.Parse(sdr["OnlineTime"].ToString());

                TimeSpan ts = DateTime.Now - OnlineTime;
                if (ts.TotalMinutes > 20)
                    DeleteList.Add(PcID);
            }
            sdr.Close();

            foreach (string PcID in DeleteList)
                logout(accountName, PcID);

        }
    }
}



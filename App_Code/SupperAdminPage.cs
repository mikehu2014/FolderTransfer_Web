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

namespace IMSite.Admin
{
    public class SupperAdminPage : System.Web.UI.Page
    {
        public SupperAdminPage(){}

        public static int AdminId
        {
            get
            {
                Security security = new Security();
                if (security.IsAuthenticated)
                {
                    try {
                        return security.AdminId;
                    }
                    catch { }
                }
                return 0;
            }
        }        

        protected override void OnLoad(EventArgs e)
        {
            if (!ValidateUser) {
                Server.Transfer("Login.aspx?ReturnUrl=" + Request.Path);
            }
            base.OnLoad(e);
        }


        protected virtual bool ValidateUser
        {
            get {
                return (new Security()).IsAuthenticated;
            }
        }
    }

    public class Security
    {
        string idname = "adminid";
        private int _adminId = 0;
        private bool _isAuthed=false;

        public Security()
        {
            IsAuth();
        }

        public int AdminId
        {
            get
            {
                return _adminId;
            }
        }       

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthed;
            }
        }

        public bool ClientLogin(string username, string password)
        {
            return Login(username, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"));
        }

        public bool Login(string userName, string md5_password)
        {
            string sql = string.Empty;

            sql = "select adminid from Administrator where password = '" + md5_password + "' and Username = '" + userName + "'";

            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
            {
                _isAuthed = true;
                _adminId = int.Parse(dt.Rows[0]["adminid"].ToString());
                return true;
            }
            else
            {
                _isAuthed = false;
                return false;
            }
        }

        public void WebLogin(string username, string password)
        {
            if (ClientLogin(username, password))
            {
                HttpContext.Current.Session[idname] = _adminId;
            }
            else
                Logout();
        }

        public void Logout()
        {
            HttpContext.Current.Session.Abandon();
        }

        bool IsAuth()
        {
            if (HttpContext.Current.Session == null)
            {
                _isAuthed = false;
                return false;
            }
            else if (HttpContext.Current.Session[idname] != null)
            {
                _isAuthed = true;
                _adminId = (int)HttpContext.Current.Session[idname];                
                return true;
            }
            else
                return false;
        }
    }
}

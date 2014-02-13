using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using IMSite.Data;

namespace BackupCowWeb
{
    public class Global : System.Web.HttpApplication
    {        
        protected void Application_Start(object sender, EventArgs e)
        {
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["KeywordComptingConnectionString"].ConnectionString;
        }
    }
}
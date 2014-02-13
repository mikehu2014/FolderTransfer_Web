using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IMSite.Admin;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Security security = new Security();
            security.WebLogin(txtUser.Text, txtPass.Text);
            if (security.IsAuthenticated)
                Response.Redirect("download.aspx");
            else
                Label1.Visible = true;
        }
    }
}

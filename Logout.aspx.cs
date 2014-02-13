using IMSite.Admin;
using System;

public partial class Admin_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Security se = new Security();
        se.Logout();
        Response.Redirect("login.aspx");
    }
}

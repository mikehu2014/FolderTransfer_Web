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
using BackupCowWeb;

public partial class RemoteGroupSignup : System.Web.UI.Page
{
    static string Signup_Exist = "exist";
    static string Signup_Completed = "completed";

    static string HttpSinup_GroupName = "groupname";
    static string HttpSinup_Email = "email";
    static string HttpSinup_Password = "password";

    protected void Page_Load(object sender, EventArgs e)
    {
        string groupName = HttpContext.Current.Request[ HttpSinup_GroupName ];
        string email = HttpContext.Current.Request[HttpSinup_Email];
        string password = HttpContext.Current.Request[HttpSinup_Password];

        string key = string.Empty;
        if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Response.Clear();
            Response.Write(key);
            Response.End();
            return;
        }

        string resultStr = "";
        if (CompanyInfo.checkExist(groupName))
            resultStr = Signup_Exist;
        else
        {
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            CompanyInfo.addCompany(groupName, email, password);
            resultStr = Signup_Completed;
        }

        Response.Clear();
        Response.Write(resultStr);
        Response.End();
    }
}

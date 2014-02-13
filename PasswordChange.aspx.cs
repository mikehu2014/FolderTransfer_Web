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

public partial class PasswordChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["IsResetPassword"] == null) || (Session["IsResetPassword"].ToString() != "Yes"))
            {
                Response.Redirect("RemoteGroup.aspx");
                return;
            }
        }

        lbAccountName.Text = Request.QueryString["accountname"].ToString();
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        string AccountName = Session["AccountName"].ToString();
        string Password = txtPassword.Text;
        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "MD5");
        CompanyInfo.changePassword(AccountName, Password);

        Session["IsResetPassword"] = null;
        ForgetPasswordInfo.removeForgetPassword(AccountName);

        lbResult.Text = "Reset Password successfully.";
    }
}

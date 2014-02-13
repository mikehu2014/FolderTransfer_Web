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

public partial class PasswordForgetCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string AccountName = Request.QueryString["accountname"];
        string Uid = Request.QueryString["sid"];

        if (ForgetPasswordInfo.checkForgetPassword(AccountName, Uid))
        {
            Session["IsResetPassword"] = "Yes";
            Session["AccountName"] = AccountName;
            Response.Redirect("PasswordChange.aspx?accountname=" + AccountName);
        }
        else
            lbResult.Text = "the link is expired";

    }
}

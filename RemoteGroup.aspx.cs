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
using IMSite.Data;
using BackupCowWeb;

namespace BackupCowWeb
{
    public partial class RemoteGroup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RemoteNavigate.ShowNavigate(this, "Group Signup");


        }


        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!checkCheckCode())
                return;

            string accountName = txtAccountName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (checkExist(accountName))
                lbResult.Text = "Group name is exist";
            else
            {
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                registerAccount(accountName, email, password);
                Response.Redirect("SignupCompleted.aspx");
            }

        }

        private bool checkExist(string accountName)
        {
            return CompanyInfo.checkExist(accountName);
        }

        private void registerAccount(string accountName, string email, string password)
        {
            CompanyInfo.addCompany(accountName, email, password);
        }

        private Boolean checkCheckCode()
        {
            Boolean IsValid = false;
            if (HttpContext.Current.Session["CheckCode"] != null)
            {
                string InputCode = txtSecurityCode.Text;
                string CheckCode = HttpContext.Current.Session["CheckCode"].ToString();
                IsValid = string.Compare(CheckCode, InputCode, true) == 0;
            }
            return IsValid;
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = checkCheckCode();
        }
}
}

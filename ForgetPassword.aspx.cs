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
using System.Net.Mail;
using System.Net; 

public partial class ForgetPassword : System.Web.UI.Page
{
    const string SmtpServer = "smtp.gmail.com";
    const int SmtpPort = 587;
    const string Gmail_Account = "backupcowforget@gmail.com";
    const string Gmail_Password = "share18bc";

    protected void Page_Load(object sender, EventArgs e)
    {
        RemoteNavigate.ShowNavigate(this, "Forget Password");
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!checkCheckCode() || !checkCompanyExist())
            return;

        string AccountName = txtAccountName.Text;
        string email = CompanyInfo.getEmail( AccountName );
        if (email == "")
            return;

        string Uid = System.Guid.NewGuid().ToString();
        
            // 添加到数据库
        ForgetPasswordInfo.addForgetPassword(AccountName, Uid);

            // 改密码地址
        string Url = getUrl(AccountName, Uid);
            
            // 邮件内容
        string EmailContent = "Please click the link " + Url + " to Reset Password.";

        SmtpClient mailClient = new SmtpClient(SmtpServer, SmtpPort);
        mailClient.EnableSsl = true;
        NetworkCredential crendetial = new NetworkCredential(Gmail_Account, Gmail_Password);
        mailClient.Credentials = crendetial;
        MailMessage message = new MailMessage(Gmail_Account, email, "BackupCow Reset Password", EmailContent);
        mailClient.Send(message);

        lbResult.Text = "Completed. Please check your email " + email + " and click the url to reset password.";

            // 清除过期信息
        ForgetPasswordInfo.ClearOutDateForgetPassword();
    }

    private string getUrl(string AccountName, string Uid)
    {
        string Url = "http://www.foldertransfer.com/";
        //string Url = "http://127.0.0.1:2772/WebSite/";
        Url = Url + "PasswordForgetCheck.aspx?accountname=" + AccountName;
        Url = Url + "&sid=" + Uid;
        return Url;
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

    private Boolean checkCompanyExist()
    {
        return CompanyInfo.checkExist(txtAccountName.Text); 
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = checkCheckCode(); 
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = checkCompanyExist();
    }
}

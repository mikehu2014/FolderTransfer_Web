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
using IMSite.Common.Tools;

namespace WebApplication1
{
    public partial class SendContactUs : System.Web.UI.Page
    {
        const String mailcontent = @"<table width='100%' border='0' cellspacing='3' cellpadding='0'>" +
                  "<tr>" +
                  "  <td width='9%'>Name</td>" +
                 "   <td width='91%'>{0}</td>" +
                 " </tr>" +
                 " <tr>" +
                 "   <td>Email</td>" +
                 "   <td>{1}</td>" +
                 " </tr>" +
                 " <tr>" +
                 "   <td>Contact</td>" +
                 "   <td>[{2}]</td>" +
                 " </tr>" +
                 " <tr>" +
                 "   <td>Subject</td>" +
                 "   <td>{3}</td>" +
                 " </tr>" +
                 " <tr>" +
                 "   <td>Message</td>" +
                 "   <td>{4}</td>" +
                 " </tr>" +
                 "</table>";

        String mailsubject = string.Empty;
        String name;
        String email;
        String contact;
        String subject;
        String message;
        String valid;

        protected void Page_Load(object sender, EventArgs e)
        {
            mailsubject = "Chat4support Contact Us";
            if (Request.RequestType == "POST")
            {
                CheckInput();
                String c = String.Format(mailcontent, name, email, contact, subject, message);
                IMSite.SendMail m = new IMSite.SendMail();
                m.ReplyTo = email;
                m.Content = c;
                m.Subject = mailsubject;
                m.MailFrom = ConfigurationManager.AppSettings["sysSendEmail"];
                m.MailTo = ConfigurationManager.AppSettings["SupportEmail"];
                m.SockSend(-1);
                //if (!String.IsNullOrEmpty(m.Error))
                //    JScript.Alert("Submit Failed. Please try again");
                //else
                    JScript.Alert("Your mail has been sent successfully! We will reply you later. Thanks!");
                JScript.GoBack();                
            }
        }

        private void CheckInput()
        {
            name = GetString("name") + " (" + GetRealIP() + ")";
            email = GetString("email");
            contact = GetString("contact");
            subject = GetString("subject");
            message = GetString("message");
            valid = GetString("valid");

            //if (Session["CheckCode"] == null || string.Compare(Session["CheckCode"].ToString(), valid, false) != 0)
            //{
            //    JScript.Alert("Security Code is wrong.");
            //    JScript.GoBack();
            //}
            //else 
            if (String.IsNullOrEmpty(email))
            {
                JScript.GoBack();
            }
        }

        String GetString(String key)
        {
            if (!String.IsNullOrEmpty(Request.Form[key]))
            {
                return Request.Form[key];
            }
            else
                return String.Empty;
        }

        #region 获取真实IP

        static public string GetRealIP()
        {
            string userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIP) || userIP.IndexOf("unknown", StringComparison.OrdinalIgnoreCase) > -1)
            {
                userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                if (string.IsNullOrEmpty(userIP) || userIP.IndexOf("unknown", StringComparison.OrdinalIgnoreCase) > -1)
                    userIP = System.Web.HttpContext.Current.Request.UserHostAddress;
                else if (userIP.IndexOf(",") > -1)
                    userIP = userIP.Substring(0, userIP.IndexOf(","));
            }
            else if (userIP.IndexOf(",") > -1)
                userIP = userIP.Substring(0, userIP.IndexOf(","));

            return userIP.Trim();
        }

        #endregion
    }
}

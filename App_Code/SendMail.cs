using System;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Threading;
using IMSite.Data;
using IMSite.Common.Tools;
namespace IMSite
{
    /// <summary>
    /// SendMail 的摘要说明
    /// </summary>
    public class SendMail
    {
        string _mailFrom;
        string _mailTo;
        string _subject;
        string _content;
        String _Error;
        String _replyto;

        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }

        public string ReplyTo
        {
            get { return _replyto; }
            set { _replyto = value; }
        }

        public string MailFrom
        {
            get { return _mailFrom; }
            set { _mailFrom = value; }
        }

        public string MailTo
        {
            get { return _mailTo; }
            set { _mailTo = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }


        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public SendMail()
        {

        }

        public SendMail(string subject, string content, string from, string to)
        {
            _subject = subject;
            _content = content;
            _mailFrom = from;
            _mailTo = to;
        }
        
        public void Send(int sid)
        {
            SockSend(sid);
        }

        public void Send(object stat, int sid)
        {
            SendMail s = (SendMail)stat;
            s.SockSend(sid);
        }

        public void SockSend(int sid)
        {
            Lymph.Net.Mail.MailMessage msg = new Lymph.Net.Mail.MailMessage();
            msg.ReplyTo = _mailTo;
            msg.From = _mailFrom;
            msg.To = _mailTo;
            msg.Subject = _subject;
            msg.Body = _content;
            msg.MailFormat = Lymph.Net.Mail.MailMessage.BodyFormat.HTML;
            Lymph.Net.Mail.SmtpServer srv = new Lymph.Net.Mail.SmtpServer();
            srv.Server = ConfigurationManager.AppSettings["sysSendSmtp"];
            srv.Port = 25;
            srv.RequireAuthorization = true;
            srv.AuthUser = ConfigurationManager.AppSettings["sysSendEmail"];
            srv.AuthPass = ConfigurationManager.AppSettings["sysSendEmailPwd"];
            try
            {
                if (!srv.Send(msg))
                    _Error = "send email failed.";               
            }
            catch (Exception ex)
            {
                _Error = ex.Message;
                JScript.Alert(_Error);
            }
        }
    }
}
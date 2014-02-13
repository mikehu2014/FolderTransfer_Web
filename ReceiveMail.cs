using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using IMSite.Data;

namespace WebApplication1
{
    public class ReceiveMail
    {
        public static string _sysPop3;
        public static int _sysPort;
        public static bool _sysSSL;
        public static string _sysEmail;
        public static string _sysEmailPwd;
        public static string _sysSender;

        public static string sysPop3
        {
            get { return ReceiveMail._sysPop3; }
            set { ReceiveMail._sysPop3 = value; }
        }

        public static int sysPort
        {
            get { return ReceiveMail._sysPort; }
            set { ReceiveMail._sysPort = value; }
        }

        public static bool sysSSL
        {
            get { return ReceiveMail._sysSSL; }
            set { ReceiveMail._sysSSL = value; }
        }

        public static string sysEmail
        {
            get { return ReceiveMail._sysEmail; }
            set { ReceiveMail._sysEmail = value; }
        }

        public static string sysEmailPwd
        {
            get { return ReceiveMail._sysEmailPwd; }
            set { ReceiveMail._sysEmailPwd = value; }
        }

        public static string sysSender
        {
            get { return ReceiveMail._sysSender; }
            set { ReceiveMail._sysSender = value; }
        }

        public ReceiveMail() { }

        public ReceiveMail(string pop3, int port, bool ssl, string email, string pwd, string sender)
        {
            _sysPop3 = pop3;
            _sysPort = port;
            _sysSSL = ssl;
            _sysEmail = email;
            _sysEmailPwd = pwd;
            _sysSender = sender;
        }

        public void Receive()
        {
            Pop3MailClient GmailClient = new Pop3MailClient(_sysPop3, _sysPort, _sysSSL, _sysEmail, _sysEmailPwd);
            GmailClient.IsAutoReconnect = true;
            GmailClient.ReadTimeout = 60000;
            try
            {
                GmailClient.Connect();
            }
            catch
            {
                return;
            }

            int NumberOfMails, MailboxSize;
            GmailClient.GetMailboxStats(out NumberOfMails, out MailboxSize);
            if (NumberOfMails == 0)
            {
                GmailClient.Disconnect();
                return;
            }

            List<EmailUid> EmailUids;
            if (GmailClient.GetUniqueEmailIdList(out EmailUids))
            {
                int i, n;
                string eMailContent, OrderId = string.Empty, tmpText, tmpText2;
                DateTime OrderDate = DateTime.UtcNow.Date;
                int OrderDays = 365, OrderEdition = 0;

                string sql = string.Empty;
                Conn con = new Conn();
                for (i = 0; i < EmailUids.Count; i++)
                {
                    GmailClient.GetRawEmail(EmailUids[i].EmailId, sysSender, out eMailContent);                 
                    if (!string.IsNullOrEmpty(eMailContent))
                    {
                        sql = "Insert Into OrderEmail (Uid, EmailContent) values ('" + EmailUids[i].Uid + "', '" + eMailContent + "')";
                        try
                        {
                            con.ExecuteNonQueryD(sql);
                        }
                        catch { }
                        n = eMailContent.IndexOf("Date:");
                        if (n > -1)
                        {
                            tmpText = eMailContent.Substring(n + 6);
                            n = tmpText.IndexOf("\r\n");
                            if (n > -1)
                            {
                                tmpText = tmpText.Substring(0, n).Trim();
                                n = tmpText.IndexOf(":");
                                if (n > -1)
                                {
                                    tmpText2 = tmpText.Substring(0, n - 2);
                                    tmpText2 = tmpText2 + tmpText.Substring(n + 7);
                                    DateTime.TryParse(tmpText2, out OrderDate);
                                }
                            }
                        }

                        n = eMailContent.IndexOf("Order Number:");
                        if (n > -1)
                        {
                            OrderId = eMailContent.Substring(n + 14);
                            n = OrderId.IndexOf("\r\n");
                            if (n > -1)
                                OrderId = OrderId.Substring(0, n).Trim();
                        }
                        n = eMailContent.IndexOf("Product Number:");
                        if (n > -1)
                        {
                            tmpText = eMailContent.Substring(n + 15);
                            n = tmpText.IndexOf("\r\n");
                            if (n > -1)
                            {
                                tmpText = tmpText.Substring(0, n);
                                if (string.Compare(tmpText.Trim(), "45423-kcstdm") == 0)
                                {
                                    OrderEdition = 1;
                                    OrderDays = 30;
                                }
                                else if (string.Compare(tmpText.Trim(), "45423-kcstdy") == 0)
                                {
                                    OrderEdition = 1;
                                    OrderDays = 365;
                                }
                            }
                        }
                        if (OrderEdition > 0)
                        {
                            sql = "Insert Into Orders (OrderID, Days, OrderEdition, OrderDate, adminid, isValid) Values ('" + OrderId + "', " + OrderDays + ", " + OrderEdition + ", '" + OrderDate + "', -1, 1)";
                            try
                            {
                                con.ExecuteNonQueryD(sql);
                            }
                            catch { }
                        }
                    }
                }                
            }
            GmailClient.Disconnect();
        }
    }
}

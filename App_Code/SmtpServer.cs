using System;
using System.Text;
using Lymph.Net;
using System.Net.Sockets;

namespace Lymph.Net.Mail
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class SmtpServer
    {
        private string sServer;
        private int iPort;
        private bool bAuth = false;
        private string sUser;
        private string sPass;
        private StringBuilder sbLog = null;
        private bool bLog = true;
        private SocketHelper helper = null;
        private TalkState state = TalkState.WaitInit;

        public enum TalkState
        {
            WaitInit, Initialized, SignedIn, LoginFailed, Ended
        }

        protected SocketHelper Helper
        {
            get
            {
                if (helper == null)
                    helper = new SocketHelper(this.Server, this.Port);

                return helper;
            }
        }

        public bool LogEvent
        {
            get
            {
                return bLog;
            }
            set
            {
                bLog = value;
            }
        }

        public string LogMessage
        {
            get
            {
                if (this.LogEvent)
                    return sbLog.ToString();
                else
                    return "";
            }
        }

        public string Server
        {
            get
            {
                return sServer;
            }
            set
            {
                sServer = value;
            }
        }

        public int Port
        {
            get
            {
                return iPort;
            }
            set
            {
                iPort = value;
            }
        }

        public bool RequireAuthorization
        {
            get
            {
                return bAuth;
            }
            set
            {
                bAuth = value;
            }
        }

        public string AuthUser
        {
            get
            {
                return sUser;
            }
            set
            {
                sUser = value;
            }
        }

        public string AuthPass
        {
            get
            {
                return sPass;
            }
            set
            {
                sPass = value;
            }
        }

        public SmtpServer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Init()
        {
            Log(helper.GetFullResponse());
            Helper.SendCommand("EHLO " + this.Server);
            Log(Helper.GetFullResponse());
            state = TalkState.Initialized;
        }

        public bool Login()
        {
            if (this.RequireAuthorization)
            {
                Helper.SendCommand("AUTH LOGIN");
                Log(Helper.GetFullResponse());
                Helper.SendCommand(
                    Convert.ToBase64String(
                    Encoding.Default.GetBytes(this.AuthUser)));
                Log(Helper.GetFullResponse());
                Helper.SendCommand(
                    Convert.ToBase64String(
                    Encoding.Default.GetBytes(this.AuthPass)));
                Log(Helper.GetFullResponse());

                if (Helper.GetResponseState() == 235)
                {
                    state = TalkState.SignedIn;
                    return true;
                }
                else
                {
                    state = TalkState.LoginFailed;
                    return false;
                }
                //				return (Helper.GetResponseState()==235);
            }
            else
            {
                state = TalkState.SignedIn;
                return true;
            }
        }

        public void SendKeep(MailMessage msg)
        {
            if ((state == TalkState.Initialized && !this.RequireAuthorization) || state == TalkState.SignedIn)
            {
                Helper.SendCommand("MAIL From:" + msg.From);
                Log(Helper.GetFullResponse());
                Helper.SendCommand("RCPT To:" + msg.To);
                Log(Helper.GetFullResponse());
                Helper.SendCommand("DATA");
                Log(Helper.GetFullResponse());
                Helper.SendData(Encoding.Default.GetBytes(msg.ToString()));
                Helper.SendCommand(".");
                Log(Helper.GetFullResponse());
            }
        }

        public void Quit()
        {
            Helper.SendCommand("QUIT");
            Helper.Close();
            state = TalkState.Ended;
        }

        public bool Send(MailMessage msg)
        {
            if (state == TalkState.Ended)
                return false;

            Log(Helper.GetFullResponse());
            Helper.SendCommand("EHLO " + this.Server);
            Log(Helper.GetFullResponse());
            if (this.RequireAuthorization)
            {
                Helper.SendCommand("AUTH LOGIN");
                Log(Helper.GetFullResponse());
                Helper.SendCommand(
                    Convert.ToBase64String(
                    Encoding.Default.GetBytes(this.AuthUser)));
                Log(Helper.GetFullResponse());
                Helper.SendCommand(
                    Convert.ToBase64String(
                    Encoding.Default.GetBytes(this.AuthPass)));
                Log(Helper.GetFullResponse());
                if (Helper.GetResponseState() != 235)
                {
                    Helper.SendCommand("QUIT");
                    Helper.Close();
                    state = TalkState.Ended;
                    return false;
                }
            }
            Helper.SendCommand("MAIL From:<" + msg.From+">");
            Log("MAIL From:" + Helper.GetFullResponse());
            Helper.SendCommand("RCPT To:<" + msg.To + ">");
            Log("RCPT To:" + Helper.GetFullResponse());
            Helper.SendCommand("DATA");
            Log(Helper.GetFullResponse());
            Helper.SendData(Encoding.Default.GetBytes(msg.ToString()));
            Helper.SendCommand(".");
            Log(Helper.GetFullResponse());
            Helper.SendCommand("QUIT");
            Helper.Close();
            state = TalkState.Ended;

            return true;
        }

        private void Log(string msg)
        {
            if (this.LogEvent)
            {
                if (sbLog == null)
                    sbLog = new StringBuilder();

                sbLog.AppendFormat("{0}\r\n", msg);
            }
        }

    }
}
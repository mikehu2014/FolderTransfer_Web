using System;
using System.Text;
using System.Security;

namespace Lymph.Net.Mail
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class MailMessage
	{
		public enum BodyFormat
		{
			HTML,TEXT
		}

		private string sFrom;
		private string sTo;
		private string sSubject;
		private BodyFormat bfFormat=BodyFormat.TEXT;
		private string sBody;
		private string sFromName;
		private string sToName;

        public string _CharSet;
        

		public string FromName
		{
			get
			{
				if(sFromName==null || sFromName=="")
					sFromName=sFrom;

				return sFromName;
			}
			set
			{
				sFromName=value;
			}
		}

        String _replyto;
		public string ReplyTo
		{
			get
			{
                return _replyto;
			}
			set
			{
                _replyto = value;
			}
		}

		public string ToName
		{
			get
			{
				if(sToName==null || sToName=="")
					sToName=sTo;

				return sToName;
			}
			set
			{
				sToName=value;
			}
		}

		public string From
		{
			get
			{
				return sFrom;
			}
			set
			{
				sFrom=value;
			}
		}

		public string To
		{
			get
			{
				return sTo;
			}
			set
			{
				sTo=value;
			}
		}

		public string Subject
		{
			get
			{
				return sSubject;
			}
			set
			{
				sSubject=value;
			}
		}

		public string Body
		{
			get
			{
				return sBody;
			}
			set
			{
				sBody=value;
			}
		}

		public BodyFormat MailFormat
		{
			get
			{
				return bfFormat;
			}
			set
			{
				bfFormat=value;
			}
		}

		public MailMessage()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override string ToString()
		{
			StringBuilder sb=new StringBuilder();

            //add by peter 2008.6.13
            
            _CharSet = "utf-8";
            Subject = "=?" + _CharSet + "?B?" + encodebase64(_CharSet, Subject) + "?=";
            Body = encodebase64(_CharSet, Body);



            if(!String.IsNullOrEmpty(_replyto))
                sb.AppendFormat("Reply-To: <{0}>\r\n", _replyto);
			sb.AppendFormat("From: {0}\r\n",this.FromName);
			sb.AppendFormat("To: {0}\r\n",this.ToName);
            sb.AppendFormat("Date: {0}\r\n", string.Concat(DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss ", new System.Globalization.CultureInfo("en-us")) , DateTime.Now.ToString("zzz").Replace(":", "")));
			sb.AppendFormat("Subject: {0}\r\n",this.Subject);
			sb.AppendFormat("X-Mailer: Net.Mail.SmtpServer\r\n");
			switch(this.MailFormat)
			{
				case BodyFormat.TEXT:
                    sb.AppendFormat("Content-type:text/plain;Charset=" + _CharSet + "\r\n");
					break;
				case BodyFormat.HTML:
                    sb.AppendFormat("Content-type:text/html;Charset=" + _CharSet + "\r\n");
					break;
				default:
					break;
			}
            sb.AppendFormat("Content-Transfer-Encoding: base64\r\n");
            
			sb.AppendFormat("\r\n{0}\r\n",this.Body);
			return sb.ToString();
        }

        public string encodebase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        } 

        public void test()
        {
            Lymph.Net.Mail.MailMessage msg = new Lymph.Net.Mail.MailMessage();
            msg.ReplyTo = "root@chat4support.jp";
            msg.From = "root@chat4support.jp";
            msg.To = "peta@chat4support.com";
           /*
            msg._CharSet = "ISO-2022-JP";
            msg.Subject = "=?"+msg._CharSet+"?B?" + encodebase64(msg._CharSet, "現在のフォルダ") + "?=";
            msg.Body = encodebase64(msg._CharSet, "現在のフォルダ");

            msg._CharSet = "utf-8";
            msg.Subject = "=?" + msg._CharSet + "?B?" + encodebase64(msg._CharSet, "現在のフォルダ") + "?=";
            msg.Body = encodebase64(msg._CharSet, "現在のフォルダ中文");
           */

            msg.Subject ="現在の一合计フォルダ";
            msg.Body = "現在のフォルダ<asdasd>asdaskdhq21knz8sdya`12`1";
            
            msg.MailFormat = Lymph.Net.Mail.MailMessage.BodyFormat.HTML;
            msg.MailFormat = BodyFormat.TEXT;

            Lymph.Net.Mail.SmtpServer srv = new Lymph.Net.Mail.SmtpServer();
            srv.Server = "smtp.chat4support.jp";
            srv.Port = 25;
            srv.RequireAuthorization = true;
            srv.AuthUser = "root@chat4support.jp";
            srv.AuthPass = "c4sjp";
            try
            {
                srv.Send(msg);
            }
            catch (Exception ex)
            {
                
                //_Error = 
                //WebLog.Error("send email error:" + _mailTo + " ,subject=" + _subject + ",exception=" + ex.Message);
            }
        }
	}
}
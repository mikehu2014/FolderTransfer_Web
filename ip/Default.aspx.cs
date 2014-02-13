using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text; 

namespace ip
{
    public partial class _Default : System.Web.UI.Page
    {
        const string HttpParams_Act = "act";
        const string HttpParams_Ip = "ip";
        const string HttpParams_Port = "port";
        const string HttpParams_PcID = "pcid";

        const string Action_GetIP = "getip";
        const string Action_GetIsConn = "getisconn";
        const string Action_PcOS = "pcos"; 

        const string TcpParams_InternetCheck = "internetcheck";

        const string HttpResult_Success = "success";
        const string HttpResult_Failure = "failure";

        protected void Page_Load(object sender, EventArgs e)
        {
            string act = Request.QueryString[ HttpParams_Act ];
            if (string.IsNullOrEmpty(act)) act = Action_GetIP;;

            if (act == Action_GetIP)
            {
                Response.Write(GetRealIP());
            }
            else if (act == Action_PcOS)
            {
        
                Response.Clear();
                Response.Write(Request.UserAgent);
                Response.End();
               
            }
            else if (act == Action_GetIsConn)
            {
                string Ip = Request.QueryString[HttpParams_Ip];
                string Port = Request.QueryString[HttpParams_Port];
                string PcID = Request.QueryString[HttpParams_PcID];

                TcpClient TcpSocket = new TcpClient();
                TcpSocket.Connect(IPAddress.Parse(Ip), Int32.Parse(Port));
                NetworkStream ns = TcpSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                string result = "";
                try
                {
                    //result = sr.ReadLine();
                    sw.WriteLine(TcpParams_InternetCheck);
                    result = sr.ReadLine();
                }
                catch
                {

                }

                sw.Close();
                sr.Close();
                ns.Close();
                TcpSocket.Close();

                string HttpResult = "";
                if (result == PcID)
                    HttpResult = HttpResult_Success;
                else
                    HttpResult = HttpResult_Failure;

                Response.Clear();
                Response.Write(HttpResult);
                Response.End();
            }
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

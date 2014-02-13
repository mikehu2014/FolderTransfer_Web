using System;
using System.Web;

namespace IMSite.Common.Tools
{
    public class JScript
    {
        public static void Alert(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "');</script>");
        }

        public static void GoBack()
        {
            HttpContext.Current.Response.Write("<script>history.go(-1);</script>");
        }

        public static void AlertAndGoBack(string msg)
        {
            Alert(msg);
            GoBack();
            HttpContext.Current.Response.End();
        }
    }
}

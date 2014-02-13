using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace BackupCowWeb
{
    /// <summary>
    /// RemoteNavigate 的摘要说明
    /// </summary>
    public class RemoteNavigate
    {
        public RemoteNavigate()
        {

        }

        static public void ShowNavigate(System.Web.UI.Page p, string showstr)
        {
            Label lb = p.Master.FindControl("lbNavigite1") as Label;
            lb.Visible = true;

            lb = p.Master.FindControl("lbNavigite2") as Label;
            lb.Visible = true;
            lb.Text = showstr;
        }
    }
}

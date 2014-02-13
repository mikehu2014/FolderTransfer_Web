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
using BackupCowWeb.Activate;
 

public partial class DataBaseManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Cmd = Request.QueryString["cmd"];
        if (Cmd == "refreshtrialpcid")
        {
            TrialHandle.refreshTrial();

            Response.Clear();
            Response.Write("Refresh Completed");
            Response.End();
        }
        else if (Cmd == "refreshactivatepcid")
        {
            Activation.refreshActivate();

            Response.Clear();
            Response.Write("Refresh Completed");
            Response.End();
        }

    }
}

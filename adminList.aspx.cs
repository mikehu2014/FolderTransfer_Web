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

namespace WebApplication1
{
    public partial class adminList : IMSite.Admin.SupperAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            e.Values["password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(e.Values["password"].ToString(), "MD5");
        }

        protected void DetailsView1_DataBound(object sender, EventArgs e)
        {
            if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
            {
                ((TextBox)DetailsView1.Rows[2].FindControl("txtPassword")).Text = string.Empty;
            }
        }

        protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (DetailsView1.CurrentMode == DetailsViewMode.Edit &&
                string.IsNullOrEmpty(((TextBox)DetailsView1.Rows[2].FindControl("txtPassword")).Text.Trim()))
            {
                e.NewValues["password"] = e.OldValues["password"];
            }
            else
                e.NewValues["password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(e.NewValues["password"].ToString(), "MD5");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (GridView1.Rows.Count == 1)
            {
                e.Cancel = true;
                return;
            }
        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            GridView1.DataBind();
            GridView1.SelectedIndex = GridView1.Rows.Count - 1;
        }

        protected void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            GridView1.DataBind();
        }
    }
}

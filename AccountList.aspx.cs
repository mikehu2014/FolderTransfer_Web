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

namespace BackupCowWeb
{
    public partial class AccountList : IMSite.Admin.SupperAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvCompany_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string password = e.NewValues["Password"].ToString();
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            e.NewValues["Password"] = password;
        }

        protected void gvCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanyName = gvCompany.SelectedRow.Cells[1].Text;
            string url = "AccountPcList.aspx?CompanyName=" + CompanyName; 
            Response.Redirect(url);
        }

        protected void btnAddAcount_Click(object sender, EventArgs e)
        {

        }
        protected void btnSoryBySignpTime_Click(object sender, EventArgs e)
        {
            sqlDataCompany.SelectCommand = "SELECT CompanyName, SignupTime, GroupUser FROM CompanyNetwork ORDER BY SignupTime DESC";
            sqlDataCompany.DataBind();
            gvCompany.DataBind();
        }
        protected void btnSoryByGroupUser_Click(object sender, EventArgs e)
        {
            sqlDataCompany.SelectCommand = "SELECT CompanyName, SignupTime, GroupUser FROM CompanyNetwork ORDER BY GroupUser DESC";
            sqlDataCompany.DataBind();
            gvCompany.DataBind();
        }
}
}

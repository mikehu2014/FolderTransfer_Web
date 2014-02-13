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
using Decryption;
using BackupCowWeb.Order;

namespace WebApplication1
{
    public partial class WebForm2 : IMSite.Admin.SupperAdminPage
    {
        const int dvOrderRot_OrderID = 1;
        const int dvOrderRot_ = 2;
        const int dvOrderRow_Edition = 5;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            Boolean isSuccess;
            string dayStr = e.NewValues["Days"].ToString();
            string additionUserStr = e.NewValues["AdditionCount"].ToString();
            int days, additionUserCount;

            e.NewValues["adminid"] = AdminId;

            isSuccess = int.TryParse(dayStr, out days);
            isSuccess = isSuccess && int.TryParse(additionUserStr, out additionUserCount);
            
            lbSuccess.Visible = isSuccess;
            lbFailure.Visible = !isSuccess;
            e.Cancel = !isSuccess;
        }

        protected void DetailsView1_DataBound(object sender, EventArgs e)
        {
            //if (DetailsView1.Rows.Count > 0 )
            //{
            //    string orderDateStr = DetailsView1.Rows[dvOrderRot_OrderID].Cells[1].Text.Trim();
            //    DateTime t;
            //    if (DateTime.TryParse(orderDateStr, out t))
            //        DetailsView1.Rows[dvOrderRot_OrderID].Cells[1].Text = t.ToLongDateString();    
            //}
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string orderID = GridView1.Rows[e.RowIndex].Cells[0].Text;
            Order.addActivateCount(orderID, -1);
            DetailsView1.DataBind();
        }
    }
}

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
using IMSite.Data;

namespace WebApplication1
{
    public partial class OrderAdd : IMSite.Admin.SupperAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtBuyDays.Text = "" + 36500;
                ddlEdition.SelectedIndex = 1;
                txtAdditionUsers.Text = "" + 0;
                txtOrderDate.Text = DateTime.UtcNow.ToString();
                Conn con = new Conn();
                DataTable dt = con.ExecuteDataTable("select Username from Administrator where adminId = " + AdminId, false);
                if (dt.Rows.Count > 0)
                    lbOperator.Text = dt.Rows[0]["Username"].ToString();
            }
        }

        protected void BtnAddOrder_Click(object sender, EventArgs e)
        {
            int days = 0, userCount = 0, additionCount = 0;           
            DateTime t = DateTime.UtcNow;
            int edition = int.Parse(ddlEdition.SelectedValue);
            int isvalid = chkIsVerifled.Checked?1:0;
            Boolean IsSuccess = !string.IsNullOrEmpty(txtOrderID.Text.Trim());
            IsSuccess = IsSuccess && int.TryParse(txtBuyDays.Text.Trim(), out days);
            IsSuccess = IsSuccess && int.TryParse(txtBuyUsers.Text, out userCount);
            IsSuccess = IsSuccess && int.TryParse(txtAdditionUsers.Text, out additionCount);
            IsSuccess = IsSuccess && DateTime.TryParse(txtOrderDate.Text.Trim(), out t);


            if( IsSuccess )
            {
                string sql = "insert into Orders (OrderID, Days, OrderEdition, OrderDate, isValid, adminid, UserCount, ActiveUser, AdditionCount)"; 
                sql = sql + " values ('" + txtOrderID.Text.Trim() + "', " + days;
                sql = sql + ", " + edition + ", '" + t + "', " + isvalid + ", " + AdminId +"," + userCount + ", " + 0 + ", " + additionCount + ")";
                Conn con = new Conn();
                int obj = 0;
                try {
                    obj = con.ExecuteNonQueryD(sql);
                }
                catch{}
                IsSuccess = obj == 1? true:false;
            }
            lbSuccess.Visible = IsSuccess;
            lbFailure.Visible = !IsSuccess;

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }


    }
}

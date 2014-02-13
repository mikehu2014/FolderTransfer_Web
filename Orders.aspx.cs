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
using BackupCowWeb.Activate;

namespace WebApplication1
{
    public partial class WebForm1 : IMSite.Admin.SupperAdminPage
    {
        const int orderRow_OrderID = 1;
        const int ordersRow_IsVaild = 10;
        const string isValidSign_True = "¡Ì";
        const string isValidSign_False = "¡Á";
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView1.SelectedIndex = e.RowIndex;

            string orderID = GridView1.Rows[e.RowIndex].Cells[orderRow_OrderID].Text;
            Activation.deleteOrder(orderID);

        }

        private string getSelectOrderSql()
        {
            string sql = "SELECT isValid, OrderID, UserCount, ActiveUser, Username, Days, OrderDate, AccountName, Email, EditionName ";
            sql = sql + "FROM Orders INNER JOIN Edition ON Orders.OrderEdition = Edition.EditionID ";
            sql = sql + "LEFT OUTER JOIN Administrator ON Administrator.adminId = Orders.adminid";

            return sql;
        }

        private string getSelectOrderSqlOrderBy()
        {
            return " ORDER BY Orders.OrderDate DESC";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string orderID = txtOrderID.Text.Trim();
            if (string.IsNullOrEmpty(orderID))
                return;
            string wheresql = "where Orders.OrderID = '" + orderID + "'";
            string sql = "SELECT Orders.isValid, Orders.OrderID, Orders.UserCount, Orders.ActiveUser, CASE WHEN orders.adminid = - 1 THEN 'Administrator' ELSE Administrator.Username END as UserName, Orders.Days, Orders.OrderDate, Orders.Email, Edition.EditionName, Orders.AccountName, Orders.AdditionCount FROM Orders INNER JOIN Edition ON Orders.OrderEdition = Edition.EditionID LEFT OUTER JOIN Administrator ON Administrator.adminId = Orders.adminid " + wheresql + " ORDER BY Orders.OrderDate DESC";

            SqlDataSource1.SelectCommand = sql;
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {                
                //e.Row.Cells[7].Text = DateTime.Parse(e.Row.Cells[7].Text.Trim()).ToLongDateString();
                if (string.Compare(e.Row.Cells[ordersRow_IsVaild].Text, "True") == 0)
                    e.Row.Cells[ordersRow_IsVaild].Text = isValidSign_True;
                else if (string.Compare(e.Row.Cells[ordersRow_IsVaild].Text, "False") == 0)
                    e.Row.Cells[ordersRow_IsVaild].Text = isValidSign_False;
            }
        }

        protected void BtnViewAll_Click(object sender, EventArgs e)
        {
            string sql = getSelectOrderSql();
            sql = sql + getSelectOrderSqlOrderBy();

            SqlDataSource1.SelectCommand = sql;
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string isValidText = GridView1.Rows[e.NewSelectedIndex].Cells[ordersRow_IsVaild].Text.ToString().Trim(); 
            int isValid = string.Compare(isValidText, isValidSign_True) == 0?0:1;

            string orderID = GridView1.Rows[e.NewSelectedIndex].Cells[orderRow_OrderID].Text.ToString().Trim();

            string sql = "Update Orders set Isvalid = " + isValid + " where OrderID = '" + orderID + "'";

            Conn con = new Conn();
            con.ExecuteNonQueryD(sql);
            GridView1.DataBind();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            Conn con = new Conn();
            DataTable dt = con.ExecuteDataTable("select count(*) from orders", false);
            if (dt.Rows.Count > 0)
                Label2.Text = "Orders Count: " + dt.Rows[0][0].ToString();
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
         
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void btnActivate_Click(object sender, EventArgs e)
        {

        }
}
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IMSite.Data;

namespace BackupCowWeb.Order
{
    public class TOrderInfo
    {
        public string orderID;
        public int edition, days;
        public int userCount, activateCount;
        public int additionCount;

        public TOrderInfo(string _orderID)
        {
            orderID = _orderID;
        }

        public void setOrderInfo( int _edition, int _days )
        {
            edition = _edition;
            days = _days;
        }

        public void setUserInfo(int _userCount, int _activateCount)
        {
            userCount = _userCount;
            activateCount = _activateCount;
        }

        public void setAdditionCount(int _additionCount)
        {
            additionCount = _additionCount;
        }

    }

    public class TOrderAddInfo
    { 
        int AdminId;
        string OrderID;
        int Days, OrderEdition; 
        DateTime OrderDate; 
        int isValid;
        int UserCount, ActiveUser, AdditionCount;
        public TOrderAddInfo(string _OrderID, DateTime _OrderDate, int _UserCount)
        {
            OrderID = _OrderID;
            OrderDate = _OrderDate;
            UserCount = _UserCount;

            AdminId = -1;
            isValid = 1;
            ActiveUser = 0;
            AdditionCount = 0;
           
        }

        public void setEditionInfo( int _OrderEdition, int _Days )
        {
            OrderEdition = _OrderEdition;
            Days = _Days;
        }

        public void Add()
        {
            string sql = "insert into Orders (OrderID, Days, OrderEdition, OrderDate, isValid, adminid,";
            sql = sql + " UserCount, ActiveUser, AdditionCount)";
            sql = sql + " values ('" + OrderID + "', " + Days + ", " + OrderEdition + ", '" + OrderDate + "', " + isValid + ", " + AdminId + ",";
            sql = sql + + UserCount + ", " + ActiveUser + ", " + AdditionCount + ")"; 
            
            Conn con = new Conn();
            int obj = 0;
            try
            {
                obj = con.ExecuteNonQueryD(sql);
            }
            catch { };
        }
    }

    public class Order
    {


        static public Boolean addActivateCount( string orderID, int addCount)
        {
            TOrderInfo OrderInfo = getOrderInfo(orderID);

            if (OrderInfo == null)
                return false;

            int remainCount = OrderInfo.userCount + OrderInfo.additionCount;
            remainCount = remainCount - OrderInfo.activateCount;

            if (remainCount >= addCount)
            {
                int activeCount = OrderInfo.activateCount + addCount;

                Conn con = new Conn();
                string sql = "Update Orders set ActiveUser = " + activeCount + " where OrderID = '" + orderID + "'";
                con.ExecuteNonQueryD(sql);

                return true;
            }
            else
            {
                return false;
            }

        }

        static public TOrderInfo getOrderInfo(string orderID)
        {
            Conn con = new Conn();
            string sql = "SELECT UserCount, ActiveUser, AdditionCount, OrderEdition, Days FROM Orders Where OrderID = '" + orderID + "'";
            DataTable dt = con.ExecuteDataTable(sql, false);
            if (dt.Rows.Count > 0)
            {
                int userCount = 0;
                int edition = 0;
                int days = 0;
                int activateCount = 0;
                int additionCount = 0;

                int.TryParse(dt.Rows[0]["UserCount"].ToString(), out userCount);
                int.TryParse(dt.Rows[0]["OrderEdition"].ToString(), out edition);
                int.TryParse(dt.Rows[0]["Days"].ToString(), out days);
                int.TryParse(dt.Rows[0]["ActiveUser"].ToString(), out activateCount);
                int.TryParse(dt.Rows[0]["AdditionCount"].ToString(), out additionCount);

                TOrderInfo OrderInfo = new TOrderInfo(orderID);
                OrderInfo.setOrderInfo(edition, days);
                OrderInfo.setUserInfo(userCount, activateCount);
                OrderInfo.setAdditionCount(additionCount);
                return OrderInfo;
            };
            return null;
        }

        static public Boolean OrderExist(string orderID)
        {
            TOrderInfo o = getOrderInfo(orderID);
            return o != null;
        }
    }
}

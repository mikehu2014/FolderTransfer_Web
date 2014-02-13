<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="WebApplication1.WebForm1"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="contentpaneopen">
        <tr>
            <td style="height: 25px">
                Order ID:
                <asp:TextBox ID="txtOrderID" runat="server"></asp:TextBox>&nbsp; <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Width="82px" />&nbsp;&nbsp;&nbsp;
                &nbsp;
                <asp:Button ID="btnAddOrder" runat="server" PostBackUrl="~/OrderAdd.aspx" Text="Add a Order" />&nbsp;
                &nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>&nbsp; &nbsp;<asp:Button ID="btnActivate" runat="server" PostBackUrl="~/Activate/GetLicenseKey.aspx"
                    Text="Activate" Width="89px" OnClick="btnActivate_Click" /></td>
        </tr>
        <tr>
            <td style="height: 454px">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="OrderID" DataSourceID="SqlDataSource1" CellPadding="4"
                    CssClass="list versionCompare" ForeColor="#333333" GridLines="None" PageSize="20"
                    EmptyDataText="No displaying data records." OnRowDeleting="GridView1_RowDeleting"
                    OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnDataBound="GridView1_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="OrderID" DataNavigateUrlFormatString="~/OrderEdit.aspx?id={0}"
                            Text="Change" />
                        <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
                        <asp:BoundField DataField="EditionName" HeaderText="EditionName" SortExpression="EditionName" />
                        <asp:BoundField DataField="Days" HeaderText="Days" SortExpression="Days" />
                        <asp:BoundField DataField="UserCount" HeaderText="UserCount" SortExpression="UserCount" />
                        <asp:BoundField DataField="AdditionCount" HeaderText="AdditionUser" />
                        <asp:BoundField DataField="ActiveUser" HeaderText="ActiveUser" SortExpression="ActiveUser" />
                        <asp:BoundField DataField="Username" HeaderText="Operator" SortExpression="Username" />
                        <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" SortExpression="OrderDate" />
                        <asp:BoundField DataField="AccountName" HeaderText="AccountName" SortExpression="AccoutName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:CommandField DeleteText="Delete" ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
                    SelectCommand="SELECT Orders.isValid, Orders.OrderID, Orders.UserCount, Orders.ActiveUser, CASE WHEN orders.adminid = - 1 THEN 'Administrator' ELSE Administrator.Username END as UserName, Orders.Days, Orders.OrderDate, Orders.Email, Edition.EditionName, Orders.AccountName, Orders.AdditionCount FROM Orders INNER JOIN Edition ON Orders.OrderEdition = Edition.EditionID LEFT OUTER JOIN Administrator ON Administrator.adminId = Orders.adminid ORDER BY Orders.OrderDate DESC"
                    DeleteCommand="Delete From Orders where OrderID = @OrderID">
                    <DeleteParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="OrderID" PropertyName="DataKeyNames" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderEdit.aspx.cs" Inherits="WebApplication1.WebForm2"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="contentpaneopen">
        <tr>
            <td style="height: 328px">
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource1"
                    DefaultMode="Edit" Width="100%" CssClass="list versionCompare" OnItemUpdating="DetailsView1_ItemUpdating"
                    OnDataBound="DetailsView1_DataBound">
                    <Fields>
                        <asp:BoundField DataField="OrderID" HeaderText="Order ID:" ReadOnly="True">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date:" ReadOnly="True" />
                        <asp:BoundField DataField="Days" HeaderText="Days:">
                            <ControlStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserCount" HeaderText="Users:" ReadOnly="True" />
                        <asp:BoundField DataField="AdditionCount" HeaderText="Addition Users:" />
                        <asp:BoundField DataField="ActiveUser" HeaderText="Activate:" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Order Edition:" SortExpression="OrderEdition">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource3"
                                    DataTextField="EditionName" DataValueField="EditionID" SelectedValue='<%# Bind("OrderEdition") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource3"
                                    DataTextField="EditionName" DataValueField="EditionID" SelectedValue='<%# Bind("OrderEdition") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AccountName" HeaderText="Account Name:" />
                        <asp:BoundField DataField="Email" HeaderText="Email:">
                            <ControlStyle Width="400px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Memo" HeaderText="Memo:">
                            <ControlStyle Width="600px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="AdminId:" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Is Verified:" SortExpression="isValid">
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isValid") %>' Width="107px" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isValid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowCancelButton="False" />                        
                    </Fields>
                </asp:DetailsView>
                <asp:Label id="lbSuccess" runat="server" Text="Update orders successfully." Visible="False" ForeColor="Green"></asp:Label>&nbsp;
                <asp:Label ID="lbFailure" runat="server" ForeColor="Red" Text="Failed to update order."
                    Visible="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
                    SelectCommand="SELECT Orders.isValid, Orders.OrderID, Orders.OrderDate, Orders.Days, Orders.Email, Orders.UserCount, Orders.ActiveUser, Orders.OrderEdition, CASE WHEN orders.adminid = -1 THEN 'Administrator' ELSE Administrator.Username END as UserName, Orders.AccountName, Orders.AdditionCount, Orders.Memo FROM Orders LEFT OUTER JOIN Administrator ON Orders.adminid = Administrator.adminId WHERE (Orders.OrderID = @OrderID)"
                    UpdateCommand="UPDATE Orders SET Days = @Days, Email = @Email, OrderEdition = @OrderEdition, isValid = @isValid, adminid = @adminId, AccountName = @AccountName, AdditionCount = @AdditionCount, Memo = @Memo WHERE (OrderID = @OrderID)">
                    <UpdateParameters>
                        <asp:Parameter Name="Days" />
                        <asp:Parameter Name="Email" />
                        <asp:Parameter Name="OrderEdition" />
                        <asp:Parameter Name="UserCount" />
                        <asp:Parameter Name="isValid" />
                        <asp:Parameter Name="adminId" />
                        <asp:QueryStringParameter Name="OrderID" QueryStringField="id" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="OrderID" QueryStringField="id" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
                    SelectCommand="SELECT * FROM [Edition]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" CssClass="list versionCompare" DataKeyNames="ID" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="HardCode" HeaderText="HardCode" SortExpression="HardCode" />
                        <asp:BoundField DataField="PcID" HeaderText="PcID" />
                        <asp:BoundField DataField="PcName" HeaderText="PcName" SortExpression="PcName" />
                        <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                        <asp:BoundField DataField="ActivateTime" HeaderText="ActivateTime" SortExpression="ActivateTime" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:HyperLinkField DataNavigateUrlFields="OrderID,HardCode" DataNavigateUrlFormatString="~/Activate/GetLicenseKey.aspx?OrderID={0}&amp;HardCode={1}"
                            Text="License" />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
                    SelectCommand="SELECT HardCode, OrderID, ID, PcName, IP, ActivateTime, Status, PcID FROM Activate WHERE (OrderID = @OrderID)" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM Activate WHERE (ID = @original_ID)" InsertCommand="INSERT INTO [Activate] ([HardCode], [OrderID], [PcName], [IP], [ActivateTime]) VALUES (@HardCode, @OrderID, @PcName, @IP, @ActivateTime)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE Activate SET Status = @Status WHERE (ID = @original_ID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="OrderID" QueryStringField="id" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

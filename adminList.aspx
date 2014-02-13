<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminList.aspx.cs" Inherits="WebApplication1.adminList"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="contentpaneopen">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                    EmptyDataText="No displaying data records." DataKeyNames="adminId" CssClass="list versionCompare"
                    OnRowDeleting="GridView1_RowDeleting" Width="100%" AllowPaging="True">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" >
                            <ItemStyle Width="80px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="adminId" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="adminId" >
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="User Name" SortExpression="Username" >
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="password" HeaderText="Password" SortExpression="password" >
                            <ItemStyle Width="400px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="adminId"
                    DataSourceID="SqlDataSource2" Height="50px" CssClass="list versionCompare" Width="100%"
                    OnDataBound="DetailsView1_DataBound" OnItemInserting="DetailsView1_ItemInserting"
                    OnItemUpdating="DetailsView1_ItemUpdating" OnItemInserted="DetailsView1_ItemInserted"
                    OnItemUpdated="DetailsView1_ItemUpdated">
                    <Fields>
                        <asp:BoundField DataField="adminId" HeaderText="ID:" InsertVisible="False" ReadOnly="True"
                            SortExpression="adminId">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Username" HeaderText="User Name:" SortExpression="Username">
                            <HeaderStyle Width="80px" />
                            <ControlStyle Width="250px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Password:" SortExpression="password">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("password") %>' Width="250px"></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("password") %>' Width="250px"></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtPassword" runat="server" Text='<%# Bind("password") %>' Width="250px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowInsertButton="True" />
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        DeleteCommand="DELETE FROM [Administrator] WHERE [adminId] = @adminId" InsertCommand="INSERT INTO [Administrator] ([Username], [password]) VALUES (@Username, @password)"
        SelectCommand="SELECT * FROM [Administrator] WHERE ([adminId] = @adminId)" UpdateCommand="UPDATE [Administrator] SET [Username] = @Username, [password] = @password WHERE [adminId] = @adminId">
        <DeleteParameters>
            <asp:Parameter Name="adminId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="password" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="adminId" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="password" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        ProviderName="<%$ ConnectionStrings:KeywordComptingConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM [Administrator]" ConflictDetection="CompareAllValues"
        DeleteCommand="DELETE FROM [Administrator] WHERE [adminId] = @original_adminId AND [password] = @original_password AND [Username] = @original_Username"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="original_adminId" Type="Int32" />
            <asp:Parameter Name="original_password" Type="String" />
            <asp:Parameter Name="original_Username" Type="String" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>

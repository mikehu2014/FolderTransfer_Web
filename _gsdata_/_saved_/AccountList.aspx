<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountList.aspx.cs" Inherits="BackupCowWeb.AccountList" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>

    <table id="TABLE1" onclick="return TABLE1_onclick()" style="width: 805px">
        <tr>
            <td style="height: 27px; width: 833px;">
                <asp:Button ID="btnAddAcount" runat="server" Text="Add Account" PostBackUrl="~/RemoteGroup.aspx" OnClick="btnAddAcount_Click" />
                &nbsp;
                <asp:Button ID="btnSoryBySignpTime" runat="server" Text="Sort By SingupTime" OnClick="btnSoryBySignpTime_Click" />&nbsp;
                <asp:Button ID="btnSoryByGroupUser" runat="server" Text="Sort By GroupUser" OnClick="btnSoryByGroupUser_Click" />&nbsp;
                &nbsp;<asp:Button ID="btnRefresh" runat="server" PostBackUrl="~/Company/GetCompanyList.aspx?cmd=refresh"
                    Text="Refresh" /></td>
        </tr>
        <tr>
            <td style="width: 833px">
                <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
                    DataKeyNames="CompanyName" DataSourceID="sqlDataCompany" Width="100%" AllowPaging="True" OnRowUpdating="gvCompany_RowUpdating" OnSelectedIndexChanged="gvCompany_SelectedIndexChanged" PageSize="30">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" ReadOnly="True"
                            SortExpression="CompanyName" />
                        <asp:BoundField DataField="SignupTime" HeaderText="SignupTime" SortExpression="SignupTime" />
                        <asp:BoundField DataField="GroupUser" HeaderText="GroupUser" SortExpression="GroupUser" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlDataCompany" runat="server" ConflictDetection="CompareAllValues"
                    ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>" DeleteCommand="DELETE FROM [CompanyNetwork] WHERE [CompanyName] = @original_CompanyName AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL))"
                    InsertCommand="INSERT INTO [CompanyNetwork] ([CompanyName], [Password]) VALUES (@CompanyName, @Password)"
                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT CompanyName, SignupTime, GroupUser FROM CompanyNetwork ORDER BY GroupUser DESC"
                    UpdateCommand="UPDATE [CompanyNetwork] SET [Password] = @Password WHERE [CompanyName] = @original_CompanyName AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_CompanyName" Type="String" />
                        <asp:Parameter Name="original_Password" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="original_CompanyName" Type="String" />
                        <asp:Parameter Name="original_Password" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="CompanyName" Type="String" />
                        <asp:Parameter Name="Password" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
        </td>
    </tr>
    </table>

</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrialDailyPage.aspx.cs" Inherits="TrialDailyPage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataKeyNames="ID" DataSourceID="SqlDataSource1" AllowPaging="True" PageSize="30" Width="228px">
        <Columns>
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="UserCount" HeaderText="UserCount" SortExpression="UserCount" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT ID, Time, UserCount FROM TrialDaily ORDER BY Time DESC"></asp:SqlDataSource>
</asp:Content>


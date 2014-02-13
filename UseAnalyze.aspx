<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UseAnalyze.aspx.cs" Inherits="UseAnalyze" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp; &nbsp;
    <asp:Button ID="btnTenDay" runat="server" OnClick="btnTenDay_Click" Text="Ten Day" />
    &nbsp;&nbsp;
    <asp:Button ID="btnMonth" runat="server" OnClick="btnMonth_Click" Text="Month" />
    &nbsp; &nbsp;<asp:DropDownList ID="ddlYear" runat="server">
        <asp:ListItem>2012</asp:ListItem>
        <asp:ListItem Selected="True">2013</asp:ListItem>
        <asp:ListItem>2014</asp:ListItem>
        <asp:ListItem>2015</asp:ListItem>
        <asp:ListItem>2016</asp:ListItem>
        <asp:ListItem>2017</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click1" Text="Refresh" PostBackUrl="~/UseAnalyze.aspx?cmd=refresh" /><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataKeyNames="ID" DataSourceID="SqlDataSource1" Height="240px" OnRowDataBound="GridView1_RowDataBound"
        PageSize="50" Width="713px" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
            <asp:BoundField DataField="Number" HeaderText="Month" SortExpression="Number" />
            <asp:BoundField DataField="UseCount" HeaderText="UseCount" SortExpression="UseCount" />
            <asp:BoundField HeaderText="Use Change" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="OrderUse" HeaderText="OrderUse" SortExpression="OrderUse" />
            <asp:BoundField DataField="LostCount" HeaderText="LostCount" SortExpression="LostCount" />
            <asp:BoundField DataField="OrderLost" HeaderText="OrderLost" SortExpression="OrderLost" />
            <asp:BoundField DataField="TrialLost" HeaderText="QuickLost" SortExpression="TrialLost" />
            <asp:BoundField DataField="Memo" HeaderText="Memo" SortExpression="Memo" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>" OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT ID, Year, Type, Number, UseCount, OrderUse, LostCount, OrderLost, TrialLost, Memo FROM UseAnalyze WHERE (Type = 'Month')"
        UpdateCommand="UPDATE [UseAnalyze] SET [Memo] = @Memo WHERE [ID] = @original_ID">
        <UpdateParameters>
            <asp:Parameter Name="Memo" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountPcList.aspx.cs" Inherits="BackupCowWeb.AccountPcList" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvAccountPcList" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
            <asp:BoundField DataField="PcID" HeaderText="PcID" SortExpression="PcID" />
            <asp:BoundField DataField="PCName" HeaderText="PCName" SortExpression="PCName" />
            <asp:BoundField DataField="LanIp" HeaderText="LanIp" SortExpression="LanIp" />
            <asp:BoundField DataField="LanPort" HeaderText="LanPort" SortExpression="LanPort" />
            <asp:BoundField DataField="InternetIp" HeaderText="InternetIp" SortExpression="InternetIp" />
            <asp:BoundField DataField="InternetPort" HeaderText="InternetPort" SortExpression="InternetPort" />
            <asp:BoundField DataField="CloudIDNumber" HeaderText="CloudIDNumber" SortExpression="CloudIDNumber" />
            <asp:BoundField DataField="OnlineTime" HeaderText="OnlineTime" SortExpression="OnlineTime" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT * FROM [CompanyComputer] WHERE ([CompanyName] = @CompanyName)">
        <SelectParameters>
            <asp:QueryStringParameter Name="CompanyName" QueryStringField="CompanyName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

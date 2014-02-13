<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AppUninstallList.aspx.cs" Inherits="AppUninstallList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <br />
        LastDay Uninstall :
        <asp:Label ID="lbLastDayUninistall" runat="server" Text="Label"></asp:Label>
        &nbsp; &nbsp;Today Uninstall :
        <asp:Label ID="lbTodayUninistall" runat="server" Text="Label"></asp:Label>
        &nbsp; &nbsp;&nbsp;
        <asp:Button ID="btnSuggestion" runat="server" PostBackUrl="~/SuggestionList.aspx"
            Text="Suggestion" />
        &nbsp; &nbsp;
        <br />
        LastDay Inistall :
        <asp:Label ID="lbLastDayInistall" runat="server" Text="Label"></asp:Label>
        &nbsp; &nbsp; Today Install :
        <asp:Label ID="lbTodayInistall" runat="server" Text="Label"></asp:Label><br />
        <br />
        0: Network computer not found,though the software is installed on both computers.
        <br />
        1: Fails to transfer files
        <br />
        2: My computer runs abnormally since installation
        <br />
        3: I need online transfer service, not transfer between computers
        <br />
        4: I don't know how to use<br />
    </div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="PcID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound"
        PageSize="30" CssClass="list versionCompare">
        <Columns>
            <asp:BoundField DataField="PcID" HeaderText="PcID" ReadOnly="True" SortExpression="PcID" />
            <asp:BoundField DataField="WinOS" HeaderText="OS" SortExpression="WinOS" />
            <asp:BoundField DataField="FirstUse" HeaderText="FirstUse" SortExpression="FirstUse" />
            <asp:BoundField DataField="LastUse" HeaderText="LastUse" SortExpression="LastUse" />
            <asp:BoundField DataField="RealUseDate" HeaderText="ActionUse" SortExpression="RealUseDate" />
            <asp:BoundField DataField="UninstallTime" HeaderText="Uninstall" />
            <asp:BoundField DataField="UseCount" HeaderText="Use" SortExpression="UseCount" />
            <asp:BoundField DataField="NetworkMode" HeaderText="Net" SortExpression="NetworkMode" />
            <asp:BoundField DataField="NetworkPc" HeaderText="NetPc" SortExpression="NetworkPc" />
            <asp:BoundField DataField="FileSendCount" HeaderText="Send" SortExpression="FileSendCount" />
            <asp:BoundField DataField="ShareDownCount" HeaderText="Down" SortExpression="ShareDownCount" />
            <asp:BoundField DataField="AdsShowCount" HeaderText="Ads" SortExpression="AdsShowCount" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField HeaderText="Location" />
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" />
            <asp:BoundField DataField="Suggestion" HeaderText="Suggestion" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        DeleteCommand="DELETE FROM UserUninstall WHERE [PcID] = @PcID" SelectCommand="SELECT UserUninstall.PcID, AppUser.FirstUse, AppUser.LastUse, AppUser.UseCount, AppUser.NetworkMode, AppUser.FileSendCount, AppUser.ShareDownCount, AppUser.NetworkPc, AppUser.AdsShowCount, AppUser.RealUseDate, Activate.OrderID, UserUninstall.UninstallTime, Trial.PcName, Trial.IP, Trial.WinOS, UserUninstall.Reason, UserUninstall.Suggestion, UserUninstall.Email FROM UserUninstall LEFT OUTER JOIN Trial ON UserUninstall.PcID = Trial.PcID LEFT OUTER JOIN AppUser ON UserUninstall.PcID = AppUser.PcID LEFT OUTER JOIN Activate ON UserUninstall.PcID = Activate.PcID ORDER BY UserUninstall.UninstallTime DESC">
        <DeleteParameters>
            <asp:Parameter Name="PcID" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>


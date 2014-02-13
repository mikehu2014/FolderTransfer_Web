<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OtherPage.aspx.cs" Inherits="OtherPage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btnAddOrder" runat="server" PostBackUrl="~/OrderAdd.aspx" Text="Add a Order" />
    &nbsp; &nbsp; &nbsp;<asp:Button ID="btnAdminList" runat="server" PostBackUrl="~/adminList.aspx"
        Text="Admin List" />
    &nbsp;&nbsp;<br />
    <br />
    <asp:Button ID="btnAddTrial" runat="server" OnClick="btnAddTrial_Click" Text="Add Trial" />
    &nbsp; &nbsp; &nbsp;<asp:Button ID="btnAdsClick" runat="server" PostBackUrl="~/AdsClick.aspx"
        Text="AdsClick" /><br />
    <br />
</asp:Content>


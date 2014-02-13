<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="WebApplication1.Login"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <p style="margin-top: 20px">
        <span style="margin-right: 5px">User Name:</span> <span>
            <asp:TextBox ID="txtUser" runat="server" Width="150px"></asp:TextBox></span>
    </p>
    <p style="margin-top: 5px">
        <span style="margin-right: 13px">Password:</span> <span>
            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="150px"></asp:TextBox></span>
    </p>
    <p style="margin-top: 5px">
        <span style="margin-left: 76px">
            <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" Font-Size="12px"
                Width="100px" /></span>
    </p>
    <p style="margin-top: 5px">
        <span style="margin-left: 76px">
            <asp:Label ID="Label1" runat="server" Text="Username or password error!" Font-Size="12px"
                Visible="False" ForeColor="OrangeRed"></asp:Label></span></p>
</asp:Content>

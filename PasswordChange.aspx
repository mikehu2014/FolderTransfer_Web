<%@ Page Language="C#" MasterPageFile="RemoteGroup.Master" AutoEventWireup="true" CodeFile="PasswordChange.aspx.cs" Inherits="PasswordChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div > Please reset a new password: </div>
    <br />
    <div style="margin-left:20px">
    <table style="width: 653px; height: 73px">
        <tr>
            <td style="width: 100px; height: 27px">
                <asp:Label ID="Label1" runat="server" Text="Account Name"></asp:Label></td>
            <td style="width: 100px; height: 27px">
                <asp:Label ID="lbAccountName" runat="server" Width="166px"></asp:Label></td>
            <td style="width: 102px; height: 27px">
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 24px;">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>
            <td style="width: 100px; height: 24px;">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 102px; height: 24px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input password"
                    Width="315px" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label3" runat="server" Text="Retype Password" Width="113px"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password"></asp:TextBox></td>
            <td style="width: 102px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please retype password" ControlToValidate="txtRetypePassword" Display="Dynamic" Width="186px"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and Retype Password are not matched" ControlToCompare="txtPassword" ControlToValidate="txtRetypePassword" Display="Dynamic" Width="326px"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 25px;">
                </td>
            <td style="width: 100px; height: 25px;">
                <br />
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" Width="146px" OnClick="btnChangePassword_Click" /></td>
            <td style="width: 102px; height: 25px;">
                <br />
                <asp:Label ID="lbResult" runat="server" Width="363px"></asp:Label></td>
        </tr>
    </table>
    </div>
</asp:Content>


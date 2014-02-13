<%@ Page Language="C#" MasterPageFile="RemoteGroup.Master" AutoEventWireup="true" CodeFile="RemoteGroup.aspx.cs" Inherits="BackupCowWeb.RemoteGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-left:20px">
        <table style="width: 805px; height: 60px;">
            <tr>
                <td style="width: 56px; height: 24px">
                    <asp:Label ID="Label1" runat="server" Text="Group Name" Width="99px" ForeColor="Black"></asp:Label></td>
                <td style="width: 135px; height: 24px">
                    <asp:TextBox ID="txtAccountName" runat="server" Width="201px"></asp:TextBox></td>
                <td style="width: 463px; height: 24px">
                    <asp:RequiredFieldValidator ID="rfdAccountName" runat="server" ControlToValidate="txtAccountName"
                        ErrorMessage="Please input group name" Width="298px"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 56px; height: 23px">
                    <asp:Label ID="Label4" runat="server" Text="Email" ForeColor="Black"></asp:Label></td>
                <td style="width: 135px; height: 23px">
                    <asp:TextBox ID="txtEmail" runat="server" Width="201px"></asp:TextBox></td>
                <td style="width: 463px; height: 23px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Please input email" Width="131px" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ErrorMessage="Email address is invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Width="160px"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td style="width: 56px; height: 23px">
                    <asp:Label ID="Label2" runat="server" Text="Password" ForeColor="Black"></asp:Label></td>
                <td style="width: 135px; height: 23px">
                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password" Width="201px"></asp:TextBox></td>
                <td style="width: 463px; height: 23px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Please input password" Width="208px"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 56px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Text="Retype Password" Width="109px" ForeColor="Black"></asp:Label></td>
                <td style="width: 135px; height: 23px">
                    <asp:TextBox ID="txtPasswordRetype" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                <td style="width: 463px; height: 23px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPasswordRetype"
                        ErrorMessage="Please retype password" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtPasswordRetype" ErrorMessage="Password and Retype Password are not matched" Width="309px" Display="Dynamic"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td style="width: 56px; height: 23px">
    <asp:Label ID="Label5" runat="server" Text="Security Code" Width="100px" ForeColor="Black"></asp:Label></td>
                <td style="width: 135px; height: 23px">
                    <asp:TextBox
        ID="txtSecurityCode" runat="server" Width="198px"></asp:TextBox></td>
                <td style="width: 463px; height: 23px">
                    &nbsp;<asp:RequiredFieldValidator
            ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSecurityCode"
            Display="Dynamic" ErrorMessage="Please input security code" Width="171px"></asp:RequiredFieldValidator><asp:CustomValidator
                ID="CustomValidator1" runat="server" ControlToValidate="txtSecurityCode" Display="Dynamic"
                ErrorMessage="Security code is incorrect" OnServerValidate="CustomValidator1_ServerValidate"
                Width="186px"></asp:CustomValidator></td>
            </tr>           
           <tr>
                <td style="width: 56px; height: 24px">
                </td>
                <td style="width: 135px; height: 24px">
                    &nbsp;<img id="Img1" alt="" src="ValidCode.aspx" />
                    &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Sign up" Width="96px" />&nbsp;&nbsp;
                    &nbsp; &nbsp; &nbsp;</td>
                <td style="width: 463px; height: 24px">
                    <asp:Label ID="lbResult" runat="server" ForeColor="Black"></asp:Label></td>
            </tr>
        </table>
   </div>
</asp:Content>
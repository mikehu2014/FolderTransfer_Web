<%@ Page Language="C#" MasterPageFile="RemoteGroup.Master" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="margin-left:20px">
    <div> Please reset your password with your account name. (A url will be sent to your email to reset your password.) </div> 
    <br />
    <table style="width: 667px" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr>
            <td style="width: 100px; height: 24px">
                <asp:Label ID="Label1" runat="server" Text="Group Name"></asp:Label></td>
            <td style="width: 91px; height: 24px">
                <asp:TextBox ID="txtAccountName" runat="server" Width="162px"></asp:TextBox></td>
            <td style="width: 267px; height: 24px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAccountName"
                    ErrorMessage="Please input account name" Width="169px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Account name dost not exist" ControlToValidate="txtAccountName" Display="Dynamic" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 19px">
                <asp:Label ID="Label2" runat="server" Text="Security Code"></asp:Label></td>
            <td style="width: 91px; height: 19px">
                <asp:TextBox ID="txtSecurityCode" runat="server" Width="163px"></asp:TextBox></td>
            <td style="width: 267px; height: 19px">
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please input security code" Width="171px" ControlToValidate="txtSecurityCode" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtSecurityCode"
                    Display="Dynamic" ErrorMessage="Security code is incorrect" OnServerValidate="CustomValidator1_ServerValidate"
                    Width="186px"></asp:CustomValidator></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 19px">
            </td>
            <td style="width: 91px; height: 19px">
                &nbsp;<img alt="" src="ValidCode.aspx" id="Img1" />
                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send to email"
                    Width="115px" /></td>
            <td style="width: 267px">
                <asp:Label ID="lbResult" runat="server" Width="386px" Height="47px"></asp:Label></td>

        </tr>
    </table>
    </div>

</asp:Content>


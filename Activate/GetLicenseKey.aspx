<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetLicenseKey.aspx.cs" Inherits="BackupCowWeb.Activate.GetLicenseKey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 609px; height: 246px;">
            <tr>
                <td style="width: 100px; height: 29px;">
                    &nbsp;<asp:Label ID="lbOrderID" runat="server" Text="Order Number"></asp:Label></td>
                <td style="width: 175px; height: 29px;">
                    <asp:TextBox ID="txtOrderID" runat="server" Width="332px"></asp:TextBox></td>
                 <td style="height: 29px">
                     &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOrderID"
                         ErrorMessage="Input order number"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label1" runat="server" Text="Hard Code"></asp:Label></td>
                <td style="width: 175px">
                    <asp:TextBox ID="txtHardCode" runat="server" Width="329px"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHardCode"
                        ErrorMessage="Input hard code"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label2" runat="server" Text="License"></asp:Label></td>
                <td style="width: 175px">
                    <asp:TextBox ID="txtLicense" runat="server" Height="100px" ReadOnly="True" TextMode="MultiLine"
                        Width="328px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 175px">
                    <asp:Button ID="btnGetLicense" runat="server" OnClick="btnGetLicense_Click" Text="Get License" /></td>
                <td>
                    <asp:Label ID="lbResult" runat="server"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

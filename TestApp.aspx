<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestApp.aspx.cs" Inherits="TestApp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                                                    <ul style="list-style-type:square" >
                                            <li>
                                                 <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/RemoteGroup.aspx">Account Signup</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ForgetPassword.aspx">Forget Password</asp:LinkButton>
                                            </li>
                                        </ul>  
    </div>
    </form>
</body>
</html>

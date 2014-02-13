<%@ Page Language="C#" MasterPageFile="RemoteGroup.Master" AutoEventWireup="true" CodeFile="Instruction.aspx.cs" Inherits="Instruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div style="margin-left:20px">
  
    <div style="color:Blue;font-size:small;font-weight:bold">
        Backup Cow users can connect together by joining a same remote network group. 
     </div>
     <div style="font-size:small;font-weight:bold">
        Here are the steps to use this feature:
     </div>
    <br />
    <br />
    
    <div style="font-size:small">
       1. Click <a href="RemoteGroup.aspx" target="_blank">here</a> to create a network group account. (If you already have a group account, please
       disregard this step.)
    </div>
     <br />
      
      <asp:Image ID="Image1" runat="server" ImageUrl="~/images1/Manual/Signup.jpg" />
      
      <br />
      <br />
    
    <div style="font-size:small">  
    2. Input the group name and password, then click OK. (Remote Network - Join a Group
      )
    </div>
    
      <br />
      <asp:Image ID="Image5" runat="server" ImageUrl="~/images1/Manual/StartJoin.PNG" /><br />
      <br />
      <asp:Image ID="Image2" runat="server" ImageUrl="~/images1/ss/120_RenoteNetwork_Input.PNG" />
      <br />
      <br />
      <br />
    <div style="font-size:small">  
    3. Thereafter, you just need to select the group name showing under "Remote Network"
      to join the group, no need to input the group name and password again.
    </div>
      <br />
      <asp:Image ID="Image4" runat="server" ImageUrl="~/images1/ss/130_RemoteNetwork_Enter.PNG" /><br />
      <br />
    Notes: 
      <br />
      * You can also make the remote connection settings in "Settings-- Remote Network"
      <br />
    * If you forget your
            account password, you can click here to reset the password.
      <br />
    * You can set up more
            than one remote network group, and choose any network group to join.
      <br />
    * Backcow Server will act as a IP/Port parser so that Backup Cow users in the
            same group can connect together automatically. 
    <br />
    <br />
    <br />
    <asp:Image ID="Image3" runat="server" ImageUrl="~/images1/ss/Remote1.jpg" Height="567px" Width="693px" />
  </div>
</asp:Content>


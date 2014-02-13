<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trial.aspx.cs" Inherits="WebApplication1._Default"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="contentpaneopen">
        <tr>
            <td>
                <div style="padding: 5px 5px 3px 5px; margin: 0px; color: White; background-color: #5064A0">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Hard Code: "></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtHardCode" runat="server" Font-Size="12px" Width="168px"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label4" runat="server" Text="PcName"></asp:Label>
                    <asp:TextBox ID="txtPcName" runat="server" Width="167px"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Font-Size="12px" Text="Search" Width="100px"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnViewall" runat="server" Font-Size="12px" Text="View All" Width="100px"
                        OnClick="btnViewall_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="; padding-top:10px; height: 23px; width: 896px;">
                <asp:Button
                    ID="btnDailyAnaly" runat="server" Text="Daily Analye" Width="106px" PostBackUrl="~/TrialDailyPage.aspx" OnClick="btnDailyAnaly_Click" />
                <asp:Button ID="btnAnalyze" runat="server" PostBackUrl="~/TrialAnalyze.aspx" Text="User Analyze" Width="116px" />
                <asp:Button ID="btnCountryAnaylze" runat="server" PostBackUrl="~/CountryAnalyze.aspx"
                    Text="Country Analyze" Width="129px" OnClick="btnCountryAnaylze_Click" />&nbsp;<asp:Button
                        ID="btnTrialOne" runat="server" OnClick="btnTrialOne_Click" PostBackUrl="~/TrialOne.aspx"
                        Text="Same IP" />&nbsp;
                <asp:Label ID="Label2" runat="server"></asp:Label>&nbsp;<asp:Label ID="Label3" runat="server"></asp:Label><br />
                Yesterday Trial :
                <asp:Label ID="lbLastDay" runat="server" Text="10000"></asp:Label>
                &nbsp;Today Trial: &nbsp;<asp:Label ID="lbToday" runat="server" Text="10000"></asp:Label>&nbsp;&nbsp;
                &nbsp; Last 7 Day :
                <asp:Label ID="lbLastSeven" runat="server" Text="10000"></asp:Label>
                &nbsp; &nbsp;Last 30 Day : &nbsp;<asp:Label ID="lbLastMonth" runat="server" Text="10000"></asp:Label>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="ID" DataSourceID="SqlDataSource1" CellPadding="4"
                    CssClass="list versionCompare" ForeColor="#333333" GridLines="None"
                    EmptyDataText="No displaying data records." OnRowDeleting="GridView1_RowDeleting"
                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="HardCode" HeaderText="HardCode" SortExpression="HardCode" />
                        <asp:BoundField DataField="PcID" HeaderText="PcID" />
                        <asp:BoundField DataField="PcName" HeaderText="PcName" SortExpression="PcName" />
                        <asp:BoundField DataField="WinOS" HeaderText="OS" />
                        <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
                        <asp:BoundField HeaderText="Location">
                            <ItemStyle Width="110px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TrialDate" HeaderText="TrialDate" SortExpression="TrialDate" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
                    SelectCommand="SELECT ID, HardCode, PcName, IP, TrialDate, PcID, WinOS FROM Trial ORDER BY TrialDate DESC" DeleteCommand="DELETE FROM [Trial] WHERE [ID] = @original_ID" ConflictDetection="CompareAllValues" InsertCommand="INSERT INTO [Trial] ([HardCode], [PcName], [IP], [TrialDate], [isValid]) VALUES (@HardCode, @PcName, @IP, @TrialDate, @isValid)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Trial] SET [HardCode] = @HardCode, [PcName] = @PcName, [IP] = @IP, [TrialDate] = @TrialDate WHERE [ID] = @original_ID" ProviderName="<%$ ConnectionStrings:KeywordComptingConnectionString.ProviderName %>">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdsClick.aspx.cs" Inherits="AdsDownload" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="SqlDataSource1" Height="189px" Width="572px" CssClass="list versionCompare" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="30">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
            <asp:BoundField DataField="PcName" HeaderText="PcName" SortExpression="PcName" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField HeaderText="Location" />
            <asp:BoundField DataField="ClickDate" HeaderText="ClickDate" SortExpression="ClickDate" />
        </Columns>
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>" DeleteCommand="DELETE FROM [AdsClick] WHERE [ID] = @original_ID AND (([ProductName] = @original_ProductName) OR ([ProductName] IS NULL AND @original_ProductName IS NULL)) AND (([PcID] = @original_PcID) OR ([PcID] IS NULL AND @original_PcID IS NULL)) AND (([PcName] = @original_PcName) OR ([PcName] IS NULL AND @original_PcName IS NULL)) AND (([IP] = @original_IP) OR ([IP] IS NULL AND @original_IP IS NULL)) AND (([ClickDate] = @original_ClickDate) OR ([ClickDate] IS NULL AND @original_ClickDate IS NULL))"
        InsertCommand="INSERT INTO [AdsClick] ([ProductName], [PcID], [PcName], [IP], [ClickDate]) VALUES (@ProductName, @PcID, @PcName, @IP, @ClickDate)"
        OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT ID, ProductName, PcID, PcName, IP, ClickDate FROM AdsClick ORDER BY ClickDate DESC"
        UpdateCommand="UPDATE [AdsClick] SET [ProductName] = @ProductName, [PcID] = @PcID, [PcName] = @PcName, [IP] = @IP, [ClickDate] = @ClickDate WHERE [ID] = @original_ID AND (([ProductName] = @original_ProductName) OR ([ProductName] IS NULL AND @original_ProductName IS NULL)) AND (([PcID] = @original_PcID) OR ([PcID] IS NULL AND @original_PcID IS NULL)) AND (([PcName] = @original_PcName) OR ([PcName] IS NULL AND @original_PcName IS NULL)) AND (([IP] = @original_IP) OR ([IP] IS NULL AND @original_IP IS NULL)) AND (([ClickDate] = @original_ClickDate) OR ([ClickDate] IS NULL AND @original_ClickDate IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_ProductName" Type="String" />
            <asp:Parameter Name="original_PcID" Type="String" />
            <asp:Parameter Name="original_PcName" Type="String" />
            <asp:Parameter Name="original_IP" Type="String" />
            <asp:Parameter DbType="Date" Name="original_ClickDate" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="PcID" Type="String" />
            <asp:Parameter Name="PcName" Type="String" />
            <asp:Parameter Name="IP" Type="String" />
            <asp:Parameter DbType="Date" Name="ClickDate" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_ProductName" Type="String" />
            <asp:Parameter Name="original_PcID" Type="String" />
            <asp:Parameter Name="original_PcName" Type="String" />
            <asp:Parameter Name="original_IP" Type="String" />
            <asp:Parameter DbType="Date" Name="original_ClickDate" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ProductName" Type="String" />
            <asp:Parameter Name="PcID" Type="String" />
            <asp:Parameter Name="PcName" Type="String" />
            <asp:Parameter Name="IP" Type="String" />
            <asp:Parameter DbType="Date" Name="ClickDate" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>


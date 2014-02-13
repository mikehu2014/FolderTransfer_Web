<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrialAnalyze.aspx.cs" Inherits="TrialAnalyze" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:Button ID="btnTenDay" runat="server" OnClick="btnTenDay_Click" Text="Ten Day" />
    &nbsp; &nbsp;<asp:Button ID="btnMonth" runat="server" OnClick="btnMonth_Click" Text="Month" />
    &nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="ddlYear" runat="server">
        <asp:ListItem>2012</asp:ListItem>
        <asp:ListItem Selected="True">2013</asp:ListItem>
        <asp:ListItem>2014</asp:ListItem>
        <asp:ListItem>2015</asp:ListItem>
        <asp:ListItem>2016</asp:ListItem>
        <asp:ListItem>2017</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnRefresh" runat="server" PostBackUrl="~/TrialAnalyze.aspx?cmd=refresh"
        Text="Refresh" /><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataSourceID="SqlDataSource1" Height="250px" OnRowDataBound="GridView1_RowDataBound"
        PageSize="50" Width="607px" DataKeyNames="ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
            <asp:BoundField DataField="Number" HeaderText="Number" SortExpression="Number" />
            <asp:BoundField DataField="Value" HeaderText="Trial" SortExpression="Value" />
            <asp:BoundField HeaderText="Change" SortExpression="Year" />
            <asp:BoundField HeaderText="Precentage" SortExpression="Year" />
            <asp:BoundField DataField="Memo" HeaderText="Memo" SortExpression="Memo" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT Year, Type, Number, Value, ID, Memo FROM TrialAnalyze WHERE (Type = @Type)" OldValuesParameterFormatString="original_{0}" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [TrialAnalyze] WHERE [ID] = @original_ID AND (([Year] = @original_Year) OR ([Year] IS NULL AND @original_Year IS NULL)) AND (([Type] = @original_Type) OR ([Type] IS NULL AND @original_Type IS NULL)) AND (([Number] = @original_Number) OR ([Number] IS NULL AND @original_Number IS NULL)) AND (([Value] = @original_Value) OR ([Value] IS NULL AND @original_Value IS NULL)) AND (([Memo] = @original_Memo) OR ([Memo] IS NULL AND @original_Memo IS NULL))" InsertCommand="INSERT INTO [TrialAnalyze] ([Year], [Type], [Number], [Value], [Memo]) VALUES (@Year, @Type, @Number, @Value, @Memo)" UpdateCommand="UPDATE [TrialAnalyze] SET [Memo] = @Memo WHERE [ID] = @original_ID ">
        <SelectParameters>
            <asp:Parameter DefaultValue="Month" Name="Type" Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Year" Type="Int32" />
            <asp:Parameter Name="original_Type" Type="String" />
            <asp:Parameter Name="original_Number" Type="Int32" />
            <asp:Parameter Name="original_Value" Type="Int32" />
            <asp:Parameter Name="original_Memo" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Memo" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Year" Type="Int32" />
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="Number" Type="Int32" />
            <asp:Parameter Name="Value" Type="Int32" />
            <asp:Parameter Name="Memo" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>


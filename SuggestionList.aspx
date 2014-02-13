<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SuggestionList.aspx.cs" Inherits="SuggestionList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataKeyNames="PcID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="PcID" HeaderText="PcID" ReadOnly="True" SortExpression="PcID" />
            <asp:BoundField DataField="Suggestion" HeaderText="Suggestion" SortExpression="Suggestion" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        InsertCommand="INSERT INTO [UserSuggestion] ([PcID], [Suggestion], [Email]) VALUES (@PcID, @Suggestion, @Email)"
        OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [UserSuggestion]">
        <InsertParameters>
            <asp:Parameter Name="PcID" Type="String" />
            <asp:Parameter Name="Suggestion" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>


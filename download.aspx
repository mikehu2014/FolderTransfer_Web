<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Admin_download" CodeFile="download.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="contentpaneopen">
        <tr>
            <td>
                Sumary</td>
        </tr>
        <tr>
            <td>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CssClass="list versionCompare"
                    DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Height="50px" CellPadding="4"
                    Width="100%">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <RowStyle BackColor="#E3EAEB" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="totalcount" HeaderText="totalcount" ReadOnly="True">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thisyearcount" HeaderText="thisyearcount" ReadOnly="True">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="thismonthcount" HeaderText="thismonthcount" ReadOnly="True">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="today" HeaderText="today" ReadOnly="True">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                    </Fields>
                    <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:DetailsView>
            </td>
        </tr>
        <tr>
            <td style="padding-top:10px">
                Top 100 Downer
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CssClass="list versionCompare" DataSourceID="SqlDataSource2" ForeColor="#333333"
                    GridLines="None" Width="100%" EmptyDataText="No displaying data records.">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" >
                            <HeaderStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ip" HeaderText="Ip" >
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="downDate" HeaderText="downDate" >
                            <HeaderStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="country" HeaderText="country" />
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT COUNT(0) AS totalcount, (SELECT COUNT(0) AS Expr1 FROM download WHERE (DATEDIFF(year, downDate, GETDATE()) = 0)) AS thisyearcount, (SELECT COUNT(0) AS Expr1 FROM download AS download_3 WHERE (DATEDIFF(month, downDate, GETDATE()) = 0)) AS thismonthcount, (SELECT COUNT(0) AS Expr1 FROM download AS download_2 WHERE (DATEDIFF(day, downDate, GETDATE()) = 0)) AS today FROM download AS download_1">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT top 100 [vTag] As [ID], [Ip], [downDate], [country] FROM [download] ORDER BY [downDate] DESC">
    </asp:SqlDataSource>
</asp:Content>

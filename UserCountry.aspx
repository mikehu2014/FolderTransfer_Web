<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserCountry.aspx.cs" Inherits="UserCountry" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DropDownList ID="ddlMonth" runat="server">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>6</asp:ListItem>
        <asp:ListItem>7</asp:ListItem>
        <asp:ListItem>8</asp:ListItem>
        <asp:ListItem>9</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>11</asp:ListItem>
        <asp:ListItem>12</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnMonth" runat="server" OnClick="btnMonth_Click" Text="Month" />
    &nbsp;
    <asp:Button ID="btnTenDays" runat="server" OnClick="btnTenDays_Click" Text="10 Days" /><br />
    <br />
    &nbsp;<table style="width: 974px; height: 274px">
        <tr>
            <td style="width: 100px; height: 274px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
                    DataKeyNames="ID" DataSourceID="SqlDataSource1" Height="238px" OnRowDataBound="GridView1_RowDataBound"
                    Width="342px">
                    <Columns>
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                        <asp:BoundField DataField="Number" HeaderText="Number" SortExpression="Number" />
                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                    </Columns>
                </asp:GridView>
            </td>
            <td style="width: 100px; height: 274px">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
                    DataKeyNames="ID" DataSourceID="SqlDataSource2" Height="239px" OnRowDataBound="GridView1_RowDataBound"
                    Width="321px">
                    <Columns>
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                        <asp:BoundField DataField="Number" HeaderText="Month" SortExpression="Number" />
                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                    </Columns>
                </asp:GridView>
            </td>
            <td style="width: 100px; height: 274px">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
                    DataKeyNames="ID" DataSourceID="SqlDataSource3" Height="234px" OnRowDataBound="GridView1_RowDataBound"
                    Width="273px">
                    <Columns>
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                        <asp:BoundField DataField="Number" HeaderText="Month" SortExpression="Number" />
                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    &nbsp; &nbsp;
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT * FROM [UserCountry] WHERE (([Type] = @Type) AND ([Number] = @Number)) ORDER BY [Value] DESC, [Country]">
        <SelectParameters>
            <asp:Parameter DefaultValue="Month" Name="Type" Type="String" />
            <asp:Parameter DefaultValue="8" Name="Number" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT * FROM [UserCountry] WHERE (([Type] = @Type) AND ([Number] = @Number)) ORDER BY [Value] DESC, [Country]">
        <SelectParameters>
            <asp:Parameter DefaultValue="Month" Name="Type" Type="String" />
            <asp:Parameter DefaultValue="9" Name="Number" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        SelectCommand="SELECT * FROM [UserCountry] WHERE (([Type] = @Type) AND ([Number] = @Number)) ORDER BY [Value] DESC, [ID]">
        <SelectParameters>
            <asp:Parameter DefaultValue="Month" Name="Type" Type="String" />
            <asp:Parameter DefaultValue="10" Name="Number" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


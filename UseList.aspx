<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UseList.aspx.cs" Inherits="UseList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language = "JavaScript" type="text/javascript">
    function readValue(TypeStr)
    {
        var SearchType = TypeStr;
        var SearchValue = "";
        if (SearchType == "NetworkPc")
           SearchValue = "2";
        else
        if (SearchType == "FileSend")
            SearchValue = "1";
        else
        if (SearchType == "ShareDown")
            SearchValue = "1";
        else
        if (SearchType == "AdsShow")
            SearchValue = "1"
        else
        {
            var currentDate = new Date();
            var yy = currentDate.getFullYear();
            var mm = currentDate.getMonth() + 1;
            var dd = currentDate.getDate();
            var DateStr = yy + "-" + mm + "-" + dd;
            if (SearchType == "FirstUse")
                SearchValue = DateStr + "|" + DateStr
            else
            if (SearchType == "LastUse")
                SearchValue = DateStr + "|" + DateStr
            else
            if (SearchType == "ActionUse")
                SearchValue = DateStr + "|" + DateStr
        }
        return SearchValue;
    }
    
    function changearea1()
    {
        var SearchType = document.all("<% =ddlSearchType.ClientID %>").value;
        document.all("<% =tbSearchValue.ClientID %>").value = readValue( SearchType );
    }
    
    function changearea2()
    {
        var SearchType = document.all("<% =ddlSearchType2.ClientID %>").value;
        document.all("<% =tbSearchValue2.ClientID %>").value = readValue( SearchType );
    }
    
    function changearea3()
    {
         var SearchType = document.all("<% =ddlSearchType3.ClientID %>").value;
        document.all("<% =tbSearchValue3.ClientID %>").value = readValue( SearchType );
    }
    
    function changearea4()
    {
        var SearchType = document.all("<% =ddlSearchType4.ClientID %>").value;
        document.all("<% =tbSearchValue4.ClientID %>").value = readValue( SearchType );
    }
    
    function changearea5()
    {
        var SearchType = document.all("<% =ddlSearchType5.ClientID %>").value;
        document.all("<% =tbSearchValue5.ClientID %>").value = readValue( SearchType );
    }
    
    
    </script>                

    <div style="width: 975px; height: 88px">
    <asp:DropDownList ID="ddlSearchType" runat="server">
        <asp:ListItem>All</asp:ListItem>
        <asp:ListItem>PcID</asp:ListItem>
        <asp:ListItem>PcName</asp:ListItem>
        <asp:ListItem>NetworkPc</asp:ListItem>
        <asp:ListItem>FileSend</asp:ListItem>
        <asp:ListItem>ShareDown</asp:ListItem>
        <asp:ListItem>Ip</asp:ListItem>
        <asp:ListItem>OrderID</asp:ListItem>
        <asp:ListItem>AdsShow</asp:ListItem>
        <asp:ListItem>FirstUse</asp:ListItem>
        <asp:ListItem>LastUse</asp:ListItem>
        <asp:ListItem>ActionUse</asp:ListItem>
    </asp:DropDownList>
        &gt;= &nbsp;<asp:TextBox ID="tbSearchValue" runat="server"></asp:TextBox>&nbsp;<asp:DropDownList ID="ddlSearchType3" runat="server">
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>PcID</asp:ListItem>
            <asp:ListItem>PcName</asp:ListItem>
            <asp:ListItem>NetworkPc</asp:ListItem>
            <asp:ListItem>FileSend</asp:ListItem>
            <asp:ListItem>ShareDown</asp:ListItem>
            <asp:ListItem>Ip</asp:ListItem>
            <asp:ListItem>OrderID</asp:ListItem>
            <asp:ListItem>AdsShow</asp:ListItem>
            <asp:ListItem>FirstUse</asp:ListItem>
            <asp:ListItem>LastUse</asp:ListItem>
            <asp:ListItem>ActionUse</asp:ListItem>
        </asp:DropDownList>
        &gt;= &nbsp;<asp:TextBox ID="tbSearchValue3" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAnalyze" runat="server" PostBackUrl="~/UseAnalyze.aspx" Text="Analyze" />&nbsp;
        &nbsp;&nbsp;
        <br />
        <asp:DropDownList ID="ddlSearchType2" runat="server">
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>PcID</asp:ListItem>
            <asp:ListItem>PcName</asp:ListItem>
            <asp:ListItem>NetworkPc</asp:ListItem>
            <asp:ListItem>FileSend</asp:ListItem>
            <asp:ListItem>ShareDown</asp:ListItem>
            <asp:ListItem>Ip</asp:ListItem>
            <asp:ListItem>OrderID</asp:ListItem>
            <asp:ListItem>AdsShow</asp:ListItem>
            <asp:ListItem>FirstUse</asp:ListItem>
            <asp:ListItem>LastUse</asp:ListItem>
            <asp:ListItem>ActionUse</asp:ListItem>
        </asp:DropDownList>
        &gt;= &nbsp;<asp:TextBox ID="tbSearchValue2" runat="server" OnTextChanged="tbSearchValue2_TextChanged"></asp:TextBox>&nbsp;<asp:DropDownList ID="ddlSearchType5" runat="server">
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>PcID</asp:ListItem>
            <asp:ListItem>PcName</asp:ListItem>
            <asp:ListItem>NetworkPc</asp:ListItem>
            <asp:ListItem>FileSend</asp:ListItem>
            <asp:ListItem>ShareDown</asp:ListItem>
            <asp:ListItem>Ip</asp:ListItem>
            <asp:ListItem>OrderID</asp:ListItem>
            <asp:ListItem>AdsShow</asp:ListItem>
            <asp:ListItem>FirstUse</asp:ListItem>
            <asp:ListItem>LastUse</asp:ListItem>
            <asp:ListItem>ActionUse</asp:ListItem>
        </asp:DropDownList>
        &gt;= &nbsp;<asp:TextBox ID="tbSearchValue5" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnDailyAnalyze" runat="server" PostBackUrl="~/DailyAnalyze.aspx"
            Text="Daily Analyze" OnClick="btnDailyAnalyze_Click" /><br />
        <asp:DropDownList ID="ddlSearchType4" runat="server">
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>PcID</asp:ListItem>
            <asp:ListItem>PcName</asp:ListItem>
            <asp:ListItem>NetworkPc</asp:ListItem>
            <asp:ListItem>FileSend</asp:ListItem>
            <asp:ListItem>ShareDown</asp:ListItem>
            <asp:ListItem>Ip</asp:ListItem>
            <asp:ListItem>OrderID</asp:ListItem>
            <asp:ListItem>AdsShow</asp:ListItem>
            <asp:ListItem>FirstUse</asp:ListItem>
            <asp:ListItem>LastUse</asp:ListItem>
            <asp:ListItem>ActionUse</asp:ListItem>
        </asp:DropDownList>
        &gt;= &nbsp;<asp:TextBox ID="tbSearchValue4" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnSearch" runat="server" Text="OK" Width="63px" OnClick="btnSearch_Click" Height="25px" />
    &nbsp; &nbsp; &nbsp;
        Filter User : &nbsp;<asp:Label ID="lbFilter" runat="server" Text="0"></asp:Label>
        &nbsp;
    </div>
    
    <span style="display: inline! important; float: none; word-spacing: 0px; font: 12px 'Helvetica Neue', Helvetica, Arial, sans-serif;
        text-transform: none; color: rgb(0,0,0); text-indent: 0px; white-space: normal;
        letter-spacing: normal; orphans: 2; widows: 2; webkit-text-size-adjust: auto;
        webkit-text-stroke-width: 0px">
        <div style="width: 781px; height: 24px">
            Yesterday User :
            <asp:Label ID="lbLastDay" runat="server" Text="10000"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Today User: &nbsp;<asp:Label ID="lbToday" runat="server"
                Text="10000"></asp:Label>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp;&nbsp; &nbsp; Last 7 Day:&nbsp;
            <asp:Label ID="lbLastSeven" runat="server" Text="10000"></asp:Label>
            &nbsp; &nbsp;&nbsp; &nbsp; Last 30 Day:
            <asp:Label ID="lbLastMonth" runat="server" Text="10000"></asp:Label></div>
    </span>
    
    <asp:GridView ID="GvUseList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        CssClass="list versionCompare" DataKeyNames="PcID" DataSourceID="SqlDataSource1"
        PageSize="30" OnRowDataBound="GvUseList_RowDataBound" OnSelectedIndexChanging="GvUseList_SelectedIndexChanging" OnSelectedIndexChanged="GvUseList_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="PcID" HeaderText="PcID" ReadOnly="True" SortExpression="PcID" />
            <asp:BoundField DataField="PcName" HeaderText="PcName" SortExpression="PcName" />
            <asp:BoundField DataField="WinOS" HeaderText="OS" />
            <asp:BoundField DataField="FirstUse" HeaderText="FirstUse" SortExpression="FirstUse" />
            <asp:BoundField DataField="LastUse" HeaderText="LastUse" SortExpression="LastUse" />
            <asp:BoundField DataField="RealUseDate" HeaderText="ActionUse" />
            <asp:BoundField DataField="UseCount" HeaderText="UseCount" SortExpression="UseCount" />
            <asp:BoundField DataField="NetworkMode" HeaderText="Network" SortExpression="NetworkMode" />
            <asp:BoundField DataField="NetworkPc" HeaderText="NetworkPc" SortExpression="NetworkPc" />
            <asp:BoundField DataField="FileSendCount" HeaderText="FileSend" SortExpression="FileSendCount" />
            <asp:BoundField DataField="ShareDownCount" HeaderText="ShareDown" SortExpression="ShareDownCount" />
            <asp:BoundField DataField="AdsShowCount" HeaderText="AdsCount" SortExpression="AdsShowCount" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField HeaderText="Location" SortExpression="OrderID" >
                <ItemStyle Width="110px" />
            </asp:BoundField>
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID" />
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>"
        DeleteCommand="DELETE FROM [AppUser] WHERE [PcID] = @PcID"
        SelectCommand="SELECT DISTINCT AppUser.PcID, AppUser.PcName, AppUser.FirstUse, AppUser.LastUse, AppUser.UseCount, AppUser.NetworkMode, AppUser.IP, AppUser.FileSendCount, AppUser.ShareDownCount, AppUser.NetworkPc, Activate.OrderID, AppUser.AdsShowCount, AppUser.RealUseDate, AppUser.WinOS FROM AppUser LEFT OUTER JOIN Activate ON AppUser.PcID = Activate.PcID ORDER BY AppUser.LastUse DESC">
        <DeleteParameters>
            <asp:Parameter Name="PcID" Type="String" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>


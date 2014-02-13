<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TrialOne.aspx.cs" Inherits="TrialOne" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        <asp:ListItem>2012</asp:ListItem>
        <asp:ListItem>2013</asp:ListItem>
    </asp:DropDownList>&nbsp;<br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="list versionCompare"
        DataKeyNames="ID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" PageSize="30">
        <Columns>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
            <asp:BoundField DataField="Number" HeaderText="Month" SortExpression="Number" />
            <asp:BoundField DataField="OnePc" HeaderText="OnePc" SortExpression="OnePc" />
            <asp:BoundField DataField="TwoPc" HeaderText="TwoPc" SortExpression="TwoPc" />
            <asp:BoundField DataField="ThreePc" HeaderText="ThreePc" SortExpression="ThreePc" />
            <asp:BoundField DataField="FourPc" HeaderText="FourPc" SortExpression="FourPc" />
            <asp:BoundField DataField="FivePlusPc" HeaderText="FivePlusPc" SortExpression="FivePlusPc" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:KeywordComptingConnectionString %>" DeleteCommand="DELETE FROM [TrialOne] WHERE [ID] = @original_ID AND (([Year] = @original_Year) OR ([Year] IS NULL AND @original_Year IS NULL)) AND (([Number] = @original_Number) OR ([Number] IS NULL AND @original_Number IS NULL)) AND (([OnePc] = @original_OnePc) OR ([OnePc] IS NULL AND @original_OnePc IS NULL)) AND (([TwoPc] = @original_TwoPc) OR ([TwoPc] IS NULL AND @original_TwoPc IS NULL)) AND (([ThreePc] = @original_ThreePc) OR ([ThreePc] IS NULL AND @original_ThreePc IS NULL)) AND (([FourPc] = @original_FourPc) OR ([FourPc] IS NULL AND @original_FourPc IS NULL)) AND (([FivePlusPc] = @original_FivePlusPc) OR ([FivePlusPc] IS NULL AND @original_FivePlusPc IS NULL))"
        InsertCommand="INSERT INTO [TrialOne] ([Year], [Number], [OnePc], [TwoPc], [ThreePc], [FourPc], [FivePlusPc]) VALUES (@Year, @Number, @OnePc, @TwoPc, @ThreePc, @FourPc, @FivePlusPc)"
        OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [TrialOne] WHERE ([Year] = @Year)"
        UpdateCommand="UPDATE [TrialOne] SET [Year] = @Year, [Number] = @Number, [OnePc] = @OnePc, [TwoPc] = @TwoPc, [ThreePc] = @ThreePc, [FourPc] = @FourPc, [FivePlusPc] = @FivePlusPc WHERE [ID] = @original_ID AND (([Year] = @original_Year) OR ([Year] IS NULL AND @original_Year IS NULL)) AND (([Number] = @original_Number) OR ([Number] IS NULL AND @original_Number IS NULL)) AND (([OnePc] = @original_OnePc) OR ([OnePc] IS NULL AND @original_OnePc IS NULL)) AND (([TwoPc] = @original_TwoPc) OR ([TwoPc] IS NULL AND @original_TwoPc IS NULL)) AND (([ThreePc] = @original_ThreePc) OR ([ThreePc] IS NULL AND @original_ThreePc IS NULL)) AND (([FourPc] = @original_FourPc) OR ([FourPc] IS NULL AND @original_FourPc IS NULL)) AND (([FivePlusPc] = @original_FivePlusPc) OR ([FivePlusPc] IS NULL AND @original_FivePlusPc IS NULL))">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="Year" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Year" Type="Int32" />
            <asp:Parameter Name="original_Number" Type="Int32" />
            <asp:Parameter Name="original_OnePc" Type="Int32" />
            <asp:Parameter Name="original_TwoPc" Type="Int32" />
            <asp:Parameter Name="original_ThreePc" Type="Int32" />
            <asp:Parameter Name="original_FourPc" Type="Int32" />
            <asp:Parameter Name="original_FivePlusPc" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Year" Type="Int32" />
            <asp:Parameter Name="Number" Type="Int32" />
            <asp:Parameter Name="OnePc" Type="Int32" />
            <asp:Parameter Name="TwoPc" Type="Int32" />
            <asp:Parameter Name="ThreePc" Type="Int32" />
            <asp:Parameter Name="FourPc" Type="Int32" />
            <asp:Parameter Name="FivePlusPc" Type="Int32" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Year" Type="Int32" />
            <asp:Parameter Name="original_Number" Type="Int32" />
            <asp:Parameter Name="original_OnePc" Type="Int32" />
            <asp:Parameter Name="original_TwoPc" Type="Int32" />
            <asp:Parameter Name="original_ThreePc" Type="Int32" />
            <asp:Parameter Name="original_FourPc" Type="Int32" />
            <asp:Parameter Name="original_FivePlusPc" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Year" Type="Int32" />
            <asp:Parameter Name="Number" Type="Int32" />
            <asp:Parameter Name="OnePc" Type="Int32" />
            <asp:Parameter Name="TwoPc" Type="Int32" />
            <asp:Parameter Name="ThreePc" Type="Int32" />
            <asp:Parameter Name="FourPc" Type="Int32" />
            <asp:Parameter Name="FivePlusPc" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>


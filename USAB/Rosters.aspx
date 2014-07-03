<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Rosters.aspx.cs" Inherits="Rosters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
        CellPadding="4" GridLines="Horizontal"
        Width="100%" AllowPaging="True" Height="100px"
        AllowSorting="True" EnableSortingAndPagingCallbacks="True" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ForeColor="Black">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <PagerSettings FirstPageText="First" LastPageText="Last" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
         <Columns>
             <asp:BoundField AccessibleHeaderText="Player ID" DataField="PlayerID" HeaderText="Player ID " />
             <asp:BoundField AccessibleHeaderText="First Name" DataField="FirstName" HeaderText="First Name" />
             <asp:BoundField AccessibleHeaderText="Last Name" DataField="LastName" HeaderText="Last Name" />
             <asp:BoundField AccessibleHeaderText="Position" DataField="Position" HeaderText="Position" />
             <asp:BoundField AccessibleHeaderText="Height" DataField="Height" HeaderText="Height" />
             <asp:BoundField AccessibleHeaderText="Weight" DataField="Weight" HeaderText="Weight" />
             <asp:BoundField AccessibleHeaderText="City" DataField="City" HeaderText="City" />
             <asp:BoundField AccessibleHeaderText="State" DataField="State" HeaderText="State" />
         </Columns>
        <EmptyDataRowStyle BackColor="Gainsboro" ForeColor="Red" Font-Bold="True" Font-Italic="True" />
        <EmptyDataTemplate>
            There is no data for this request.
        </EmptyDataTemplate>
         <SortedAscendingCellStyle BackColor="#F7F7F7" />
         <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
         <SortedDescendingCellStyle BackColor="#E5E5E5" />
         <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

</asp:Content>


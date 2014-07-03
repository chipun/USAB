<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <link href="Styles/colorbox.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery.colorbox-min.js"></script>
  <%--  <script type="text/javascript" src="Scripts/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("a[rel='images']").colorbox({ transition: "fade" });
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $("a[rel='images']").colorbox({ transition: "fade" });
            }
        }
    </script>
    
    <asp:Label ID="userID" runat="server"></asp:Label>
 
    <div id="imagecontent" style="padding:20px" >
      
        <asp:ListView ID="ThumbnailsList" runat="server">
                <ItemTemplate>
                         <a href="<%# Eval("MediumUrl") %>" rel="images" title="<%# Eval("Title") %>">
                            <img id="ThumbnailImage" alt="<%# Eval("Title") %>" src="<%# Eval("SquareThumbnailUrl") %>" /></a>
                  </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>


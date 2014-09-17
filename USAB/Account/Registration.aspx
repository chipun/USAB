<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Account_Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="../bootstrapvalidator/vendor/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="../bootstrapvalidator/dist/css/bootstrapValidator.css" />
    <link rel="stylesheet" href="../bootstrapvalidator/vendor/bootstrap/css/bootstrap.min.css"  />
</head>
<body>
    <form id="register_form" runat="server">
        <div class="navbar navbar-inverse">


            <a class="navbar-brand" href="../Default.aspx">USAB</a>

        </div>
        <div class="container" role="main">
            <div class="form-horizontal jumbotron">
                <fieldset>
                    <legend>Sign Up for USAB Account</legend>

                    <div class="form-group">

                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="email" CssClass="col-lg-3 control-label" Text="Email Address"></asp:Label>

                        <div class="col-xs-4">

                            <asp:TextBox ID="email" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblPasswordASP" runat="server" AssociatedControlID="passwordASP" CssClass="col-lg-3 control-label" Text="Password"></asp:Label>
                        <div class="col-xs-4">
                            <asp:TextBox ID="passwordASP" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="passwordASP2" CssClass="col-lg-3 control-label" Text="Confrim Password"></asp:Label>
                        <div class="col-xs-4">
                            <asp:TextBox ID="passwordASP2" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
                <div class="form-group">
                    <div class="col-lg-5 col-lg-offset-3">

                        <asp:Button ID="btnsubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                    </div>

                </div>
            </div>

        </div>
       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>

                <asp:ScriptReference Path="http://code.jquery.com/jquery.js" />
                <asp:ScriptReference Path="~/bootstrapvalidator/vendor/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/bootstrapvalidator/dist/js/bootstrapValidator.js" />
                <asp:ScriptReference Path="~/Scripts/app.js" />
            </Scripts>
        </asp:ScriptManager>

        <script type="text/javascript">
            $(document).ready(function () {
                App.BootstrapValidator();
            });
           
        </script>
    </form>
</body>
</html>

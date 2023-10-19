<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Guest_login.aspx.cs" Inherits="Gabay_Final_V2.Views.LoginPages.Guest_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Guest Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="../../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <link href="../../Resources/CustomStyleSheet/LoginPageStyle.css" rel="stylesheet" />
    <link href="../../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../../FontAwesome/css/all.css" rel="stylesheet" />
    <script src="../../Bootstrap/Scripts/bootstrap.js"></script>
    <script src="../../FontAwesome/js/all.js"></script>
</head>
<body>
    <div class="container">
        <div class="row p-3">
            <div class="col-md-5 d-none d-md-block custom-bg shadow-lg">
                <asp:HyperLink ID="goBack" runat="server" CssClass="fs-3 text-decoration-none backButton " NavigateUrl="~/Landing_Page/LandingPage.aspx">
                    <i class="bi bi-arrow-left-square"></i>
                </asp:HyperLink>
            </div>
            <div class="col-md-7 col-sm-12 p-5 justify-content-center bg-light shadow-lg">
                <asp:HyperLink ID="goBack1" runat="server" CssClass="fs-3 d-block d-md-none text-decoration-none backButton" NavigateUrl="~/Landing_Page/LandingPage.aspx">
                    <i class="bi bi-arrow-left-square"></i>
                </asp:HyperLink>
                <div class="text-center">
                    <img class="logo-size text-center" src="../../Resources/Images/UC-LOGO.png" />
                </div>
                <div class="divider mb-2">
                    <div class="left-line"></div>
                    <div class="divider-text">Gabay</div>
                    <div class="right-line"></div>
                </div>
                <form id="form1" runat="server">
                    <div id="errorDiv" runat="server" class="alert alert-danger d-none" role="alert">
                        <i class="bi bi-exclamation-triangle"></i>Please provide your name to address you properly.     
                    </div>
                    <p class=" text-center text-uppercase fw-bold fs-2">
                        welcome!
                    </p>
                    <div class="input-group mb-3">
                        <span class="input-group-text"><i class="bi bi-file-person"></i></span>
                        <div class="form-floating">
                            <asp:TextBox ID="guestNameBx" CssClass="form-control" runat="server" placeholder="Guest"></asp:TextBox>
                            <label for="guestNameBx">Name</label>
                        </div>
                    </div>
                    <div class="d-grid">
                        <asp:Button ID="gst_lgnBtn" CssClass="btn lgn_btn mb-3" runat="server" Text="Proceed" OnClick="gst_lgnBtn_Click" />
                    </div>
                </form>
                <p class="d-flex justify-content-center">
                    <a class="text-decoration-none custom-text" href="#">Don't have an Account?</a>
                </p>
            </div>
        </div>
    </div>
</body>

</html>

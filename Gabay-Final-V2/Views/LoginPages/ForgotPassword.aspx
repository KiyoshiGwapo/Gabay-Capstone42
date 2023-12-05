<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Gabay_Final_V2.Views.LoginPages.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Guest Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
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
                <asp:HyperLink ID="goBack" runat="server" CssClass="fs-3 text-decoration-none backButton" NavigateUrl="~/Views/LoginPages/Student_login.aspx">
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
                    <div class="card-body">
                        <h2 class="card-title text-center">Retrieve Password</h2>
                        <asp:Label ID="GreenMessage" runat="server" Text="" CssClass="text-success"></asp:Label>
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        <div class="mb-3">
                            <asp:TextBox ID="txtUserID" runat="server" placeholder="Enter your User ID" CssClass="form-control" />
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter your Email" CssClass="form-control" />
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnRetrievePassword" runat="server" Text="Retrieve Password" OnClick="btnRetrievePassword_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
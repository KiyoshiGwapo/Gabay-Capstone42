<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_login.aspx.cs" Inherits="Gabay_Final_V2.Views.LoginPages.Student_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Student Login</title>
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
            <div class="col-md-5 d-none d-md-block custom-bg1 shadow-lg">
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
                        <i class="bi bi-exclamation-triangle">Incorrect Login Credentials</i>
                    </div>
                    <div id="pendingError" runat="server" class="alert alert-danger d-none" role="alert">
                        <i class="bi bi-exclamation-triangle">Your account is still pending approval. Please wait for approval.</i>
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text"><i class="bi bi-person-fill"></i></span>
                        <div class="form-floating">
                            <asp:TextBox ID="login_ID" runat="server" class="form-control" placeholder="ID Number"></asp:TextBox>
                            <label for="login_ID">Student ID</label>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text"><i class="bi bi-lock-fill"></i></span>
                        <div class="form-floating">
                            <asp:TextBox ID="login_Pass" runat="server" CssClass="form-control" PlaceHolder="Login Password"></asp:TextBox>
                            <label for="login_Pass">Password</label>
                        </div>
                    </div>
                    <div class=" d-flex justify-content-between mb-2">
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" />
                            <label class="form-check-label" for="flexCheckDefault">
                                Remember Me
                            </label>
                        </div>
                    </div>
                    <div class="d-grid">
                        <asp:Button ID="btn_studLogin" CssClass="btn lgn_btn mb-3" runat="server" Text="Login" OnClick="btn_studLogin_Click" />
                    </div>
                </form>
                <p class="d-flex justify-content-between">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="text-decoration-none custom-text" NavigateUrl="~/Views/RegistrationPages/Student_Reg.aspx">Don't have an Account?</asp:HyperLink>
                    <asp:HyperLink ID="frgtPass" CssClass="text-decoration-none custom-text" runat="server" NavigateUrl="~/Views/LoginPages/ForgotPassword.aspx" >Forgot Password?</asp:HyperLink>

                </p>
            </div>
        </div>
    </div>
</body>
</html>

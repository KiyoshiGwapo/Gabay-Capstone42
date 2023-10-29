<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JavaScript.js"></script>
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.min.js"></script>
    <style>
        .headerContainer{
            display:flex;
            justify-content:center;
            align-items:center;
            margin-bottom: 10px;
        }
        .LogoImg{
            width:100px;
            height:auto;
            margin-right:5px;
        }
        .LogoBrand{
            letter-spacing:3px;
            color:#003366;
            font-weight:600;

        }
        .bodyContainer{
            display: flex;
            flex-direction:column;
            align-items:center;
            justify-content:center;
        }
        .verifiedImg{
            width:10rem;
            height:auto;
        }
        .TextHeader{
            font-size:2rem;
            letter-spacing:2px;
            font-weight:700;
        }
        .bodyTextContainer{
            margin-top: 20px;
            text-align:center;
            font-size:1rem;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
       <div class="headerContainer">
           <img src="../Resources/Images/UC-LOGO.png" class="LogoImg" />
           <span class="LogoBrand">GABAY</span>
       </div>
        <div class="bodyContainer">
            <img src="../Resources/Images/tempIcons/verified.png" class="verifiedImg" />
            <span class="TextHeader">Account Verified</span>
            <div class="bodyTextContainer">

                <span>Hello! (place name here), Your account has been verified and activated.<br />
                Follow the link here <a href='https://localhost:44341/Views/LoginPages/Student_login.aspx'>Gabay Login</a>
                to login your account.</span>
            </div>
        </div>
    </form>
</body>
</html>

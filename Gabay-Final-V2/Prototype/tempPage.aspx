<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tempPage.aspx.cs" Inherits="Gabay_Final_V2.Prototype.tempPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QR VERIFIED</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="../FontAwesome/css/all.css" rel="stylesheet" />
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <style>
        .verifiedImage{
            width:200px;
            height:auto;
        }
        .verifiedItems{
            display:flex;
            justify-content:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="verifiedItems">
            <img src="../Resources/Images/tempIcons/verified.png" class="verifiedImage" />
        </div>
        <div class="verifiedItems mb-3">
            <span class="fs-3 fw-bold">Appointment Verified!</span>
        </div>
       
        <div class="d-flex flex-column justify-content-center align-items-center">
            <div class="AppointmentID">
                <label for="appointID" class="fw-bold">Appointment ID:</label>
                <asp:Label ID="appointID" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="Appointee">
                <label for="Name" class="fw-bold">Appointee:</label>
                 <asp:Label ID="Name" runat="server" Text="Label"></asp:Label>
            </div>
           <div class="AppointmentDate">
               <label class="fw-bold">Date:</label>
               <asp:Label ID="Date" runat="server" Text="Label"></asp:Label>
           </div>
            <div class="AppointmentTime">
                <label class="fw-bold">Time:</label>
                <asp:Label ID="Time" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="Concern">
                <label class="fw-bold">Concern:</label>
                <asp:Label ID="Concern" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

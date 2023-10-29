<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm15.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm15" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
    <style>
        .img-container{
            display:flex;
            justify-content:center;
            height: 200px;
            width:auto;
        }
        .appointmentLbl{
            text-align:end;
        }
        .appointmentHolder{
            margin-left:50px;
        }
        .appointmentLbls{
            margin: 20px 20px;
        }
        .appointment-Body{
            margin-top:20px;
            display: flex;
            flex-direction:column;
            align-items:center;
        }
        table{
            width: 600px !important;
        }
        table td{
             vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="Appointment-Status">
                        <div class="img-container">
                            <img src="../Resources/Images/tempIcons/database.png" class="img-fluid" />
                        </div>
                        <div class="d-flex justify-content-around text-center mt-3">
                            <%--<asp:Label ID="StatusLabel" runat="server" Text="Pending" CssClass="fs-2"></asp:Label>--%>
                            <span class="fs-5">
                                You have successfully booked an appointment.<br /> 
                                Our team is currently verifying the availability of the chosen time and date.<br /> 
                                Please stay connected with your email for additional updates regarding your appointment schedule.
                            </span>
                        </div>
                        <div class="appointment-Body">
                            <table class="table table-borderless">
                                <tr class="appointmentLbls">
                                    <th class="appointmentLbl">
                                        <asp:Label ID="appointmentIDLbl" runat="server" Text="Appointment ID:" ></asp:Label>
                                    </th>
                                    <td>
                                        <asp:Label ID="appointmentID" runat="server" Text="Sample ID" CssClass="appointmentHolder"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="appointmentLbls">
                                    <th class="appointmentLbl">
                                        <asp:Label ID="appointmentStatusLbl" runat="server" Text="Status:"></asp:Label>
                                    </th>
                                    <td>
                                        <asp:Label ID="appointmentStatus" runat="server" Text="Sample Status" CssClass="appointmentHolder"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="appointmentLbls">
                                    <th class="appointmentLbl">
                                        <asp:Label ID="appointmentDateLbl" runat="server" Text="Date:"></asp:Label>
                                    </th>
                                    <td>
                                        <asp:Label ID="appointmentDate" runat="server" Text="Sample Date" CssClass="appointmentHolder"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="appointmentLbls">
                                    <th class="appointmentLbl">
                                        <asp:Label ID="appointmentTimeLbl" runat="server" Text="Time:"></asp:Label>
                                    </th>
                                    <td>
                                        <asp:Label ID="appointmentTime" runat="server" Text="Sample Time" CssClass="appointmentHolder"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="appointmentLbls">
                                    <th class="appointmentLbl">
                                        <asp:Label ID="appointmentConcernLbl" runat="server" Text="Concern:"></asp:Label>
                                    </th>
                                    <td>
                                        <asp:Label ID="appointmentConcern" runat="server" Text="Sample Concern" CssClass="appointmentHolder">
                                            You have successfully booked an appointment.
                                Our team is currently verifying the availability of the chosen time and date.
                                Please stay connected with your email for additional updates regarding your appointment schedule.
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

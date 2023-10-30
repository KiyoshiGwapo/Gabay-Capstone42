<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Appointment_Status.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Appointment_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .img-container {
            display: flex;
            justify-content: center;
            height: 200px;
            width: auto;
        }

        .appointmentLbl {
            text-align: end;
        }

        .appointmentHolder {
            margin-left: 50px;
        }

        .appointmentLbls {
            margin: 20px 20px;
        }

        .appointment-Body {
            margin-top: 20px;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        table {
            width: 600px !important;
        }

            table td {
                vertical-align: middle;
            }

        .custom-button {
            background-color: #007bff; 
            color: #fff;
            padding: 10px 20px; 
            border: none; 
            border-radius: 5px;
            cursor: pointer; 
            margin-top: 20px;
            float: right;
        }

            .custom-button:hover {
                background-color: #0056b3; 
            }
    </style>
    <asp:Button ID="ViewHistoryButton" runat="server" Text="View My History" CssClass="custom-button" OnClick="ViewHistoryButton_Click" />
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="Appointment-Status">
                    <div class="img-container">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~\Resources\Images\tempIcons\database.png" />
                    </div>
                    <div class="d-flex justify-content-around text-center mt-3">
                        <%--<asp:Label ID="StatusLabel" runat="server" Text="Pending" CssClass="fs-2"></asp:Label>--%>
                        <span class="fs-5">
                            <asp:Label ID="Indication" runat="server" Text="Label"></asp:Label>
                        </span>
                    </div>
                    <div class="appointment-Body">
                        <table class="table table-borderless">
                            <tr class="appointmentLbls">
                                <th class="appointmentLbl">
                                    <asp:Label ID="appointmentIDLbl" runat="server" Text="Appointment ID:"></asp:Label>
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
                                        </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

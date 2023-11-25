<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Manage_Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    .
   
    <script src="../Resources/CustomJS/jquery-3.5.1.slim.min.js"></script>
    <script src="../Resources/CustomJS/bootstrap.min.js"></script>
    <link href="../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <style>
        .appointeeName {
            color: #003366;
            font-weight: 600;
        }

        .appointmentSchedule {
            color: white;
            border: 1px solid #dcdcdc;
            padding: 10px;
            border-radius: 10px;
            background-color: #003366;
        }

        .appointmentStatus {
            border-radius: 15px;
            background-color: white;
            height: 25px;
            color: #003366;
            padding: 0 8px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .appointmentActions a {
            text-decoration: none;
        }

        .appointmentConcern {
            border: 1px solid #003366 !important;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .appointmentID {
            font-size: 13px;
        }

        .short-dropdown {
            width: 180px;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-10 col-md-12">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control float-end mb-3" placeholder="Search student..."></asp:TextBox>
            </div>
            <div class="col-lg-2 col-md-12">
                <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle w-100" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Appointment Status
                       
                    </button>
                    <ul class="dropdown-menu w-100">
                        <li>
                            <asp:LinkButton ID="displayPending" CssClass="dropdown-item" runat="server">Pending</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="displayActive" CssClass="dropdown-item" runat="server">Reschedule</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="displayDeactivated" CssClass="dropdown-item" runat="server">Approved</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-4 col-sm-12">
                <div class="d-flex flex-row justify-content-end mt-3">
                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-select me-2 short-dropdown">
                        <asp:ListItem Text="Choose Format" Value="" Disabled="true" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Excel" Value="Excel"></asp:ListItem>
                        <asp:ListItem Text="PDF" Value="PDF"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnDownloadReports" runat="server" Text="Generate Reports" OnClick="btnDownloadReports_Click" CssClass="btn btn-success" />
                </div>
            </div>
            <div class="mt-2"></div>
            <div class="col-12">
                <asp:GridView ID="GridView1" runat="server" CssClass="table" DataKeyNames="ID_appointment" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ID_appointment" HeaderText="Appointment ID" />
                        <asp:BoundField DataField="full_name" HeaderText="Recipient" />
                        <asp:BoundField DataField="role" HeaderText="User Type" />
                        <asp:BoundField DataField="appointment_date" HeaderText="Date" DataFormatString="{0:dd MMM, yyyy}" />
                        <asp:BoundField DataField="appointment_time" HeaderText="Time" />
                        <asp:BoundField DataField="appointment_status" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="ViewConcernModal" runat="server" Text="View Content" CssClass="btn bg-primary text-light" OnClick="ViewConcernModal_Click" OnClientClick='<%# "return getAppointmentID(" + Eval("ID_appointment") + ");" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:HiddenField ID="HiddenFieldAppointment" runat="server" />
        </div>
    </div>
    <%-- View Content Modal --%>
    <div class="modal fade" id="exampleModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Appointment Content</h1>
                    <asp:Button ID="CloseViewModal" runat="server" CssClass="btn-close" OnClick="CloseViewModal_Click" />
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row g-2">
                            <div class="col-12 mb-2">
                                <div class="appointeeName">
                                    <asp:Label ID="appointmentName" runat="server" CssClass="fs-4"></asp:Label>
                                </div>
                                <div class="appointmentID">
                                    <label for="Label1" class="text-secondary">Appointment ID: </label>
                                    <asp:Label ID="Label1" runat="server" CssClass="text-secondary"></asp:Label>
                                </div>
                            </div>

                            <div class="col-12 mb-2">
                                <div class="form-floating">
                                    <asp:TextBox ID="appointmentConcern" CssClass="form-control appointmentConcern" runat="server" placeholder="Concern" Style="height: 120px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    <label for="appointmentConcern">Concern</label>
                                </div>
                            </div>
                            <div class="col-12 mb-2">
                                <div class="appointmentSchedule d-flex flex-column">
                                    <div class="d-flex justify-content-between">
                                        <span class="fs-5 fw-medium">Schedule</span>
                                        <div class="appointmentStatus">
                                            <asp:Label ID="AppointmentStatus" runat="server" Text="Status"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:Label ID="AppointmentDate" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="AppointmentTime" runat="server" Text="Time"></asp:Label>
                                </div>
                            </div>
                            <div class="col-12 mb-2" id="resched" runat="server">
                                <div class="appointmentActions resched">
                                    <asp:LinkButton ID="appointmentReschedule" runat="server" OnClick="appointmentReschedule_Click">
                                            <i class="bi bi-calendar-minus-fill"></i>
                                            <span>Reschedule Appointment</span>
                                        </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-8 mb-2 d-grid" id="approved" runat="server">
                                <asp:LinkButton ID="ApproveLink" runat="server" CssClass="btn btn-primary" OnClick="ApproveLink_Click">Approve Appointment</asp:LinkButton>
                            </div>
                            <div class="col-4 mb-2 d-grid" id="reject" runat="server">
                                <asp:LinkButton ID="RejectLink" runat="server" CssClass="btn btn-danger" OnClick="RejectLink_Click">Reject</asp:LinkButton>
                            </div>
                            <div class="col-8 mb-2 d-grid" id="servedlnk" runat="server">
                                <asp:LinkButton ID="served" runat="server" CssClass="btn btn-primary" OnClick="served_Click">Served</asp:LinkButton>
                            </div>
                            <div class="col-4 mb-2 d-grid" id="noShowlnk" runat="server">
                                <asp:LinkButton ID="noshow" runat="server" CssClass="btn btn-danger" OnClick="noshow_Click">No Show</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Reschedule Modal --%>
    <div class="modal fade" id="reschedModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton ID="gobackToViewAppointment" runat="server" CssClass="fs-5 text-secondary" OnClick="gobackToViewAppointment_Click">
                             <i class="bi bi-chevron-compact-left"></i>
                        </asp:LinkButton>
                    <asp:LinkButton ID="closeReschedModal" runat="server" CssClass="btn-close" OnClick="closeReschedModal_Click">
                        </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="contianer-fluid">
                        <div class="row g-2">
                            <div class="col-12 mb-2">
                                <span class="fs-4">Reschedule Appointment</span>
                                <div class="appointmentSchedule d-flex flex-column">
                                    <div class="d-flex justify-content-between">
                                        <span class="fs-5 fw-medium">Current Schedule</span>
                                        <div class="appointmentStatus">
                                            <asp:Label ID="CurrentAppointmentStatus" runat="server" Text="Status"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:Label ID="CurrentAppointmentDate" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="CurrentAppointmentTime" runat="server" Text="Time"></asp:Label>
                                </div>
                            </div>
                            <div class="col-6 mb-2">
                                <div class="form-floating">
                                    <asp:DropDownList ID="newtime" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="" Selected="True">Selec Available Time</asp:ListItem>
                                        <asp:ListItem Value="8:00 AM">8:00 AM</asp:ListItem>
                                        <asp:ListItem Value="9:00 AM">9:00 AM</asp:ListItem>
                                        <asp:ListItem Value="10:00 AM">10:00 AM</asp:ListItem>
                                        <asp:ListItem Value="11:00 AM">11:00 AM</asp:ListItem>
                                        <asp:ListItem Value="1:00 PM">1:00 PM</asp:ListItem>
                                        <asp:ListItem Value="2:00 PM">2:00 PM</asp:ListItem>
                                        <asp:ListItem Value="3:00 PM">3:00 PM</asp:ListItem>
                                        <asp:ListItem Value="4:00 PM">4:00 PM</asp:ListItem>
                                    </asp:DropDownList>
                                    <label for="newtime">Available Time</label>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="newdate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                    <label for="newdate">Available Date</label>
                                </div>
                            </div>
                            <div class="col-12 d-grid">
                                <asp:LinkButton ID="updtSchedule" runat="server" CssClass="btn bg-primary text-light" OnClick="LinkButton1_Click">Update Schedule</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Reject Modal --%>
    <div class="modal fade" id="rejectModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton ID="goBackToViewFromReject" runat="server" CssClass="fs-5 text-secondary" OnClick="gobackToViewAppointment_Click">
                            <i class="bi bi-chevron-compact-left"></i>
                        </asp:LinkButton>
                    <asp:LinkButton ID="closeReject" runat="server" CssClass="btn-close" OnClick="closeReschedModal_Click">
                        </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="contianer-fluid">
                        <div class="row">
                            <div class="col-12">
                                <span class="fs-5">Reason for rejecting</span>
                                <asp:TextBox ID="rejectReason" CssClass="form-control" runat="server" Style="height: 100px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn bg-secondary text-light" OnClick="gobackToViewAppointment_Click">Cancel</asp:LinkButton>
                    <asp:Button ID="rejectBtn" runat="server" Text="Reject Appointment" CssClass="btn bg-primary text-light" OnClick="rejectBtn_Click" />
                </div>
            </div>
        </div>
    </div>

    <%-- Approved Confirmation Modal --%>
    <div class="modal fade" id="ApproveModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton ID="goBackToViewFromApprove" runat="server" CssClass="fs-5 text-secondary" OnClick="gobackToViewAppointment_Click">
                            <i class="bi bi-chevron-compact-left"></i>
                        </asp:LinkButton>
                    <asp:LinkButton ID="closeApproved" runat="server" CssClass="btn-close" OnClick="closeReschedModal_Click">
                        </asp:LinkButton>
                </div>
                <div class="modal-body">
                    <p id="approveMessage"></p>
                </div>
                <div class="modal-footer d-grid">
                    <asp:Button ID="ApproveButton" runat="server" Text="Proceed" CssClass="btn bg-primary text-light" OnClick="ApproveButton_Click" />
                </div>
            </div>
        </div>
    </div>
    <%--Reschedule Confirmation Modal --%>
    <div class="modal fade" id="ConfirmationModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="fs-5 text-secondary" OnClick="gobackToViewAppointment_Click">
                            <i class="bi bi-chevron-compact-left"></i>
                        </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn-close" OnClick="closeReschedModal_Click"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <p id="confirmationMessage"></p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="goBacktoReschedModal" runat="server" CssClass="btn bg-secondary" OnClick="goBacktoReschedModal_Click">Cancel</asp:LinkButton>
                    <asp:Button ID="updtSchedBtn" runat="server" Text="Update Schedule" CssClass="btn bg-primary text-light" OnClick="updtSchedBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <%-- Reject Confirmation Modal --%>
    <div class="modal fade" id="RejectModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fs-5 text-secondary" OnClick="gobackToViewAppointment_Click">
                            <i class="bi bi-chevron-compact-left"></i>
                        </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn-close" OnClick="closeReschedModal_Click"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <p id="confirmRejectMessage"></p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="goBackViewFromReject" runat="server" CssClass="btn bg-secondary" OnClick="gobackToViewAppointment_Click">Cancel</asp:LinkButton>
                    <asp:LinkButton ID="rejectAppointmentBtnLink" runat="server" CssClass="btn bg-primary text-light" OnClick="rejectAppointmentBtnLink_Click">Proceed</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>


    <%-- Success modal --%>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <p id="successMessage"></p>
                </div>
            </div>
        </div>
    </div>
    <%-- Error modal --%>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-danger text-center text-light">
                    <i class="bi bi-exclamation-circle-fill"></i>
                    <p id="errorMessage"></p>
                </div>
            </div>
        </div>
    </div>
    <script>
        function getAppointmentID(id) {
            document.getElementById('<%= HiddenFieldAppointment.ClientID %>').value = id;
        }
    </script>
</asp:Content>

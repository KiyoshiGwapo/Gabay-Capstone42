<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_profile.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Student_Homepage.Student_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style>
        .profile-icon {
            display: flex;
            justify-content: center;
            font-size: 9rem;
            color: #003366;
        }

        .IDnumber-placeholder {
            display: flex;
            text-align: center;
            flex-direction: column;
            font-size: 25px;
        }

        .idnum-label {
            font-size: 15px;
            font-weight: bold;
        }

        #editBtn {
            color: #003366;
            opacity: 75%;
        }

            #editBtn:hover {
                opacity: 100%;
            }

        .table-container {
            text-align: center;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .custom-table {
            border-collapse: collapse;
            border-radius: 50px !important;
            width: 600px;
        }

            .custom-table th, .custom-table td {
                padding: 8px 12px;
                border: 1px solid #000;
                border-radius: 50px !important;
            }

            .custom-table td {
                text-align: left;
            }

            .custom-table th {
                text-align: right;
            }

        .border-modal {
            border-radius: 10px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        .pass {
            border-bottom: 1px solid rgba(0, 0, 0, 0.2);
        }

            .pass:hover {
                background-color: #003366;
                color: white;
                cursor: pointer;
                border-radius: 10px 10px 0 0;
            }

        .dpt {
            border-bottom: 1px solid rgba(0, 0, 0, 0.2);
        }

            .dpt:hover {
                background-color: #003366;
                color: white;
                cursor: pointer;
            }

        .email:hover {
            background-color: #003366;
            color: white;
            cursor: pointer;
            border-radius: 0 0 10px 10px;
        }

        .modalRouting:hover {
            cursor: pointer;
            color: #0c0c0c;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <span class="fs-2">Profile Details</span>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="profile-icon">
                    <i class="bi bi-person-circle"></i>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="IDnumber-placeholder">
                    <span class="idnum-label">ID Number</span>
                    <asp:Label ID="IDNumberLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <button type="button" id="editBtn" class="btn float-end" data-bs-toggle="modal" data-bs-target="#editModal">
                    <i class="bi bi-pencil-square fs-3"></i>
                </button>
            </div>
            <div class="col-12">
                <div class="table-container">
                    <table class="custom-table">
                        <tr>
                            <th>Student Name</th>
                            <td>
                                <asp:Label ID="StudentNameLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>Course</th>
                            <td>
                                <asp:Label ID="CourseLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>Year Level</th>
                            <td>
                                <asp:Label ID="YearLevelLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>College</th>
                            <td>
                                <asp:Label ID="CollegeLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>Contact Number</th>
                            <td>
                                <asp:Label ID="ContactNumberLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>Email Address</th>
                            <td>
                                <asp:Label ID="EmailAddressLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <%--Edit Modal--%>
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 d-flex justify-content-around align-items-center p-4">
                                <asp:Label ID="m_studName" CssClass="fs-3" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-12">
                                <div class="border-modal border boder-black">
                                    <div class="pass p-3" data-bs-toggle="modal" data-bs-target="#changePassModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Change Password</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                    <div class="email p-3" data-bs-toggle="modal" data-bs-target="#changeEmailModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Change Email</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Change password modal --%>
    <div class="modal fade" id="changePassModal" tabindex="-1" aria-labelledby="changePasswordModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="modal-title modalRouting fs-5" id="chngPss" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 d-flex justify-content-around align-items-center p-4">
                                <span class="fs-3">Change Password</span>
                            </div>
                            <!-- Inside the "Change password modal" -->
                            <div class="col-12 d-flex align-items-center">
                                <i class="bi bi-lock-fill mb-2 p-2"></i>
                                <div class="d-grid w-100">
                                    <asp:TextBox runat="server" ID="currentPasswordTextBox" CssClass="form-control mb-2" TextMode="Password" placeholder="Enter Current Password" aria-label="default input example"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 d-flex align-items-center">
                                <i class="bi bi-lock-fill mb-2 p-2"></i>
                                <div class="d-grid w-100">
                                    <asp:TextBox runat="server" ID="newPasswordTextBox" CssClass="form-control mb-2" TextMode="Password" placeholder="New Password" aria-label="default input example"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 d-flex align-items-center">
                                <i class="bi bi-lock-fill mb-2 p-2"></i>
                                <div class="d-grid w-100">
                                    <asp:TextBox runat="server" ID="confirmPasswordTextBox" CssClass="form-control mb-2" TextMode="Password" placeholder="Confirm Password" aria-label="default input example"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12 d-flex justify-content-center">
                                <asp:Button ID="changePass" runat="server" CssClass="btn bg-primary text-light" Text="Change Password" OnClientClick="return validatePassword();" OnClick="ChangePassword_Click" />
                                <div style="margin-left: 10px;"></div>
                                <button type="button" id="cancel" class="btn bg-secondary text-light" data-bs-dismiss="modal" aria-label="Close">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Change email modal --%>
    <div class="modal fade" id="changeEmailModal" tabindex="-1" aria-labelledby="changeEmailModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="modal-title modalRouting fs-5" id="chngEml" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 d-flex justify-content-around align-items-center p-4">
                                <span class="fs-3">Change Email</span>
                            </div>
                            <!-- Inside the "Change email modal" -->
                            <div class="col-12 d-flex align-items-center">
                                <i class="bi bi-envelope-fill mb-2 p-2"></i>
                                <div class="d-grid w-100">
                                    <asp:TextBox runat="server" ID="currentEmailTextBox" CssClass="form-control mb-2" TextMode="Email" placeholder="Enter Current Email" aria-label="default input example"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 d-flex align-items-center">
                                <i class="bi bi-envelope-fill mb-2 p-2"></i>
                                <div class="d-grid w-100">
                                    <asp:TextBox runat="server" ID="newEmailTextBox" CssClass="form-control mb-2" TextMode="Email" placeholder="Enter New Email" aria-label="default input example"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 d-grid">
                                <asp:Button runat="server" ID="updateEmailButton" CssClass="btn bg-primary text-light" Text="Update" OnClick="UpdateEmail_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- success modal --%>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <span>Successfully Updated</span>
                </div>
            </div>
        </div>
    </div>
    <%-- error modal --%>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-danger-subtle text-center text-light">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

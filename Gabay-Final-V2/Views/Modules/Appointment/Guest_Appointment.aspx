<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Guest_Appointment" ViewStateMode="Enabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../../../Resources/CustomJS/Chatbot/AppointmentJS.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Custom CSS for the form */
        .form-wrapper {
            border-radius: 10px;
            margin-top: 20px;
            box-shadow: 0 40px 40px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            background-color: #fff;
            padding: 20px;
            text-align: center;
        }

        .form-heading {
            background-color: #00008B;
            color: #fff;
            text-align: center;
            padding: 10px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 24px;
        }

        .form-group {
            text-align: left;
            margin-bottom: 15px;
        }

        .form-control {
            border: 1px solid #ccc;
            padding: 10px;
            border-radius: 5px;
            width: 100%;
            font-size: 14px;
        }

            .form-control.invalid {
                border: 2px solid red;
            }

            .form-control.valid {
                border: 2px solid green;
            }

        .btn-submit {
            background-color: #00008B;
            color: #fff;
            border: none;
            padding: 15px 30px;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

            .btn-submit:hover {
                background-color: #333;
            }

        /* Custom styles for status labels */
        .status-not-submitted {
            color: red;
        }

        .status-submitted {
            color: green;
        }

        .img-placeholder {
            width: 100px;
            height: auto;
        }

        .reschedBtn {
            margin-left: 5px;
        }

            .reschedBtn:hover {
                opacity: 80%;
            }

        .acceptBtn {
            width: 160px;
        }
    </style>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const fullNameInput = document.getElementById("<%= FullName.ClientID %>");
            const contactNumberInput = document.getElementById("<%= ContactN.ClientID %>");

            fullNameInput.addEventListener("input", function () {
                // Allow only letters, spaces, and a dot after a space
                this.value = this.value.replace(/[^A-Za-z\s.]+/g, '');
            });

            contactNumberInput.addEventListener("input", function () {
                // Allow only numbers
                this.value = this.value.replace(/\D/g, '');
            });
        });

    </script>
    <div class="form-wrapper">
        <div class="row">
            <div class="col-md-6">
                <h2 class="form-heading">Appointment Form</h2>
                <%--  <asp:Label ID="SubmissionStatusSubmitted" runat="server" Text="" CssClass="submission-status-Submitted" />
                <asp:Label ID="SubmitStatusNotSubmitted" runat="server" Text="" CssClass="submit-status-NotSubmitted" />--%>
                <div class="form-group">
                    <label for="DepartmentDropDown" class="form-label">Department</label>
                    <asp:DropDownList ID="departmentChoices" CssClass="departmentChoices form-control text-input" runat="server" aria-label="Departments" AutoPostBack="True" OnSelectedIndexChanged="departmentChoices_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="">
                            Choose a Department...
                        </asp:ListItem>
                    </asp:DropDownList>
                    <div class="departmentError text-danger d-none" id="departmentError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please select a department</span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col">
                            <label for="selectedDate" class="form-label">Date</label>
                            <asp:TextBox ID="date" runat="server" TextMode="Date" CssClass="date form-control text-input" OnTextChanged="date_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                            <asp:HiddenField ID="SelectedDate" runat="server" />
                            <div class="dateError text-danger d-none" id="dateError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please select a Date</span>
                            </div>
                        </div>
                        <asp:HiddenField ID="deptID" runat="server" />
                        <div class="col">
                            <label for="time" class="form-label">Time</label>
                            <asp:DropDownList ID="time" runat="server" CssClass="time form-control text-input" Enabled="False">
                                <asp:ListItem Value="" Selected="True">Select Available Time</asp:ListItem>
                                <asp:ListItem Value="8:00 AM">8:00 AM</asp:ListItem>
                                <asp:ListItem Value="9:00 AM">9:00 AM</asp:ListItem>
                                <asp:ListItem Value="10:00 AM">10:00 AM</asp:ListItem>
                                <asp:ListItem Value="11:00 AM">11:00 AM</asp:ListItem>
                                <asp:ListItem Value="1:00 PM">1:00 PM</asp:ListItem>
                                <asp:ListItem Value="2:00 PM">2:00 PM</asp:ListItem>
                                <asp:ListItem Value="3:00 PM">3:00 PM</asp:ListItem>
                                <asp:ListItem Value="4:00 PM">4:00 PM</asp:ListItem>
                            </asp:DropDownList>
                            <div class="timeError text-danger d-none" id="timeError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please select a time</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="FullName" class="form-label">Full Name</label>
                   <asp:TextBox ID="FullName" runat="server" CssClass="FullName form-control text-input" placeholder="Full Name"></asp:TextBox>
<asp:RegularExpressionValidator ID="FullNameValidator" runat="server"
    ControlToValidate="FullName"
    ErrorMessage="Please provide a valid input (Ex. Juan Dela Cruz A.)"
    ValidationExpression="^[A-Za-z\s]+$"
    Display="Dynamic"
    CssClass="nameError text-danger"
/>
                </div>

                <div class="form-group">
                    <label for="Email" class="form-label">Email Address</label>
                 <asp:TextBox ID="Email" runat="server" CssClass="Email form-control text-input" placeholder="Email Address" type="email"></asp:TextBox>
<asp:RegularExpressionValidator ID="EmailValidator" runat="server"
    ControlToValidate="Email"
    ErrorMessage="Please enter a valid email"
    ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
    Display="Dynamic"
    CssClass="emailError text-danger"
/>

                </div>

                <div class="form-group">
                    <label for="ContactN" class="form-label">Contact Number</label>
                    <asp:TextBox ID="ContactN" runat="server" CssClass="ContactN form-control text-input" placeholder="Contact Number" type="tel"></asp:TextBox>
<asp:RegularExpressionValidator ID="ContactNValidator" runat="server"
    ControlToValidate="ContactN"
    ErrorMessage="Please provide a valid contact number"
    ValidationExpression="^\d*$"
    Display="Dynamic"
    CssClass="contactError text-danger"
/>
                </div>


                <div class="form-group">
                    <label for="Message" class="form-label">Concern</label>
                    <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="Message form-control text-input" placeholder="Your Concern"></asp:TextBox>
                    <div class="concernError text-danger d-none" id="concernError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please state your concern</span>
                    </div>
                </div>
                <br>
                <button type="button" class="btn btn-primary btn-submit" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                    Proceed
                </button>

            </div>
            <%--SEARCH NI--%>
            <style>
                .search-bar {
                    margin: 10px 0;
                }

                .input-group {
                    display: flex;
                    align-items: center;
                    width: 100%;
                }

                    .input-group input {
                        flex-grow: 1;
                    }

                .sky-blue-button {
                    background-color: skyblue;
                    margin-left: 10px;
                }

                .auto-style1 {
                    border-style: none;
                    border-color: inherit;
                    border-width: 0;
                    --bs-btn-close-color: #000;
                    --bs-btn-close-bg: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23000'%3e%3cpath d='M.293.293a1 1 0 0 1 1.414 0L8 6.586 14.293.293a1 1 0 1 1 1.414 1.414L9.414 8l6.293 6.293a1 1 0 0 1-1.414 1.414L8 9.414l-6.293 6.293a1 1 0 0 1-1.414-1.414L6.586 8 .293 1.707a1 1 0 0 1 0-1.414z'/%3e%3c/svg%3e");
                    --bs-btn-close-opacity: 0.5;
                    --bs-btn-close-hover-opacity: 0.75;
                    --bs-btn-close-focus-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
                    --bs-btn-close-focus-opacity: 1;
                    --bs-btn-close-disabled-opacity: 0.25;
                    --bs-btn-close-white-filter: invert(1) grayscale(100%) brightness(200%);
                    box-sizing: content-box;
                    width: 1em;
                    padding: 0.25em 0.25em;
                    color: var(--bs-btn-close-color);
                    border-radius: 0.375rem;
                    opacity: var(--bs-btn-close-opacity);
                }
            </style>

            <div class="col-md-6">
                <h2 class="form-heading">Search My Appointment</h2>
                <div class="form-group search-bar">
                    <div class="input-group">
                        <asp:TextBox ID="searchInput" runat="server" CssClass="form-control text-input" placeholder="Search by Appointment ID" />
                        <span class="input-group-btn">
                            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-search sky-blue-button" OnClick="SearchButton_Click" UseSubmitBehavior="false" />
                        </span>
                    </div>
                </div>
                <div class="search-results">
                    <asp:GridView ID="searchResultsGridView" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="ID_appointment" HeaderText="ID" />
                            <asp:BoundField DataField="full_name" HeaderText="Full Name" />
                            <asp:BoundField DataField="appointment_status" HeaderText="Appointment Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="reschedBtn" runat="server" Text="View" CssClass="btn btn-primary text-light"
                                        OnClick="reschedBtn_Click" OnClientClick='<%# "openModal(); return getAppointmentID(" + Eval("ID_appointment") + ");" %>'
                                        Visible='<%# Eval("appointment_status").ToString().Equals("reschedule") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table-header" />
                    </asp:GridView>
                    <asp:Label ID="noResultsLabel" runat="server" Text="No results found" Visible="false" CssClass="no-results-label"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <%--MODAL--%>
    <div class="modal fade" id="reschedModal" tabindex="-1" aria-labelledby="reschedModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Appointment Reschedule</h1>
                    <asp:Button ID="reschedCloseBtn" runat="server" CssClass="btn-close" OnClick="reschedCloseBtn_Click" />
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-12">
                                <div class="d-flex justify-content-center">
                                    <img class="img-placeholder" src="../../../Resources/Images/tempIcons/reschedule-icon-6.jpg" />
                                </div>
                                <div class="text-center mb-3">
                                    <p class="fs-5 fw-bold">Heads up!</p>
                                    <span>Hello,
                                        <asp:Label ID="AppointeeName" runat="server" Text="Label"></asp:Label></span>
                                    <p>Your appointment date has been changed, would you like to accept this new date?</p>
                                    <span class="mb-3">
                                        <asp:Label ID="ReschedDate" runat="server" Text="Date" CssClass="fw-bold"></asp:Label>
                                        <span>at </span>
                                        <asp:Label ID="ReschedTime" runat="server" Text="Time" CssClass="fw-bold"></asp:Label>
                                    </span>
                                    <p>
                                        Appointment ID:
                                        <asp:Label ID="AppointmentID" runat="server" Text="Label" CssClass="fw-bold"></asp:Label>
                                    </p>
                                </div>
                                <div class="d-flex justify-content-center ">
                                    <asp:LinkButton ID="acceptBtn" runat="server" CssClass="btn bg-success text-light reschedBtn acceptBtn" OnClick="acceptBtn_Click">Accept</asp:LinkButton>
                                    <asp:LinkButton ID="rejectBtn" runat="server" CssClass="btn bg-danger text-light reschedBtn" OnClick="rejectBtn_Click">Reject</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="rejectModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Button ID="rejectAppmntCls" runat="server" CssClass="btn-close" OnClick="rejectAppmntCls_Click" />
                </div>
                <div class="modal-body">
                    <p>Are you sure to reject this appoint?</p>
                    <span>Rejecting this appointment means your appointment ticket will be closed</span>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="cancel_Click" />
                    <asp:Button ID="rejectAppmntBtn" runat="server" Text="Proceed" CssClass="btn btn-primary text-light" OnClick="rejectAppmntBtn_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Appointment Request?</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Send Appointment Request?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Appointment" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary" UseSubmitBehavior="false" />
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
    <%--<asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
     <script>
         function getAppointmentID(id) {
             document.getElementById('<%= HiddenField1.ClientID %>').value = id;
         }
     </script>
    <script>
        function openModal() {
            $('#exampleModal').modal('show');
        }
    </script>
    <script>
        function checkFormFields() {
            var fullName = document.getElementById('<%= FullName.ClientID %>').value;
            var email = document.getElementById('<%= Email.ClientID %>').value;
            var contactNumber = document.getElementById('<%= ContactN.ClientID %>').value;
            var selectedTime = document.getElementById('<%= time.ClientID %>').value;
            var selectedDate = document.getElementById('<%= date.ClientID %>').value;
            var department = document.getElementById('<%= departmentChoices.ClientID %>').value;
            var message = document.getElementById('<%= Message.ClientID %>').value;

            // Check if any of the fields are empty
            if (fullName === '' || email === '' || contactNumber === '' || selectedTime === '' || selectedDate === '' || department === '' || message === '') {
                // At least one field is empty, disable the button
                document.getElementById("SubmitButton").disabled = true;
            } else {
                // All fields are filled, enable the button
                document.getElementById("SubmitButton").disabled = false;
            }
        }

        // Add event listeners to form fields to check them on input
        var inputFields = document.getElementsByClassName("text-input");
        for (var i = 0; i < inputFields.length; i++) {
            inputFields[i].addEventListener("input", checkFormFields);
        }
    </script>--%>
    <script>
        function checkField(fieldName, pattern) {
            const input = document.getElementById(fieldName);
            const isValid = pattern.test(input.value);

            if (!isValid) {
                input.classList.remove("valid");
                input.classList.add("invalid");
            } else {
                input.classList.remove("invalid");
                input.classList.add("valid");
            }
        }

        // Function to set the maximum date to 3 days from today
        function setMaxDate() {
            const today = new Date();
            today.setDate(today.getDate() + 3);

            const dd = String(today.getDate()).padStart(2, "0");
            const mm = String(today.getMonth() + 1).padStart(2, "0");
            const yyyy = today.getFullYear();

            const maxDate = yyyy + "-" + mm + "-" + dd;
            document.getElementById("date").setAttribute("max", maxDate);
        }

        // Add event listeners to input fields
        const fullNameInput = document.getElementById("<%= FullName.ClientID %>");
        const emailInput = document.getElementById("<%= Email.ClientID %>");
        const contactNumberInput = document.getElementById("<%= ContactN.ClientID %>");
        const timeInput = document.getElementById("<%= time.ClientID %>");
        const dateInput = document.getElementById("<%= date.ClientID %>")
        const departmentInput = document.getElementById("<%= departmentChoices.ClientID %>");
        const messageInput = document.getElementById("<%= Message.ClientID %>");

        fullNameInput.addEventListener("input", () => checkField("<%= FullName.ClientID %>", /^[A-Za-z\s]+$/));
        emailInput.addEventListener("input", () => checkField("<%= Email.ClientID %>", /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/));
        contactNumberInput.addEventListener("input", () => checkField("<%= ContactN.ClientID %>", /^\d*$/));
        timeInput.addEventListener("input", () => {
            timeInput.classList.remove("invalid");
            timeInput.classList.add("valid");
        });

        dateInput.addEventListener("input", () => {
            dateInput.classList.remove("invalid");
            dateInput.classList.add("valid");
        });

        departmentInput.addEventListener("change", () => {
            departmentInput.classList.remove("invalid");
            departmentInput.classList.add("valid");
        });


        messageInput.addEventListener("input", () => {
            messageInput.classList.remove("invalid");
            messageInput.classList.add("valid");
        });

    </script>


</asp:Content>

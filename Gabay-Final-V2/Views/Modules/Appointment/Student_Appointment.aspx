<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Student_Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Custom CSS for the form */
        .form-wrapper {
            border-radius: 10px;
            margin-top: 20px;
            box-shadow: 0 40px 40px rgba(0, 0, 0, 0.1);
            border-radius: 3px;
            overflow: hidden;
            background-color: #fff;
        }

        .form-container {
            padding: 20px;
            border-radius: 10px;
        }

        .form-heading {
            background-color: #00008B; /* Dark blue color */
            color: #fff;
            text-align: center;
            padding: 10px;
            margin-bottom: 20px;
            font-family: Tahoma, Arial, sans-serif; /* Change font family */
            font-size: 24px; /* Change font size */
        }

        .btn-submit {
            display: block;
            margin: 0 auto; /* Center the submit button */
        }

        /* Style for the Concern input field */
        #Message {
            max-height: 150px; /* Set a maximum height */
            overflow-y: auto; /* Add a scrollbar when necessary */
        }

        /* Custom CSS for the button */
        .custom-button {
            background-color: #007bff; /* Custom background color */
            color: #fff; /* Text color */
            padding: 10px 20px; /* Padding around the button text */
            border: none; /* Remove the default button border */
            border-radius: 5px; /* Add rounded corners */
            cursor: pointer; /* Show a hand cursor on hover */
            margin-top: 20px; /* Adjust top margin */
        }

            .custom-button:hover {
                background-color: #0056b3; /* Change background color on hover */
            }

        /* Custom styles for status labels */
        .status-not-submitted {
            color: red;
        }

        .status-submitted {
            color: green;
        }

        /* Additional styling for validation */
        .valid {
            border: 2px solid green;
        }

        .invalid {
            border: 2px solid red;
        }
    </style>
 <%--   <button type="button" class="custom-button" onclick="location.href='HistoryLogs.aspx'">View My History</button>--%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6 mx-auto form-wrapper">
                <div class="form-container">
                    <h2 class="form-heading">Appointment Form</h2>
                    <asp:Label ID="SubmissionStatusSubmitted" runat="server" Text="" CssClass="submission-status-submitted" />
                    <asp:Label ID="SubmitStatusNotSubmitted" runat="server" Text="" CssClass="submit-status-not-submitted" />
                    <div class="mb-3">
                        <label for="FullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="FullName" runat="server" CssClass="form-control text-input" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="Email" class="form-label">Email Address</label>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control text-input" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="ContactN" class="form-label">Contact Number</label>
                        <asp:TextBox ID="ContactN" runat="server" CssClass="form-control text-input" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <div class="row">
                            <div class="col">
                                <label for="IdNumber" class="form-label">ID Number</label>
                                <asp:TextBox ID="IdNumber" runat="server" CssClass="form-control text-input" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label for="Year" class="form-label">Year Level</label>
                                <asp:TextBox ID="Year" runat="server" CssClass="form-control text-input" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <div class="row">
                            <div class="col">
                                <label for="time" class="form-label">Time</label>
                                <asp:DropDownList ID="time" runat="server" CssClass="form-control text-input">
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
                            </div>
                            <div class="col">
                                <label for="selectedDate" class="form-label">Date</label>
                                <input type="date" id="date" runat="server" name="date" class="form-control text-input" />
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="DepartmentDropDown" class="form-label">Department</label>
                        <asp:TextBox ID="DepartmentDropDown" CssClass="form-control text-input" runat="server" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="Message" class="form-label">Concern</label>
                        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control text-input"></asp:TextBox>
                    </div>
                    <button type="button" class="btn btn-primary btn-submit" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                        Submit Appointment
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Send appointment request?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="SubmitButton" runat="server" Text="Proceed" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="selectedDateHidden" runat="server" ClientIDMode="Static" />
    <script>
        function checkFormFields() {
            var fullName = document.getElementById('<%= FullName.ClientID %>').value;
            var email = document.getElementById('<%= Email.ClientID %>').value;
            var contactNumber = document.getElementById('<%= ContactN.ClientID %>').value;
            var selectedTime = document.getElementById('<%= time.ClientID %>').value;
            var selectedDate = document.getElementById('<%= selectedDateHidden.ClientID %>').value;
            var department = document.getElementById('<%= DepartmentDropDown.ClientID %>').value;
            var idNumber = document.getElementById('<%= IdNumber.ClientID %>').value;
            var yearLevel = document.getElementById('<%= Year.ClientID %>').value;
            var message = document.getElementById('<%= Message.ClientID %>').value;

            // Check if any of the fields are empty or if the ID Number or Year Level are invalid
            if (fullName === '' || email === '' || contactNumber === '' || selectedTime === '' || selectedDate === '' || department === '' || idNumber === '' || yearLevel === '' || message === '') {
                // At least one field is empty or invalid, disable the button
                document.getElementById("SubmitButton").disabled = true;
            } else {
                // All fields are filled and valid, enable the button
                document.getElementById("SubmitButton").disabled = false;
            }
        }

        // Add event listeners to form fields to check them on input
        var inputFields = document.getElementsByClassName("text-input");
        for (var i = 0; i < inputFields.length; i++) {
            inputFields[i].addEventListener("input", checkFormFields);
        }
    </script>


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
            document.getElementById("<%= selectedDateHidden.ClientID %>").setAttribute("max", maxDate);
        }

        // Add event listeners to input fields
        const fullNameInput = document.getElementById("<%= FullName.ClientID %>");
        const emailInput = document.getElementById("<%= Email.ClientID %>");
        const contactNumberInput = document.getElementById("<%= ContactN.ClientID %>");
        const timeInput = document.getElementById("<%= time.ClientID %>");
        const dateInput = document.getElementById("<%= selectedDateHidden.ClientID %>");
        const departmentInput = document.getElementById("<%= DepartmentDropDown.ClientID %>");
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

        // Call setMaxDate on page load
        window.onload = setMaxDate;
    </script>
</asp:Content>

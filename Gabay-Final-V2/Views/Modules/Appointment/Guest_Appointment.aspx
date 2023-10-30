<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Guest_Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    </style>

    <div class="form-wrapper">
        <div class="row">
            <div class="col-md-6">
                <h2 class="form-heading">Appointment Form</h2>
                <asp:Label ID="SubmissionStatusSubmitted" runat="server" Text="" CssClass="submission-status-Submitted" />
                <asp:Label ID="SubmitStatusNotSubmitted" runat="server" Text="" CssClass="submit-status-NotSubmitted" />
                <div class="form-group">
                    <label for="DepartmentDropDown" class="form-label">Department</label>
                    <asp:DropDownList ID="departmentChoices" CssClass="form-control text-input" runat="server" aria-label="Departments" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="">
        Choose a Department...
                        </asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group">
                    <label for="FullName" class="form-label">Full Name</label>
                    <asp:TextBox ID="FullName" runat="server" CssClass="form-control text-input" placeholder="Full Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="Email" class="form-label">Email Address</label>
                    <asp:TextBox ID="Email" runat="server" CssClass="form-control text-input" placeholder="Email Address" type="email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="ContactN" class="form-label">Contact Number</label>
                    <asp:TextBox ID="ContactN" runat="server" CssClass="form-control text-input" placeholder="Contact Number" type="tel"></asp:TextBox>
                </div>
                <div class="form-group">
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
                <div class="form-group">
                    <label for="Message" class="form-label">Concern</label>
                    <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control text-input" placeholder="Your Concern"></asp:TextBox>
                </div>
                <br>
                <button type="button" class="btn btn-primary btn-submit" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                    Proceed
                </button>

            </div>
            <%--    SEARCH NI--%>
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
            </style>

            <div class="col-md-6">
                <h2 class="form-heading">Search My Appointment</h2>
                <div class="form-group search-bar">
                    <div class="input-group">
                        <asp:TextBox ID="searchInput" runat="server" CssClass="form-control text-input" placeholder="Search by Email Address" />
                        <span class="input-group-btn">
                            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-search sky-blue-button" OnClick="SearchButton_Click" />
                        </span>
                    </div>
                </div>
                <div class="search-results">
                    <asp:GridView ID="searchResultsGridView" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="full_name" HeaderText="Full Name" />
                            <asp:BoundField DataField="appointment_status" HeaderText="Appointment Status" />
                        </Columns>
                        <HeaderStyle CssClass="table-header" />
                    </asp:GridView>
                    <asp:Label ID="noResultsLabel" runat="server" Text="No results found" Visible="false" CssClass="no-results-label"></asp:Label>
                </div>
            </div>



        </div>
    </div>

    <%--MODAL--%>
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
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Appointment" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
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
        }); s

        // Call setMaxDate on page load
        window.onload = setMaxDate;
    </script>
</asp:Content>


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
            border-radius: 3px;
            overflow: hidden;
            background-color: #fff;
        }

        .form-container {
            padding: 20px;
            border-radius: 10px;
        }

        .form-heading {
            background-color: #00008B;
            color: #fff;
            text-align: center;
            padding: 10px;
            margin-bottom: 20px;
            font-family: Tahoma, Arial, sans-serif;
            font-size: 24px;
        }

        .btn-submit {
            display: block;
            margin: 0 auto;
        }

        .form-group {
            position: relative;
        }

        .form-control {
            border: 1px solid black;
        }

            .form-control.invalid {
                border-color: red; /* Red border for invalid input */
            }

            .form-control.valid {
                border-color: green; /* Green border for valid input */
            }
    </style>

    <div class="container">
        <div class="row">
            <!-- Search Filter Form -->
            <div class="col-md-3 form-wrapper">
                <div class="form-container">
                    <h2 class="form-heading">Search Appointment</h2>
                    <div class="form-group">
                        <input type="text" id="searchInput" class="form-control text-input" placeholder="Input your appointment id)" />
                        <br>
                        <button type="button" id="searchButton" class="btn btn-primary btn-submit">Search</button>

                    </div>
                </div>
            </div>
            <div class="col-md-6 mx-auto form-wrapper">
                <div class="form-container">
                    <h2 class="form-heading">Appointment Form</h2>
                    <div class="form-group">
                        <label for="FullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="FullName" runat="server" CssClass="form-control text-input"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="Email" class="form-label">Email Address</label>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control text-input" type="email"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ContactN" class="form-label">Contact Number</label>
                        <asp:TextBox ID="ContactN" runat="server" CssClass="form-control text-input" type="tel"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col">
                                <label for="time" class="form-label">Time</label>
                                <asp:TextBox ID="time" runat="server" TextMode="Time" CssClass="form-control text-input" />
                            </div>
                            <div class="col">
                                <label for="selectedDate" class="form-label">Date</label>
                                <input type="date" id="selectedDate" runat="server" name="date" class="form-control text-input" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="DepartmentDropDown" class="form-label">Department</label>
                        <asp:DropDownList ID="DepartmentDropDown" runat="server" class="form-control text-input" required>
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="College of Business Administration" Value="College_of_Business_Administration"></asp:ListItem>
                            <asp:ListItem Text="College of Accountancy" Value="College_of_Accountancy"></asp:ListItem>
                            <asp:ListItem Text="College of Computer Studies" Value="College_of_Computer_Studies"></asp:ListItem>
                            <asp:ListItem Text="College of Criminology" Value="College_of_Criminology"></asp:ListItem>
                            <asp:ListItem Text="College of Customs ADM" Value="College_of_Customs_ADM"></asp:ListItem>
                            <asp:ListItem Text="College of Hospitality and Tourism" Value="College_of_Hospitality_and_Tourism"></asp:ListItem>
                            <asp:ListItem Text="College of Teachers Education" Value="College_of_Teachers_Education"></asp:ListItem>
                            <asp:ListItem Text="College of Engineer" Value="College_of_Engineer"></asp:ListItem>
                            <asp:ListItem Text="College of Maritime Studies" Value="College_of_Maritime_Studies"></asp:ListItem>
                            <asp:ListItem Text="College of Nursing" Value="College_of_Nursing"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="Message" class="form-label">Concern</label>
                        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control text-input"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
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
            document.getElementById("selectedDate").setAttribute("max", maxDate);
        }

        // Add event listeners to input fields
        const fullNameInput = document.getElementById("<%= FullName.ClientID %>");
        const emailInput = document.getElementById("<%= Email.ClientID %>");
        const contactNumberInput = document.getElementById("<%= ContactN.ClientID %>");
        const timeInput = document.getElementById("<%= time.ClientID %>");
        const dateInput = document.getElementById("<%= selectedDate.ClientID %>")
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


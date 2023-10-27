<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Guest_Appointment" %>

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

            .form-group input:invalid {
                border: 2px solid red;
            }

                .form-group input:invalid + label:after {
                    content: "X";
                    position: absolute;
                    top: 0;
                    right: 0;
                    color: red;
                }

            .form-group input:valid {
                border: 2px solid green;
            }

                .form-group input:valid + label:after {
                    content: "✓";
                    position: absolute;
                    top: 0;
                    right: 0;
                    color: green;
                }
        /* Style for the Concern input field */
        #concern {
            max-height: 150px; /* Set a maximum height */
            overflow-y: auto; /* Add a scrollbar when necessary */
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto form-wrapper">
                <div class="form-container">
                    <h2 class="form-heading">Appointment Form</h2>
                    <div class="form-group">
                        <label for="FullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="FullName" runat="server" CssClass="form-control" ValidationExpression="^[A-Za-z]+$" oninput="return preventNumbers(event);" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="Email" class="form-label">Email Address</label>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control" type="email" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ContactN" class="form-label">Contact Number</label>
                        <asp:TextBox ID="ContactN" runat="server" CssClass="form-control" type="tel" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col">
                                <label for="time" class="form-label">Time</label>
                                <!-- Replace with your ASP.NET TextBox for Time -->
                                <asp:TextBox ID="time" runat="server" TextMode="Time" CssClass="form-control" />
                            </div>
                            <div class="col">
                                <label for="selectedDateHidden" class="form-label">Date</label>
                                <input type="date" id="selectedDateHidden" runat="server" name="date" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="DepartmentDropDown" class="form-label">Department</label>
                        <asp:DropDownList ID="DepartmentDropDown" runat="server" CssClass="form-control" required>
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
                        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control"></asp:TextBox>
                    </div>
                    <%-- <asp:Button ID="SubmitButton" runat="server" Text="SUBMIT" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary btn-submit" />--%>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            // Get a reference to the form
            const form = document.querySelector('.form-wrapper form');

            // Add an event listener for form submission
            form.addEventListener("submit", function (event) {
                // Validate each field
                if (!checkField("FullName", /^[A-Za-z]+$/)) {
                    event.preventDefault(); // Prevent form submission
                }
                if (!checkField("Email", /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/)) {
                    event.preventDefault(); // Prevent form submission
                }
                if (!checkField("ContactN", /^\d{10}$/)) {
                    event.preventDefault(); // Prevent form submission
                }
            });

            function checkField(fieldName, pattern) {
                const input = form.querySelector(`#${fieldName}`);
                const isValid = pattern.test(input.value);
                if (!isValid) {
                    input.setCustomValidity("Invalid");
                } else {
                    input.setCustomValidity("");
                }
                return isValid;
            }
        });
    </script>
</asp:Content>

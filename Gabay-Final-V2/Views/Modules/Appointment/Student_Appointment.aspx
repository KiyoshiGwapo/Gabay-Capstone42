<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Student_Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
        <link href="../../../Resources/CustomStyleSheet/Appointment/AppointmentStyle.css" rel="stylesheet" />
        <script src="../../../Scripts/sweetalert-dev.js"></script>
        <script src="../../../Scripts/sweetalert.min.js"></script>
        <section class="container">
            <h1>Appointment Form</h1><br><br>
            <div class="input-box">
                <label for="FirstName">First name:</label>
                <asp:TextBox ID="FirstName" runat="server" type="text" onkeypress="return validateLettersOnly(event)" ValidationExpression="^[A-Za-z]+$"></asp:TextBox>
            </div>
            <div class="input-box">
                <label for="LastName">Last name:</label>
                <asp:TextBox ID="LastName" runat="server" type="text" onkeypress="return validateLettersOnly(event)" ValidationExpression="^[A-Za-z]+$"></asp:TextBox>
                <asp:RegularExpressionValidator
                    ID="LastNameValidator" runat="server"         
                    ControlToValidate="LastName"                                                
                    ErrorMessage="Should only contain letters"
                    ValidationExpression="^[A-Za-z]+$"
                    ValidationGroup="FormValidation">
                </asp:RegularExpressionValidator>
            </div>
            <div class="input-box address">
                <label for="Email">Email address:</label>
                <asp:TextBox ID="Email" runat="server" type="email" class="required"></asp:TextBox>
            </div>
            <div class="column">
                <div class="input-box">
                    <label for="IDNumber">ID Number:</label>
                    <asp:TextBox ID="IdNumber" runat="server" type="number" class="required"></asp:TextBox>
                </div>
                <div class="form-field">
                    <label for="Year">Year:</label>
                    <asp:DropDownList ID="Year" runat="server" CssClass="form-control" type="number" required>
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="1st Year" Value="1st Year"></asp:ListItem>
                        <asp:ListItem Text="2nd Year" Value="2nd Year"></asp:ListItem>
                        <asp:ListItem Text="3rd Year" Value="3rd Year"></asp:ListItem>
                        <asp:ListItem Text="4th Year" Value="4th Year"></asp:ListItem>
                        <asp:ListItem Text="5th Year" Value="5th Year"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="column">
                <div class="input-box time">
                    <label for="Time">Time</label>
                    <asp:TextBox ID="time" runat="server" TextMode="Time"></asp:TextBox>
                </div>
                <div class="input-box">
                    <label for="Date">Date:</label>
                    <input type="date" id="selectedDateHidden" runat="server" name="date" />
                </div>
            </div>
            <div class="form-field">
                <label for="Department">Department:</label>
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
            <div class="input-box">
                <label for="message">Concern:</label>
                <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" class="required-field message-box"></asp:TextBox>
                <span class="required-field"></span>
            </div>

            <div class="button text-center">
                <asp:Button ID="SubmitButton" runat="server" Text="SUBMIT" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="dark-blue-button" />
            </div>

            <asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
        </section>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>
        <script>
            $(document).ready(function () {
                $('#calendar').fullCalendar({
                    header: {
                        right: 'next',
                        left: 'prev',
                        center: 'title, ,today',
                    },
                    selectable: true,
                    editable: true,
                    select: function (start, end, jsEvent, view) {
                        console.log('Selected Start Date:', start.format());
                        console.log('Selected End Date:', end.format());
                        $('#selectedDateHidden').val(start.format('YYYY-MM-DD'));
                    }
                });
            });
            function validateLettersOnly(event) {
                var key = event.keyCode || event.which;
                var keyChar = String.fromCharCode(key);
                var regex = /^[A-Za-z]+$/;
                if (!regex.test(keyChar)) {
                    event.preventDefault();
                    return false;
                }
            }
            function showBranchOptions(element) {
                var branchOptionsContainer = document.getElementById('branchOptionsContainer');
                if (element.selectedIndex === 3) {
                    branchOptionsContainer.style.display = 'block';
                } else {
                    branchOptionsContainer.style.display = 'none';
                }
            }
        </script>
        <script>
            function showSuccessMessage() {
                swal({
                    title: '',
                    text: 'Appointment Successfully Submitted!',
                    icon: 'success',
                    buttons: false,
                    timer: 3000 // 3 seconds
                });
            }
        </script>
            </div>
</asp:Content>

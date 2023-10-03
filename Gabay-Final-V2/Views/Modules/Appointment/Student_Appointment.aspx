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
            <div class="mb-3">
              <label for="FullName" class="form-label">Full Name</label>
              <asp:TextBox ID="FullName" runat="server" CssClass="form-control" ValidationExpression="^[A-Za-z]+$" oninput="return preventNumbers(event);"></asp:TextBox>
            </div>
          <div class="mb-3">
            <label for="Email" class="form-label">Email Address</label>
            <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
        <div class="mb-3">
          <label for="ContactN" class="form-label">Contact Number</label>
          <asp:TextBox ID="ContactN" runat="server" CssClass="form-control" type="number" oninput="this.value = this.value.replace(/[^0-9]/g, '');"></asp:TextBox>
        </div>
          <div class="mb-3">
            <div class="row">
              <div class="col">
                <label for="IdNumber" class="form-label">ID Number</label>
                <asp:TextBox ID="IdNumber" runat="server" CssClass="form-control" type="number" oninput="this.value = this.value.replace(/[^0-9]/g, '');" ></asp:TextBox>
              </div>
              <div class="col">
                <label for="Year" class="form-label">Year Level</label>
                <asp:DropDownList ID="Year" runat="server" CssClass="form-control" required>
                  <asp:ListItem Text="" Value=""></asp:ListItem>
                  <asp:ListItem Text="1st Year" Value="1st Year"></asp:ListItem>
                  <asp:ListItem Text="2nd Year" Value="2nd Year"></asp:ListItem>
                  <asp:ListItem Text="3rd Year" Value="3rd Year"></asp:ListItem>
                  <asp:ListItem Text="4th Year" Value="4th Year"></asp:ListItem>
                  <asp:ListItem Text="5th Year" Value="5th Year"></asp:ListItem>
                </asp:DropDownList>
              </div>
            </div>
          </div>
          <div class="mb-3">
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
          <div class="mb-3">
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
          <div class="mb-3">
            <label for="Message" class="form-label">Concern</label>
            <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control"></asp:TextBox>
          </div>
          <asp:Button ID="SubmitButton" runat="server" Text="SUBMIT" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary btn-submit" />
        </div>
      </div>
    </div>
  </div>
  <asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />
    <script>
        function preventNumbers(event) {
            const input = event.target;
            const value = input.value;

            // Remove any numeric characters from the input value
            const newValue = value.replace(/\d/g, '');

            // Update the input value with the filtered value
            input.value = newValue;

            return true; // Allow the updated value
        }

    </script>
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            const form = document.querySelector('.form1');

            const nameInput = form.querySelector('.name');
            const errorName = form.querySelector('.nameError');

            const addressInput = form.querySelector('.address');
            const errorlAddress = form.querySelector('.addressError');

            const contactInput = form.querySelector('.contact');
            const errorContact = form.querySelector('.contactError');

            const DOBInput = form.querySelector('.DOB');
            const errorDOB = form.querySelector('.DOBError');

            const passInput = form.querySelector('.password');
            const errorPass = form.querySelector('.passError');

            const cpassInput = form.querySelector('.cpassword');
            const errorcPass = form.querySelector('.cpassError');

            const emailInput = form.querySelector('.email');
            const errorEmail = form.querySelector('.emailError');

            const idNumberInput = form.querySelector('.idNumber');
            const errorIdNum = form.querySelector('.idNumError');

            const deptInput = form.querySelector('.department');
            const errorDept = form.querySelector('.departmentError');

            const yearInput = form.querySelector('.courseYear');
            const errorYear = form.querySelector('.courseYearError');

            // Define your validation functions here (e.g., checkName, checkAddress, etc.)

            function checkForm() {
                let isValid = true;

                if (!checkName()) {
                    isValid = false;
                }

                if (!checkAddress()) {
                    isValid = false;
                }

                if (!checkContact()) {
                    isValid = false;
                }

                if (!checkDOB()) {
                    isValid = false;
                }

                if (!checkPassword()) {
                    isValid = false;
                }

                if (!checkCpasword()) {
                    isValid = false;
                }

                if (!checkEmail()) {
                    isValid = false;
                }

                if (!checkIdnumber()) {
                    isValid = false;
                }

                if (!checkDepartment()) {
                    isValid = false;
                }

                if (!checkCourseYear()) {
                    isValid = false;
                }

                if (!isValid) {
                    // Prevent form submission if validation fails
                    event.preventDefault();
                }
            }

            form.addEventListener("submit", checkForm);

            // Add event listeners for input fields (e.g., keyup, change) to trigger validation functions
            nameInput.addEventListener('keyup', checkName);
            addressInput.addEventListener('keyup', checkAddress);
            contactInput.addEventListener('keyup', checkContact);
            DOBInput.addEventListener('change', checkDOB);
            passInput.addEventListener('keyup', checkPassword);
            cpassInput.addEventListener('keyup', checkCpasword);
            emailInput.addEventListener('keyup', checkEmail);
            idNumberInput.addEventListener('keyup', checkIdnumber);
            deptInput.addEventListener('change', checkDepartment);
            yearInput.addEventListener('change', checkCourseYear);
        });
    </script>
</asp:Content>

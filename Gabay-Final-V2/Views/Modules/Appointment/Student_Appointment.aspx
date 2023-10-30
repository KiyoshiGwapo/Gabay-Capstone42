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
    </style>
    <button type="button" class="custom-button" onclick="location.href='HistoryLogs.aspx'">View My History</button>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6 mx-auto form-wrapper">
                <div class="form-container">
                    <h2 class="form-heading">Appointment Form</h2>
                    <div class="mb-3">
                        <label for="FullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="FullName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="Email" class="form-label">Email Address</label>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="ContactN" class="form-label">Contact Number</label>
                        <asp:TextBox ID="ContactN" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <div class="row">
                            <div class="col">
                                <label for="IdNumber" class="form-label">ID Number</label>
                                <asp:TextBox ID="IdNumber" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label for="Year" class="form-label">Year Level</label>
                                <asp:TextBox ID="Year" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <%--<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Views/Modules/Appointment/Appointment_Status.aspx">HyperLink</asp:HyperLink>--%>
                    <div class="mb-3">
                        <div class="row">
                            <div class="col">
                                <label for="time" class="form-label">Time</label>
                                <!-- Replace with your ASP.NET TextBox for Time -->
                                <%--<asp:TextBox ID="time" runat="server" TextMode="Time" CssClass="form-control" />--%>
                                <asp:DropDownList ID="time" runat="server" CssClass="form-select">
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
                            </div>
                            <div class="col">
                                <label for="selectedDateHidden" class="form-label">Date</label>
                                <%--<input type="date" id="selectedDateHidden" runat="server" name="date" class="form-control" />--%>
                                <asp:TextBox ID="selectedDateHidden" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="DepartmentDropDown" class="form-label">Department</label>
                        <asp:TextBox ID="DepartmentDropDown" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="Message" class="form-label">Concern</label>
                        <asp:TextBox ID="Message" runat="server" TextMode="MultiLine" Rows="6" Columns="30" CssClass="form-control"></asp:TextBox>
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
                    Send appointment request?s
			
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="SubmitButton" runat="server" Text="Proceed" OnClick="SubmitButton_Click" ValidationGroup="FormValidation" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
    <%--<asp:HiddenField ID="FormSubmittedHiddenField" runat="server" Value="false" />--%>
    <%--<script>
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
	</script>--%>
</asp:Content>

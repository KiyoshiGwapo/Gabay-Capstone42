<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Appointment.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Manage_Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        /* Add custom CSS styles for the table */
        .unique-table {
            width: 100%;
        }

            .unique-table th, .unique-table td {
                padding: 8px;
                text-align: center;
                background-color: white;
                font-size: 14px;
            }

            .unique-table th {
                font-weight: bold;
                background-color: #428bca; /* Blue header background color */
                color: white; /* White text color */
            }

            /* Add some hover effect on rows to make it more interactive */
            .unique-table tbody tr:hover {
                background-color: #e0e0e0; /* Lighter gray on hover */
            }

        /* Media query for responsive design */
        @media (max-width: 768px) {
            .unique-table th, .unique-table td {
                padding: 6px; /* Adjust padding for smaller screens */
                font-size: 14px; /* Adjust font size for smaller screens */
            }
        }
        /* katung status nga dropdown button*/
        .custom-dropdown {
            width: 150px; /* Adjust the width as needed */
            padding: 5px; /* Add padding for better appearance */
            border: 1px solid #ccc; /* Add a border for styling */
            border-radius: 5px; /* Add rounded corners */
            background-color: #fff; /* Set the background color */
            color: #333; /* Set the text color */
            font-size: 14px; /* Set the font size */
            text-align: center;
        }
    </style>
    <div class="table-responsive-sm">
        <table class="table unique-table">
            <thead class="c-table__header">
                <tr style="background-color: #E3FDFD;">
                    <th class="c-table__col-label"><i class="fa fa-id-card"></i>ID Number</th>
                    <th class="c-table__col-label"><i class="fa fa-user"></i>Students Name</th>
                    <th class="c-table__col-label"><i class="fa fa-calendar-alt"></i>Year</th>
                    <th class="c-table__col-label"><i class="fa fa-building"></i>Department</th>
                    <th class="c-table__col-label"><i class="fa fa-envelope"></i>Email</th>
                    <th class="c-table__col-label"><i class="fa fa-id-card"></i>Contact Number</th>
                    <th class="c-table__col-label"><i class="fa fa-envelope-open"></i>Message</th>
                    <th class="c-table__col-label"><i class="fa fa-calendar-check"></i>Appointment Date</th>
                    <th class="c-table__col-label"><i class="fa fa-clock"></i>Time</th>
                    <th class="c-table__col-label">
                        <i class="fa fa-info-circle"></i>Status
                            <br />
                        <asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" CssClass="custom-dropdown">
                            <asp:ListItem Text="Select Down Below :" Value="" />
                            <asp:ListItem Text="PENDING" Value="Pending" style="color: black;" />
                            <asp:ListItem Text="SERVE" Value="SERVE" style="color: orange;" />
                            <asp:ListItem Text="APPROVED" Value="APPROVED" style="color: green;" />
                            <asp:ListItem Text="DENIED" Value="DENIED" style="color: red;" />
                            <asp:ListItem Text="RESCHEDULED" Value="RESCHEDULED" style="color: blue;" />                      
                        </asp:DropDownList>
                    </th>

                    <th class="c-table__col-label"><i class="fa fa-cog"></i>Actions</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="yourTablePlaceholder" runat="server"></asp:PlaceHolder>
            </tbody>
        </table>
    </div>
    <!-- Add the following code inside the <body> tag, below the existing table -->
    <!-- "View" Modal -->
    <div class="modal fade" id="viewModal" tabindex="-1" role="dialog" aria-labelledby="viewModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="viewModalLabel">View Appointment Message</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p><strong>Message:</strong></p>
                    <p id="appointmentMessage"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Email Modal -->
    <div class="modal fade" id="emailModal" tabindex="-1" aria-labelledby="emailModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="toEmail" class="form-label">To</label>
                        <input type="text" id="toEmailTextBox" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="date">Date</label>
                        <input type="date" id="messagedate" runat="server" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="time">Time</label>
                        <input type="time" id="messagetime" runat="server" class="form-control" />

                    </div>
                    <div class="mb-3">
                        <label for="message" class="form-label">Message</label>
                        <textarea id="messageTextArea" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="ReplyButton" runat="server" Text="Reply" OnClick="SendEmailButton_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


    <input type="hidden" id="appointmentIdHiddenField" runat="server" />
    <!-- "Update" Modal -->
    <div class="modal fade" id="updateModal" tabindex="-1" role="dialog" aria-labelledby="updateModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="updateModalLabel">Update Appointment</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close" onclick="reloadPage()">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="updateDate">Date:</label>
                        <input type="date" class="form-control" id="updateDate" name="updateDate" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="updateTime">Time:</label>
                        <input type="time" class="form-control" id="updateTime" name="updateTime" runat="server" />
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="reloadPage()">Close</button>
                    <asp:Button ID="updateModalButton" runat="server" Text="Update" CssClass="btn btn-primary" OnClientClick="return UpdateAppointment();" OnClick="UpdateModalButton_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function EditButton_Click(appointmentId, selectedDate, selectedTime) {
            // Assuming you have elements with IDs for date and time inputs and a hidden field
            document.getElementById('updateDate').value = selectedDate;
            document.getElementById('updateTime').value = selectedTime;
            document.getElementById('appointmentIdHiddenField').value = appointmentId;

            // Assuming you have a modal for editing, e.g., 'updateModal'
            var updateModal = new bootstrap.Modal(document.getElementById('updateModal'));
            updateModal.show();
        }

    </script>
    <script>
        var emailModal = new bootstrap.Modal(document.getElementById('emailModal'));

        // Event listener for opening the "Email" modal and populating the "To" email input
        document.querySelectorAll('[data-bs-toggle="modal"][data-bs-target="#emailModal"]').forEach(function (button) {
            button.addEventListener('click', function () {
                var toEmail = button.getAttribute('data-to');
                document.getElementById('toEmailTextBox').value = toEmail;

                emailModal.show();
            });
        });

        // Use event delegation to handle the click event for all buttons with data-bs-target="#updateModal"
        document.addEventListener('click', function (event) {
            var target = event.target;
            if (target.dataset.bsTarget === "#updateModal") {
                event.preventDefault();
                event.stopPropagation();
                var appointmentId = target.getAttribute('data-id');
                var selectedDate = target.getAttribute('data-date');
                var selectedTime = target.getAttribute('data-time');
                populateUpdateModal(appointmentId, selectedDate, selectedTime);
            }
        }); s

        function populateUpdateModal(appointmentId, selectedDate, selectedTime) {
            document.getElementById('updateDate').value = selectedDate;
            document.getElementById('updateTime').value = selectedTime;
            document.getElementById('<%= appointmentIdHiddenField.ClientID %>').value = appointmentId;

            var updateModal = new bootstrap.Modal(document.getElementById('updateModal'));
            updateModal.show();
        }

        function deleteAppointment(appointmentId) {
            if (confirm('Are you sure you want to delete this appointment?')) {
                window.location.href = 'Manage_Appointment.aspx?deleteId=' + appointmentId;
            }
        }

        function changeColor(event, color) {
            event.preventDefault();
            event.target.style.color = color;
        }
        function SendEmailButton_Click() {
            document.getElementById('<%= ReplyButton.ClientID %>').click();
        }
        function goBack() {
            history.back();
        }
        function viewAppointmentMessage(message) {
            var viewModal = new bootstrap.Modal(document.getElementById('viewModal'));
            document.getElementById('appointmentMessage').innerText = message;
            viewModal.show();
        }
        function reloadPage() {
            location.reload();
        }

    </script>
</asp:Content>


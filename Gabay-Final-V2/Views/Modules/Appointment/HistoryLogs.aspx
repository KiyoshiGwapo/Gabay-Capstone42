<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryLogs.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Announcement.HistoryLogs" %>

<!DOCTYPE html>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.7.2/dist/css/bootstrap.min.css">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.7.2/dist/js/bootstrap.bundle.min.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>lol</title>
</head>
<body>
    <form id="form1" runat="server">
        <style>
            /* Add the following styles to your CSS file or within a <style> block */
            .myGridView {
                width: 100%; /* Set the width of the GridView */
                border: 1px solid #ccc; /* Add a border around the GridView */
            }

                .myGridView th {
                    background-color: #007bff; /* Header background color */
                    color: #fff; /* Header text color */
                    text-align: center; /* Header text alignment */
                    padding: 10px; /* Header cell padding */
                }

                .myGridView tr:nth-child(even) {
                    background-color: #f2f2f2; /* Alternate row background color */
                }

                .myGridView tr:nth-child(odd) {
                    background-color: #fff; /* Alternate row background color */
                }

                .myGridView td {
                    text-align: left; /* Content text alignment */
                    padding: 8px; /* Content cell padding */
                }
        </style>
        <div class="table-responsive">
            <h1>lol</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:BoundField DataField="ID_appointment" HeaderText="Appointment ID" />
                    <asp:BoundField DataField="deptName" HeaderText="Department" />
                    <asp:BoundField DataField="full_name" HeaderText="Full Name" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="student_ID" HeaderText="Student ID" />
                    <asp:BoundField DataField="course_year" HeaderText="Course Year" />
                    <asp:BoundField DataField="contactNumber" HeaderText="Contact Number" />
                    <asp:BoundField DataField="appointment_date" HeaderText="Date" />
                    <asp:BoundField DataField="appointment_time" HeaderText="Time" />
                    <asp:BoundField DataField="concern" HeaderText="Concern" />
                    <asp:BoundField DataField="appointment_status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="AppointmentHistory.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.AppointmentHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container {
            max-width: 100%;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
        }

        h1 {
            color: #007bff;
            text-align: center;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            padding: 12px 15px;
            text-align: left;
            border-bottom: 1px solid #ddd;
            text-align: center;
        }

        th {
            background-color: #007bff;
            color: #fff;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr:nth-child(odd) {
            background-color: #fff;
        }

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

        .custom-button {
            background-color: darkblue;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 20px;
            float: right;
        }

            .custom-button:hover {
                background-color: #007bff;
            }
    </style>
    <asp:Button ID="GoBackButton" runat="server" Text="Go Back" CssClass="custom-button" OnClick="GoBackButton_Click" />
    <div class="container">
        <h1>Latest Appointment</h1>
        <asp:GridView ID="GridViewLatest" runat="server" AutoGenerateColumns="false" CssClass="myGridView">
            <Columns>
                <asp:BoundField DataField="ID_appointment" HeaderText="Appointment ID" />
                <asp:BoundField DataField="deptName" HeaderText="Department" />
                <%--<asp:BoundField DataField="full_name" HeaderText="Full Name" />--%>
                <%--<asp:BoundField DataField="email" HeaderText="Email" />--%>
                <%--<asp:BoundField DataField="student_ID" HeaderText="Student ID" />--%>
                <%--<asp:BoundField DataField="course_year" HeaderText="Course Year" />--%>
                <%--<asp:BoundField DataField="contactNumber" HeaderText="Contact Number" />--%>
                <%--<asp:BoundField DataField="appointment_date" HeaderText="Date" />--%>
                <asp:TemplateField HeaderText="View Concern">
                    <ItemTemplate>
                        <div class="d-flex justify-content-center">
                            <asp:Button runat="server" CssClass="btn btn-primary btn-sm view-concern-button" data-toggle="modal" data-target="#concernModal" OnClientClick='<%# "getAppointmentID(" + Eval("ID_appointment") + "); displayConcern(\"" + Eval("concern") + "\"); return false;" %>' Text="View" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="appointment_status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
        <h1>Appointment History</h1>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="myGridView">
            <Columns>
                <asp:BoundField DataField="ID_appointment" HeaderText="Appointment ID" />
                <asp:BoundField DataField="deptName" HeaderText="Department" />
                <%--<asp:BoundField DataField="full_name" HeaderText="Full Name" />--%>
                <%--<asp:BoundField DataField="email" HeaderText="Email" />--%>
                <%--<asp:BoundField DataField="student_ID" HeaderText="Student ID" />--%>
                <%--<asp:BoundField DataField="course_year" HeaderText="Course Year" />--%>
                <%--<asp:BoundField DataField="contactNumber" HeaderText="Contact Number" />--%>
                <%--<asp:BoundField DataField="appointment_date" HeaderText="Date" />--%>
                <asp:TemplateField HeaderText="View Concern">
                    <ItemTemplate>
                        <div class="d-flex justify-content-center">
                            <asp:Button runat="server" CssClass="btn btn-primary btn-sm view-concern-button" data-toggle="modal" data-target="#concernModal" OnClientClick='<%# "getAppointmentID(" + Eval("ID_appointment") + "); displayConcern(\"" + Eval("concern") + "\"); return false;" %>' Text="View" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="appointment_status" HeaderText="Status" />
                <asp:BoundField DataField="StatusChangeDate" HeaderText="Date of History" />
                <asp:BoundField DataField="NewStatus" HeaderText="Previous Status" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:HiddenField ID="HiddenFieldAppointment" runat="server" />

    <div class="modal fade" id="concernModal" tabindex="-1" aria-labelledby="concernModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="concernModalLabel">Appointment Concern</h5>
                </div>
                <div class="modal-body">
                    <div id="concernContent"></div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="CloseViewModal" runat="server" CssClass="btn-close" OnClick="CloseViewModal_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function displayConcern(concern) {
            document.getElementById("concernContent").innerHTML = concern;
            return false; // Prevent postback
        }
        function getAppointmentID(id) {
            document.getElementById('<%= HiddenFieldAppointment.ClientID %>').value = id;
        }
    </script>
</asp:Content>

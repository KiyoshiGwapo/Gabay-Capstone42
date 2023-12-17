<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="AppointmentHistory.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.AppointmentHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Appointment History</h1>
     <style>
        .container {
            text-align: center;
        }

        h1 {
            color: #007bff;
            text-align: center;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            text-align: center;
        }

        th, td {
            padding: 12px 15px;
            text-align: center;
            border-bottom: 1px solid #ddd;
            text-align: center;
        }

        th {
            background-color: #007bff;
            color: #fff;
            text-align: center;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
            text-align: center;
        }

        tr:nth-child(odd) {
            background-color: #fff;
            text-align: center;
        }

        .myGridView {
            width: 100%; /* Set the width of the GridView */
            border: 1px solid #ccc; /* Add a border around the GridView */
            text-align: center;
        }

            .myGridView th {
                background-color: #007bff; /* Header background color */
                color: #fff; /* Header text color */
                text-align: center; /* Header text alignment */
                padding: 10px; /* Header cell padding */
                text-align: center;
            }

            .myGridView tr:nth-child(even) {
                background-color: #f2f2f2; /* Alternate row background color */
                text-align: center;
            }

            .myGridView tr:nth-child(odd) {
                background-color: #fff; /* Alternate row background color */
                text-align: center;
            }

            .myGridView td {
                text-align: left; /* Content text alignment */
                padding: 8px; /* Content cell padding */
                text-align: center;
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
            text-align: center;
        }

            .custom-button:hover {
                background-color: #007bff;
            }

        .my-custom-width {
            width: 200px;
            margin: 0 auto;
            text-align: center;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-15 col-md-12">
               <%-- <asp:Button ID="GoBackButton" runat="server" Text="Go Back" CssClass="custom-button" OnClick="GoBackButton_Click" />--%>
                <asp:HyperLink ID="GoBackButton" runat="server" CssClass="custom-button" NavigateUrl="~/Views/Modules/Appointment/Student_Appointment.aspx">Go Back</asp:HyperLink>
            </div>
            <div class="col-12">
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

            </div>
        </div>

        <div class="row">
            <%--   add drop down here--%>
              <div class="mt-2"></div>
            <div class="col-lg-15 col-md-12">
                <asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" CssClass="form-select my-custom-width" Style="border-width: 3px; border-color: #007bff;">
                    <asp:ListItem Text="All Status" Value="" />
                    <asp:ListItem Text="Pending" Value="Pending" />
                    <asp:ListItem Text="Served" Value="Served" style="color: orange;" />
                    <asp:ListItem Text="Approved" Value="Approved" style="color: green;" />
                    <asp:ListItem Text="Reschedule" Value="Reschedule" style="color: blue;" />
                    <asp:ListItem Text="Rejected" Value="Rejected" style="color: red;" />
                </asp:DropDownList>
            </div>

            <div class="col-12">
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
        </div>
    </div>
    <asp:HiddenField ID="HiddenFieldAppointment" runat="server" />

    <div class="modal fade" id="concernModal" tabindex="-1" aria-labelledby="concernModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-center">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="concernModalLabel">Appointment Concern</h5>
                    <asp:Button ID="CloseViewModal" runat="server" CssClass="btn-close" OnClick="CloseViewModal_Click" />
                </div>
                <div class="modal-body">
                    <div id="concernContent"></div>
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

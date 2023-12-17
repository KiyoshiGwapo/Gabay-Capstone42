<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_deptInfo.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Department_Info.Guest_deptInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        /* Custom styles for accordion headers */
        .custom-accordion .accordion-button {
            background-color: white !important;
            color: #000000 !important;
            z-index: 1; /* Add z-index property */
        }

            .custom-accordion .accordion-button:hover {
                background-color: #003366 !important;
                color: white !important;
            }

        /* Custom styles for accordion bodies */
        .custom-accordion .accordion-body {
            background-color: #f5f5f5;
            border: 1px solid #ddd;
            z-index: 1; /* Add z-index property */
        }

        /* Custom styles for accordion active headers */
        .custom-accordion .accordion-button:not(.collapsed) {
            background-color: #0056b3;
        }

        .accordion-body li {
            list-style: none;
        }

        .accordion-item {
            z-index: 1; /* Add z-index property to sdsdensure it's in front */
        }
    </style>

     <script>
        function openModal(departmentID) {
            $('#departmentModal' + departmentID).modal('show');
        }
     </script>



   <div class="container">
        <div class="row">
            <div class="col-12">
                <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Department Information</h1>
                <div class="accordion custom-accordion" id="departmentAccordion">
                    <asp:Repeater ID="departmentRepeater" runat="server">
                        <ItemTemplate>
                            <div class="accordion-item item-hover">
                                <h2 class="accordion-header" id="heading<%# Eval("DepartmentID") %>">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse<%# Eval("DepartmentID") %>" aria-expanded="true" aria-controls="collapse<%# Eval("DepartmentID") %>">
                                        <div class="fs-5 fw-bold">
                                            <%# Eval("DepartmentName") %>
                                        </div>
                                    </button>
                                </h2>
                                <div id="collapse<%# Eval("DepartmentID") %>" class="accordion-collapse collapse" aria-labelledby="heading<%# Eval("DepartmentID") %>" data-bs-parent="#departmentAccordion">
                                    <div class="accordion-body">
                                        <!-- Accordion content as before -->

                                        <!-- Button to open modal -->
                                        <button type="button" class="btn btn-primary" onclick="openModal('<%# Eval("DepartmentID") %>')">View Details</button>

                                        <!-- Modal -->
                                        <div class="modal fade custom-modal" id="departmentModal<%# Eval("DepartmentID") %>" tabindex="-1" role="dialog" aria-labelledby="departmentModalLabel<%# Eval("DepartmentID") %>" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="departmentModalLabel<%# Eval("DepartmentID") %>"><%# Eval("DepartmentName") %></h5>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <!-- Your detailed modal content here -->
                                                        <ul>
                                                            <li>
                                                                <div class="d-flex justify-content-center align-items-center">
                                                                    <span>
                                                                        <%# Eval("DepartmentDescription") %>
                                                                    </span>
                                                                </div>
                                                            </li>
                                                            <hr />
                                                            <li>
                                                                <div class="row">
                                                                    <div class="col-4">
                                                                        <span class="fs-5 fw-bold">Department Information</span>
                                                                    </div>
                                                                    <div class="col-8">
                                                                        <div class="deptHeadCont mb-1">
                                                                            <div>
                                                                                <span class="fs-5">Dean/Department Head:</span>
                                                                            </div>
                                                                            <%# Eval("DepartmentHead") %>
                                                                        </div>
                                                                        <div class="deptCourses mb-1">
                                                                            <div>
                                                                                <span class="fs-5">Degree programs:</span>
                                                                            </div>
                                                                            <%# Eval("DepartmentCourses") %>
                                                                        </div>
                                                                        <div class="DeptOffHrs mb-1">
                                                                            <div>
                                                                                <span class="fs-5">Office hours:</span>
                                                                            </div>
                                                                            <%# Eval("DepartmentOffHours") %>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <hr />
                                                            <li>
                                                                <div class="row">
                                                                    <div class="col-4">
                                                                        <span class="fs-5 fw-bold">Contact Information</span>
                                                                    </div>
                                                                    <div class="col-8">
                                                                        <div class="deptConNum mb-1">
                                                                            <div>
                                                                                <span class="fs-5">Contact Number:</span>
                                                                            </div>
                                                                            <%# Eval("DepartmentContactNumber") %>
                                                                        </div>
                                                                        <div class="deptEmail mb-1">
                                                                            <div>
                                                                                <span class="fs-5">Email:</span>
                                                                            </div>
                                                                            <%# Eval("DepartmentEmail") %>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

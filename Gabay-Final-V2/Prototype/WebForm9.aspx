<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <style>
        /* Custom styles for accordion headers */
        .custom-accordion .accordion-button {
            background-color: white !important;
            color: #000000 !important;
        }
        .custom-accordion .accordion-button:hover {
            background-color: #003366 !important;
            color: white !important;
        }

        /* Custom styles for accordion bodies */
        .custom-accordion .accordion-body {
            background-color: #f5f5f5;
            border: 1px solid #ddd;
        }

        /* Custom styles for accordion active headers */
        .custom-accordion .accordion-button:not(.collapsed) {
            background-color: #0056b3;
        }

        .accordion-body li{
            list-style:none;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12">
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
                                                                    <span class="fs-5">Dean/Department Head:</span>
                                                                </div>
                                                                <%# Eval("DepartmentContactNumber") %>
                                                            </div>
                                                            <div class="deptEmail mb-1">
                                                                <div>
                                                                    <span class="fs-5">Dean/Department Head:</span>
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
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

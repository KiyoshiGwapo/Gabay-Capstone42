<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Department.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_Department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Add Department</h1>
    <style>
        .addDept{
            padding: 5px 90px;
        }
        .addDeptBtn{
            width:600px;
            height:50px;
        }
    </style>
    <div class="container addDept">
        <div class="row gx-2">
            <%-- department login id inputs and errors --%>
            <div class="col-md-6 col-sm-12">
                <div class="loginID-fields mt-2" id="loginID-field">
                    <div class="loginID-input">
                        <asp:TextBox ID="loginID" CssClass="loginID form-control form-control-lg" runat="server" placeholder="Login ID"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="loginID">Department Login ID</label>--%>
                    </div>
                    <div class="loginIDError text-danger d-none" id="loginIDError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please enter a Valid Login ID for Department</span>
                    </div>
                </div>
            </div>
            <%-- department password inputs and errors --%>
            <div class="col-md-6 col-sm-12">
                <div class="loginPass-fields mt-2" id="loginPass-field">
                    <div class="loginPass-input">
                        <asp:TextBox ID="loginPass" CssClass="loginPass form-control form-control-lg" runat="server" placeholder="Login Password"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="loginPass">Department Login Password</label>--%>
                    </div>
                    <div class="passError text-danger d-none" id="passError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Password should contain 8 character long and atleast 1 numeric and 1  capital letter </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row gx-2">
            <%-- department name inputs and errors --%>
            <div class="col-12">
                <div class="DeptName-fields mt-2" id="DeptName-field">
                    <div class="DeptName-input">
                        <asp:TextBox ID="DeptName" CssClass="DeptName form-control form-control-lg" runat="server" placeholder="Department Name"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="DeptName">Department Name</label>--%>
                    </div>
                    <div class="deptNameError text-danger d-none" id="deptNameError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please provide a proper name for department</span>
                    </div>
                </div>
            </div>
            <%-- department head inputs and errors --%>
            <div class="col-12">
                <div class="deptHead-fields mt-2" id="deptHead-field">
                    <div class="deptHead-input">
                        <asp:TextBox ID="deptHead" CssClass="deptHead form-control form-control-lg" runat="server" placeholder="Department Head"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="deptHead">Department Head/Dean</label>--%>
                    </div>
                    <div class="deptHeadError text-danger d-none" id="deptHeadError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please provide a valid department Head name</span>
                    </div>
                </div>
            </div>
        </div>

        <div class="row gx-2">
            <div class="col-12">
                <div class="deptDesc_input mt-2" id="deptDesc_input">
                    <div class="deptDescript-input">
                        <asp:TextBox ID="deptDesc" runat="server" CssClass="deptDesc form-control form-control-lg" Style="height: 120px" Placeholder="Brief Description of Department" TextMode="MultiLine"></asp:TextBox>
                        <%--<label for="deptDesc">Brief Description of Department</label>--%>
                    </div>
                    <div class="deptDescError text-danger d-none" id="deptDescError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please enter a brief description for department</span>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="deptEmail-fields mt-2" id="deptEmail-field">
                    <div class="deptEmail-input">
                        <asp:TextBox ID="deptEmail" CssClass="deptEmail form-control form-control-lg" runat="server" placeholder="Department Email"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="deptEmail">Department Email</label>--%>
                    </div>
                    <div class="deptEmailError text-danger d-none" id="deptEmailError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please enter a valid Email</span>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-12">
                <div class="deptCN-fields mt-2" id="deptCN-field">
                    <div class="deptCN-input">
                        <asp:TextBox ID="deptCN" CssClass="deptCN form-control form-control-lg" runat="server" placeholder="Department Contact Number"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="deptCN">Department Contact Number</label>--%>
                    </div>
                    <div class="deptCNError text-danger d-none" id="deptCNError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please enter a valid contact number</span>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-12">
                <div class="deptHours-fields mt-2" id="deptHours-field">
                    <div class="deptHours-input">
                        <asp:TextBox ID="deptHours" CssClass="deptHours form-control form-control-lg" runat="server" placeholder="Office Hours"></asp:TextBox>
                        <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                        <%--<label for="deptHours">Department Office Hours</label>--%>
                    </div>
                    <div class="deptHoursError text-danger d-none" id="deptHoursError">
                        <span><i class="bi bi-info-circle"></i></span>
                        <span>Please enter a valid Office Hours (Ex. 8:00 AM - 5:00 PM)</span>
                    </div>
                </div>
            </div>

            <div class="col-12 d-flex justify-content-center">
                <div class="d-grid addDeptBtn">
                    <asp:Button ID="addDeptBtn" runat="server" Text="Add Department" CssClass="btn mt-2 bg-primary text-white" OnClick="addDeptBtn_Click" />
                </div>
            </div>
            <%-- MODAL --%>
            <div class="modal fade" id="myModal">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <!-- Placeholder for content -->
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn bg-primary text-light" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>
            </div>
            <script src="../../../Resources/CustomJS/Registration/DeptRegistrationJS.js"></script>
        </div>
    </div>
</asp:Content>

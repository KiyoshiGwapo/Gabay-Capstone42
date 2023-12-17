<%@ Page Title="Pending Students" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Pending_students.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Department_Homepage.WebForm2" EnableViewState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .gridViewContainer{
            width: 100%;
            overflow: auto;
            padding:5px;
        }
    </style>
    <h1>Manage Student Account</h1>
    <div class="container-fluid mb-1">
        <div class="row">
            <div class=" position-absolute">

            </div>
            <div class="col-lg-10 col-md-12">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control float-end mb-3" placeholder="Search student..." OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
            </div>
            <div class="col-lg-2 col-md-12">
                <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle w-100" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Student Status
                    </button>
                    <ul class="dropdown-menu w-100">
                        <li>
                            <asp:LinkButton ID="displayPending" CssClass="dropdown-item" runat="server" OnClick="displayPending_Click">Pending Students</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="displayActive" CssClass="dropdown-item" runat="server" OnClick="displayActive_Click">Active Students</asp:LinkButton>
                        </li>
                        <li>
                           <asp:LinkButton ID="displayDeactivated" CssClass="dropdown-item" runat="server" OnClick="displayDeactivated_Click">Deactivated Students</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            
            <div class="col-12">

                <div class="gridViewContainer">
                    <asp:GridView ID="pending_table" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="studentID">
                        <Columns>
                            <asp:BoundField DataField="studentID" HeaderText="Student ID" />
                            <asp:BoundField DataField="name" HeaderText="Name" />
                            <asp:BoundField DataField="address" HeaderText="Address" />
                            <asp:BoundField DataField="contactNumber" HeaderText="ContactNumber" />
                            <asp:BoundField DataField="course" HeaderText="Course" />
                            <asp:BoundField DataField="course_year" HeaderText="Year Level" />
                            <asp:BoundField DataField="email" HeaderText="Email" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%# "return showApproveConfirmationModal(" + Eval("studentID") + ");" %>'>Approve</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="labelforPending" runat="server" Text="No results found in pending students" CssClass="no-results-label d-flex justify-content-center"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <asp:GridView ID="active_table" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="studentID">
                    <Columns>
                        <asp:BoundField DataField="studentID" HeaderText="Student ID" />
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="address" HeaderText="Address" />
                        <asp:BoundField DataField="contactNumber" HeaderText="ContactNumber" />
                        <asp:BoundField DataField="course" HeaderText="Course" />
                        <asp:BoundField DataField="course_year" HeaderText="Year Level" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%# "return showDeactiveConfirmationModal(" + Eval("studentID") + ");" %>'>Deactivate</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="labelforActive" runat="server" Text="No results found in active students" CssClass="no-results-label d-flex justify-content-center"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:GridView ID="deactivated_table" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="studentID">
                    <Columns>
                        <asp:BoundField DataField="studentID" HeaderText="Student ID" />
                        <asp:BoundField DataField="name" HeaderText="Name" />
                        <asp:BoundField DataField="address" HeaderText="Address" />
                        <asp:BoundField DataField="contactNumber" HeaderText="ContactNumber" />
                        <asp:BoundField DataField="course" HeaderText="Course" />
                        <asp:BoundField DataField="course_year" HeaderText="Year Level" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%# "return showDeactiveConfirmationModal(" + Eval("studentID") + ");" %>'>Deactivate</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="labelforDeactivated" runat="server" Text="No results found in deactivated student" CssClass="no-results-label d-flex justify-content-center"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>
    </div>
    
     <asp:HiddenField runat="server" ID="hidPersonID" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <%-- approve confirmation modal --%>
    <div id="ApproveconfirmationModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmation</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Approve Student?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="btn btn-danger" OnClick="btnApprove_Click" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <%-- after approved modal --%>
    <div class="modal fade" id="ApproveSuccessModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <span>Student Updated</span>
                </div>
            </div>
        </div>
    </div>

    <%-- deactive confirmation modal --%>
    <div id="DeactiveconfirmationModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmation</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Deactivate Student?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="deactiveBtn" runat="server" Text="Confirm" CssClass="btn btn-danger" UseSubmitBehavior="false" OnClick="deactiveBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <%-- after approved modal --%>
    <div class="modal fade" id="DeactiveSuccessModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <span>Student Updated</span>
                </div>
            </div>
        </div>
    </div>



    <script>
        function showApproveConfirmationModal(id) {
            // Store the ID in a hidden field or JavaScript variable to access it later in btnConfirmDelete_Click
            document.getElementById('<%= hidPersonID.ClientID %>').value = id;

            // Show the Bootstrap modal
            $('#ApproveconfirmationModal').modal('show');

            // Prevent the postback
            return false;
        }
        function showDeactiveConfirmationModal(id) {
            // Store the ID in a hidden field or JavaScript variable to access it later in btnConfirmDelete_Click
            document.getElementById('<%= hidPersonID.ClientID %>').value = id;

            // Show the Bootstrap modal
            $('#DeactiveconfirmationModal').modal('show');

            // Prevent the postback
            return false;
        }
    </script>
</asp:Content>

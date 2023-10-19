<%@ Page Title="Pending Students" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Pending_students.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Department_Homepage.WebForm2" EnableViewState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Pending Student Registration</h1>
    <asp:Label ID="displayEmailLabel" runat="server" Text=""></asp:Label>
    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control float-end mb-3" placeholder="Search student..." OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
    <asp:GridView ID="pending_table" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="studentID">
        <Columns>
            <asp:BoundField DataField="studentID" HeaderText="Student ID" />
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:BoundField DataField="address" HeaderText="Address" />
            <asp:BoundField DataField="contactNumber" HeaderText="ContactNumber" />
            <asp:BoundField DataField="course_year" HeaderText="Course Year" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:BoundField DataField="status" HeaderText="Status" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%# "return showConfirmationModal(" + Eval("studentID") + ");" %>'>Approve</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="hidPersonID" />
     <%-- confirmation modal --%>
        <div id="confirmationModal" class="modal fade" tabindex="-1" role="dialog">
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
                        <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="btn btn-danger" OnClick="btnApprove_Click" UseSubmitBehavior="false"/>
                    </div>
                </div>
            </div>
        </div>
        <%-- after deleted modal --%>
        <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
        function showConfirmationModal(id) {
            // Store the ID in a hidden field or JavaScript variable to access it later in btnConfirmDelete_Click
            document.getElementById('<%= hidPersonID.ClientID %>').value = id;

            // Show the Bootstrap modal
            $('#confirmationModal').modal('show');

            // Prevent the postback
            return false;
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Users.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- MANAGE USERS --%>
    <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Manage Users</h1>

    <div class="d-flex flex-column align-items-center mt-3">
        <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" CssClass="form-select mx-auto w-25">
            <asp:ListItem Text="Select Account Type" Value="" Disabled="true" Selected="true"></asp:ListItem>
            <asp:ListItem Text="Students Account" Value="Students"></asp:ListItem>
            <asp:ListItem Text="Departments Account" Value="Departments"></asp:ListItem>
        </asp:DropDownList>
        <div class="mt-2"></div>
    </div>

    <div class="table-responsive">
        <asp:GridView ID="GridViewStudents" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered text-center" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-CssClass="" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="" />
                <asp:BoundField DataField="StudentDepartment" HeaderText="Student Department" SortExpression="StudentDepartment" ItemStyle-CssClass="" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="" />
                <asp:TemplateField Visible="false" HeaderText="Department" SortExpression="Department">
                    <ItemTemplate>
                        <asp:Label ID="lblStudentDepartment" runat="server" Text='<%# Eval("StudentDepartment") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <%--             <asp:LinkButton ID="lnkEdit" runat="server" Text='<i class="fas fa-edit"></i>' CssClass="btn btn-primary" OnClientClick='<%# "showEditPasswordModal(" + Container.DataItemIndex + "); return false;" %>' />--%>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text='<i class="fas fa-trash-alt" style="color: white;"></i>' CssClass="btn btn-danger" OnClientClick='<%# "showConfirmationModal(" + Container.DataItemIndex + "); return false;" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="GridViewDepartments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered text-center" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-CssClass="" />
                <asp:BoundField DataField="DepartmentHead" HeaderText="Department Head" SortExpression="DepartmentHead" ItemStyle-CssClass="" />
                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" ItemStyle-CssClass="" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                       <asp:LinkButton ID="lnkEdit" runat="server" Text='<i class="fas fa-edit"></i>' CssClass="btn btn-primary" OnClientClick='<%# "showEditPasswordModal(" + Container.DataItemIndex + "); return false;" %>' />
                        <asp:LinkButton ID="lnkDelete" runat="server" Text='<i class="fas fa-trash-alt" style="color: white;"></i>' CssClass="btn btn-danger" OnClientClick='<%# "showConfirmationModal(" + Container.DataItemIndex + "); return false;" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    <asp:HiddenField ID="hfSelectedRowIndex" runat="server" />
    <div class="modal fade" id="confirmDeleteModal" tabindex="-1" role="dialog" aria-labelledby="confirmDeleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmDeleteModalLabel">Confirm Delete</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this user?
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Yes" OnClick="btnConfirmDelete_Click" CssClass="btn btn-danger" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>

    <%--  EDIT PASSWORD MODAL--%>

    <div class="modal fade" id="editPasswordModal" tabindex="-1" role="dialog" aria-labelledby="editPasswordModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editPasswordModalLabel">Change Password</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-- Password change form -->
                    <div class="form-group">
                        <label for="txtNewPassword">New Password:</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmEditPassword" runat="server" Text="Yes" OnClick="btnConfirmEditPassword_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showConfirmationModal(rowIndex) {
            console.log("Modal function called with rowIndex: " + rowIndex);
            $('#confirmDeleteModal').modal('show');
            $('#<%= hfSelectedRowIndex.ClientID %>').val(rowIndex);
        }
        function showEditPasswordModal(rowIndex) {
            console.log("Edit Password Modal function called with rowIndex: " + rowIndex);
            $('#editPasswordModal').modal('show');
            $('#<%= hfSelectedRowIndex.ClientID %>').val(rowIndex);
        }
    </script>
</asp:Content>

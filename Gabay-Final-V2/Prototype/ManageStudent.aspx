<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageStudent.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table table-bordered">
            <tr>
                <td colspan="2">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Name" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        <div class="input-group-append">
                            <button class="btn btn-primary" type="button">Search</button>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="studentList" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="ex_ID" HeaderText="ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" />
                            <asp:BoundField DataField="CourseYear" HeaderText="Course Year" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span id='<%# "status_" + Eval("ex_ID") %>'><%# Eval("Status") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <!-- Action Column -->
                                        <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                        <!-- Dropdown menu approve and disapprove -->
                        <div class="btn-group">
                            <button type="button" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Actions
                            </button>
                            <div class="dropdown-menu">
                                <button id="btnApprove" type="button" class="dropdown-item" onclick='<%# "approveRecord(" + Eval("ex_ID") + ");" %>'>Approve</button>
                                <button id="btnDisapprove" type="button" class="dropdown-item" onclick='<%# "disapproveRecord(" + Eval("ex_ID") + ");" %>'>Disapprove</button>
                            </div>
                        </div>
                        </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="hidPersonID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <!-- Confirmation Modal -->
                    <div id="confirmationModal" class="modal fade" tabindex="-1" role="dialog">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Confirmation</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    Are you sure you want to approve this record?
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm" OnClick="btnConfirmDelete_Click" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
                <td>
                    <!-- After Approved Modal -->
                    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">
                                <div class="modal-body bg-success text-center text-light">
                                    <i class="bi bi-info-circle-fill"></i>
                                    <span> Approved!</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
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
</body>
</html>

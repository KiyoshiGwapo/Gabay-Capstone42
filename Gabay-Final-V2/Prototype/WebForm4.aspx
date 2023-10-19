<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm4" %>

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
        <asp:GridView ID="pending_table" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="studentID">
            <Columns>
                <asp:BoundField DataField="studentID" HeaderText="Student ID" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='<%# "return showConfirmationModal(" + Eval("studentID") + ");" %>'>Approve</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HiddenField runat="server" ID="hidPersonID" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
                        <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="btn btn-danger" OnClick="btnApprove_Click" />
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

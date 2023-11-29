<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_FAQ.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Frequently Asked Questions</h1>

    <div style="display: flex; flex-direction: column; align-items: flex-end; margin-top: 10px;">
        <asp:Button ID="btnAddFAQ" runat="server" CssClass="btn btn-primary" Text="Add New FAQ" OnClientClick="return showAddModal();" />
        <div style="margin-top: 20px;"></div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Repeater ID="FAQRepeater" runat="server" OnItemCommand="FAQRepeater_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-responsive mx-auto">
                        <thead>
                            <tr>
                                <th style="text-align: center;">FAQ ID</th>
                                <th style="text-align: center;">Question</th>
                                <th style="text-align: center;">Answer</th>
                                <th style="text-align: center;">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center;"><%# Eval("FAQID") %></td>
                        <td style="text-align: center;"><%# Eval("Question") %></td>
                        <td style="max-width: 200px; max-height: 100px; overflow-x: auto; overflow-y: auto; text-align: center;"><%# Eval("Answer") %></td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%# Eval("FAQID") %>' Text='<i class="fas fa-edit"></i>'
                                CssClass="btn btn-primary" OnClientClick='<%# "return showFAQ(" + Eval("FAQID") + ", \"" + Eval("Question") + "\", \"" + Eval("Answer") + "\");" %>' />

                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" OnClientClick='<%# "return showConfirmationModal(" + Eval("FAQID") + ");" %>'>
                                <i class="fas fa-trash-alt" style="color: white;"></i> 
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:HiddenField ID="hdEditFAQID" runat="server" />
    <!-- Modal for editing FAQ -->
    <div id="editModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Edit FAQ</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <!-- Form for editing FAQ -->
                    <asp:TextBox ID="txtEditQuestion" runat="server" CssClass="form-control" placeholder="Edit Question"  style="width: 400px;"></asp:TextBox>
                    <br>
                    <asp:TextBox ID="txtEditAnswer" runat="server" CssClass="form-control" placeholder="Edit Answer" style="width: 450px; height: 100px; overflow-x: auto; overflow-y: auto;"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateFAQ" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdateFAQ_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Delete Modal FAQ --%>
    <div class="modal fade" id="toDeleteModal" tabindex="-1" aria-labelledby="toDeleteModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Delete this Question?
               
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button ID="dltAnnouceBtn" CssClass="btn btn-primary" runat="server" Text="Proceed" OnClick="dltAnnouceBtn_Click" />
                </div>
            </div>
        </div>
    </div>


    <%-- Success modal --%>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <p id="successMessage"></p>
                </div>
            </div>
        </div>
    </div>
    <%-- Error modal --%>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-danger text-center text-light">
                    <i class="bi bi-exclamation-circle-fill"></i>
                    <p id="errorMessage"></p>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for adding a new FAQ -->
    <div id="addModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add New FAQ</h4>
                </div>
                <div class="modal-body">
                    <!-- Form for adding new FAQ -->
                    <asp:TextBox ID="txtAddQuestion" runat="server" CssClass="form-control" placeholder="New Question" style="width: 400px;"></asp:TextBox>
                    <br>
                   <asp:TextBox ID="txtAddAnswer" runat="server" CssClass="form-control" placeholder="New Answer" style="width: 450px; height: 100px; overflow-x: auto; overflow-y: auto;"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAddNewFAQ" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAddNewFAQ_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>



    <script>
        function showConfirmationModal(id) {
            // Store the ID in a hidden field or JavaScript variable to access it later in btnConfirmDelete_Click
            document.getElementById('<%= hdEditFAQID.ClientID %>').value = id;
            // Show the Bootstrap modal
            $('#toDeleteModal').modal('show');
            // Prevent the postback
            return false;
        }
        function getAnnouncementID(id) {
            document.getElementById('<%= hdEditFAQID.ClientID %>').value = id;
        }
        function showAddModal() {

            document.getElementById('<%= txtAddQuestion.ClientID %>').value = '';
            document.getElementById('<%= txtAddAnswer.ClientID %>').value = '';

            $('#addModal').modal('show');


            return false;
        }

    </script>
</asp:Content>

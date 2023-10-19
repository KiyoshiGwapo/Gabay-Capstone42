<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm11.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <link href="../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        .AnnouncementList td{
           vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#toAddModal">
                Add Announcement
            </button>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control float-end mb-3" placeholder="Search Announcement..."></asp:TextBox>
            <asp:GridView ID="AnnouncementList" HeaderStyle-CssClass="text-center" runat="server" AutoGenerateColumns="false" CssClass="table AnnouncementList" DataKeyNames="AnnouncementID">
                <Columns>
                    <asp:BoundField DataField="AnnouncementID" HeaderText="ID" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"
                                ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ImagePath")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ShortDescription" HeaderText="Short Description" />
                    <asp:BoundField DataField="DetailedDescription" HeaderText="Detailed Description" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="gridviewEdit" CssClass="btn bg-primary text-light" runat="server" Text="Edit" OnClientClick='<%# "return getAnnouncementID(" + Eval("AnnouncementID") + ");" %>' OnClick="gridviewEdit_Click" />
                            <asp:LinkButton ID="gridviewDeleteBtn" CssClass="btn bg-danger text-light" runat="server" OnClientClick='<%# "return showConfirmationModal(" + Eval("AnnouncementID") + ");" %>'>
                                Delete
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="HidAnnouncementID" runat="server" />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Prototype/WebForm13.aspx">Show Carousel Picture</asp:HyperLink>
            
        </div>
        <%-- to add modal --%>
        <div class="modal fade" id="toAddModal" tabindex="-1" aria-labelledby="AddModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addTitlebx" CssClass="form-control" runat="server" placeholder="Title"></asp:TextBox>
                            <label for="Titlebx">Title</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addDatebx" CssClass="form-control" runat="server" placeholder="Title" TextMode="Date"></asp:TextBox>
                            <label for="Datebx">Date</label>
                        </div>
                        <div class="mb-3">
                            <asp:FileUpload ID="addFilebx" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addShrtbx" CssClass="form-control" runat="server" placeholder="Short Description"></asp:TextBox>
                            <label for="addShrtbx">Short Description</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addDtldbx" CssClass="form-control DtlDescBx" Style="height: 100px" runat="server" placeholder="Detailed Description" TextMode="MultiLine"></asp:TextBox>
                            <label for="DtlDescBx">Detailed Description</label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="SaveAnnouncement" runat="server" Text="Add Announcement" CssClass="btn bg-primary text-light" OnClick="SaveAnnouncement_Click"/>
                    </div>
                </div>
            </div>
        </div>
        
        <%-- to edit modal --%>
        <div class="modal fade" id="toEditModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Button ID="closeEditModal" runat="server" CssClass="btn-close" OnClick="closeEditModal_Click" />
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="Titlebx" CssClass="form-control" runat="server" placeholder="Title"></asp:TextBox>
                                <label for="Titlebx">Title</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="Datebx" CssClass="form-control" runat="server" placeholder="Title" TextMode="Date"></asp:TextBox>
                                <label for="Datebx">Date</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="ShortDescbx" CssClass="form-control" runat="server" placeholder="Short Description"></asp:TextBox>
                                <label for="ShortDescbx">Short Description</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="DtlDescBx" CssClass="form-control DtlDescBx" style="height: 100px" runat="server" placeholder="Detailed Description" TextMode="MultiLine"></asp:TextBox>
                                <label for="DtlDescBx">Detailed Description</label>
                            </div>
                            <div class="mb-3">
                                <asp:FileUpload ID="Imgbx" CssClass="form-control" runat="server"/>
                            </div>
                            <div class=" d-grid ">
                                <asp:Button ID="updtAnnouncement" CssClass="btn bg-primary" runat="server" Text="Update Announcement" OnClick="updtAnnouncement_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <%-- to delete modal --%>
        <div class="modal fade" id="toDeleteModal" tabindex="-1" aria-labelledby="toDeleteModal" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Delete this annoucement?
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

    </form>
    <script>
        function showConfirmationModal(id) {
            // Store the ID in a hidden field or JavaScript variable to access it later in btnConfirmDelete_Click
            document.getElementById('<%= HidAnnouncementID.ClientID %>').value = id;
            // Show the Bootstrap modal
            $('#toDeleteModal').modal('show');
            // Prevent the postback
            return false;
        }
        function getAnnouncementID(id) {
            document.getElementById('<%= HidAnnouncementID.ClientID %>').value = id;
        }
    </script>
</body>
</html>

<%@ page title="" language="C#" masterpagefile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" autoeventwireup="true" codebehind="Department_Announcement.aspx.cs" inherits="Gabay_Final_V2.Views.Modules.Announcement.Department_Announcement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../Resources/CustomJS/Announcement/Announcement.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .AnnouncementList td {
            vertical-align: middle;
        }
    </style>
    <div class="container mt-5">
        <!-- Add Announcement Button (Modal Trigger) -->
        <div class="row">
            <div class="col-2">
                <div class="d-grid">
                    <button type="button" class="btn btn-primary mb-3" data-bs-toggle="modal" data-bs-target="#toAddModal">
                        Add Announcement
                   
                    </button>
                </div>
            </div>
            <div class="col-10">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control float-end mb-3" placeholder="Search Announcement by Title..." AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
            </div>
        </div>

        <!-- Bootstrap Table -->
        <asp:GridView ID="AnnouncementList" HeaderStyle-CssClass="text-center" runat="server" AutoGenerateColumns="false" CssClass="table AnnouncementList" DataKeyNames="AnnouncementID">
            <Columns>
                <asp:BoundField DataField="AnnouncementID" HeaderText="ID" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" DataFormatString="{0:hh:mm tt}" />
                <asp:BoundField DataField="EndTime" HeaderText="End Time" DataFormatString="{0:hh:mm tt}" />
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
                        <div class="d-flex justify-content-evenly">
                            <asp:LinkButton ID="gridviewEdit" CssClass="btn bg-primary text-light" runat="server" OnClientClick='<%# "return getAnnouncementID(" + Eval("AnnouncementID") + ");" %>' OnClick="gridviewEdit_Click"><i class="bi bi-pencil-square"></i></asp:LinkButton>
                            <asp:LinkButton ID="gridviewDeleteBtn" CssClass="btn bg-danger text-light" runat="server" OnClientClick='<%# "return showConfirmationModal(" + Eval("AnnouncementID") + ");" %>'>
                                <i class="bi bi-trash-fill"></i>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="d-flex justify-content-center">
            <asp:Label ID="noResultsLabel" runat="server" Text="No results found" Visible="false" CssClass="no-results-label"></asp:Label>
        </div>
        <asp:HiddenField ID="HidAnnouncementID" runat="server" />
    </div>

    <!-- Edit Announcement Modal -->
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
                            <asp:TextBox ID="StartTimebx" CssClass="form-control" runat="server" placeholder="Start Time"  TextMode="Time"></asp:TextBox>
                            <label for="StartTime">Start Time</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="EndTimebx" CssClass="form-control" runat="server" placeholder="Start Time"  TextMode="Time"></asp:TextBox>
                            <label for="EndTime">End Time</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="ShortDescbx" CssClass="form-control" runat="server" placeholder="Short Description"></asp:TextBox>
                            <label for="ShortDescbx">Short Description</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="DtlDescBx" CssClass="form-control DtlDescBx" Style="height: 100px" runat="server" placeholder="Detailed Description" TextMode="MultiLine"></asp:TextBox>
                            <label for="DtlDescBx">Detailed Description</label>
                        </div>
                        <div class="mb-3">
                            <asp:FileUpload ID="Imgbx" CssClass="form-control" runat="server" />
                        </div>
                        <div class=" d-grid ">
                            <asp:Button ID="updtAnnouncement" CssClass="btn bg-primary" runat="server" Text="Update Announcement" OnClick="updtAnnouncement_Click" accept=".jpg, .png, .jpeg" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Add Announcement Modal -->
    <div class="modal fade" id="toAddModal" tabindex="-1" aria-labelledby="AddModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="addTitlebx" CssClass="addTitlebx form-control" runat="server" placeholder="Title"></asp:TextBox>
                        <label for="Titlebx">Title</label>
                        <div class="annoucementitle text-danger d-none" id="annoucementitle">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please provide a title</span>
                        </div>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="addDatebx" CssClass="addDatebx form-control" runat="server" placeholder="Title" TextMode="Date"></asp:TextBox>
                        <label for="Datebx">Date</label>
                        <div class="addDatebxError text-danger d-none" id="addDatebxError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please provide a date</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col">
                                <label for="StartTime">Start Time</label>
                                <asp:TextBox ID="addStartTime" CssClass="form-control clockpicker" runat="server" placeholder="Start Time" TextMode="Time"></asp:TextBox>
                            </div>
                            <div class="col">
                                <label for="EndTime">End Time</label>
                                <asp:TextBox ID="addEndTime" CssClass="form-control clockpicker" runat="server" placeholder="End Time" TextMode="Time"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-floating mb-3">
                    </div>
                    <div class="mb-3">
                        <asp:FileUpload ID="addFilebx" CssClass="addFilebx form-control" runat="server" accept=".jpg, .png, .jpeg" />
                        <div class="imgError text-danger d-none" id="imgError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Image is required</span>
                        </div>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="addShrtbx" CssClass="addShrtbx form-control" runat="server" placeholder="Short Description"></asp:TextBox>
                        <label for="addShrtbx">Short Description</label>
                        <div class="shrtDescError text-danger d-none" id="shrtDescError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>add short description (50 characters max)</span>
                        </div>
                        <div class="maxError text-danger d-none" id="maxError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Max character reached</span>
                        </div>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="addDtldbx" CssClass="addDtldbx form-control DtlDescBx" Style="height: 100px" runat="server" placeholder="Detailed Description" TextMode="MultiLine"></asp:TextBox>
                        <label for="DtlDescBx">Detailed Description</label>
                        <div class="lngDescError text-danger d-none" id="lngDescError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please provide a description</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="SaveAnnouncement" runat="server" Text="Add Announcement" CssClass="btn bg-primary text-light" OnClick="SaveAnnouncement_Click" />
                </div>
            </div>
        </div>
    </div>
    <%-- Delete Announcement Modal --%>
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


    <!-- Bootstrap JS and jQuery -->
    <script src="../../../Scripts/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/css/bootstrap-timepicker.min.css" integrity="sha256-oaR6kWgM/X73YzHy1l2hAwt2xpfrIQ34qR/vsUmbtOY=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-timepicker/0.5.2/js/bootstrap-timepicker.min.js" integrity="sha256-DeIe6jTh3bFrP6Hh9gZzZ6lc7zU6o6ax/MSMdVivmcM=" crossorigin="anonymous"></script>
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
</asp:Content>

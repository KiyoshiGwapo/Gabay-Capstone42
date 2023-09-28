<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="DepartmentAnnouncement.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Announcement.DepartmentAnnouncement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Bootstrap JS and jQuery -->
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
 <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
        <div class="container mt-5">
            <!-- Add Announcement Button (Modal Trigger) -->
            <button type="button" class="btn btn-primary mb-3" data-toggle="modal" data-target="#addAnnouncementModal">Add Announcement</button>
            
            <!-- Bootstrap Table -->
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Image</th>
                        <th>Date</th>
                        <th>Short Description</th>
                        <th>Detailed Description</th>
                        <th>Actions</th>
                    </tr>
                </thead>
               <tbody>
                    <asp:Repeater ID="rptAnnouncements" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("AnnouncementID") %></td>
                                <td><%# Eval("Title") %></td>
                                <td>
                                   <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' alt="Announcement Image" width="100" height="100" />
                                </td>
                                 <td><%# Eval("Date", "{0:yyyy-MM-dd}") %></td>
                               <td class="limited-width">
                                    <div class="scrollable-description"><%# Eval("ShortDescription") %></div>
                                </td>
                                <td class="limited-width">
                                    <div class="scrollable-description"><%# Eval("DetailedDescription") %></div>
                                </td>
                                <td>  
                              <button type="button" class="btn btn-primary btn-sm"
                                data-toggle="modal" data-target="#editAnnouncementModal"
                                data-id='<%# Eval("AnnouncementID") %>' 
                                data-title='<%# Eval("Title") %>' 
                                data-date='<%# Eval("Date") %>'
                                data-shortdesc='<%# Eval("ShortDescription") %>' 
                                data-detaileddesc='<%# Eval("DetailedDescription") %>'
                                data-imagepath='<%# Eval("ImagePath") %>'>Edit</button>
                                   <asp:Button ID="deleteButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="DeleteButton_Click" CommandArgument='<%# Eval("AnnouncementID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!-- Edit Announcement Modal -->
<div class="modal fade" id="editAnnouncementModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Edit Announcement</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Edit Announcement Form Fields -->
                <label for="EditTitle">Title:</label>
                <asp:TextBox ID="txtEditTitle" runat="server" CssClass="form-control" />
                <br />
                <label for="EditDate">Date:</label>
                <asp:TextBox ID="txtEditDate" runat="server" placeholder="Date (YYYY-MM-DD)" CssClass="datepicker" />
  
                <br />
                <label for="EditShortDescription">Short Description:</label>
                <asp:TextBox ID="txtEditShortDescription" runat="server" CssClass="form-control" />
                <br />
                <label for="EditDetailedDescription">Detailed Description:</label>
                <asp:TextBox ID="txtEditDetailedDescription" runat="server" TextMode="MultiLine" CssClass="form-control" />
                <br />
                <img id="imgEditAnnouncement" src="" alt="Announcement Image" width="100" height="100" />
                <!-- File input for image upload -->
                <label for="EditImage">Image:</label>
               <asp:FileUpload ID="ImageFileEdit" runat="server" accept=".jpg, .jpeg, .png" />
            </div>
             <!-- Hidden field to store the Announcement ID -->
                <asp:HiddenField ID="hdnEditAnnouncementID" runat="server" />
            <!-- Hidden field to store the image path -->
                <asp:HiddenField ID="hdnEditImagePath" runat="server" />
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <asp:Button ID="btnSaveEditedAnnouncement" runat="server" Text="Save" OnClick="SaveEditedAnnouncement" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>

<!-- Add Announcement Modal -->
<div class="modal fade" id="addAnnouncementModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModal">Add Announcement</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Add Announcement Form Fields -->
                <label for="Title">Title:</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                <br />
                <label for="Date">Date:</label>
                <asp:TextBox ID="txtDate" runat="server" placeholder="Date (YYYY-MM-DD)" CssClass="datepicker" />
                <br />
                <label for="ShortDescription">Short Description:</label>
                <asp:TextBox ID="txtShortDescription" runat="server" CssClass="form-control" />
                <br />
                <label for="DetailedDescription">Detailed Description:</label>
                <asp:TextBox ID="txtDetailedDescription" runat="server" TextMode="MultiLine" CssClass="form-control" />
                <br />
                <!-- File input for image upload -->
                <label for="Image">Image:</label>
              <asp:FileUpload ID="ImageFileUploadModal" runat="server" accept=".jpg, .jpeg, .png" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveAnnouncement" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>

<style>
    /* Style to make the date input look like readonly */
    .datepicker[readonly] {
        background-color: #f8f8f8; /* Light gray background color */
        border: none; /* Remove border */
    }
    .scrollable-description {
    max-height: 100px; /* Adjust the maximum height as needed */
    overflow-y: auto;
    }
    /* CSS to limit the width of the "Detailed Description" column */
    .limited-width {
        max-width: 300px; /* Adjust the value as needed */
        word-wrap: break-word; /* Wrap long words to next line */
    }
</style>
 <script type="text/javascript">
     $(document).ready(function () {
         var initialEditValues = {}; // Object to store initial field values

         $('.datepicker').datepicker({
             format: 'yyyy-mm-dd',  // Format of the displayed date
             autoclose: true,      // Close the datepicker when a date is selected
         });

         // Set the image source in the edit modal
         $('.edit-announcement').click(function () {
             var button = $(this);
             var modal = $('#editAnnouncementModal');

             // Store initial field values
             initialEditValues.title = button.data('title');
             initialEditValues.date = button.data('date');
             initialEditValues.shortdesc = button.data('shortdesc');
             initialEditValues.detaileddesc = button.data('detaileddesc');

             modal.find('#txtEditTitle').val(initialEditValues.title);
             modal.find('#txtEditDate').val(initialEditValues.date);
             modal.find('#txtEditShortDescription').val(initialEditValues.shortdesc);
             modal.find('#txtEditDetailedDescription').val(initialEditValues.detaileddesc);

             var imagePath = button.data('imagepath');
             modal.find('#hdnEditImagePath').val(imagePath);
             modal.find('#imgEditAnnouncement').attr('src', imagePath);

             modal.find('#hdnEditAnnouncementID').val(button.data('id'));

             modal.modal('show');
         });

         // Handle the cancel button click event in the edit modal
         $('#editAnnouncementModal').on('hidden.bs.modal', function (event) {
             var modal = $(this);

             // Restore initial field values
             modal.find('#txtEditTitle').val(initialEditValues.title);
             modal.find('#txtEditDate').val(initialEditValues.date);
             modal.find('#txtEditShortDescription').val(initialEditValues.shortdesc);
             modal.find('#txtEditDetailedDescription').val(initialEditValues.detaileddesc);
         });
     });


 </script>
</asp:Content>

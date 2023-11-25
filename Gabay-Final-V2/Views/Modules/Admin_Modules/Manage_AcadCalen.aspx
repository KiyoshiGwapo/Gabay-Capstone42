<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_AcadCalen.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_AcadCalen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../Resources/CustomJS/OpenModal.js"></script>
    <script src="../../../Resources/CustomJS/AcadCalendar/AcadCalendarJS.js"></script>
    <link href="../../../Resources/CustomStyleSheet/Acad_Calendar/AcadCalenStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container {
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
            min-height: 100%;
            position: relative;
            padding: 20px;
        }
        .upload-container, .download-container {
            padding: 20px;
            margin-bottom: 20px;
        }
        .upload-container {
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .download-container {
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .selected {
            background-color: #f0f0f0; /* Add your desired style */
        }
        .delete-button {
            background-color: #f44336;
            border: none;
            color: white;
            padding: 6px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
        }
         h1 {
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 24px;
            text-align: left; /* Align to the left */
        }
         h2{
           font-weight: bold;
           font-family: Arial, Helvetica, sans-serif;
           font-size: 19px;
           text-align: left; 
        }
        td{
            margin: 0 0 10px;
            line-height: 1.42857143;
            color: #337AB7;
            background-color: transparent;
        }
        td:active, a:hover {
            outline: 0;
        }
        /* Center the table content */
        .table-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100%;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            border: none;
        }
        th, td {
            padding: 8px;
            text-align: left;
            text-decoration: none;
        }
        th {
            text-align: center;
        }
        td:hover {
            text-decoration: underline;
        }
    </style>
        <div class="container">
        <div class="text-right">
            <!-- Button to trigger the modal -->
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#uploadModal">
                Upload File
            </button>
        </div>
            <h2>Academic Calendar</h2>
            <!-- Display the list of uploaded files in a Bootstrap-styled table -->
            <div class="table-container">
                <table style="border-collapse: collapse;">
                    <thead>
                        <tr>
                            <th class="text-center"></th>
                            <th class="text-center"><i class="fas fa-cogs"></i></th>
                        </tr>
                    </thead>
                    <tbody>
                     <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("FileName") %>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FileId") %>' Text="Delete" CssClass="delete-button" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>

            <!-- Bootstrap Modal for Upload -->
            <div class="modal fade" id="uploadModal" tabindex="-1" role="dialog" aria-labelledby="uploadModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="uploadModalLabel">PDF File Upload</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                          <asp:FileUpload ID="fileUpload" runat="server" />     
                        </div>
                        <div class="modal-body">
                          <asp:TextBox ID="txtFileName" runat="server" placeholder="Enter File Name" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="BtnUpload_Click" CssClass="btn btn-primary" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
     <!-- JavaScript to show/hide modal -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
        <!-- Add Bootstrap CSS link -->
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" />

    
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
</asp:Content>

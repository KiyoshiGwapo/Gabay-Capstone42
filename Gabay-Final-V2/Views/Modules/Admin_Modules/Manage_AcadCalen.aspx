<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_AcadCalen.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_AcadCalen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../Resources/CustomJS/OpenModal.js"></script>
    <script src="../../../Resources/CustomJS/AcadCalendar/AcadCalendarJS.js"></script>
    <link href="../../../Resources/CustomStyleSheet/Acad_Calendar/AcadCalenStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Academic Calendar</h1>

    <div style="display: flex; flex-direction: column; align-items: flex-end; margin-top: 10px;">
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#uploadModal">
            Upload File 
        </button>
    </div>
    <div class="row">
        <div class="col-md-12">
            <!-- Display the list of uploaded files in a Bootstrap-styled table -->
            <div class="table-container">
                <table class="table table-responsive mx-auto">
                    <thead>
                        <tr>
                            <th style="text-align: center;">File Name</th>
                            <th style="text-align: center;"><i class="fas fa-cogs"></i></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <%# Eval("FileName") %>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FileId") %>' CssClass="btn btn-danger">
                                            <i class="fas fa-trash-alt" style="color: white;"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
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

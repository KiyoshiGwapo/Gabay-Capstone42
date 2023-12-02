<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_AcadCalen.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Academic_Calendar.Student_AcadCalen" EnableViewState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../../Resources/CustomStyleSheet/Acad_Calendar/AcadCalenStyle.css" rel="stylesheet" />
    <div class="acadCalen_container">
        <div class="container">
            <div class="row">
                <div class="col banner-Container">
                </div>
            </div>
            <div class="row">
                <div class="col mt-3">
                    <div class=" text-center">
                        <span class="fs-1 fw-bold">Academic Calendar & Events</span>
                        <p class="fs-4 mt-4">
                            The University’s academic calendar lists
                        all the key dates throughout AY 2023-2024,
                        including dates for major examinations
                        and holidays.
                        </p>
                    </div>
                </div>
            </div>
            <div class="row d-flex text-center">
                <div class="col mt-4 ddl-Container">
                    <asp:DropDownList ID="ddlFiles" runat="server" CssClass="ddlFiles text-center" AutoPostBack="true" OnSelectedIndexChanged="ddlFiles_SelectedIndexChanged">
                         <asp:ListItem Text="Select File" Value="" Disabled="true" Selected="true"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="LinkButton1" CssClass="dwnldLnk btn" runat="server" Text="View/Download" OnClick="lnkDownload_Click" OnClientClick="openInNewTab();">
                        <i class="bi bi-file-earmark-arrow-down-fill"></i>
                    </asp:LinkButton>                  
                </div>
                <asp:Label ID="Selected" runat="server" ForeColor="Green" />
                <asp:Label ID="DownloadErrorLabel" runat="server" ForeColor="Red" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function openInNewTab() {
            // Get the URL of the clicked button's page
            var url = '<%= ResolveUrl("Student_AcadCalen.aspx") %>';
            window.open(url, '_blank');
            return false; // To prevent the default postback action
        }
    </script>

</asp:Content>

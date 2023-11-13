<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_deptInfo.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Department_Info.Guest_deptInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <style>
        /* Add some basic styling for the dropdown */
        .select-menu {
            text-align: center; /* Center the dropdown */
            margin-top: 40px; /* Adjust margin as needed */
        }

        .select-btn {
            position: relative;
            display: inline-block;
            cursor: pointer;
            font-size: 18px;
        }

        .select-btn i {
            position: absolute;
            top: 50%;
            right: 10px;
            transform: translateY(-50%);
        }

        /* Add styling for the active state */
        .select-menu.active .select-btn {
            background-color: #f8f9fa;
            border-radius: 8px;
        }

        /* Style the dropdown list */
        .select-dropdown {
            width: 300px; /* Adjust the width as needed */
            padding: 10px;
            border: 1px solid #ced4da;
            border-radius: 8px;
            margin-top: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            display: none;        
            background-color: #ffffff;
        }

        /* Show the dropdown list when the parent is active */
        .select-menu.active .select-dropdown {
            display: block;
        }

        /* Style for department details */
        .department-details {
            margin-top: 20px;
            text-align: center;
        }
    </style>
      <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Department Information</h1>
    <div class="containerButton">
        <div class="select-menu" id="optionMenu">
            <div class="select-btn">
                <asp:DropDownList ID="ddlDepartments" CssClass="select-dropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged">
                </asp:DropDownList>
                <i class="bx bx-chevron-down"></i>
            </div>
        </div>
    </div>

    <div class="department-details" id="departmentDetails">
        <div>
            <h2 id="lblDepartmentHead" runat="server"></h2>
            <p id="lblDepartmentDescription" runat="server"></p>
            <p id="lblDepartmentCourses" runat="server"></p>
            <p id="lblDepartmentOffHours" runat="server"></p>
            <p id="lblDepartmentContactNumber" runat="server"></p>
            <p id="lblDepartmentEmail" runat="server"></p>
        </div>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const optionMenu = document.getElementById("optionMenu");
            optionMenu.classList.add("active");
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_AcadCalen.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Academic_Calendar.Student_AcadCalen" EnableViewState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .spaceni {
            width: 100%;
            padding-bottom: 20px;
            border-bottom: 2px solid black;
        }

        .download-container {
            padding-top: 10px;
            width: 1050px;
            max-width: 100%;
            margin: 0 auto 91px;
            text-align: center;
        }

            .download-container h2 {
                font-family: Baskerville;
                line-height: 100%;
                color: #000;
                font-size: 60px;
                margin: 0;
                padding: 0;
                list-style: none;
            }

            .download-container p {
                color: #1a1a1a;
                font-size: 18px;
                margin: 27px 0 40px;
            }

            .download-container a.btn {
                margin: 0 auto;
                display: block;
                position: relative;
                width: 449px;
                line-height: 54px;
                background: #fbbf16;
                text-align: left;
                color: #1a1a1a;
                font-size: 18px;
                font-weight: 500;
                padding: 0 65px 0 34px;
            }

        .p {
            display: block;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
        }

        .non_ban_img {
            width: 1920px;
            margin-left: 50%;
            transform: translateX(-50%);
        }

        @media only screen and (max-width: 1010px) {
            .non_ban_img {
                width: 150%;
                height: auto;
                margin-left: -25%;
                transform: none;
                position: static;
            }
        }
        /* Style the dropdown button */
        .custom-dropdown {
            display: inline-block;
            text-decoration: none;
            margin: 10px;
            padding: 10px 20px;
            background-color: #003366;
            color: white; /* Set text color to white */
            font-size: 18px;
            font-weight: 500;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            text-align: center;
        }

            /* Style the dropdown list */
            .custom-dropdown:hover .dropdown-content,
            .custom-dropdown:active .dropdown-content {
                background-color: white; /* Set the background color to white */
                color: white; /* Set text color to your desired color */
            }

        /* Hover effect for the custom dropdown */
    </style>
      <!-- Banner -->
    <div id="banner">
		<div class="wrapper">
			<div class="bnr_con animatedParent animateOnce">
				<div class="non_ban animatedParent animateOnce">
				<div class="non_ban_img">
			   <img width="1920" height="531" src="../../../Resources/Images/model.png" class="attachment-full size-full wp-post-image" alt="" loading="lazy"/>							</div>
				<div class="page_title animated fadeInUp go">
			</div>
			</div>
			</div>
		</div>
	</div>
    	<div class="spaceni">
            
    	</div>
        <div class="download-container">
            <h2>Academic Calendar & Events</h2>
            <p>The University’s academic calendar lists all the key dates throughout AY 2023-24, including dates for major examinations and holidays.</p>
           <div class="dropdown-container">
             <asp:DropDownList ID="ddlFiles" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlFiles_SelectedIndexChanged" style="width: 100%;">
                <asp:ListItem Text="Select School Year" Value="" />
            </asp:DropDownList>
           </div>

            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" Text="View/Download" OnClick="lnkDownload_Click" />
          <asp:Label ID="DownloadErrorLabel" runat="server" ForeColor="Red" />
        </div>
</asp:Content>

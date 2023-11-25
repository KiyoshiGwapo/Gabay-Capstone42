<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_Dashboard.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Guest_Homepage.Guest_Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <link href="../../../Resources/CustomStyleSheet/GuestDashboard.css" rel="stylesheet" />
   <section id="homeSection"> 
        <div class="container-fluid homeSection">
            <div class="row h-100" >
                <div class="col-lg-5 col-md-12">
                    <div class="hLeft-cont">
                        <div class="hLeft d-flex flex-column text-center">
                            <h1 class="titleHeader fs-1 fw-bold">WELCOME!</h1>
                            <span class="fs-2 ">
                                <asp:Label ID="GuestName" runat="server" Text="Guest Name" CssClass="GuestName"></asp:Label></span>
                            <div class="text-secondary">to a</div>
                            <h3 class="text-secondary">Modern Approach of University Information Sharing</h3>
                            
                        </div>
                    </div>
                </div>
                <div class="col-7 d-none d-md-block">
                </div>
            </div>
        </div>
    </section>
    <section class="footer">
        <div class="d-flex flex-column align-items-center">
            <span>All Rights Reserved</span>
            <span>For more info please contact Us: Gabay_Team7@gmail.com</span>
        </div>
    </section>
    
</asp:Content>

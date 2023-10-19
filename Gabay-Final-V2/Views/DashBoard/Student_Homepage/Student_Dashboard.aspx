<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_Dashboard.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Student_Homepage.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        .card-container{
            height:500px;
            display: flex;
            justify-content: space-evenly;
        }
        .card{
            margin:13px;
            width:25rem;
            height:100%;
            border: 2px solid #425ab7;
            border-radius: 20px;
            box-shadow: 10px 10px 8px #888888;
        }
        .image-container{
            max-width: 100%;
            height: 250px;
            overflow: hidden;
            border-radius: 18px 18px 0 0;
        }
        .imagePlaceholder{
            width: 100%;
            height: 100%;
        }
        .learnMoreBtn{
            height: 45px;
            width: 130px;
            color:white;
            font-size:18px;
        }
        .carousel .card {
            display: none;
            transition: transform 0.5s ease-in-out;
            transform: translateX(100%);
        }

        .carousel .card.active {
            display: block;
            transform: translateX(0);
        }
    </style>
     <script src="../Scripts/jquery-3.7.1.js"></script>
    <div class="container">
        <h1 class="text-center mt-4">Announcements</h1>
        <!-- Manual controls for sliding (moved to the right) -->
        <div class="d-flex justify-content-end mt-3">
            <button type="button" class="btn btn-light mr-2" id="prevButton">&lt;</button>
            <button type="button" class="btn btn-light" id="nextButton">&gt;</button>
        </div>
        <!-- Announcement Container -->
        <div class="card-container">
            <asp:Repeater runat="server" ID="rptAnnouncements">
                <ItemTemplate>
                    <div class=" carousel">
                        <div class=" carousel-inner">
                            <div class="card">
                                <div class="image-container">
                                    <asp:Image ID="imgPlaceholder" runat="server" class="img-fluid imagePlaceholder" alt="..."
                                        ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ImagePath")) %>' />
                                </div>
                                <div class="p-2 d-flex justify-content-center align-items-center flex-column">
                                    <p><%# Eval("Date", "{0:MMMM-dd-yyyy}") %></p>
                                    <span class="card-title fs-2 fw-medium"><%# Eval("Title") %></span>
                                    <p class="card-text text-center"><%# Eval("ShortDescription") %></p>
                                    <asp:LinkButton ID="learnMoreBtn" CssClass="btn bg-primary learnMoreBtn text-center"
                                        runat="server" OnClientClick='<%# "setAnnouncementID(" + Eval("AnnouncementID") + ");" %>'
                                        OnClick="learnMoreBtn_Click">
                                            Learn More
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <%-- Detailed Modal --%>
    <div class="modal fade" id="dtldModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="modal-title fs-5" id="detailedHeader">
                        <asp:Label ID="dtldTitle" runat="server" Text="Title"></asp:Label>
                    </span>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="image-container">
                        <asp:Image ID="dtldimgPlaceholder" runat="server" class="img-fluid imagePlaceholder" alt="..." 
                            ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ImagePath")) %>'/>
                    </div>
                    <div class="d-flex justify-content-center flex-column">
                        <asp:Label ID="dtldDate" runat="server" Text="Date"></asp:Label>
                        <asp:Label ID="dtldDescrp" runat="server" Text="Detailed Description"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        var currentSlide = 0;
        var totalSlides = 0;
        var slidingInterval;

        $(document).ready(function () {
            // Initialize the total number of slides
            totalSlides = $(".card").length;

            // Show the initial set of slides
            showSlides(currentSlide);

            // Add event handlers for "Previous" and "Next" buttons
            $("#prevButton").click(prevSlide);
            $("#nextButton").click(nextSlide);

            // Add event handlers for card hover
            $(".card").hover(stopSliding, startSliding);

            // Start automatic sliding
            startSliding();
        });

        function showSlides(currentSlide) {
            // Remove the "active" class from all cards
            $(".card").removeClass("active");

            // Calculate the next three slides in a looped manner
            var firstIndex = currentSlide % totalSlides;
            var secondIndex = (currentSlide + 1) % totalSlides;
            var thirdIndex = (currentSlide + 2) % totalSlides;

            // Add the "active" class to the selected cards for animation
            $(".card").eq(firstIndex).addClass("active");
            $(".card").eq(secondIndex).addClass("active");
            $(".card").eq(thirdIndex).addClass("active");
        }

        // Function to show the next set of slides
        function nextSlide() {
            currentSlide = (currentSlide + 1) % totalSlides;
            showSlides(currentSlide);
        }

        // Function to show the previous set of slides
        function prevSlide() {
            currentSlide = (currentSlide - 1 + totalSlides) % totalSlides;
            showSlides(currentSlide);
        }

        // Function to stop automatic sliding
        function stopSliding() {
            clearInterval(slidingInterval);
        }

        // Function to start automatic sliding
        function startSliding() {
            slidingInterval = setInterval(nextSlide, 5000); // Change slide every 5 seconds
        }
        function setAnnouncementID(announcementID) {
            document.getElementById('<%= HiddenField1.ClientID %>').value = announcementID;
        }
    </script>
</asp:Content>

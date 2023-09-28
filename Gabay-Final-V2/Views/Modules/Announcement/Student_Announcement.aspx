<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_Announcement.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Announcement.Student_Announcement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
      <div class="container">
        <h1 class="text-center mt-4">Announcements</h1>
        <!-- Manual controls for sliding (moved to the right) -->
        <div class="d-flex justify-content-end mt-3">
                <button class="btn btn-light mr-2" id="prevButton">&lt;</button>
                <button class="btn btn-light" id="nextButton">&gt;</button>
         </div>
        <!-- Announcement Container -->
        <div class="announcement-container">
           <asp:Repeater ID="rptAnnouncements" runat="server">
            <ItemTemplate>
                <!-- Inside the Repeater ItemTemplate -->
                <div class="announcement-card">
                    <div class="image-container">
                        <img src='<%# ResolveUrl(Eval("ImagePath").ToString()) %>' alt="Announcement Image" />
                        <div class="announcement-details">
                            <div class="announcement-date">
                                <p><%# Eval("Date") %></p>
                            </div>
                            <div class="announcement-title">
                                <h2><%# Eval("Title") %></h2>
                            </div>
                            <p>Short Description</p>
                            <div class="announcement-description">
                                <p><%# Eval("ShortDescription") %></p>
                            </div>
                            <div class="button-container">
                                <button class="learn-more-btn" data-toggle="modal" data-target='<%# "#announcementModal" + Eval("AnnouncementID") %>' data-id='<%# Eval("AnnouncementID") %>'>Learn More</button>
                            </div>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
        </div>
           </div>

    <script>
        // JavaScript to handle modal functionality
        $(document).ready(function () {
            $('.learn-more-btn').click(function () {
                var announcementID = $(this).data('id');
                var modal = $('.modal[data-announcementid="' + announcementID + '"]');
                // Rest of your code to show the modal...
            });
        })
    </script>
    <!-- Para Slide2x -->
    <script>
        var currentSlide = 0;
        var totalSlides = 0;
        var slidingInterval;

        $(document).ready(function () {
            // Initialize the total number of slides
            totalSlides = $(".announcement-card").length;

            // Show only the first three slides initially
            showSlides(currentSlide, currentSlide + 2);

            // Add event handlers for "Previous" and "Next" buttons
            $("#prevButton").click(prevSlide);
            $("#nextButton").click(nextSlide);

            // Start automatic sliding
            slidingInterval = setInterval(nextSlide, 5000); // Change slide every 5 seconds
        });

        // Function to display slides within a range
        function showSlides(startIndex, endIndex) {
            $(".announcement-card").hide();
            $(".announcement-card").slice(startIndex, endIndex + 1).show();
        }

        // Function to show the next set of slides
        function nextSlide() {
            currentSlide = (currentSlide + 3) % totalSlides;
            var endIndex = currentSlide + 2;
            showSlides(currentSlide, endIndex);
        }

        // Function to show the previous set of slides
        function prevSlide() {
            if (currentSlide > 0) {
                currentSlide = (currentSlide - 3 + totalSlides) % totalSlides;
                var endIndex = currentSlide + 2;
                showSlides(currentSlide, endIndex);
            }
            // Stop automatic sliding when clicking "Previous"
            clearInterval(slidingInterval);
        }
    </script>





    <style>
        /* Container for Announcements */
        .announcement-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            gap: 20px;
            padding: 20px;
        }

        .announcement-card {
            width: calc(33.33% - 20px); /* 33.33% with a gap of 20px between cards */
            display: flex;
            flex-direction: column;
            border: 2px solid #3498db;
            border-radius: 15px;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.2);
            height: 700px; /* Adjust this value as needed */
            overflow: hidden;
        }


        .image-container {
            flex-grow: 1;
            text-align: center;
        }

        .image-container img {
            max-width: 100%;
            max-height: 200px;
            object-fit: cover;
            border-radius: 10px;
        }

       .announcement-details {
            flex-grow: 1;
            display: flex;
            flex-direction: column;
            justify-content: flex-end; /* Align elements to the bottom */
            padding: 10px 0;
        }

        .announcement-date,
        .announcement-title,
        .announcement-description {
            margin-bottom: 10px; /* Add some space between elements */
        }

        .announcement-date {
            font-size: 14px;
            color: #777;
        }

        .announcement-title {
            font-size: 20px;
            font-weight: bold;
            color: #333;
        }

        .announcement-description {
            font-size: 16px;
            color: #555;
            max-height: 200px;
            max-width: auto;
            overflow-y: auto;
            max-height: auto;
            text-align: justify-all;
        }


       .button-container {
            display: flex;
            justify-content: flex-end; 
            align-items: center; /* Center the button vertically */
            flex-direction: column; /* Stack the button below other content */
            margin-top: auto;
        }

        .learn-more-btn {
            background-color: #3498db;
            color: #fff;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.2s;
        }

        .learn-more-btn:hover {
            background-color: #2980b9;
        }
    </style>
    <script>
        function openModal(announcementID) {
            $('#announcementModal' + announcementID).modal('show');
        }
    </script>
</asp:Content>

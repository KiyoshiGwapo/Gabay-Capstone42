<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <link href="../FontAwesome/css/all.css" rel="stylesheet" />
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../Resources/CustomStyleSheet/Test.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <script src="../FontAwesome/js/all.js"></script>
    <style>
        .slider-container {
            overflow: hidden;
            width:300px;
            display: flex;
            align-items: center;
            padding: 10px;
        }

        .cards {
            display: flex;
            transition: transform 0.5s;
        }

        .card {
            width: 300px;
            margin-right: 10px;
            display:flex !important;
        }

        .hidden-card {
            width: 300px;
            height: 300px;
            display: none;
        }

        .container-slider{
            display: flex;
            align-items:center;
        }

        .buttons{
            margin: 10px;
            height:30px;
            width: 10px;
            font-size:20px;
            font-weight:bold;
            border:none;
        }

        .card-title{
            font-size: 15px;
            font-weight: bold;
        }

        .choices{
            display: flex;
            flex-direction: column;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="container-fluid chatbot-container">
           <div class="header">
               <span>Chat with Gab</span>
           </div>
           <div class="chat-container">
               <div class="message-container bot-message">Hello, Kent Gerald! to assist you better, 
                   please choose an option in the menu or if you can't find what are you looking for,
                   just type your concern in a few words. 
               </div>
               <div class="message-container bot-message">and if I can't answer you queries you can book
                   and appointment to a designated department for your concern
               </div>
               <div class="message-container bot-message-menu">
                   <div class="container-slider">
                       <button id="prevButton" type="button" class="btn buttons d-flex justify-content-center align-items-center">
                           <i class="bi bi-chevron-compact-left"></i>
                       </button>
                       <div class="slider-container">
                           <div class="cards">
                               <div class="card bot-message" style="width: 15rem;">
                                   <div class="card-body mt-auto">
                                       <span class="card-title">Admission Process</span>
                                       <p class="card-text card-body-p">Students who wish to enroll in the campus</p>
                                       <div class="choices">
                                           <a href="#" class="btn btn-primary mb-1">New Student</a>
                                           <a href="#" class="btn btn-primary mb-1">Old Student</a>
                                           <a href="#" class="btn btn-primary mb-1">More</a>
                                       </div>
                                   </div>
                               </div>
                               <div class="card bot-message" style="width: 15rem;">
                                   <div class="card-body mt-auto">
                                       <span class="card-title">Scholarship Program</span>
                                       <p class="card-text card-body-p">Scholarship programs offered by the university.</p>
                                       <div class="choices">
                                           <a href="#" class="btn btn-primary mb-1">Academic</a>
                                           <a href="#" class="btn btn-primary mb-1">Working</a>
                                           <a href="#" class="btn btn-primary mb-1">More</a>
                                       </div>
                                   </div>
                               </div>
                               <div class="card bot-message" style="width: 15rem;">
                                   <div class="card-body">
                                       <span class="card-title">Academics Program</span>
                                       <p class="card-text card-body-p">Academics that are offered by the university</p>
                                       <div class="choices">
                                           <a href="#" class="btn btn-primary mb-1">Colleges</a>
                                           <a href="#" class="btn btn-primary mb-1">Basic Education</a>
                                           <a href="#" class="btn btn-primary mb-1">More</a>
                                       </div>
                                   </div>
                               </div>
                               <div class="card bot-message" style="width: 15rem;">
                                   <div class="card-body">
                                       <span class="card-title">About UC</span>
                                       <p class="card-text card-body-p">Academics that are offered by the university</p>
                                       <div class="choices">
                                           <a href="#" class="btn btn-primary mb-1">Campus</a>
                                           <a href="#" class="btn btn-primary mb-1">History</a>
                                           <a href="#" class="btn btn-primary mb-1">More</a>
                                       </div>
                                   </div>
                               </div>
                           </div>
                       </div>
                       <button id="nextButton" type="button" class="btn buttons d-flex justify-content-center align-items-center">
                           <i class="bi bi-chevron-compact-right"></i>
                       </button>
                   </div>
               </div>
               <div class="message-container user-message">Hi, How to enroll in University of Cebu?</div>
           </div>
           <div class="input-group userInput">
               <asp:TextBox ID="userInput" runat="server" CssClass="form-control" aria-describedby="sndBtn" placeholder="Enter your message here..."></asp:TextBox>
               <asp:Button ID="sndBtn" runat="server" Text="Send" CssClass="btn btn-outline-secondary" UseSubmitBehavior="false"/>
           </div>
       </div>
    </form>
    <script>
        const cardsContainer = document.querySelector(".cards");
        const cardWidth = 240; // Adjust this based on card width and margin
        const nextButton = document.getElementById("nextButton");
        const prevButton = document.getElementById("prevButton");
        const cardCount = 4; // Change this to the total number of cards

        let currentPosition = 0;

        updateButtonVisibility(); // Initialize button visibility

        nextButton.addEventListener("click", () => {
            currentPosition -= cardWidth;
            if (currentPosition < -(cardWidth * (cardCount - 1))) {
                currentPosition = 0;
            }
            updateSliderPosition();
            updateButtonVisibility();
        });

        prevButton.addEventListener("click", () => {
            currentPosition += cardWidth;
            if (currentPosition > 0) {
                currentPosition = -(cardWidth * (cardCount - 1));
            }
            updateSliderPosition();
            updateButtonVisibility();
        });

        function updateSliderPosition() {
            cardsContainer.style.transform = `translateX(${currentPosition}px)`;
        }

        function updateButtonVisibility() {
            if (currentPosition === 0) {
                prevButton.style.opacity = "0%";
                nextButton.style.opacity = "100%";
                prevButton.disabled = true;
                nextButton.disabled = false;
            } else if (currentPosition === -(cardWidth * (cardCount - 1))) {
                prevButton.style.opacity = "100%";
                nextButton.style.opacity = "0%";
                prevButton.disabled = false;
                nextButton.disabled = true;
            } else {
                prevButton.style.opacity = "100%";
                nextButton.style.opacity = "100%";
                prevButton.disabled = false;
                nextButton.disabled = false;
            }
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm19.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm19" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css"/>
    <style>
        .slider-container {
            overflow: hidden;
            width: 300px;
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
            height: 250px;
            margin-right: 10px;
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
            font-size:20px;
            font-weight:bold;
            border:none;
        }

        .card-title{
            font-size: 15px;
            font-weight: bold;
        }

        .choices{
            margin-top: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-slider">
            <button id="prevButton" type="button" class="btn buttons d-flex justify-content-center align-items-center">
                <i class="bi bi-chevron-compact-left"></i>
            </button>
            <div class="slider-container">
                <div class="cards">
                    <div class="card" style="width: 15rem;">
                        <div class="card-body">
                            <span class="card-title">Enrollment Process</span>
                            <p class="card-text card-body-p">New/Old students who wish to enroll in the campus</p>
                            <div class="d-grid choices">
                                <a href="#" class="btn btn-primary mb-1">Requirements</a>
                                <a href="#" class="btn btn-primary mb-1">Enrollment Date</a>
                                <a href="#" class="btn btn-primary mb-1">Choice 3</a>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 15rem;">
                        <div class="card-body">
                            <span class="card-title">Scholarship Program</span>
                            <p class="card-text card-body-p">Scholarship programs offered by the university.</p>
                            <div class="d-grid choices">
                                <a href="#" class="btn btn-primary mb-1">Academic</a>
                                <a href="#" class="btn btn-primary mb-1">Working</a>
                                <a href="#" class="btn btn-primary mb-1">More</a>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 15rem;">
                        <div class="card-body">
                            <h5 class="card-title">Card title</h5>
                            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                            <a href="#" class="btn btn-primary">Go somewhere</a>
                        </div>
                    </div>
                    <div class="card" style="width: 15rem;">
                        <div class="card-body">
                            <h5 class="card-title">Card title</h5>
                            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                            <a href="#" class="btn btn-primary">Go somewhere</a>
                        </div>
                    </div>
                </div>
            </div>
            <button id="nextButton" type="button" class="btn buttons d-flex justify-content-center align-items-center">
                <i class="bi bi-chevron-compact-right"></i>
            </button>
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

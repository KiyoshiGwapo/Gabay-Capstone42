<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm20.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm20" EnableViewState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8"/>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../Resources/CustomStyleSheet/Chatbot/ChatbotStyle.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="form1 d-flex justify-content-center">
        <div class="chatbot-wrapper">
            <div class="chatbot-container">
                <div class="header custom-background custom-font">
                    <span>Chat with Gab</span>
                </div>
                <div class="chatContainer" id="chatContainer" runat="server"></div>
                <div class="input-group userInput-group">
                    <asp:TextBox ID="txtUserInput" runat="server" CssClass="form-control userInput" aria-describedby="btnSend" placeholder="Enter your message here..."></asp:TextBox>
                    <asp:Button ID="btnSend" runat="server" CssClass="btn sndBtn" Text="Send" OnClick="btnSend_Click" />
                </div>
            </div>
        </div>
    </form>
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            const studentForm = document.querySelector('.form1');

            function scrollToBottom() {
                var chatContainer = studentForm.querySelector('.chatContainer');
                chatContainer.scrollTop = chatContainer.scrollHeight;
            }
            scrollToBottom();

           <%-- function buttonClick(buttonText) {
                // Send the clicked button's text as user input to the server
                document.getElementById('<%= txtUserInput.ClientID %>').value = buttonText;
                document.getElementById('<%= btnSend.ClientID %>').click();
            }--%>
           

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

        });
        function menuButtonClick(choice) {
            document.getElementById('<%= txtUserInput.ClientID %>').value = choice;
             document.getElementById('<%= btnSend.ClientID %>').click();
         }
    </script>
</body>
</html>

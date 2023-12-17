<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm20.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm20" EnableViewState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8"/>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../Resources/CustomStyleSheet/Chatbot/ChatbotStyle.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

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
                </div>x
            </div>
        </div>
    </form>
    <script>
        //automatically scroll to the bottom
        function scrollToBottom() {
            var chatContainer = document.querySelector('.chatContainer');
            chatContainer.scrollTop = chatContainer.scrollHeight;
        }
        scrollToBottom();

        function menuButtonClick(choice) {
            document.getElementById('<%= txtUserInput.ClientID %>').value = choice;
            document.getElementById('<%= btnSend.ClientID %>').click();
        }

        const cardsContainer = document.querySelector(".cards");
        const cardWidth = 240; // Adjust this based on card width and margin
        const NextButton = document.querySelector('.nextButton');
        const PrevButton = document.querySelector('.prevButton');
        const cardCount = 4; // Change this to the total number of cards

        let currentPosition = 0;

        updateButtonVisibility(); // Initialize button visibility

        window.onload = NextButton.addEventListener("click", () => {
            currentPosition -= cardWidth;
            if (currentPosition < -(cardWidth * (cardCount - 1))) {
                currentPosition = 0;
            }
            updateSliderPosition();
            updateButtonVisibility();
        });

        window.onload = PrevButton.addEventListener("click", () => {
            currentPosition += cardWidth;
            if (currentPosition > 0) {
                currentPosition = -(cardWidth * (cardCount - 1));
            }
            updateSliderPosition();
            updateButtonVisibility();
        });

        menuNextButton.addEventListener("click", () => {
            currentPosition -= cardWidth;
            if (currentPosition < -(cardWidth * (cardCount - 1))) {
                currentPosition = 0;
            }
            updateSliderPosition();
            updateButtonVisibility();
        });

        menuPrevButton.addEventListener("click", () => {
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
                PrevButton.style.opacity = "0%";
                NextButton.style.opacity = "100%";
                PrevButton.disabled = true;
                NextButton.disabled = false;
            } else if (currentPosition === -(cardWidth * (cardCount - 1))) {
                PrevButton.style.opacity = "100%";
                NextButton.style.opacity = "0%";
                PrevButton.disabled = false;
                NextButton.disabled = true;
            } else {
                PrevButton.style.opacity = "100%";
                NextButton.style.opacity = "100%";
                PrevButton.disabled = false;
                NextButton.disabled = false;
            }
        }

    </script>
</body>
</html>

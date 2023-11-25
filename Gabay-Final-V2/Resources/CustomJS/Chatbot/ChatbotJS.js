document.addEventListener("DOMContentLoaded", () => {
    const studentFom = document.querySelector('.studentMaster');

    function scrollToBottom() {
        var chatContainer = studentFom.querySelector('.chatContainer');
        chatContainer.scrollTop = chatContainer.scrollHeight;
    }
    scrollToBottom();

    function buttonClick(buttonText) {
        // Send the clicked button's text as user input to the server
        document.getElementById('<%= txtUserInput.ClientID %>').value = buttonText;
        document.getElementById('<%= btnSend.ClientID %>').click();
    }
    function scrollToBottom() {
        var chatContainer = document.querySelector('.chatContainer');
        chatContainer.scrollTop = chatContainer.scrollHeight;
    }
    scrollToBottom();

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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm10.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <link href="../Resources/CustomStyleSheet/Chatbot/ChatbotStyle.css" rel="stylesheet" />
    <style>
        .button-container{
            margin-top:3px;
        }
        .button-container button{
            margin-right:3px;
            margin-bottom: 5px;
        }
        .predefined-button{
            height:35px;
            background-color: #003366;
            color: white;
            border-radius: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
    <script>
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
    </script>
</body>
</html>

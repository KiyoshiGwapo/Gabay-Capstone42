<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm20.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm20" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8"/>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../Resources/CustomStyleSheet/Chatbot/ChatbotStyle.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
    <script src="../Resources/CustomJS/Chatbot/ChatbotJS.js"></script>

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
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
    <script>
        function menuButtonClick(choice) {
            document.getElementById('<%= txtUserInput.ClientID %>').value = choice;
            document.getElementById('<%= btnSend.ClientID %>').click();
        }
    </script>
</body>
</html>

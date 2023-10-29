<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm17.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm17" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="SendEmailButton" runat="server" Text="Send Email" OnClick="SendEmailButton_Click" />
            <asp:Label ID="SuccessLabel" runat="server" ForeColor="Green" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>

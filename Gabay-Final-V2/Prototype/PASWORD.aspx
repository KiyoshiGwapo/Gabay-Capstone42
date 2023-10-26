<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PASWORD.aspx.cs" Inherits="Gabay_Final_V2.Prototype.PASWORD" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retrieve Password</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="text-center">Retrieve Password</h2>
            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtUserID" runat="server" placeholder="Enter your User ID" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter your Email" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnRetrievePassword" runat="server" Text="Retrieve Password" OnClick="btnRetrievePassword_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</body>
</html>

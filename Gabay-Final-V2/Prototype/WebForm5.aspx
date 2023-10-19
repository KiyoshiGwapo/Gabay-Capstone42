<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:Button ID="Button1" CssClass="form-control bg-primary text-light" runat="server" Text="Button" OnClick="Button1_Click" UseSubmitBehavior="false"/>
            <br />
            <asp:TextBox ID="TextBox2" style="height:300px; width:300px;" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />

            <asp:DropDownList ID="DropDownList1" runat="server">

            </asp:DropDownList>
        </div>
    </form>
</body>
</html>

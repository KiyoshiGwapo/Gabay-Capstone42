<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm22.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm22" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <div class="errorTest">
                <span class="text-danger"></span>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox1" ValidationGroup="group1"></asp:RequiredFieldValidator>

            <script type="text/javascript">
                function validateClientSide() {
                    var txtName = document.getElementById('<%= TextBox1.ClientID %>');
                    if (txtName.value === '') {
                        alert('Name cannot be empty.');
                        return false;
                    }
                    return true;
                }
            </script>
            <asp:Button ID="Button1" runat="server" Text="Button" ValidationGroup="group1" />
        </div>
    </form>
</body>
</html>

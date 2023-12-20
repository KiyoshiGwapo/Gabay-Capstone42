<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm24.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm24" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:GridView ID="GridViewStudents" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered text-center" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID_student" HeaderText="ID_student" SortExpression="ID" ItemStyle-CssClass="" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="" />
                <asp:BoundField DataField="StudentDepartment" HeaderText="Student Department" SortExpression="StudentDepartment" ItemStyle-CssClass="" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <%--             <asp:LinkButton ID="lnkEdit" runat="server" Text='<i class="fas fa-edit"></i>' CssClass="btn btn-primary" OnClientClick='<%# "showEditPasswordModal(" + Container.DataItemIndex + "); return false;" %>' />--%>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text='<i class="fas fa-trash-alt" style="color: white;"></i>' CssClass="btn btn-danger" OnClientClick='<%# "showConfirmationModal(" + Container.DataItemIndex + "); return false;" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Users.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <asp:GridView runat="server" ID="usrsTbl" CssClass="table table-striped" HeaderStyle-CssClass="text-center sticky-top" AlternatingRowStyle-BackColor="#c1d6fe" runat="server" AutoGenerateColumns="False" OnRowEditing="ScriptTable_RowEditing" DataKeyNames="res_ID">
                    <Columns>
                        <asp:TemplateField>
                            <asp:Label ID="res_ID" runat="server" Text='<%# Bind("res_ID") %>'></asp:Label>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

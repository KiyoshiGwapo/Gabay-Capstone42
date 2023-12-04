<%@ Page Title="Manage Chatbot" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Manage_Chatbot.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Admin_Modules.Manage_Chatbot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Resources/CustomStyleSheet/Chatbot/ManageChatbotStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Manage Chatbot</h1>
    <div class="manageChatbot-container">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4 col-md-12">
                    <div class="scriptInputs">
                        <asp:TextBox ID="ScriptTextArea" CssClass="form-control ScriptTextArea" runat="server" TextMode="MultiLine" placeholder="Write script here..."></asp:TextBox>
                        <asp:TextBox ID="KeywordTextArea" CssClass="form-control KeywordTextArea" runat="server" placeholder="Write keywords here..."></asp:TextBox>
                        <div class="keywordError text-danger d-none" id="keywordError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please provide commas everyword and avoid spaces</span>
                        </div>
                    </div>
                    <div id="saveBtnCont" class="d-grid" runat="server">
                        <asp:Button ID="scriptBtn" CssClass="btn scrptBtn" runat="server" Text="Add Script" OnClick="scriptBtn_Click" />
                    </div>
                    <div id="updtBtnCont" class="d-flex justify-content-evenly d-none" runat="server">
                        <asp:Button ID="updtBtn" CssClass="btn updtBtn" runat="server" Text="Update" UseSubmitBehavior="false" OnClick="updtBtn_Click"/>
                        <asp:Button ID="cnclBtn" CssClass="btn updtBtn" runat="server" Text="Cancel" UseSubmitBehavior="false" OnClick="cnclBtn_Click"/>
                    </div>
                </div>
                <div class="col-lg-8 col-md-12 tableArea">
                    <div class="ScriptTable-container">
                        <asp:GridView ID="ScriptTable" CssClass="table table-striped scriptTable" HeaderStyle-CssClass="text-center sticky-top" AlternatingRowStyle-BackColor="#c1d6fe" runat="server" AutoGenerateColumns="False" OnRowEditing="ScriptTable_RowEditing" DataKeyNames="res_ID" OnRowDeleting="ScriptTable_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="Script ID" ItemStyle-CssClass="cntrlItm">
                                    <ItemTemplate>
                                        <asp:Label ID="res_ID" runat="server" Text='<%# Bind("res_ID") %>'></asp:Label>
                                    </ItemTemplate> 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Script Content">
                                    <ItemTemplate>
                                        <asp:Label ID="ScriptContentLabel" runat="server" Text='<%# Bind("response") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Keywords">
                                    <ItemTemplate>
                                        <asp:Label ID="KeywordsLabel" runat="server" Text='<%# Bind("keywords") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="cntrlItm" ControlStyle-CssClass="btn cntrlBtn">
                                    <ItemTemplate>
                                        <asp:Button ID="EditButton"  runat="server" Text="Edit" CommandName="Edit" UseSubmitBehavior="false"/>
                                        <asp:Button ID="DeleteButton"  runat="server" Text="Delete" CommandName="Delete" UseSubmitBehavior="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../../../Resources/CustomJS/Chatbot/ManageChatbot.js"></script>
      <%-- Success modal --%>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <p id="successMessage"></p>
                </div>
            </div>
        </div>
    </div>
     <%-- Error modal --%>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-danger text-center text-light">
                    <i class="bi bi-exclamation-circle-fill"></i>
                    <p id="errorMessage"></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

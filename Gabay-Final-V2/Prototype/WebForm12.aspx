<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm12.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm12" EnableEventValidation="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <link href="../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        .AnnouncementList td {
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container-fluid">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#toAddModal">
                Add Announcement
            </button>
            <asp:GridView ID="AnnouncementList" HeaderStyle-CssClass="text-center" runat="server" AutoGenerateColumns="false" CssClass="table AnnouncementList" DataKeyNames="ex_ID">
                <Columns>
                    <asp:BoundField DataField="ex_ID" HeaderText="ID" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Name" HeaderText="Title" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"
                                ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ImgPath")) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Address" HeaderText="Short Description" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="gridviewEdit" runat="server" Text="Button" CssClass="btn bg-primary text-light"
                                OnClientClick='<%# "return getAnnouncementID(" + Eval("ex_ID") + ");" %>'
                                OnClick="gridviewEdit_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="HidAnnouncementID" runat="server" />
        </div>
        <%-- to add modal --%>
        <div class="modal fade" id="toAddModal" tabindex="-1" aria-labelledby="AddModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addNamebx" CssClass="form-control" runat="server" placeholder="addNamebx"></asp:TextBox>
                            <label for="addNamebx">Name</label>
                        </div>
                        <div class="mb-3">
                            <asp:FileUpload ID="addFilebx" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="addAddressbx" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                            <label for="addAddressbx">Address</label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="SaveAnnouncement" runat="server" Text="Add Announcement" CssClass="btn bg-primary text-light" OnClick="SaveAnnouncement_Click"/>
                    </div>
                </div>
            </div>
        </div>
        <%-- to edit modal --%>
        <div class="modal fade" id="toEditModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Button ID="closeEditModal" runat="server" CssClass="btn-close"/>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="Titlebx" CssClass="form-control" runat="server" placeholder="Title"></asp:TextBox>
                                <label for="Titlebx">Title</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="Datebx" CssClass="form-control" runat="server" placeholder="Title" TextMode="Date"></asp:TextBox>
                                <label for="Datebx">Date</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="ShortDescbx" CssClass="form-control" runat="server" placeholder="Short Description"></asp:TextBox>
                                <label for="ShortDescbx">Short Description</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="DtlDescBx" CssClass="form-control DtlDescBx" style="height: 100px" runat="server" placeholder="Detailed Description" TextMode="MultiLine"></asp:TextBox>
                                <label for="DtlDescBx">Detailed Description</label>
                            </div>
                            <div class="mb-3">
                                <asp:FileUpload ID="Imgbx" CssClass="form-control" runat="server"/>
                            </div>
                            <div class=" d-grid ">
                                <asp:Button ID="updtAnnouncement" CssClass="btn bg-primary" runat="server" Text="Update Announcement"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        function getAnnouncementID(id) {
            document.getElementById('<%= HidAnnouncementID.ClientID %>').value = id;
         }
    </script>
</body>
</html>

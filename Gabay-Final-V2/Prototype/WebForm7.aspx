<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="header text-center mt-4">
                        <div class="mb-3">
                            <img class="picCont img-fluid" src="../Resources/Images/tempIcons/ccs%20logo.png" />
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="deptName" runat="server" Text="College of Computer Studies" CssClass="fs-1 fw"></asp:Label>
                        </div>
                    </div>
                    <hr  />
                    <div class="body mt-5">
                        <div class=" d-flex justify-content-center">
                            <div class="deptDescCont">
                                <asp:Label CssClass="fs-3" ID="Label1" runat="server"> College of computer studies is study about programming and computers</asp:Label>
                            </div>
                        </div>
                        <div class="bodyContent">
                            <span>Dean: Dr.Aurora Miro</span>
                        </div>
                        <div>
                            <span>Courses Offerred: Bachelor of Information Technology</span>
                        </div>
                         <div>
                            <span>Office Hours: 8:00 AM - 5:00 PM</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

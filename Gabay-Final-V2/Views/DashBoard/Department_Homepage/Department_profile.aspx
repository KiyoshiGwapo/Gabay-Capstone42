<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Department_profile.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Department_Homepage.Department_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.min.js"></script>
    <link href="../../../Resources/CustomStyleSheet/Dept_profile/DeptProfStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-center align-items-center">
                    <%--<img class="picCont img-fluid" src="../Resources/Images/tempIcons/ccs%20logo.png" />--%>
                </div>
                <button type="button" class="btn float-end editButton" data-bs-toggle="modal" data-bs-target="#editModal">
                    <i class="bi bi-pencil-square fs-4"></i>
                </button>
                <div class="text-center">
                    <asp:Label ID="deptName" CssClass="fs-1 fw-bold" runat="server" Text="Department name"></asp:Label>
                </div>
            </div>
            <div class="col-12">
                <div class="departmentInfo">
                    <ul>
                        <li>
                            <hr />
                        </li>
                        <li class="headerLi">
                            <asp:Label ID="deptDesc" runat="server" CssClass="fs-3" Text="Description"></asp:Label>
                        </li>
                        <li>
                            <hr />
                        </li>
                        <li>
                            <div class="departmentInfoContainer row">
                                <div class="col-4">
                                    <h3>Department information</h3>
                                </div>
                                <div class="col-8">
                                    <ul>
                                        <li>
                                            <div class="deanCont">
                                                <div>
                                                    <span class="fs-5">Dean/Department Head:</span>
                                                </div>
                                                <asp:Label ID="deptHead" runat="server" Text="Department Head"></asp:Label>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="courseCont">
                                                <div>
                                                    <span class="fs-5">Degree programs:</span>
                                                </div>
                                                <div>
                                                    <asp:Label ID="courses" runat="server" Text="Courses"></asp:Label>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="offhrsCont">
                                                <div>
                                                    <span class="fs-5">Office hours:</span>
                                                </div>
                                                <asp:Label ID="offHrs" runat="server" Text="Time"></asp:Label>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="FilesCont">
                                                <div>
                                                    <span class="fs-5">Files:</span>
                                                </div>
                                                <asp:DropDownList ID="ddlFiles" runat="server" CssClass="ddlFiles text-center" AutoPostBack="true" OnSelectedIndexChanged="ddlFiles_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select School Year" Value="" />
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="LinkButton1" CssClass="dwnldLnk btn" runat="server" Text="View/Download" OnClick="lnkDownload_Click" OnClientClick="openInNewTab();">
                        <i class="bi bi-file-earmark-arrow-down-fill"></i>
                                                </asp:LinkButton>
                                                <asp:Label ID="Selected" runat="server" ForeColor="Green" />
                                            </div>
                                            <asp:Label ID="DownloadErrorLabel" runat="server" ForeColor="Red" />


                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </li>
                        <li>
                            <hr />
                        </li>
                        <li>
                            <div class="contactInformation row">
                                <div class="col-4">
                                    <h3>Contact information</h3>
                                </div>
                                <div class="col-8">
                                    <ul>
                                        <li>
                                            <div>
                                                <span class="fs-5">Contact number:</span>
                                            </div>
                                            <asp:Label ID="conNum" runat="server" Text="Contact number"></asp:Label>
                                        </li>
                                        <li>
                                            <div>
                                                <span class="fs-5">Email:</span>
                                            </div>
                                            <asp:Label ID="emailbx" runat="server" Text="Email"></asp:Label>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <%-- shows up when edit button is clicked --%>
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 d-flex justify-content-around align-items-center p-4">
                                <asp:Label ID="m_deptName" CssClass="fs-3" runat="server" Text="Department Name"></asp:Label>
                            </div>
                            <div class="col-12">
                                <div class="border-modal border boder-black">
                                    <div class="lgn p-3" data-bs-toggle="modal" data-bs-target="#loginCredentialsModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Login Credentials</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                    <div class="dpt p-3" data-bs-toggle="modal" data-bs-target="#deptInfoModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Department Information</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                    <div class="dpt p-3" data-bs-toggle="modal" data-bs-target="#coursesModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Degree Programs</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                    <div class="cn p-3" data-bs-toggle="modal" data-bs-target="#ContactNumberModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Contact Information</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                    <div class="cn p-3" data-bs-toggle="modal" data-bs-target="#filesModal">
                                        <div class="d-flex justify-content-between modal-items">
                                            <span>Files</span>
                                            <span><i class="bi bi-chevron-right"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- modal for login credentials update --%>
    <div class="modal fade" id="loginCredentialsModal" aria-hidden="true" aria-labelledby="lgnCrndtls" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="modal-header">
                            <span class="modal-title modalRouting fs-5" id="lgnCrndtls" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <span class="fs-4">Login Credentials</span>
                            <div class="p-2">
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="lgnIDBx" CssClass="lgnIDBx form-control" runat="server" placeholder="name@example.com"></asp:TextBox>
                                    <label for="floatingInput">Username</label>
                                    <div class="errorLgn text-danger d-none" id="errorLgn">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please Provide a Valid Input (Ex. Juan Dela Cruz A.)</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="pssBx" CssClass="pssBx form-control" runat="server" placeholder="name@example.com"></asp:TextBox>
                                    <label for="floatingInput">Password</label>
                                    <div class="errorPass text-danger d-none" id="errorPass">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Password should contain 8 character long and atleast 1 numeric and 1  capital letter</span>
                                    </div>
                                </div>
                                <div class="d-grid">
                                    <asp:Button ID="updtBtnLgn" CssClass="btn updtBtn" runat="server" Text="Save Changes" OnClick="updtBtnLgn_Click" UseSubmitBehavior="false" />
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <%-- modal for department information --%>
    <div class="modal fade" id="deptInfoModal" tabindex="-1" aria-labelledby="deptInfoModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="modal-header">
                            <span class="modal-title modalRouting fs-5" id="dptInfo" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <span class="fs-4">Deparment Information</span>
                            <div class="p-2">
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="deptDescription" CssClass="deptDescription form-control" placeholder="Department" runat="server" Style="height: 120px" TextMode="MultiLine"></asp:TextBox>
                                    <label for="deptDescription">Description</label>
                                    <div class="errDeptDesc text-danger d-none" id="errDeptDesc">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide description</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="deptNameBx" CssClass="deptNameBx form-control" placeholder="Department Name" runat="server"></asp:TextBox>
                                    <label for="deptDean">Department Name</label>
                                    <div class="errDeptName text-danger d-none" id="errDeptName">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide valid department name</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="deptHeadBx" CssClass="deptHeadBx form-control" placeholder="Department Dead" runat="server"></asp:TextBox>
                                    <label for="deptDean">Department Head/Dean</label>
                                    <div class="errDeptHead text-danger d-none" id="errDeptHead">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide valid name</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="officeHour" CssClass="officeHour form-control" runat="server" placeholder="Office Hours"></asp:TextBox>
                                    <label for="Office">Office Hours</label>
                                    <div class="errOffHrs text-danger d-none" id="errOffHrs">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide office hours</span>
                                    </div>
                                </div>
                                <div class="d-grid">
                                    <asp:Button ID="updBtnDeptInfo" CssClass="btn updtBtn" runat="server" Text="Save Changes" UseSubmitBehavior="false" OnClick="updBtnDeptInfo_Click" />
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- modal for department courses --%>
    <div class="modal fade" id="coursesModal" tabindex="-1" aria-labelledby="Courses" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" class="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="modal-header">
                            <span class="modal-title modalRouting fs-5" id="dptCourse" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <span class="fs-4">Degree Programs</span>
                            <div class="p-2">
                                <div class="CourseContainer">
                                    <asp:TextBox ID="CoursesAppended" CssClass="CoursesAppended form-control mb-3" Style="height: 120px;" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <div class="errCourse text-danger d-none" id="errCourse">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide Courses</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="coursesInput" CssClass="coursesInput form-control mb-3" runat="server" placeholder="Add course here..."></asp:TextBox>
                                    <label for="coursesInput">Add Course here</label>
                                </div>
                                <asp:Label ID="noInput" runat="server" CssClass="text-danger"></asp:Label>
                                <div class="container-fluid mb-3">
                                    <div class="row g-2">
                                        <div class="col-lg-8 col-md-12">
                                            <asp:LinkButton ID="AddBtn" CssClass="btn updtBtn addBtn" runat="server" OnClick="AddBtn_Click">Add</asp:LinkButton>
                                        </div>
                                        <div class="col-lg-4 col-md-12">
                                            <asp:LinkButton ID="RstBtn" CssClass="btn updtBtn addBtn" runat="server" OnClientClick="resetCourses(); return false;">Reset</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="d-grid">
                                    <asp:Button ID="updBtnCoursesModal" CssClass="btn updtBtn" runat="server" Text="Save Changes" OnClick="updBtnCoursesModal_Click" />
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- modal for contact information --%>
    <div class="modal fade" id="ContactNumberModal" aria-hidden="true" aria-labelledby="contactInfo" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="modal-header">
                            <span class="modal-title modalRouting fs-5" data-bs-toggle="modal" data-bs-target="#editModal"><i class="bi bi-chevron-left"></i></span>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <span class="fs-4">Contact Information</span>
                            <div class="p-2">
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="contactNumber" CssClass="contactNumber form-control" runat="server" placeholder="Contact Number"></asp:TextBox>
                                    <label for="floatingInput">Contact Number</label>
                                    <div class="errConNum text-danger d-none" id="errConNum">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide valid contact number</span>
                                    </div>
                                </div>
                                <div class="form-floating mb-3">
                                    <asp:TextBox ID="emailTxtBx" CssClass="emailTxtBx form-control" runat="server" placeholder="Email"></asp:TextBox>
                                    <label for="floatingInput">Email</label>
                                    <div class="errEmail text-danger d-none" id="errEmail">
                                        <span><i class="bi bi-info-circle"></i></span>
                                        <span>Please provide valid email</span>
                                    </div>
                                </div>
                                <div class="d-grid">
                                    <asp:Button ID="updtBtnContactInfo" CssClass="btn updtBtn" runat="server" Text="Save Changes" UseSubmitBehavior="false" OnClick="updtBtnContactInfo_Click" />
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- KATU NI SA FILES NA--%>
    <div class="modal fade" id="filesModal" aria-hidden="true" aria-labelledby="files" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="uploadModalLabel">File Upload</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">          
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>File Name</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                        <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand" EnableViewState="true">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("FileName") %></td>
                                        <td>
                                            <asp:Button ID="btnDeleteFile" runat="server" Text="Delete" CommandName="DeleteFile" CommandArgument='<%# Eval("FileId") %>' CssClass="btn btn-danger btn-sm" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="modal-body">
                    <asp:FileUpload ID="fileUpload" runat="server" accept=".pdf"  />
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtFileName" runat="server" placeholder="Enter File Name" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="BtnUpload_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>







    <%-- success modal --%>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-success text-center text-light">
                    <i class="bi bi-info-circle-fill"></i>
                    <span>Successfully Updated</span>
                </div>
            </div>
        </div>
    </div>
    <%-- error modal --%>
    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body bg-danger-subtle text-center text-light">
                </div>
            </div>
        </div>
    </div>

    <script src="../../../Resources/CustomJS/DeptProfile/DeptprofileJS.js"></script>
    <script>
        function openSuccessModal() {
            $('#successModal').modal('show');
        }

        function openErrorrModal() {
            $('#errorModal').modal('show');
        }
        function updatePanelFunction() {
            __doPostBack(document.getElementById('<%= UpdatePanel1.ClientID %>'), '');
        }
        function resetCourses() {
            var coursesAppended = document.getElementById('<%= CoursesAppended.ClientID %>');
            var currentContent = coursesAppended.value;
            var courses = currentContent.split('\n');

            if (courses.length > 0) {
                courses.pop(); // Remove the last item
                coursesAppended.value = courses.join('\n');
            } else {
                // Handle the case where there are no courses left
                alert('No Courses Available');
            }
        }
    </script>

    <script type="text/javascript">
        function openInNewTab() {
            // Get the URL of the clicked button's page
            var url = '<%= ResolveUrl("Department_profile.aspx") %>';
            window.open(url, '_blank');
            return false; // To prevent the default postback action
        }
    </script>


     <!-- JavaScript to show/hide modal -->
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</asp:Content>

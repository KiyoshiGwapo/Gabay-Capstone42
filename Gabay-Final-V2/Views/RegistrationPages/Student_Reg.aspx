<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Reg.aspx.cs" Inherits="Gabay_Final_V2.Views.RegistrationPages.Student_Reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Student Registration</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="../../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <link href="../../Resources/CustomStyleSheet/RegistrationStyle.css" rel="stylesheet" />
    <link href="../../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../../Resources/CustomJS/Registration/RegistrationJS.js"></script>
</head>
<body>
    <div class="container">
        <div class="row p-3">
            <div class="col-md-8 col-sm-12 p-3 justify-content-center bg-light shadow-lg">
                <asp:HyperLink ID="goBack1" runat="server" CssClass="d-block text-decoration-none" NavigateUrl="~/Views/LoginPages/Student_login.aspx"><h4 class="text-dark"><i class="bi bi-arrow-left-square"></i></h4></asp:HyperLink>
                <div class="text-center">
                    <img class="logo-size1 text-center" src="../../Resources/Images/UC-LOGO.png" />
                </div>
                <div class="divider mb-2">
                    <div class="left-line"></div>
                    <div class="divider-text">Gabay</div>
                    <div class="right-line"></div>
                </div>
                <div class="header">
                    <span>Registration Form</span>
                </div>
                <form id="form1" runat="server" class="form1 row gx-2">
                    <%--Department Selection Inputs--%>
                    <div class="col-12">
                        <div class="department-fields mt-2" id="department-field">
                            <div class="department-input form-floating">
                                <asp:DropDownList ID="departmentChoices" CssClass="department form-select" runat="server" aria-label="Departments" AutoPostBack="True" OnSelectedIndexChanged="departmentChoices_SelectedIndexChanged">
                                   <asp:ListItem Selected="True" Value="">
                                       Choose a Department...
                                   </asp:ListItem>
                                </asp:DropDownList>
                                <label for="departmentChoices">Department...</label>
                            </div>
                        </div>
                        <div class="departmentError text-danger d-none" id="departmentError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please select a department</span>
                        </div>
                    </div>
                    <%--Course Selection Inputs--%>
                     <div class="col-md-6 col-sm-12">
                        <div class="course-fields mt-2" id="course-field">
                            <div class="course-input form-floating">
                                <asp:DropDownList ID="courseList" CssClass="course form-select" runat="server" aria-label="Course">
                                   <asp:ListItem Selected="True" Value="">
                                       Choose a Course...
                                   </asp:ListItem>
                                </asp:DropDownList>
                                <label for="courseList">Courses...</label>
                            </div>
                        </div>
                        <div class="courseError text-danger d-none" id="courseError">
                            <span><i class="bi bi-info-circle"></i></span>
                            <span>Please select a course</span>
                        </div>
                    </div>
                    <%--Course Year Selection Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="courseYear-fields mt-2" id="courseYear-field">
                            <div class="courseYear-input form-floating">
                                <asp:DropDownList ID="courseYearChoices" CssClass="courseYear form-select" runat="server" aria-label="Course Year">
                                    <asp:ListItem Selected="True" Value="">
                                        Choose a Course Year...
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">1st Year</asp:ListItem>
                                    <asp:ListItem Value="2">2nd Year</asp:ListItem>
                                    <asp:ListItem Value="3">3rd Year</asp:ListItem>
                                    <asp:ListItem Value="4">4th Year</asp:ListItem>
                                    <asp:ListItem Value="5">5th Year</asp:ListItem>
                                </asp:DropDownList>
                                <label for="courseYearChoices">Course Year:</label>
                            </div>
                            <div class="courseYearError text-danger d-none" id="courseYearError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please select your Course Year</span>
                            </div>
                        </div>
                    </div> 
                    <%--Full Name Inputs--%>
                    <div class="col-12">
                        <div class="name-fields mt-2" id="name-field">
                            <div class="name-input form-floating">
                                <asp:TextBox ID="name" CssClass="name form-control" runat="server" placeholder="First Name"></asp:TextBox>
                               <%-- <input type="text" class="fname form-control" id="fname" placeholder="First Name"/>--%>
                                <label for="name" placeholder="Name">Full Name (First Name, Last Name, Middle Initial)</label>
                            </div>
                            <div class="nameError text-danger d-none" id="nameError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please Provide a Valid Input (Ex. Juan Dela Cruz A.)</span>
                            </div>
                        </div>
                    </div>
                    <%--Address Inputs--%>
                    <div class="col-12">
                        <div class="address-fields mt-2" id="address-field">
                            <div class="address-input form-floating">
                                <asp:TextBox ID="address" CssClass="address form-control" runat="server" placeholder="Last Name"></asp:TextBox>
                                <%--<input type="text" class="lname form-control" id="lname" placeholder="Last Name" />--%>
                                <label for="address" placeholder="Address">Address</label>
                            </div>
                            <div class="addressError text-danger d-none" id="addressError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Provide an Address</span>
                            </div>
                        </div>
                    </div>
                    <%--Contact Number Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="contact-fields mt-2" id="contact-field">
                            <div class="contact-input form-floating">
                                <asp:TextBox ID="contact" CssClass="contact form-control" runat="server" placeholder="Last Name"></asp:TextBox>
                                <%--<input type="text" class="initial form-control" id="initial" placeholder="Last Name" />--%>
                                <label for="contact" placeholder="Contact Number">Contact Number</label>
                            </div>
                            <div class="contactError text-danger d-none" id="contactError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please Provide Valid Contact Number</span>
                            </div>
                        </div>
                    </div>
                    <%--DOB Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="DOB-fields mt-2" id="DOB-field">
                            <div class="DOB-input form-floating">
                                <asp:TextBox ID="DOB" CssClass="DOB form-control" runat="server" placeholder="DOB" TextMode="Date"></asp:TextBox>
                                <%--<input type="text" class="suffix form-control" id="suffix" placeholder="Suffix" />--%>
                                <label for="DOB" placeholder="DOB">Date of Birth</label>
                            </div>
                            <div class="DOBError text-danger d-none" id="DOBError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please Provide Date of Birth</span>
                            </div>
                        </div>
                    </div>
                    <%--Id Number Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="idNumber-fields mt-2" id="idNumber-field">
                            <div class="idNumber-input form-floating">
                                <asp:TextBox ID="idNumber" CssClass="idNumber form-control" runat="server" placeholder="Id Number"></asp:TextBox>
                                <%--<input type="text" class="idNumber form-control" id="idNumber" placeholder="Id Number" />--%>
                                <label for="idNumber">Student Id Number</label>
                            </div>
                            <div class="idNumError text-danger d-none" id="idNumError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please enter a valid Student Id Number</span>
                            </div>
                        </div>
                    </div>
                    <%--Email Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="email-fields mt-2" id="email-field">
                            <div class="email-input form-floating">
                                <asp:TextBox ID="email" CssClass="email form-control" runat="server" placeholder="Email"></asp:TextBox>
                                <%--<input type="email" class="email form-control" id="email" placeholder="Email" />--%>
                                <label for="email">Email</label>
                            </div>
                            <div class="emailError text-danger d-none" id="emailError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Please enter a valid email</span>
                            </div>
                        </div>
                    </div>
                    <%--Password Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="password-fields mt-2" id="password-field">
                            <div class="password-input form-floating">
                                <asp:TextBox ID="password" CssClass="password form-control" runat="server" placeholder="Password"></asp:TextBox>
                                <%--<input type="text" class="password form-control" id="password" placeholder="Password"/>--%>
                                <label for="password">Password</label>
                            </div>
                            <div class="passError text-danger d-none" id="passwordError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Password should contain 8 character long and atleast 1 numeric and 1  capital letter </span>
                            </div>
                        </div>
                    </div>
                    <%--Confirm Password Inputs--%>
                    <div class="col-md-6 col-sm-12">
                        <div class="cpassword-fields mt-2" id="cpassword-field">
                            <div class="cpassword-input form-floating">
                                <asp:TextBox ID="cpassword" CssClass="cpassword form-control" runat="server" placeholder="Confirm Password"></asp:TextBox>
                                <%--<input type="text" class="cpassword form-control" id="cpassword" placeholder="Confirm Password" />--%>
                                <label for="cpassword">Confirm Password</label>
                            </div>
                            <div class="cpassError text-danger d-none" id="cpasswordError">
                                <span><i class="bi bi-info-circle"></i></span>
                                <span>Password not matched</span>
                            </div>
                        </div>
                    </div>
                                       
                    <asp:Button ID="regBtn" runat="server" Text="Register" CssClass="btn regBtn mt-3" OnClick="regBtn_Click" />
                </form>
            </div>
            <div class="col-md-4 d-none d-md-block custom-bg3 shadow-lg">
            </div>
        </div>
    </div>
</body>
</html>

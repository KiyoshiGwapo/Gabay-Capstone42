<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.bundle.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css"/>
    <style>
        .picCont {
            width: 180px;
            height: auto;
            border-radius: 200px;
        }

        .departmentInfo li {
            list-style: none;
        }

        .headerLi {
            display: flex;
            justify-content: center;
        }

        .departmentInfoContainer {
            display: flex;
        }

        .departmentInfoContainer li {
             list-style: none;
             padding: 5px; 
        }
        .contactInformation {
            display: flex;
        }
        .contactInformation li {
            list-style: none;
             padding: 5px; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
           <div class="row">
               <div class="col-12">
                   <div class="d-flex justify-content-center align-items-center">
                       <img class="picCont img-fluid" src="../Resources/Images/tempIcons/ccs%20logo.png" />
                   </div>
                   <div class="text-center">
                       <asp:Label ID="deptName" CssClass="fs-1 fw-bold" runat="server" Text="Department name"></asp:Label>
                   </div>
                  
               </div>
              <div class="col-12">
                  <div class="departmentInfo">
                      <ul>
                          <li><hr /></li>
                          <li class="headerLi">
                              <asp:Label ID="deptDesc" runat="server" CssClass="fs-3" Text="Description"></asp:Label>
                          </li>
                          <li><hr /></li>
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
                                      </ul>
                                  </div>
                                  
                              </div>
                          </li>
                          <li><hr /></li>
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
    </form>
</body>
</html>

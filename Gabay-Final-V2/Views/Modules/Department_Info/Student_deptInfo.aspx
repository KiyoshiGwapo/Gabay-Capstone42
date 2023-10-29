<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_deptInfo.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Department_Info.Student_deptInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Resources/CustomStyleSheet/DeptInfo/DeptInfo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
           <div class="row">
               <div class="col-12">
                   <div class="d-flex justify-content-center align-items-center">
                       
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
</asp:Content>

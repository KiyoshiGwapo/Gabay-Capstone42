<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Admin_Homepage/Admin_Master.Master" AutoEventWireup="true" CodeBehind="Admin_DashBoard.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Admin_Homepage.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        .sulod .container {
            margin-top: 10vh;
            min-height: 90vh;
        }

        /* CSS for the chart container */
        .sulod .chart-container {
            width: 60%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        /* CSS for the chart canvas */
        .sulod .chart-canvas {
            width: 100%; /* 100% width to fill the chart container */
            height: 100%; /* 100% height to fill the chart container */
            display: flex; /* Center the chart within its container */
            justify-content: center;
            align-items: center;
        }

        .sulod h1 {
            text-align: center;
            margin: 0;
            margin-bottom: 20px;
            position: relative;
        }

            .sulod h1 span.blue {
                color: #007bff;
                border-bottom: 2px solid #007bff;
            }

            .sulod h1 span.black {
                color: #000000;
                border-bottom: 2px solid #000000;
            }
    </style>
    <div class="sulod">
        <div class="container">
            <h1>
                <span class="blue">Welcome!</span> <span class="black">
                    <asp:Label ID="lblDept_name" runat="server" Text="Label"></asp:Label>
            </h1>
            <div class="row">
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">All Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="StudentuserCountLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <img src="../../../Resources/Images/tempIcons/students.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">All Departments</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="DepatmentuserCountLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <img src="../../../Resources/Images/tempIcons/teachers.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">All Active Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="StudentApprovedUserCountLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <img src="../../../Resources/Images/add-friend.png" alt="" width="50" height="50" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">All Pending Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="StudentPendingUserCountLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <img src="../../../Resources/Images/file.png" alt="" width="50" height="50" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--            <div class="chart-container">
                <canvas id="myDonutChart" class="chart-canvas"></canvas>
            </div>--%>

            <div class="chart-container">
                <canvas id="myBarChart" class="chart-canvas"></canvas>
            </div>
        </div>
    </div>
<%--    <asp:HiddenField ID="StudentApprovedUserCountLabel" runat="server" />
    <asp:HiddenField ID="StudentPendingUserCountLabel" runat="server" />--%>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <%--    <script>
        // Function to create and update the smaller donut chart
        function createDonutChart(studentApprovedCount, studentPendingCount) {
            var ctx = document.getElementById('myDonutChart').getContext('2d');
            var data = {
                datasets: [{
                    data: [studentCount, departmentCount],
                    backgroundColor: ['blue', 'red'],
                }],
                labels: ['Approved Students', 'Pending Students'],
            };
            var options = {
                cutoutPercentage: 60, // Adjust this value to control the thickness of the donut
            };
            var donutChart = new Chart(ctx, {
                type: 'doughnut',
                data: data,
                options: options,
            });
        }

        // Call the function to create the smaller donut chart
        createDonutChart(<%= StudentApprovedUserCountLabel.Value %>, <%= StudentPendingUserCountLabel.Value %>);
    </script>--%>
    <asp:HiddenField ID="BarStudentsUserCountLabel" runat="server" />
    <asp:HiddenField ID="BarStudentsNursingUserCountLabel" runat="server" />
    <script>
        // Function to create and update the bar chart
        function createBarChart(bsitCount, bsnCount, bsmCount, bsaCount, bseCount) {
            var ctx = document.getElementById('myBarChart').getContext('2d');
            var data = {
                labels: ['All Students', 'College of Computer Studies', 'College of Nursing', 'College of Maritime', 'College of Customs Administrations', 'College of Allied Engineering'],
                datasets: [
                    {
                        label: ['All Students'],
                        backgroundColor: ['green','purple', 'red', 'black', 'blue', 'brown'],
                        data: [bsitCount, bsnCount, bsmCount, bsaCount, bseCount],
                        barThickness: 40, 
                    }
                ]
            };

            var options = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            };

            var barChart = new Chart(ctx, {
                type: 'bar',
                data: data,
                options: options
            });
        }

        // Call the function to create the bar chart
        createBarChart(<%= StudentuserCountLabel.Text %>,<%= BarStudentsUserCountLabel.Value %>,
            <%= BarStudentsNursingUserCountLabel.Value %>);
    </script>
</asp:Content>

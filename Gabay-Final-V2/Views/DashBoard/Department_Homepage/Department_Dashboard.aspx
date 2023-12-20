<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Department_Homepage/Department_Master.Master" AutoEventWireup="true" CodeBehind="Department_Dashboard.aspx.cs" Inherits="Gabay_Final_V2.Views.DashBoard.Department_Homepage.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .sulod .container {
            margin-top: 10vh;
        }

        /* CSS for the chart container */
        .sulod .chart-container {
            width: 500px;
            height: 500px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        /* CSS for the chart canvas */
        .sulod .chart-canvas {
            width: 100%;
            height: 100%;
            display: flex;
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

        .row {
            justify-content: space-evenly;
        }
        .custom-card-width {
          width: 120px;
}

        .card-body {
        padding: 15px; /* Adjust the value as needed */
    }

    
        /*
            .row div {
                width: 360px;
            }*/
    </style>
    <div class="sulod">
        <div class="container">
            <h1>
                <span class="blue">Welcome!</span> <span class="black">
                    <asp:Label ID="lblDept_name" runat="server" Text="Label"></asp:Label>
                </span>
            </h1>
            <div class="row">
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="userCountLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <img src="../../../Resources/Images/students.png" alt="" />
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
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Active Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="ActiveuserCountLabel" runat="server" Text=""></asp:Label>
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
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Pending Students</div>
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:Label ID="PendinguserCountLabel" runat="server" Text=""></asp:Label>
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

            <%--  end sa row--%>
            <div class="row ">
                <div class="chart-container">
                    <canvas id="myDonutChart" class="chart-canvas"></canvas>

                    <!-- New card on the right side -->
                    <div class="container" data-aos="fade-up">

                        <div class="section-title">
                            <h2>Appointments</h2>
                        </div>

                        <div class="row align-items-center">

                            <!-- First Card -->
                            <div class="col-md-6 mb-4" data-aos="zoom-in" data-aos-delay="100">
                                <div class="card custom-card-width bg-success text-white shadow align-items-center">
                                    <div class="card-body d-flex align-items-center">
                                        <div class="icon-circle bg-light mr-4">
                                        </div>
                                        <div>
                                            <div class="text-xs font-weight-bold text-uppercase mb-1">Approved</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800 text-center">
                                                <asp:Label ID="ApprovedAppointmentCountLabel" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Second Card -->
                            <div class="col-md-6 mb-4" data-aos="zoom-in" data-aos-delay="200">
                                <div class="card custom-card-width bg-danger text-white shadow align-items-center">
                                    <div class="card-body d-flex align-items-center">
                                        <div class="icon-circle bg-light mr-4">
                                        </div>
                                        <div>
                                            <div class="text-xs font-weight-bold text-uppercase mb-1">Rejected</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800 text-center">
                                                <asp:Label ID="DeniedAppointmentCountLabel" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Add spacing between the rows -->
                            <div class="w-100"></div>

                            <!-- Third Card -->
                            <div class="col-md-6 mb-4" data-aos="zoom-in" data-aos-delay="300">
                                <div class="card custom-card-width bg-primary text-white shadow align-items-center">
                                    <div class="card-body d-flex align-items-center">
                                        <div class="icon-circle bg-light mr-4">
                                        </div>
                                        <div>
                                            <div class="text-xs font-weight-bold text-uppercase mb-1">Reschedules</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800 text-center">
                                                <asp:Label ID="RescheduleAppointmentCountLabel" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Fourth Card -->
                            <div class="col-md-6 mb-4" data-aos="zoom-in" data-aos-delay="400">
                                <div class="card custom-card-width bg-warning text-white shadow align-items-center">
                                    <div class="card-body d-flex align-items-center">
                                        <div class="icon-circle bg-light mr-4">
                                        </div>
                                        <div>
                                            <div class="text-xs font-weight-bold text-uppercase mb-1">Pending</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800 text-center">
                                                <asp:Label ID="PendingAppointmentCountLabel" runat="server" Text=""></asp:Label>
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
    </div>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        // Function to create and update the smaller donut chart
        function createDonutChart(studentCount, departmentCount) {
            var ctx = document.getElementById('myDonutChart').getContext('2d');
            var data = {
                datasets: [{
                    data: [studentCount, departmentCount],
                    backgroundColor: ['green', 'red'],
                }],
                labels: ['Active Students', 'Pending Students'],
            };
            var options = {
                cutoutPercentage: 50, // Adjust this value to control the thickness of the donut
            };
            var donutChart = new Chart(ctx, {
                type: 'doughnut',
                data: data,
                options: options,
            });
        }

        // Call the function to create the smaller donut chart
        createDonutChart(<%= ActiveuserCountLabel.Text %>, <%= PendinguserCountLabel.Text %>);
    </script>
   <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>

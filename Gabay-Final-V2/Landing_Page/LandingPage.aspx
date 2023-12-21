<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="Gabay_Final_V2.Landing_Page.LandingPage" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Landing Page</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../Resources/CustomStyleSheet/DefaultStyle.css" rel="stylesheet" />
    <link href="../FontAwesome/css/all.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="../FontAwesome/js/all.js"></script>
    <link href="../Resources/CustomStyleSheet/Landing_Page/LandingPageStyle.css" rel="stylesheet" />
</head>

<body>
    <%-- Top Header --%>
    <nav class="navbar sticky-top navbar-expand-lg bg-body-tertiary" id="nav">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Gabay</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse flex-row-reverse" id="navbarNavAltMarkup">
                <div class="navbar-nav text-center">
                    <a class="nav-link" href="#homeSection">Home</a>
                    <a class="nav-link" href="#services">Features</a>
                    <a class="nav-link" href="#aboutUS">About us</a>
                    <button class="btn bg-primary" data-toggle="modal" data-target="#loginSelection">Get Started</button>
                </div>
            </div>
        </div>
    </nav>

    <%-- Modal for login options --%>
    <div class="modal fade" id="loginSelection" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Login as</h5>
                </div>
                <div class="modal-body">
                    <!-- Cards inside the modal, horizontally aligned -->
                    <div class="row">
                        <div class="col-4">
                            <div class="card" style="width: auto;">
                                <h5 class="card-title text-center mt-1">Walk-in<br />
                                    Guest</h5>
                                <img src="../../Resources/Images/walk.gif" />
                                <div class="card-body">
                                    <p class="card-text">Explore our university as a walk-in guest and learn about admissions and tours using our kiosk.</p>
                                </div>
                                <asp:HyperLink CssClass="btn btn-outline-primary" ID="HyperLink1" runat="server" NavigateUrl="~/Views/LoginPages/Guest_login.aspx">Proceed</asp:HyperLink>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: auto;">
                                <h5 class="card-title text-center mt-1">Old/New<br />
                                    Students</h5>
                                <img src="../../Resources/Images/id.gif" />
                                <div class="card-body">
                                    <p class="card-text">For returning students and newcomers, discover the university's offerings and resources.</p>
                                </div>
                                <asp:HyperLink CssClass="btn btn-outline-primary" ID="btnStdnt" runat="server" NavigateUrl="~/Views/LoginPages/Student_login.aspx">Proceed</asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: auto;">
                                <h5 class="card-title text-center mt-1">Department<br />
                                    Personnel</h5>
                                <img src="../../Resources/Images/school.gif" />
                                <div class="card-body">
                                    <p class="card-text">Manage department's facilities, faculty, and programs for a fulfilling academic journey.</p>
                                </div>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-outline-primary" NavigateUrl="~/Views/LoginPages/Department_login.aspx">Proceed</asp:HyperLink>
                                <%--<a href="#" class="btn btn-outline-primary">Card link</a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <%-- Home Section --%>
    <section id="homeSection" class="mb-5"> 
        <div class="container-fluid homeSection">
            <div class="row h-100" >
                <div class="col-lg-5 col-md-12">
                    <div class="hLeft-cont">
                        <div class="hLeft">
                            <h1 class="titleHeader">A Web-based Information Kiosk</h1>
                            <div class="divider"></div>
                            <h3 class="text-secondary">Modern Approach to University Information Sharing</h3>
                        </div>
                        <div class="button-area mt-3">
                            <a class="btn exploreBtn" href="#services">Start Exploring</a>
                        </div>
                    </div>
                </div>
                <div class="col-7 d-none d-md-block">
                </div>
            </div>
        </div>
    </section>
    <%-- Feature Section --%>
   <%-- <section  id="featureSection" class="featureSection">
        <div class="container-fluid featSection"">
            <div class="row">
                <div class="col-12 mt-3 featHeader">
                    <span class="fw-semibold">Features</span>
                </div>
            </div>
            <div class="row align-items-center g-2">
                <div class="col-12">
                    <div class="card d-md-flex">
                        <div class="card-body">
                            <div class="d-flex align-items-center justify-content-around">
                                <div class="d-flex align-items-center justify-content-center">
                                   
                                    <img src="../Resources/Images/tempIcons/cutomModel.jpg" class="customImg rounded float-start d-none d-lg-block m-0"/>
                                </div>
                                <div class="mainFeat text-md-center">
                                    <span class="fs-1 fw-semibold">Disseminate Information</span>
                                    <p class="mt-3">
                                        Enhancing the communication infrastructure to facilitate faster and more 
                                    efficient information dissemination is crucial for the benefit of all 
                                    stakeholders involved in campus activities. One pivotal component of 
                                    this upgraded system is the inclusion of an information section that
                                    serves as a central hub for all essential news and events occurring 
                                    on the campus.
                                    </p>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card d-md-flex">
                        <div class="card-body d-block">
                           <div class="header">
                               <h5 class="card-title fs-3 fw-semibold text-center">Appointment Booking</h5>
                           </div>
                            <div class="iconsTemp">
                                <img src="../Resources/Images/tempIcons/schedule.png" class="tempIcons"/>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row align-items-center p-3 g-4">
                <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Appointment Booking</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/schedule.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">Allowing appointment scheduling for concerned departments and colleges.</p>
                        </div>
                    </div>
                </div>
                 <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Chatbot</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/chat.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">A simple chatbot for 24/7 university related queries.</p>
                        </div>
                    </div>
                </div>
                 <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Wayfinding</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/route.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">An easy-to-read maps and clear directions using a wayfinding system.</p>
                        </div>
                    </div>
                </div>
            </div>--%>
    
    <%-- Features Section --%>
    <section id="services" class="services section-bg mb-5">
      <div class="container" data-aos="fade-up">
        <div class="section-title">
          <h2>Features</h2>
          <p>"Discover the Gabay app, your one-stop solution for school navigation and information. Easily find your way around campus, access essential school information, and schedule appointments with ease. Streamline your school experience today with Gabay!".</p>
        </div>

        <div class="row justify-content-center">
          <div class="col-xl-3 col-md-6 d-flex align-items-stretch" data-aos="zoom-in" data-aos-delay="100">
            <div class="icon-box">
              <div class="icon"><i class="bi bi-robot"></i></div>
              <h4><a href="#">Chatbot</a></h4>
              <p>"Chatbots: Your 24/7 virtual assistant, always ready to provide quick answers and assistance, making your life easier and more efficien"</p>
            </div>
          </div>

          <div class="col-xl-3 col-md-6 d-flex align-items-stretch mt-4 mt-md-0" data-aos="zoom-in" data-aos-delay="200">
            <div class="icon-box">
              <div class="icon"><i class="bi bi-journal-arrow-up"></i></div>
              <h4><a href="#">Appointment Bookings</a></h4>
              <p>"Your time is your most valuable asset. Booking appointments wisely makes every moment count."</p>
            </div>
          </div>

          <div class="col-xl-3 col-md-6 d-flex align-items-stretch mt-4 mt-xl-0" data-aos="zoom-in" data-aos-delay="300">
            <div class="icon-box">
              <div class="icon"><i class="bi bi-geo-alt"></i></div>
              <h4><a href="#">Wayfinding</a></h4>
              <p>"Wayfinding for schools helps users efficiently navigate the campus, locating classrooms, offices, and facilities. It includes signs, maps, and digital tools to simplify the journey, ensuring students, staff, and visitors can find their way with ease."</p>
            </div>
          </div>

          



        </div>

      </div>

        <br/>
        <br/>
</section>
    <!-- ======= About Us Section ======= -->
    <section id="aboutUS" class="about mb-5">
      <div class="container" data-aos="fade-up">

        <div class="section-title">
          <h2>About Us</h2>
        </div>

        <div class="row content">
          <div class="col-lg-6">
            <p>
             Gabay for Schools:
            </p>
            <ul>
              <li><i class="bi bi-check2-all"></i> Navigate with ease using interactive maps. </li>
              <li><i class="bi bi-check2-all"></i> Get instant answers from our chatbot.</li>
              <li><i class="bi bi-check2-all"></i> Efficiently book appointments online.</li>
            </ul>
          </div>
          <div class="col-lg-6 pt-4 pt-lg-0">
            <p>

GABAY is more than just a word; it's a beacon of support and wisdom that lights the path for all of us. In times of uncertainty, when we seek direction or a helping hand, GABAY reminds us that we are never alone. Together, we can navigate the complexities of life and emerge stronger.

So let GABAY be your compass, your guiding star, and your source of inspiration. Let it be a reminder that, even in the darkest of moments, there's a way forward. Let us embrace GABAY as a symbol of unity, strength, and the unwavering power of human connection.

Let us always remember that in this journey called life, we are each other's greatest GABAY. 🤝💫"
            </p>
            <a href="#" class="btn-learn-more">Learn More</a>
          </div>
        </div>

      </div>
    </section><!-- End About Us Section -->
    <!-- ======= Team Section ======= -->
    <section id="team" class="team section-bg">
      <div class="container" data-aos="fade-up">

        <div class="section-title">
          <h2>Team</h2>
          <p>"The strength of the team is each individual member. The strength of each member is the team."</p>
        </div>

        <div class="row justify-content-center">

          <div class="col-lg-5" data-aos="zoom-in" data-aos-delay="100">
            <div class="member d-flex align-items-start">
              <div class="pic"> <img src="../Resources/Images/tempIcons/teamPics/luab.jpg" class="img-fluid" />  </div>
              <div class="member-info">
                <h4>Kerr John Luab</h4>
                <span>Tester</span>
                <p>"Test with purpose; purpose with test."</p>
                   <div class="social">
                  <a href="#"><i class="bi bi-facebook"></i></a>
                  <a href="#"><i class="bi bi-linkedin"></i> </a>
                </div>
               
              </div>
            </div>
          </div>

          <div class="col-lg-5 mt-4 mt-lg-0" data-aos="zoom-in" data-aos-delay="200">
            <div class="member d-flex align-items-start">
              <div class="pic"> <img src="../Resources/Images/tempIcons/teamPics/Jeremiah.jpg" class="img-fluid" /> </div>
              <div class="member-info">
                <h4>Jeremiah James Rodrigez</h4>
                <span>Hustler</span>
                <p>"A hustler turns dreams into plans and plans into reality."</p>
                     <div class="social">
                  <a href="#"><i class="bi bi-facebook"></i></a>
                  <a href="#"><i class="bi bi-linkedin"></i> </a>
                </div>
              </div>
            </div>
          </div>

          <div class="col-lg-5 mt-4" data-aos="zoom-in" data-aos-delay="300">
            <div class="member d-flex align-items-start">
              <div class="pic"> <img src="../Resources/Images/tempIcons/teamPics/rowel.png"  class="img-fluid" /> </div>
              <div class="member-info">
                <h4>Rowel Padillo</h4>
                <span>Project manager</span>
                <p>"A project manager: turning chaos into clarity, one milestone at a time."</p>
                     <div class="social">
                      <a href="#"><i class="bi bi-facebook"></i></a>
                      <a href="#"><i class="bi bi-linkedin"></i> </a>
                    </div>
                
              </div>
            </div>
          </div>

                   <div class="col-lg-5 mt-4" data-aos="zoom-in" data-aos-delay="200">
            <div class="member d-flex align-items-start">
              <div class="pic"> <img src="../Resources/Images/tempIcons/teamPics/Kent%20(2).jpg"  class="img-fluid" /> </div>
              <div class="member-info">
                <h4>Kent Gerald Quiros</h4>
                <span>Hustler</span>
                <p>"Hustle in silence; let your success make the noise."</p>
                     <div class="social">
                  <a href="#"><i class="bi bi-facebook"></i></a>
                  <a href="#"><i class="bi bi-linkedin"></i> </a>
                </div>
              </div>
            </div>
          </div>

            <div class="col-lg-5 mt-4" data-aos="zoom-in" data-aos-delay="500">
              <div class="member d-flex align-items-start">
                <div class="pic"> <img src="../Resources/Images/tempIcons/teamPics/Arnan.jpg"  class="img-fluid" /> </div>
                <div class="member-info">
                  <h4>Johndel E. Arnan</h4>
                  <span>Hipster</span>
                  <p>"Hipsters: rewriting the rules, one unique expression at a time."</p>
                     <div class="social">
                      <a href="#"><i class="bi bi-facebook"></i></a>
                      <a href="#"><i class="bi bi-linkedin"></i> </a>
                    </div>
                </div>
              </div>
            </div>

        </div>

      </div>
    </section>
        
        <!-- End Team Section -->

        <!-- ======= Footer ======= -->
            <br />
               <br />
                  <br/>
  <footer id="footer" class="mb-5">
    <div class="footer-top">
      <div class="container">
        <div class="row">

          <div class="col-lg-3 col-md-6 footer-contact">
            <h3>Gabay</h3>
            <p>
             UCLM <br/>
              Lapu-lapu mandaue<br/>
              PH <br/><br/>
              <strong>Phone:</strong> +63 961204300<br/>
              <strong>Email:</strong> Gabayapp@gmail.com<br/>
            </p>
          </div>

          <div class="col-lg-3 col-md-6 footer-links">
            <h4>Useful Links</h4>
            <ul>
              <li><i class="bx bx-chevron-right"></i> <a href="#homeSection">Home</a></li>
              <li><i class="bx bx-chevron-right"></i> <a href="#aboutUS">About us</a></li>
              <li><i class="bx bx-chevron-right"></i> <a href="#services">Features</a></li>
              
            </ul>
          </div>

          <div class="col-lg-3 col-md-6 footer-links">
            <h4>Our Features</h4>
            <ul>
              <li><i class="bx bx-chevron-right"></i> <a href="#">Chatbot</a></li>
              <li><i class="bx bx-chevron-right"></i> <a href="#">Appointment Booking</a></li>
              <li><i class="bx bx-chevron-right"></i> <a href="#">Wayfinding</a></li>
            </ul>
          </div>

          <div class="col-lg-3 col-md-6 footer-links">
            <h4>Our Social Networks</h4>
            <p>"Do more things that make you forget to check your phone."</p>
            <div class="social-links mt-3">
              <a href="#" class="twitter"><i class="bi bi-twitter"></i></a>
              <a href="#" class="facebook"><i class="bi bi-facebook"></i></a>
              <a href="#" class="instagram"><i class="bi bi-instagram""></i></a>
              <a href="#" class="google-plus"><i class="bi bi-skype"></i></a>
              <a href="#" class="linkedin"><i class="bi bi-linkedin"></i></a>
            </div>
          </div>

        </div>
      </div>
    </div>

    <div class="container footer-bottom clearfix">
      <div class="copyright">
        &copy; Copyright <strong><span>Gabay</span></strong>. All Rights Reserved
      </div>
    </div>
  </footer><!-- End Footer -->






<!-- End Services Section -->
           <%-- <div class="row align-items-center p-3 g-4">
                <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Appointment Booking</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/schedule.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">Allowing appointment scheduling for concerned departments and colleges.</p>
                        </div>
                    </div>
                </div>
                 <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Chatbot</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/chat.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">A simple chatbot for 24/7 university related queries.</p>
                        </div>
                    </div>
                </div>
                 <div class="col-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title fs-3 fw-semibold text-center">Wayfinding</h5>
                            <div class="d-flex justify-content-center">
                                <img src="../Resources/Images/tempIcons/route.png" class="tempIcons"/>
                            </div>
                            <p class="card-text mt-4">An easy-to-read maps and clear directions using a wayfinding system.</p>
                        </div>
                    </div>
                </div>
            </div>--%>



</body>
</html>

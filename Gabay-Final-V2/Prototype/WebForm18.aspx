<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm18.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm18" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <script src="../Bootstrap/Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <style>
        .slider-ct {
            width:600px;
            overflow: hidden;
            padding: 0;
            position: relative;
        }

        .slide-wr {
            position: relative;
            flex-wrap: nowrap;
        }

        #sec div {
            display: block;
            background-color: #aaecaa;
            padding: 10px 20px;
            float: left;

            transform: translateX(0px);
        }

        #sec:after {
            content: "";
            display: table;
            clear: both;
        }

        .slide {
            margin-left: 5px;
            height: 300px;
            display:flex;
            justify-content:center;
            align-items:center;
        }

        #back {
            position: absolute;
            top: 45%;
        }

        #forward {
            position: absolute;
            top: 45%;
            right: 0;
        }

        .carousel-container {
            border: 1px solid #808080;
            display: flex;
            align-items: center;
        }

        .carousel-inner {
            width: 300px;
            padding: 10px 0;
            display: flex;
            flex-direction: row;
            overflow: hidden;
        }

        .carousel-items {
            padding-left: 10px;
            margin-right: 3px;
        }

        .card-body-p{
            font-size: 12px;
        }

        .card-title{
            font-size: 15px;
            font-weight: bold;
        }
        .card{
            height: 230px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 40px;"></div>
        <div class="container slider-ct">
            <div class="row slide-wr">
                <div class="col-sm-4 slide">
                    <div class="card" style="width: 20rem;">
                        <div class="card-body">
                            <span class="card-title">Enrollment</span>
                            <p class="card-text card-body-p">New/Old students who wish to enroll in the campus</p>
                            <div class="d-grid choices">
                                <a href="#" class="btn btn-primary mb-1">Requirements</a>
                                <a href="#" class="btn btn-primary mb-1">Enrollment Date</a>
                                <a href="#" class="btn btn-primary mb-1">Choice 3</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4 slide">
                    <div class="card" style="width: 20rem;">
                        <div class="card-body">
                            <span class="card-title">Scholarship Program</span>
                            <p class="card-text card-body-p">Scholarship programs offered by the university.</p>
                            <div class="d-grid choices">
                                <a href="#" class="btn btn-primary mb-1">Academic</a>
                                <a href="#" class="btn btn-primary mb-1">Working</a>
                                <a href="#" class="btn btn-primary mb-1">More</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4 slide">
                    <div class="card" style="width: 20rem;">
                      <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <a href="#" class="btn btn-primary">Go somewhere</a>
                      </div>
                    </div>
                </div>
                <div class="col-sm-4 slide">
                    <div class="card" style="width: 20rem;">
                      <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <a href="#" class="btn btn-primary">Go somewhere</a>
                      </div>
                    </div>
                </div>
            </div>
            <button id="back" type="button" class="btn bg-secondary opacity-50"><i class="bi bi-chevron-compact-left"></i></button>
            <button id="forward" type="button" class="btn bg-secondary opacity-50"><i class="bi bi-chevron-compact-right"></i></button>
        </div>
    </form>
    <script src="../Scripts/jquery-3.7.1.min.js"></script>
    <script>
        var slider = document.querySelector(".slide-wr");

        document.getElementById("back").onclick = () => {
            const c = 33.33;
            let left = slider.style.transform.split("%")[0].split("(")[1];
            if (left) {
                var num = Number(left) + Number(c);
            } else {
                var num = Number(c);
            }
            slider.style.transform = `translateX(${num}%)`;

            if (left == -166.65) {
                slider.addEventListener("transitionend", myfunc);
                function myfunc() {
                    this.style.transition = "none";
                    this.style.transform = `translateX(-299.97%)`;
                    slider.removeEventListener("transitionend", myfunc);
                }
            } else {
                slider.style.transition = "all 0.5s";
            }
        };

        document.getElementById("forward").onclick = () => {
            const c = -33.33;
            let left = slider.style.transform.split("%")[0].split("(")[1];
            if (left) {
                var num = Number(left) + Number(c);
            } else {
                var num = Number(c);
            }

            slider.style.transform = `translateX(${num}%)`;

            if (left == -299.97) {
                console.log("reached the border");
                slider.addEventListener("transitionend", myfunc);
                function myfunc() {
                    this.style.transition = "none";
                    this.style.transform = `translateX(-166.65%)`;
                    slider.removeEventListener("transitionend", myfunc);
                }
            } else {
                slider.style.transition = "all 0.5s";
            }
        };

        const sliderChildren = document.getElementsByClassName("slide-wr")[0].children;
        slider.style.transform = `translateX(${sliderChildren.length * -33.33}%)`;
        Array.from(sliderChildren)
            .slice()
            .reverse()
            .forEach((child) => {
                let cln = child.cloneNode(true);
                cln.classList += " cloned before";
                slider.insertBefore(cln, sliderChildren[0]);
            });

        Array.from(sliderChildren).forEach((child) => {
            let cln = child.cloneNode(true);
            if (child.classList.contains("cloned") === false) {
                cln.classList += " cloned after";
                slider.appendChild(cln);
            }
        });
    </script>
</body>
</html>

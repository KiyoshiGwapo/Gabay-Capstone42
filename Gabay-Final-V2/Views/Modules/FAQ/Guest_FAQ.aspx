<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Guest_Homepage/Guest_Master.Master" AutoEventWireup="true" CodeBehind="Guest_FAQ.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.FAQ.Guest_FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Frequently Asked Questions</h1>
        <asp:Repeater ID="FAQRepeater" runat="server">
            <ItemTemplate>
                <div class="tab">
                    <input type="radio" name="acc" id='<%# "acc" + Eval("FAQID") %>' />
                    <label for='<%# "acc" + Eval("FAQID") %>'>
                        <h2><%# Eval("FAQID") %></h2>
                        <h3><%# Eval("Question") %></h3>
                    </label>
                    <div class="content">
                        <p><%# Eval("Answer") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>


        <!-- Placeholder for displaying selected FAQ -->
        <div>
            <h3 id="faqQuestion" runat="server"></h3>
            <p id="faqAnswer" runat="server"></p>
        </div>
    </div>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800;900&display=swap');

        .container {
            margin: auto; /* Center horizontally */
            max-width: 600px;
            display: flex;
            flex-direction: column;
            gap: 20px;
            justify-content: center; /* Center vertically */
            align-items: center; /* Center horizontally */
        }

            .container h1 {
                color: #333;
            }

            .container .tab {
                position: relative;
                background: #fff;
                padding: 0 20px 20px;
                box-shadow: 0 15px 25px rgba(0,0,0,0.05);
                border-radius: 5px;
                overflow: hidden;
            }

                .container .tab input {
                    appearance: none;
                }

                .container .tab label {
                    display: flex;
                    align-items: center;
                    cursor: pointer;
                }

                    .container .tab label::after {
                        content: '+';
                        position: absolute;
                        right: 20px;
                        font-size: 2em;
                        color: rgba(0,0,0,0.1);
                        transition: transform 1s;
                    }

                .container .tab:hover label::after {
                    color: #333;
                }

                .container .tab input:checked ~ label::after {
                    transform: rotate(135deg);
                    color: #fff;
                }

                .container .tab label h2 {
                    width: 40px;
                    height: 40px;
                    background: #333;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    color: #fff;
                    font-size: 1.25em;
                    border-radius: 5px;
                    margin-right: 10px;
                }

                .container .tab input:checked ~ label h2 {
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    color: rgba(255,255,255,0.2);
                    font-size: 8em;
                    justify-content: flex-end;
                    padding: 20px;
                }

                .container .tab:nth-child(2) label h2 {
                    background: linear-gradient(135deg,#70f570,#49c628);
                }

                .container .tab:nth-child(3) label h2 {
                    background: linear-gradient(135deg,#3c8ce7,#00eafff);
                }

                .container .tab:nth-child(4) label h2 {
                    background: linear-gradient(135deg,#ff96f9,#c32bac);
                }

                .container .tab:nth-child(5) label h2 {
                    background: linear-gradient(135deg,#fd6e6a,#ffc600);
                }

                .container .tab label h3 {
                    position: relative;
                    font-weight: 500;
                    color: #333;
                    z-index: 10;
                }

                .container .tab input:checked ~ label h3 {
                    background: #fff;
                    padding: 2px 10px;
                    color: #333;
                    border-radius: 2px;
                    box-shadow: 0 5px 15px rgba(0,0,0,0.5);
                }

                .container .tab .content {
                    max-height: 0;
                    transition: 1s;
                    overflow: hidden;
                    min-width: 700px;
                }

                .container .tab input:checked ~ .content {
                    max-height: 100vh;
                }

                .container .tab .content p {
                    position: relative;
                    padding: 10px 0;
                    color: #333;
                    z-index: 10;
                }

                .container .tab input:checked ~ .content p {
                    color: #fff;
                }
    </style>
    <script>
        function showFAQ(faqID) {
            // You can make an AJAX request to retrieve the FAQ data based on faqID
            // Replace the placeholders with the actual question and answer data
            document.getElementById("faqQuestionPlaceholder").innerText = "Question for FAQ " + faqID;
            document.getElementById("faqAnswerPlaceholder").innerText = "Answer for FAQ " + faqID;

            // Return false to prevent the default click behavior
            return false;
        }
    </script>
</asp:Content>

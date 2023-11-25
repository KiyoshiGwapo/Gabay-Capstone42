using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Gabay_Final_V2.Views.Modules.Chatbot
{
    public partial class Student_Chatbot : System.Web.UI.Page
    {
        Chatbot_model conn = new Chatbot_model();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["countUnAnswered"] = 0;
                string greetingMessage1 = @"Hello! to assist you better, 
                   please choose an option in the menu or if you can't find what are you looking for,
                   just type your concern in a few words. If I can't answer you queries you can book
                   and appointment to a designated department for your concern";
                string greetingMessage = @"<div class='container-slider'>
                                                   <button id='prevButton' type='button' class='btn buttons d-flex justify-content-center align-items-center'>
                                                       <i class='bi bi-chevron-compact-left'></i>
                                                   </button>
                                                   <div class='slider-container'>
                                                       <div class='cards'>
                                                           <div class='card bot-message' style='width: 15rem;'>
                                                               <div class='card-body mt-auto'>
                                                                   <span class='card-title'>Admission Process</span>
                                                                   <p class='card-text card-body-p'>Students who wish to enroll in the campus</p>
                                                                   <div class='choices'>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('New Student');"">New Student</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Old Student');"">Old Student</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('More from Admission');"">More</button>
                                                                   </div>
                                                               </div>
                                                           </div>
                                                           <div class='card bot-message' style='width: 15rem;'>
                                                               <div class='card-body mt-auto'>
                                                                   <span class='card-title'>Scholarship Program</span>
                                                                   <p class='card-text card-body-p'>Scholarship programs offered by the university.</p>
                                                                   <div class='choices'>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Academic Scholarship');"">Academic Scholarship</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Working Scholarship');"">Working Scholarship</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('More Scholarship');"">More</button>
                                                                   </div>
                                                               </div>
                                                           </div>
                                                           <div class='card bot-message' style='width: 15rem;'>
                                                               <div class='card-body'>
                                                                   <span class='card-title'>Academics Program</span>
                                                                   <p class='card-text card-body-p'>Academics that are offered by the university</p>
                                                                   <div class='choices'>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('College Programs');"">Colleges</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Senior High School Programs');"">Senior High</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Basic Education Programs');"">Basic Education</button>
                                                                   </div>
                                                               </div>
                                                           </div>
                                                           <div class='card bot-message' style='width: 15rem;'>
                                                               <div class='card-body'>
                                                                   <span class='card-title'>About UC</span>
                                                                   <p class='card-text card-body-p'>Academics that are offered by the university</p>
                                                                   <div class='choices'>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('list of Campus');"">Campus</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('Campus History');"">History</button>
                                                                       <button type='button' class='btn btn-primary mb-1' OnClick=""menuButtonClick('More About UC');"">More</button>
                                                                   </div>
                                                               </div>
                                                           </div>
                                                       </div>
                                                   </div>
                                                   <button id='nextButton' type='button' class='btn buttons d-flex justify-content-center align-items-center'>
                                                       <i class='bi bi-chevron-compact-right'></i>
                                                   </button>
                                               </div>";
                AddBotMessage(greetingMessage1);
                AddBotMessageMenu(greetingMessage);
            }

        }

        private void AddBotMessage(string message)
        {
            string botMessageHtml = $"<div class=\"message-container bot-message\">{message}</div>";
            chatContainer.InnerHtml += botMessageHtml;
        }

        private void AddBotMessageMenu(string message)
        {
            string botMessageHtml = $"<div class=\"message-container\">{message}</div>";
            chatContainer.InnerHtml += botMessageHtml;
        }

        private void AddUserMessage(string message)
        {
            string userMessageHtml = $"<div class=\"message-container user-message\">{message}</div>";
            chatContainer.InnerHtml += userMessageHtml;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text;
            int countUnAnsered = (int)ViewState["countUnAnswered"];
            AddUserMessage(userInput);
            string lowerInput = userInput.ToLower();

            if (userInput != "" || userInput == null)
            {

                if (lowerInput == "hi")
                {
                    AddBotMessage("Hello! what can I assist to you today?");
                }
                else
                {
                    string scriptColumn = conn.FindMatchingScript(userInput,ref countUnAnsered);
                    scriptColumn = scriptColumn.Replace("\n", "<br>");
                    AddBotMessage(scriptColumn);
                }
                txtUserInput.Text = string.Empty;
            }
        }
    }
}
using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Gabay_Final_V2.Views.Modules.Chatbot
{
    public partial class Student_Chatbot : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        Chatbot_model conn = new Chatbot_model();
        string greetingMessage1 = @"Hello! to assist you better, 
                   please choose an option in the menu or if you can't find what are you looking for,
                   just type your concern in a few words. If I can't answer you queries you can book
                   and appointment to a designated department for your concern";
        string greetingMessage = @"<div class='menuWrapper' >
                                                       <div class='selectDialog'>
                                                            Please select your related concerns below:
                                                       </div>
                                                       <div class='container-slider'>
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
                                                    </div>
                                                   </div>";
        string menuDialog = @"
                                       <div class='menuWrapper'>
                                                       <div class='selectDialog'>
                                                            I apologize, I am unable to answer your question, 
                                                            please use the appointment within gabay to book an
                                                            appointment so that a representative can answer your question or select your choices below:
                                                       </div>
                                                       <div class='container-slider'>
                                                       <button id='menuPrevButton' type='button' class='menuPrevButton btn buttons d-flex justify-content-center align-items-center'>
                                                           <i class='bi bi-chevron-compact-left'></i>
                                                       </button>
                                                       <div class='menuslider-container'>
                                                           <div class='menucards'>
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
                                                       <button id='menuNextButton' type='button' class='menuNextButton btn buttons d-flex justify-content-center align-items-center'>
                                                           <i class='bi bi-chevron-compact-right'></i>
                                                       </button>
                                                    </div>
                                                   </div>";
        string menuDialog1 = @"
                                       <div class='menuWrapper'>
                                                       <div class='selectDialog'>
                                                            Please select your related concerns below:
                                                       </div>
                                                       <div class='container-slider'>
                                                       <button id='menuPrevButton' type='button' class='menuPrevButton btn buttons d-flex justify-content-center align-items-center'>
                                                           <i class='bi bi-chevron-compact-left'></i>
                                                       </button>
                                                       <div class='menuslider-container'>
                                                           <div class='menucards'>
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
                                                       <button id='menuNextButton' type='button' class='menuNextButton btn buttons d-flex justify-content-center align-items-center'>
                                                           <i class='bi bi-chevron-compact-right'></i>
                                                       </button>
                                                    </div>
                                                   </div>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["countUnAnswered"] = 0;

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
            int countUnAnswered = (int)ViewState["countUnAnswered"];
            AddUserMessage(userInput);
            string lowerInput = userInput.ToLower();
            string testMessage = @"Hello! what can I assist to you today?";
            string botInfo = @"Hi! I am Gab, my purpose is to assist you for your queries regarding to
                               campus information within the University of Cebu Lapu-Lapu and Mandaue.";
            string botInfo1 = @"Sometimes I may answer not related to your question, but rest assured you
                               can book an appointment for a representative to assist you or choose your queries in the menu ";
            string closingStatement = @"If you have more questions, feel free to ask.";
            if (userInput != "" || userInput == null)
            {

                if (lowerInput == "hi")
                {
                    AddBotMessage(testMessage);
                }
                else if (userInput == "who are you?" || userInput == "what do you do?"
                    || userInput == "who are you" || userInput == "what do you do"
                    || userInput == "what do you do" || userInput == "what do you do?")
                {
                    AddBotMessage(botInfo);
                    AddBotMessage(botInfo1);
                    AddBotMessageMenu(menuDialog1);

                }
                else
                {
                    string scriptColumn = FindMatchingScript(userInput, ref countUnAnswered);
                    scriptColumn = scriptColumn.Replace("\n", "<br>");
                    AddBotMessage(scriptColumn);
                    AddBotMessage(closingStatement);

                    // Update ViewState with the new count from the returned value
                    ViewState["countUnAnswered"] = conn.CountUnAnswered;

                }

                txtUserInput.Text = string.Empty;
            }
        }
        public string FindMatchingScript(string userInput, ref int countUnAnswered)
        {
            Dictionary<string, int> keywordCount = new Dictionary<string, int>();

            // Tokenize the user input
            string[] userTokens = userInput.ToLower().Split(' ');
            string bestScript = "";
            int maxCount = 0;
            string unAnswered = @"I'm sorry, I didn't understand your question. Could you please rephrase it?";
            //string referToAppointment = @"I apologize, I am unable to answer your question, 
            //                      please use the appointment within gabay to book an
            //                      appointment so that a representative can answer your question.";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT response, keywords FROM Chat_Response";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string script = reader.GetString(0);
                            string keywords = reader.GetString(1);
                            int count = 0;

                            foreach (string keyword in keywords.Split(','))
                            {
                                if (userTokens.Contains(keyword.Trim(), StringComparer.OrdinalIgnoreCase))
                                {
                                    count++;
                                }
                            }

                            if (!keywordCount.ContainsKey(script))
                            {
                                keywordCount.Add(script, count);
                            }
                            else
                            {
                                keywordCount[script] += count;
                            }
                        }
                    }
                }
            }

            // Find the script with the highest keyword count
            foreach (var pair in keywordCount)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    bestScript = pair.Key;
                }
            }

            if (maxCount > 0)
            {
                // Matching keyword found
                bestScript = bestScript.Replace("\n", "<br>");
                countUnAnswered = 0;
                conn.CountUnAnswered = countUnAnswered;
            }
            else if (!conn.IsEnglish(userInput))
            {
                // No matching keyword, and input is not in English
                bestScript = "I apologize, I am unable to understand any language. Please ask your question in English.";
            }
            else
            {
                // No matching keyword, input is in English
                countUnAnswered++;

                // if the ViewState counts are more than or equal to 1, it sets bestScript to referToAppointment, otherwise bestScript is set to unAnswered
                if (countUnAnswered >= 2)
                {
                    AddBotMessageMenu(menuDialog);
                    countUnAnswered = 0;
                }
                else
                {
                    bestScript = unAnswered;
                }

                // Update the ViewState count
                conn.CountUnAnswered = countUnAnswered;
            }
            return bestScript;
        }
    }
}
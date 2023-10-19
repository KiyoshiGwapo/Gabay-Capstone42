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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string greetingMessage = "Hi! How can I assist you today? " + "<br />" +
                                       "<div class='button-container text-center'>" +
                                       "<button class='btn predefined-button' onclick='buttonClick(\"enrollment\")'>Enrollment</button>" +
                                       "<button class='btn predefined-button' onclick='buttonClick(\"tuition payment\")'>Tuition Payment</button>" +
                                       "</div>";
                AddBotMessage(greetingMessage);
            }
           
        }

        private void AddBotMessage(string message)
        {
            string botMessageHtml = $"<div class=\"message-container bot-message\">{message}</div>";
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
            AddUserMessage(userInput);
            string lowerInput = userInput.ToLower();

            if (userInput != "" || userInput == null)
            {
                // Handle predefined buttons/links
                if (lowerInput == "enrollment")
                {
                    // User clicked the "Enroll" button
                    AddBotMessage("To enroll in computer studies, please follow these steps: ...");
                }
                else if (lowerInput == "tuition payment")
                {
                    // User clicked the "Other Option" button
                    AddBotMessage("To pay tuition fee just approach the guard to get payment form and after that fill up the form then you can proceed in the cashier.");
                }
                // Add more predefined button/link checks as needed

                // If not a predefined button/link, use your chatbot logic
                else if (lowerInput == "hi")
                {
                    AddBotMessage("Hello! what can I assist to you today?");
                }
                else if (lowerInput == "where")
                {
                    AddBotMessage("");
                }
                else
                {
                    string scriptColumn = Chatbot_model.FindMatchingScript(userInput);
                    scriptColumn = scriptColumn.Replace("\n", "<br>");
                    AddBotMessage(scriptColumn);
                }
                txtUserInput.Text = string.Empty;
            }
        }
    }
}
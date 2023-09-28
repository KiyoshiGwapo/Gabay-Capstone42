using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
//For Email
using System.IO;
//For Qr Code
using QRCoder;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
//for json
using Newtonsoft.Json;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Student_Appointment : System.Web.UI.Page
    {
        // Define SweetAlertMessageType enum
        public enum SweetAlertMessageType
        {
            Success,
            Error,
            Warning,
            Info
        }

        // Function to convert the image to base64 data
        private string ConvertImageToBase64(string imagePath)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to base64 string
                    string base64Image = Convert.ToBase64String(imageBytes);
                    return base64Image;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FormSubmitted"] != null && (bool)Session["FormSubmitted"])
            {
                // Clear the session variable to avoid showing the popup again on page refresh
                Session["FormSubmitted"] = false;

                // Register the script to show the success message
                ClientScript.RegisterStartupScript(this.GetType(), "successMessageScript",
                    "showSuccessMessage();", true);
            }

            else if (FormSubmittedHiddenField.Value == "true")
            {
                // If the form was submitted on the same page load, show the success message
                ClientScript.RegisterStartupScript(this.GetType(), "successMessageScript",
                    "showSuccessMessage();", true);
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string firstName = FirstName.Text.Trim();
                string lastName = LastName.Text.Trim();
                string idNumber = IdNumber.Text.Trim();
                string year = Year.SelectedValue;
                string department = DepartmentDropDown.SelectedValue;
                string email = Email.Text.Trim();
                string userMessage = Message.Text.Trim();
                string selectedDate = selectedDateHidden.Value;
                string selectedTime = time.Text.Trim();

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(idNumber)
                    && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(email)
                    && !string.IsNullOrEmpty(userMessage) && !string.IsNullOrEmpty(selectedDate) && !string.IsNullOrEmpty(selectedTime))
                {
                    string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SaveAppointment(connection, firstName, lastName, idNumber, year, department, email, userMessage, selectedDate, selectedTime);
                    }

                    string emailBody = "<!DOCTYPE html>" +
                        "<html>" +
                        "<header>" +
                        "<style>" +
                        "@font-face {" +
                        "    font-family: CoolFont;" +
                        "    src: url('coolfont.woff2') format('woff2');" +
                        "}" +
                        "</style>" +
                        "</header>" +
                        "<body style=\"background-color: #F5F5F5; text-align: center;\">" +
                        "<img src='cid:UC-GABAY-LOGO' alt='UC Gabay Logo' style='width: 100px; height: 100px;' />" +
                        "<h1 style=\"color: #051a80; font-family: Arial, sans-serif; font-weight: bold\">Welcome to UC Gabay</h1>" +
                        "<div style='text-align: left; margin-left: 10%; margin-right: 10%;'>" +
                        "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Appointment is sent, and now your appointment is still Pending</p>" +
                        "</div>" +
                        "<h2>From KJ Department</h2>" +
                        "</body>" +
                        "</html>";

                    string fromEmail = "universityofcebulapu2x@gmail.com";
                    string emailSubject = "Appointment Status";
                    string emailPassword = "kmvdzryamibzbswz";


                    // Get the absolute path to the image file
                    string imageFilePath = Server.MapPath("../../../Resources/Images/UC-LOGO.png");


                    // Convert the image to base64 data
                    string base64Image = ConvertImageToBase64(imageFilePath);

                    // Create a new MailMessage
                    MailMessage message = new MailMessage(fromEmail, email, emailSubject, emailBody);
                    message.IsBodyHtml = true;

                    // Create an AlternateView for the HTML body
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");

                    // Create a LinkedResource for the embedded image
                    LinkedResource linkedImage = new LinkedResource(imageFilePath, "image/png");
                    linkedImage.ContentId = "UC-GABAY-LOGO";
                    htmlView.LinkedResources.Add(linkedImage);

                    // Add the AlternateView with the embedded image to the MailMessage
                    message.AlternateViews.Add(htmlView);

                    // Create and configure the SMTP client
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);

                    try
                    {
                        // Send the email
                        smtpClient.Send(message);

                        // Set the flag indicating successful email sending
                        Session["EmailSent"] = true;
                    }
                    catch (Exception ex)
                    {
                        // Handle any email sending errors here
                        Session["EmailSent"] = false;
                    }

                    // Clear the form fields
                    FirstName.Text = string.Empty;
                    LastName.Text = string.Empty;
                    IdNumber.Text = string.Empty;
                    Year.SelectedValue = string.Empty; // Clear the selected year
                    DepartmentDropDown.SelectedValue = string.Empty;
                    Email.Text = string.Empty;
                    Message.Text = string.Empty;
                    time.Text = string.Empty;
                    selectedDateHidden.Value = string.Empty;

                    // Set the hidden field value to indicate the form was submitted successfully
                    FormSubmittedHiddenField.Value = "true";

                    // Redirect the user or show a success message based on your requirements
                    if ((bool)Session["EmailSent"])
                    {
                        ShowSweetAlert("Appointment sent, wait for the approval of your Appointment, it will be sent to your email", SweetAlertMessageType.Success);
                    }
                    else
                    {
                        ShowSweetAlert("Appointment Successfully Submitted. However, there was an issue sending the confirmation email. Please check your email.", SweetAlertMessageType.Warning);
                    }
                }
            }
        }

        private void ShowSweetAlert(string message, SweetAlertMessageType messageType)
        {
            string script = GetSweetAlertScript(message, messageType);
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
        }

        private string GetSweetAlertScript(string message, SweetAlertMessageType messageType)
        {
            string type = GetSweetAlertMessageTypeString(messageType);
            return $@"swal({{
                title: '',
                text: '{message}',
                icon: '{type}',
                buttons: false,
                timer: 3000, // 3 seconds
            }});";
        }

        private string GetSweetAlertMessageTypeString(SweetAlertMessageType messageType)
        {
            switch (messageType)
            {
                case SweetAlertMessageType.Success:
                    return "success";
                case SweetAlertMessageType.Error:
                    return "error";
                case SweetAlertMessageType.Warning:
                    return "warning";
                case SweetAlertMessageType.Info:
                    return "info";
                default:
                    return "info";
            }
        }

        private void SaveAppointment(SqlConnection connection, string firstName, string lastName, string idNumber, string year, string department, string email, string message, string selectedDate, string selectedTime)
        {
            string insertQuery = "INSERT INTO Appointments (FirstName, LastName, IdNumber, Year, Department, Email, Message, SelectedDate, SelectedTime, Status) " +
                "VALUES (@FirstName, @LastName, @IdNumber, @Year, @Department, @Email, @Message, @SelectedDate, @SelectedTime, @Status)";
            SqlCommand command = new SqlCommand(insertQuery, connection);

            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@IdNumber", idNumber);
            command.Parameters.AddWithValue("@Year", year);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Message", message);
            command.Parameters.AddWithValue("@SelectedDate", selectedDate);
            command.Parameters.AddWithValue("@SelectedTime", selectedTime);
            command.Parameters.AddWithValue("@Status", "PENDING");

            connection.Open();
            command.ExecuteNonQuery();
        }



        private bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("your@email.com"); // Replace with your email address
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.yourprovider.com"); // Replace with your SMTP server details
                smtpClient.Port = 587; // Replace with your SMTP port
                smtpClient.Credentials = new NetworkCredential("yourusername", "yourpassword"); // Replace with your SMTP credentials
                smtpClient.EnableSsl = true; // Enable SSL if required

                smtpClient.Send(mailMessage);

                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or return false
                return false;
            }
        }


    }
}
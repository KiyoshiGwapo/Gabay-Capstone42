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


namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Student_Appointment : System.Web.UI.Page
    {

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
                string fullName = FullName.Text.Trim();
                string idNumber = IdNumber.Text.Trim();
                string year = Year.SelectedValue;
                string department = DepartmentDropDown.SelectedValue;
                string email = Email.Text.Trim();
                string contactNumber = ContactN.Text.Trim(); // Add this line to get the contact number
                string userConcern = Message.Text.Trim();
                string selectedDate = selectedDateHidden.Value;
                string selectedTime = time.Text.Trim();

                if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(idNumber)
                    && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(email)
                    && !string.IsNullOrEmpty(contactNumber) && !string.IsNullOrEmpty(userConcern)
                    && !string.IsNullOrEmpty(selectedDate) && !string.IsNullOrEmpty(selectedTime))
                {
                    string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True"; // Replace with your connection string
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Save appointment details to the "appointment" table
                        SaveAppointment(connection, fullName, idNumber, year, department, email, contactNumber, userConcern, selectedDate, selectedTime);
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
                    FullName.Text = string.Empty;
                    IdNumber.Text = string.Empty;
                    Year.SelectedValue = string.Empty; // Clear the selected year
                    DepartmentDropDown.SelectedValue = string.Empty;
                    Email.Text = string.Empty;
                    ContactN.Text = string.Empty; // Clear the contact number
                    Message.Text = string.Empty;
                    time.Text = string.Empty;
                    selectedDateHidden.Value = string.Empty;

                    // Set the hidden field value to indicate the form was submitted successfully
                    FormSubmittedHiddenField.Value = "true";

                    // Redirect the user or show a success message based on your requirements
                    if ((bool)Session["EmailSent"])
                    {
                      
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        private void SaveAppointment(SqlConnection connection, string fullName, string idNumber, string year, string department, string email, string contactNumber, string message, string selectedDate, string selectedTime)
        {
            string insertQuery = "INSERT INTO Appointments (full_name, IdNumber, Year, department_ID, Email, ContactNumber, Message, SelectedDate, SelectedTime, Status) " +
                "VALUES (@FullName, @IdNumber, @Year, @Department, @Email, @ContactNumber, @Message, @SelectedDate, @SelectedTime, 'PENDING')";
            SqlCommand command = new SqlCommand(insertQuery, connection);

            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@IdNumber", idNumber);
            command.Parameters.AddWithValue("@Year", year);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@ContactNumber", contactNumber);
            command.Parameters.AddWithValue("@Message", message);
            command.Parameters.AddWithValue("@SelectedDate", selectedDate);
            command.Parameters.AddWithValue("@SelectedTime", selectedTime);
            connection.Open();
            command.ExecuteNonQuery();
        }

    }
}
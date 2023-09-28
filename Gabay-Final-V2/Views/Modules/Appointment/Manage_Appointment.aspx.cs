using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
    public partial class Manage_Appointment : System.Web.UI.Page
    {
        // Declare the variable at the class level
        HtmlGenericControl yourTableBody = new HtmlGenericControl("tbody");
        protected string appointmentData;
        private int appointmentId;

        public enum SweetAlertMessageType
        {
            Success,
            Error,
            Warning,
            Info
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Fetch all appointments initially
                appointmentData = FetchAppointmentData();

                // Bind the data to your table
                BindAppointmentDataToTable(appointmentData);

                if (Request.QueryString["deleteId"] != null)
                {
                    if (int.TryParse(Request.QueryString["deleteId"], out int appointmentId))
                    {
                        DeleteAppointment(appointmentId);
                    }
                }

                if (Request.QueryString["id"] != null && Request.QueryString["status"] != null)
                {
                    if (int.TryParse(Request.QueryString["id"], out int appointmentId))
                    {
                        string status = Request.QueryString["status"].ToLower();

                        if (status == "approved" || status == "denied" || status == "rescheduled")
                        {
                            UpdateAppointmentStatus(appointmentId, status.ToUpper());

                            // Set the color of the button based on the status
                            string script = $"document.addEventListener('DOMContentLoaded', function() {{ var dropdownButton = document.getElementById('statusDropdown'); dropdownButton.style.backgroundColor = '{GetStatusColor(status)}'; dropdownButton.style.color = 'white'; }});";
                            ScriptManager.RegisterStartupScript(this, GetType(), "SetStatusColor", script, true);
                        }
                        else if (status == "serve")
                        {
                            // Handle the "Serve" status here
                            // For example, update the status in the database
                            UpdateAppointmentStatus(appointmentId, "SERVE");

                            // Redirect to the same page without the "status" parameter
                            Response.Redirect("AppointmentsList.aspx");
                            return; // Ensure that the response is terminated to prevent further processing
                        }

                    }
                }


                if (Request.QueryString["id"] != null && Request.QueryString["status"] != null)
                {
                    if (int.TryParse(Request.QueryString["id"], out int appointmentId))
                    {
                        // Assign the value to the private field
                        appointmentId = appointmentId;

                        string status = Request.QueryString["status"].ToLower();
                    }
                }

                // Handle fetchId request and populate the "Update" modal if needed
                if (!string.IsNullOrEmpty(Request.QueryString["fetchId"]))
                {
                    int fetchAppointmentId = Convert.ToInt32(Request.QueryString["fetchId"]);
                    AppointmentData appointmentData = FetchAppointmentDataById(fetchAppointmentId);
                    string json = JsonConvert.SerializeObject(appointmentData);
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(json);
                    Response.End();
                    return;
                }

                if (Request.QueryString["id"] != null)
                {
                    if (int.TryParse(Request.QueryString["id"], out int appointmentId))
                    {
                        appointmentData = FetchAppointmentData(appointmentId.ToString());
                    }
                    else
                    {
                        appointmentData = FetchAppointmentData();
                    }
                }
                else
                {
                    appointmentData = FetchAppointmentData();
                }
                // Register the JavaScript function to open the "View" modal
                string viewScript = @"
                    function viewAppointmentMessage(message) {
                        var viewModal = new bootstrap.Modal(document.getElementById('viewModal'));
                        document.getElementById('appointmentMessage').innerText = message;
                        viewModal.show();
                    }
                ";

                ScriptManager.RegisterStartupScript(this, GetType(), "ViewAppointmentScript", viewScript, true);
            }
        }


        private string FetchAppointmentData(string searchQuery = "")
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Appointments WHERE FirstName LIKE '%' + @SearchQuery + '%' OR LastName LIKE '%' + @SearchQuery + '%' ORDER BY id DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", searchQuery);
                DataTable appointmentData = new DataTable();
                adapter.Fill(appointmentData);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in appointmentData.Rows)
                {
                    string firstName = row["FirstName"].ToString();
                    string lastName = row["LastName"].ToString();
                    string idNumber = row["IdNumber"].ToString();
                    string year = row["Year"].ToString();
                    string department = row["Department"].ToString();
                    string email = row["Email"].ToString();
                    string message = row["Message"].ToString();
                    string selectedDate = row["SelectedDate"].ToString();
                    string selectedTime = row["SelectedTime"].ToString();
                    int appointmentId = Convert.ToInt32(row["id"]);
                    string status = row["Status"].ToString();
                    // Parse the selectedTime to a DateTime object
                    DateTime militaryTime = DateTime.ParseExact(selectedTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    string standardTime = militaryTime.ToString("hh:mm tt");

                    if (appointmentId == this.appointmentId) // Check if the current row's appointmentId matches the selected appointmentId
                    {
                        // Set the selected date and time for the appointment being edited
                        selectedDate = row["SelectedDate"].ToString();
                        selectedTime = row["SelectedTime"].ToString();
                    }

                    sb.Append("<tr>");
                    sb.Append("<td style='font-family:Poppins; text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>" + firstName + " " + lastName + "</td>");
                    sb.Append("<td style='font-family:Poppins; text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>" + idNumber + "</td>");
                    sb.Append("<td style='font-family:Poppins; text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>" + year + "</td>");
                    sb.Append("<td style='font-family:Poppins; text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>" + department + " </td>");
                    sb.Append("<td style='font-family:Poppins; text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>" + email + "</td>");
                    // Add "View" button to view the appointment message
                    sb.Append("<td style='text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>");
                    sb.Append("<button type='button' class='btn btn-primary' style='background-color: blue; color: #fff;' onclick='viewAppointmentMessage(\"" + message + "\");'>");
                    sb.Append("<i class='fa fa-eye'></i> View");
                    sb.Append("</button>");
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black; '>" + selectedDate + "</td>");
                    sb.Append("<td style='text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black; '>" + standardTime + "</td>");
                    sb.Append("<td style='text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black;'>");
                    sb.Append("<span style='color:" + GetStatusClass(status) + "'><b>" + status + "</b></span>");
                    sb.Append("<div class='dropdown d-flex justify-content-end'>");
                    sb.Append("<button class='btn btn-primary dropdown-toggle custom-button btn-sm' type='button' id='statusDropdown' data-bs-toggle='dropdown' aria-expanded='false'>");
                    sb.Append("</button>");
                    sb.Append("<ul class='dropdown-menu' aria-labelledby='statusDropdown'>");

                    // Always include the "RESCHEDULE" option in the dropdown
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=rescheduled&id=" + appointmentId + "' style='color: blue;' " + (status.ToLower() == "rescheduled" ? "" : "disabled") + ">RESCHEDULE</a></li>");

                    // Include "APPROVED" and "DENIED" options for all statuses
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=denied&id=" + appointmentId + "' style='color: red;'>DENIED</a></li>");
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=approved&id=" + appointmentId + "' style='color: green;'>APPROVED</a></li>");

                    sb.Append("</ul>");
                    sb.Append("</div>");
                    sb.Append("</td>");
                    sb.Append("<script>");
                    sb.Append("var buttons = document.querySelectorAll('.custom-button');");
                    sb.Append("for (var i = 0; i < buttons.length; i++) {");
                    sb.Append("buttons[i].style.width = '40px';"); // Adjust the width as needed
                    sb.Append("}");
                    sb.Append("</script>");
                    // Add both "EMAIL" and "DELETE" buttons within the same <td> element
                    sb.Append("<td style='text-align:center; border-top: none; border-right: none; border-left: none; border-bottom: 1px solid black; '>");
                    if (status.ToLower() == "rescheduled")
                    {
                        sb.Append("<a href='#' class='btn btn-primary btn-warning' data-bs-toggle='modal' data-bs-target='#emailModal' data-to='" + email + "'>");
                        sb.Append("<i class='fa fa-envelope'></i>");
                        sb.Append("</a>");
                        sb.Append("<button type='button' class='btn btn-primary' style='background-color: blue; color: #fff;' data-bs-toggle='modal' data-bs-target='#updateModal' data-id='" + appointmentId + "' data-date='" + selectedDate + "' data-time='" + selectedTime + "' onclick='EditButton_Click(" + appointmentId + ", \"" + selectedDate + "\", \"" + selectedTime + "\");'>");
                        sb.Append("<i class='fa fa-edit'></i>");
                        sb.Append("</button>");
                    }
                    sb.Append("<a href='#' class='btn btn-primary btn-danger' onclick='deleteAppointment(" + appointmentId + ")'>");
                    sb.Append("<i class='fa fa-trash'></i>");
                    sb.Append("</a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }

                return sb.ToString();
            }
        }

        public class AppointmentData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string SelectedDate { get; set; }
            public string SelectedTime { get; set; }
            // Add other properties as needed
        }

        private AppointmentData FetchAppointmentDataById(int appointmentId)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Appointments WHERE id = @AppointmentId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);
                DataTable appointmentData = new DataTable();
                adapter.Fill(appointmentData);

                if (appointmentData.Rows.Count > 0)
                {
                    DataRow row = appointmentData.Rows[0];
                    AppointmentData appointment = new AppointmentData
                    {
                        SelectedDate = row["SelectedDate"].ToString(),
                        SelectedTime = row["SelectedTime"].ToString(),
                        // Add other properties as needed
                    };
                    return appointment;
                }

                return null; // Return null if no appointment data found for the given ID
            }
        }
        // Method to handle the "Update" button click event on the server-side
        protected void UpdateModalButton_Click(object sender, EventArgs e)
        {
            // Get the new date and time values from the modal
            string newDate = updateDate.Value; // Use the 'Value' property to get the input value
            string newTime = updateTime.Value; // Use the 'Value' property to get the input value

            // Get the appointmentId from the hidden field
            if (int.TryParse(appointmentIdHiddenField.Value, out int appointmentIdToUpdate))
            {
                // Now you can call the UpdateAppointmentData method to update the appointment data
                UpdateAppointmentData(appointmentIdToUpdate, newDate, newTime);

                // After updating the data, you can refresh the page or redirect to the same page to reflect the changes
                Response.Redirect("AppointmentsList.aspx");
            }
            else
            {
                // Handle the case where the appointmentId is not a valid integer
                // Display an error message or handle it as per your requirement
            }
        }

        private void UpdateAppointmentData(int appointmentId, string newDate, string newTime)
        {
            // Assuming you have a connection string to your SQL Server database
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Appointments SET SelectedDate = @NewDate, SelectedTime = @NewTime WHERE id = @AppointmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewDate", newDate);
                    command.Parameters.AddWithValue("@NewTime", newTime);
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void EditButton_Click(object sender, EventArgs e)
        {
            // Assuming "updateDate" and "updateTime" are the IDs of the date and time input fields in the "Update" modal
            string selectedDate = updateDate.Value;
            string selectedTime = updateTime.Value;

            // Store the appointmentId in a session variable so we can access it in the UpdateAppointment method
            Session["AppointmentId"] = appointmentId;

            // Show the "Update" modal
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowUpdateModal", "var updateModal = new bootstrap.Modal(document.getElementById('updateModal')); updateModal.show();", true);
        }

        private string GetStatusClass(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    return "green";
                case "denied":
                    return "red";
                case "rescheduled":
                    return "blue";
                default:
                    return string.Empty;
            }
        }

        private string GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    return "green";
                case "denied":
                    return "red";
                case "rescheduled":
                    return "blue";
                default:
                    return string.Empty;
            }
        }

        private void DeleteAppointment(int appointmentId)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            string deleteQuery = "DELETE FROM Appointments WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", appointmentId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions or log the error
                        // You may want to display an error message to the user
                    }
                }
            }
        }

        protected void SendEmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                string toEmail = toEmailTextBox.Value;
                string fromEmail = "universityofcebulapu2x@gmail.com";
                string emailPassword = "kmvdzryamibzbswz";
                string emailSubject = "Appointment";
                string fullName = GetFullNameForEmail(toEmail);
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
                  "<h2 style=\"color: blue; font-family: Arial, sans-serif; margin: 10px 0; font-size: 24px; font-weight: bold; border-bottom: 2px solid blue;\">Dear " + fullName + "</h2>" +
                  "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>" + Request.Form["messageTextArea"] + "</p>" +
                  "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 24px;'>" + Request.Form["messagedate"] + "</p>" +
                  "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 24px;'>" + Request.Form["messagetime"] + "</p>" +
                  "<h2 style=\"text-align: left; color: black; font-family: CoolFont, Arial, sans-serif; font-size: 10px; text-decoration: none;'\">Select one of the options below to respond</h2>" +
                  "<a href='mailto:rodriguezjezreel108@gmail.com?subject=Accept'  style='background-color: blue; color: blue; text-decoration: none; margin-right: 10px; padding: 10px 20px; font-size: 18px; border: none;'>" +
                  "<button style='background-color: green; color: white; text-decoration: none; margin-right: 10px; padding: 10px 20px; font-size: 18px; border: none;'>Accept</button>" +
                  "</a>" +
                  "<a href='mailto:rodriguezjezreel108@gmail.com?subject=Decline' style='background-color: blue; color: blue; text-decoration: none; margin-right: 10px; padding: 10px 20px; font-size: 18px; border: none;'>" +
                  "<button style='background-color: red; color: white; text-decoration: none; margin-right: 10px; padding: 10px 20px; font-size: 18px; border: none;'>Decline</button>" +
                  "</a>" +
                  "</div>" +
                  "<h2>From KJ Department</h2>" +
                  "</body>" +
                  "</html>";


                // Get the absolute path to the image file
                string imageFilePath = Server.MapPath("../resources/UC-LOGO.png");

                // Convert the image to base64 data
                string base64Image = ConvertImageToBase64(imageFilePath);

                // Create a new MailMessage
                MailMessage message = new MailMessage(fromEmail, toEmail, emailSubject, emailBody);
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

                // Send the email
                smtpClient.Send(message);

                // Set the flag indicating successful email sending
                Session["EmailSent"] = true;

                // Display success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "successAlert", "swal('Email sent successfullyd!', 'success');", true);

                // Reload the appointment data
                appointmentData = FetchAppointmentData();

                // Clear the email modal fields
                toEmailTextBox.Value = string.Empty;
                messageTextArea.Value = string.Empty;
            }
            catch (Exception ex)
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailError", $"alert('An error occurred while sending the email: {ex.Message}');", true);
            }
        }
        private int GetIdFromDatabase(string email)
        {
            // Replace the connection string with your actual database connection string
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

            // Initialize the id variable to store the result
            int id = 0;

            try
            {
                // Create a new SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // Create a SQL command to query the database
                    string query = "SELECT id FROM Appointments WHERE email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the email parameter to the query
                        command.Parameters.AddWithValue("@Email", email);

                        // Execute the query and retrieve the 'id' from the database
                        object result = command.ExecuteScalar();

                        // Check if the result is not null and convert it to an integer
                        if (result != null && int.TryParse(result.ToString(), out int resultId))
                        {
                            id = resultId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any database connection or query errors here
                // For now, we'll just print the exception message to the console
                Console.WriteLine("Error: " + ex.Message);
            }

            // Return the retrieved 'id'
            return id;
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
        private string GetFullNameForEmail(string email)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True"; // Replace with your actual database connection string
            string fullName = "Unknown";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FirstName, LastName FROM Appointments WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // If the email is found in the database, construct the full name using first name and last name
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            fullName = $"{firstName} {lastName}";
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions or log the error
                    }
                }
            }

            return fullName;
        }
        private void UpdateAppointmentStatus(int id, string status)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            string updateQuery = "UPDATE Appointments SET Status = @Status WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // After updating the status, check if the status is "APPROVED" or "DENIED"
                        // If so, automatically send an email
                        if (status.ToLower() == "approved" || status.ToLower() == "denied")
                        {
                            // Get the email address of the appointment from the database
                            string email = GetAppointmentEmail(id);

                            if (!string.IsNullOrEmpty(email))
                            {
                                // Call the function to send the email
                                SendStatusEmail(email, status.ToUpper());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions or log the error
                        // You may want to display an error message to the user
                    }
                }
            }

            // After updating the status, redirect the user back to the appointment list page
            Response.Redirect("AppointmentsList.aspx");
        }

        private string GetAppointmentEmail(int id)
        {
            // Retrieve the email address of the appointment using the appointment ID
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            string email = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Email FROM Appointments WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            email = reader["Email"].ToString();
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions or log the error
                    }
                }
            }

            return email;
        }
        private void SendStatusEmail(string toEmail, string status)
        {
            try
            {
                string fromEmail = "universityofcebulapu2x@gmail.com";
                string emailPassword = "kmvdzryamibzbswz";
                string emailSubject = "Appointment Status Update";
                string fullName = GetFullNameForEmail(toEmail);
                int id = GetIdFromDatabase(toEmail);

                // Create the email body based on the status
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
                                   "<h2 style=\"color: blue; font-family: Arial, sans-serif; margin: 10px 0; font-size: 24px; font-weight: bold; border-bottom: 2px solid blue;\">Dear " + fullName + "</h2>" +
                                   "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Your appointment status has been updated to " + status.ToUpper() + ".</p>";


                MailMessage message = new MailMessage(); // Initialize the MailMessage

                if (status.ToLower() == "approved")
                {
                    emailBody += "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Your appointment has been approved. Please proceed as planned.</p>";

                    // Generate the QR code and attach it to the email
                    string qrCodeText = $"Appointment Ticket No: {id}";

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(10);
                    MemoryStream qrCodeStream = new MemoryStream();
                    qrCodeImage.Save(qrCodeStream, ImageFormat.Png);
                    qrCodeStream.Position = 0;
                    Attachment qrCodeAttachment = new Attachment(qrCodeStream, "QRCode.png");
                    qrCodeAttachment.ContentId = "QRCodeImage";
                    message.Attachments.Add(qrCodeAttachment);

                    // Fetch date and time from the database based on the appointment ID
                    DateTime appointmentDate = GetAppointmentDateFromDatabase(id);
                    TimeSpan appointmentTime = GetAppointmentTimeFromDatabase(id);

                    emailBody += "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Appointment Date: " + appointmentDate.ToString("MM/dd/yyyy") + "</p>";
                    emailBody += "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Appointment Time: " + appointmentTime.ToString(@"hh\:mm") + "</p>";

                    emailBody += "<div style='text-align: center;'>";
                    emailBody += "<img src='cid:QRCodeImage' alt='QR Code' style='width: 200px; height: 200px;' />";
                    emailBody += "</div>";

                }
                else if (status.ToLower() == "denied")
                {
                    emailBody += "<p style='text-align: left; font-family: Arial, sans-serif; font-size: 16px;'>Unfortunately, your appointment has been denied.</p>";
                }

                // Get the absolute path to the image file
                string imageFilePath = Server.MapPath("../resources/UC-LOGO.png");

                // Convert the image to base64 data
                string base64Image = ConvertImageToBase64(imageFilePath);
                // Create an AlternateView for the HTML body
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");

                // Set the MailMessage properties
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = emailSubject;
                message.IsBodyHtml = true;
                message.Body = emailBody;

                // Create a LinkedResource for the embedded image
                LinkedResource linkedImage = new LinkedResource(imageFilePath, "image/png");
                linkedImage.ContentId = "UC-GABAY-LOGO";
                htmlView.LinkedResources.Add(linkedImage);

                // Add the AlternateView with the embedded image to the MailMessage
                message.AlternateViews.Add(htmlView);

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);

                // Send the email
                smtpClient.Send(message);

                // Display success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "successAlert", "swal('Status Changed!', 'success');", true);

            }
            catch (Exception ex)
            {
                // Handle any exceptions or log the error
                // Display error message if needed
                ScriptManager.RegisterStartupScript(this, GetType(), "EmailError", $"alert('An error occurred while sending the status email: {ex.Message}');", true);
            }
        }
        private DateTime GetAppointmentDateFromDatabase(int appointmentId)
        {
            DateTime appointmentDate = DateTime.MinValue; // Initialize with a default value

            try
            {
                string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT SelectedDate FROM Appointments WHERE id = @AppointmentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            appointmentDate = Convert.ToDateTime(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or log the error
                // For example:
                // LogError(ex);
            }

            return appointmentDate;
        }

        private TimeSpan GetAppointmentTimeFromDatabase(int appointmentId)
        {
            TimeSpan appointmentTime = TimeSpan.Zero; // Initialize with a default value

            try
            {
                string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT SelectedTime FROM Appointments WHERE id = @AppointmentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            appointmentTime = TimeSpan.Parse(result.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or log the error
                // For example:
                // LogError(ex);
            }

            return appointmentTime;
        }
        //PARA SA ALERT OR POP UP 
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

        //Kani tung sa Filtering

        protected void ddlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected status from the dropdown list
            string selectedStatus = ddlStatusFilter.SelectedValue;

            // Call the method to fetch appointment data by status
            string appointmentData = FetchAppointmentDataByStatus(selectedStatus);

            // Set the inner HTML of yourTableBody with the fetched data
            yourTableBody.InnerHtml = appointmentData;

            // Replace the following line in your code with this updated line
            yourTablePlaceholder.Controls.Add(yourTableBody);
        }



        private void BindAppointmentDataToTable(string data)
        {
            // Bind the appointmentData to your table
            // You can set it as the InnerHtml of your table's tbody
            yourTableBody.InnerHtml = data;
        }


        // Add a method to fetch appointment data by status
        private string FetchAppointmentDataByStatus(string status)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Appointments WHERE Status = @Status ORDER BY id DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Status", status);
                DataTable appointmentData = new DataTable();
                adapter.Fill(appointmentData);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in appointmentData.Rows)
                {
                    // Extract appointment data and format it as needed
                    string firstName = row["FirstName"].ToString();
                    string lastName = row["LastName"].ToString();
                    string idNumber = row["IdNumber"].ToString();
                    string year = row["Year"].ToString();
                    string department = row["Department"].ToString();
                    string email = row["Email"].ToString();
                    string message = row["Message"].ToString();
                    string selectedDate = row["SelectedDate"].ToString();
                    string selectedTime = row["SelectedTime"].ToString();
                    int appointmentId = Convert.ToInt32(row["id"]);

                    sb.Append("<tr>");
                    // Append appointment data to the string builder
                    sb.Append("<td>" + firstName + " " + lastName + "</td>");
                    sb.Append("<td>" + idNumber + "</td>");
                    sb.Append("<td>" + year + "</td>");
                    sb.Append("<td>" + department + "</td>");
                    sb.Append("<td>" + email + "</td>");
                    sb.Append("<td>"); // Add "View" button
                    sb.Append("<button type='button' class='btn btn-primary' onclick='viewAppointmentMessage(\"" + message + "\");'>");
                    sb.Append("<i class='fa fa-eye'></i> View");
                    sb.Append("</button>");
                    sb.Append("</td>");
                    sb.Append("<td>" + selectedDate + "</td>");
                    sb.Append("<td>" + selectedTime + "</td>");
                    sb.Append("<td>");
                    sb.Append("<span style='color:" + GetStatusClass(status) + "'><b>" + status + "</b></span>");
                    // Add dropdown menu for status changes
                    sb.Append("<div class='dropdown d-flex justify-content-end'>");
                    sb.Append("<button class='btn btn-primary dropdown-toggle custom-button btn-sm' type='button' id='statusDropdown' data-bs-toggle='dropdown' aria-expanded='false'>");
                    sb.Append("</button>");
                    sb.Append("<ul class='dropdown-menu' aria-labelledby='statusDropdown'>");

                    if (status.ToLower() == "pending")
                    {
                        // Add the "Serve" option only when the status is "Pending"
                        sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=serve&id=" + appointmentId + "' style='color: orange;'>SERVE</a></li>");

                    }

                    // Always include the "RESCHEDULE" option in the dropdown
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=rescheduled&id=" + appointmentId + "' style='color: blue;' " + (status.ToLower() == "rescheduled" ? "" : "disabled") + ">RESCHEDULE</a></li>");

                    // Include "APPROVED" and "DENIED" options for all statuses
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=denied&id=" + appointmentId + "' style='color: red;'>DENIED</a></li>");
                    sb.Append("<li><a class='dropdown-item' href='AppointmentsList.aspx?status=approved&id=" + appointmentId + "' style='color: green;'>APPROVED</a></li>");

                    sb.Append("</ul>");
                    sb.Append("</div>");
                    sb.Append("</td>");
                    // Add email and delete buttons
                    sb.Append("<td>");
                    sb.Append("<a href='#' class='btn btn-primary btn-warning' data-bs-toggle='modal' data-bs-target='#emailModal' data-to='" + email + "'>");
                    sb.Append("<i class='fa fa-envelope'></i>");
                    sb.Append("</a>");
                    sb.Append("<button type='button' class='btn btn-primary' style='background-color: blue; color: #fff;' data-bs-toggle='modal' data-bs-target='#updateModal' data-id='" + appointmentId + "' data-date='" + selectedDate + "' data-time='" + selectedTime + "' onclick='EditButton_Click(" + appointmentId + ", \"" + selectedDate + "\", \"" + selectedTime + "\");'>");
                    sb.Append("<i class='fa fa-edit'></i>");
                    sb.Append("</button>");
                    sb.Append("<a href='#' class='btn btn-primary btn-danger' onclick='deleteAppointment(" + appointmentId + ")'>");
                    sb.Append("<i class='fa fa-trash'></i>");
                    sb.Append("</a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }

                return sb.ToString();
            }
        }




    }
}
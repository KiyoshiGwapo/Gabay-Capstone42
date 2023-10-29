using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;


namespace Gabay_Final_V2.Views.LoginPages
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRetrievePassword_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text;
            string email = txtEmail.Text;

            // Check if the user with the given ID and email exists in the database.
            string password = RetrievePasswordFromDatabase(userID, email);

            if (!string.IsNullOrEmpty(password))
            {
                // Send the retrieved password via email.
                SendPasswordEmail(email, password);

                lblMessage.Text = "Your password has been sent to your email.";
            }
            else
            {
                lblMessage.Text = "User not found or the provided information is incorrect.";
            }
        }

        // Implement your database retrieval logic here.
        private string RetrievePasswordFromDatabase(string userID, string email)
        {
            string password = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT stud_pass FROM student WHERE studentID = @UserID AND email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                password = reader["stud_pass"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database connection or query errors.
                lblMessage.Text = "An error occurred while retrieving the password: " + ex.Message;
            }

            return password;
        }

        // email sending logic.
        private void SendPasswordEmail(string email, string password)
        {
            try
            {
                // Replace these with your Gmail credentials.
                string fromEmail = "universityofcebulapu2x@gmail.com";
                string emailPassword = "kmvdzryamibzbswz";

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, emailPassword);

                    MailMessage message = new MailMessage(fromEmail, email)
                    {
                        Subject = "Your Password Recovery",
                        Body = $"Your password is: {password}",
                        IsBodyHtml = true
                    };

                    smtpClient.Send(message);
                }

                lblMessage.Text = "Password recovery email sent successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while sending the email: " + ex.Message;
            }
        }


    }
}
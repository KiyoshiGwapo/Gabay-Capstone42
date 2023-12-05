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
            try
            {
                string userID = txtUserID.Text;
                string email = txtEmail.Text;

                // Check if the user with the given ID and email exists in the database.
                string password = RetrievePasswordFromDatabase(userID, email);

                if (!string.IsNullOrEmpty(password))
                {
                    // Send the retrieved password via email.
                    SendPasswordEmail(email, password);

                    GreenMessage.Text = "Your password has been sent to your email.";

                    // Clear the error message label.
                    lblMessage.Text = string.Empty;

                    // Clear the fields after successful retrieval and email sending.
                    txtUserID.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                }
                else
                {
                    // Display a message for user not found or incorrect information.
                    lblMessage.Text = "User not found or the provided information is incorrect.";

                    // Clear the success message label.
                    GreenMessage.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions, e.g., database connection errors.
                lblMessage.Text = "An error occurred: " + ex.Message;

                // Clear the success message label.
                GreenMessage.Text = string.Empty;
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
                        IsBodyHtml = true
                    };

                    // Email body with HTML and inline CSS styling.
                    string emailBody = $@"
                                        <html>
                                            <head>
                                                <style>
                                                    body {{
                                                        font-family: 'Arial', sans-serif;
                                                        text-align: center;
                                                        background-color: #f2f2f2;
                                                    }}
                                                    .container {{
                                                        width: 80%;
                                                        margin: 0 auto;
                                                        background-color: #ffffff;
                                                        padding: 20px;
                                                        border-radius: 10px;
                                                        text-align: center;
                                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                                    }}
                                                    h2 {{
                                                        color: #3498db;
                                                        text-align: center;
                                                    }}
                                                    p {{
                                                        color: #333;
                                                        text-align: center;
                                                    }}
                                                </style>
                                            </head>
                                            <body>
                                                <div class='container'>
                                                    <h2>Password Recovery</h2>
                                                    <p>Your password is: <strong>{password}</strong></p>
                                                </div>
                                            </body>
                                        </html>
                                    ";

                    message.Body = emailBody;

                    smtpClient.Send(message);
                }

                GreenMessage.Text = "Password recovery email sent successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while sending the email: " + ex.Message;
            }
        }

    }
}
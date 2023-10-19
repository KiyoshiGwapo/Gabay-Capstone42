using Gabay_Final_V2.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using MailKit.Net.Smtp;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayDT();
        }

        private void DisplayDT()
        {
            pending_table.DataSource = reBindPendingTable();
            pending_table.DataBind();
        }

        private DataTable reBindPendingTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT s.studentID, s.name FROM student s
                                INNER JOIN users_table u ON s.user_ID = u.user_ID
                                WHERE u.status = 'pending'";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                return dt;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string studID = hidPersonID.Value;

            UpdateStudent(studID);
            var StudInfo = getStudEmailInfo(studID);

            string StudEmail = StudInfo.Item1;
            string StudName = StudInfo.Item2;

            emailApprovedAccount(StudEmail, StudName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
            DisplayDT();
        }

        private void UpdateStudent(string studentID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string updateQuery = "UPDATE users_table SET status = 'activated' WHERE login_ID = @studentID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public Tuple<string, string> getStudEmailInfo(string studentID)
        {
            string studEmail = "";
            string studeName = "";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string queryStudEmail = "SELECT email, name FROM student WHERE studentID = @student_ID";

                using (SqlCommand cmd = new SqlCommand(queryStudEmail, conn))
                {
                    cmd.Parameters.AddWithValue("@student_ID", studentID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        studEmail = reader["email"].ToString();
                        studeName = reader["name"].ToString();
                    }
                }
            }
            Tuple<string, string> result = new Tuple<string, string>(studEmail, studeName);
            return result;
        }
        public void emailApprovedAccount(string studentEmail, string studentName)
        {
            string emailSubject = "Account Verified";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UC Gabay", "noreply@gmail.com"));
            message.To.Add(new MailboxAddress(studentName, studentEmail));
            message.Subject = emailSubject;

            var builder = new BodyBuilder();
            builder.HtmlBody = @"<p>Dear " + studentName + "," +
                "Your account has been verified and activated.</p> <p>Follo the link here <a href='https://localhost:44341/Views/LoginPages/Student_login.aspx'>Gabay Login</a> to login your account";

            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(ConfigurationManager.AppSettings["SystemEmail"], ConfigurationManager.AppSettings["SystemEmailPass"]);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending error: " + ex.Message);
            }
        }
    }
}
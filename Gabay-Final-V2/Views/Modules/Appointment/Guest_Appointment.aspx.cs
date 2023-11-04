using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.QrCode;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Guest_Appointment : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the minimum date for the date input field
            date.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            if (!IsPostBack)
            {
                // Populate the department dropdown list
                ddlDept(departmentChoices);
            }
        }

        public void ddlDept(DropDownList deptDDL)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_dept, dept_name FROM department";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["dept_name"].ToString(), reader["ID_dept"].ToString());
                    deptDDL.Items.Add(item);
                }

                conn.Close();
            }
        }

        private bool IsAppointmentExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM appointment WHERE [email] = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Get the values from the form fields
            string email = Email.Text;

            // Check if an appointment with the given email already exists
            if (IsAppointmentExists(email))
            {
                SubmitStatusNotSubmitted.Text = "Appointment with this email already exists. (Appointment was Not Sended)";
                SubmitStatusNotSubmitted.CssClass = "status-not-submitted";
            }
            else
            {
                // The appointment doesn't exist; proceed with insertion
                string fullName = FullName.Text;
                string contactNumber = ContactN.Text;
                string selectedTime = time.SelectedValue;
                string selectedDate = date.Value;
                string deptName = departmentChoices.SelectedItem.Text; // Get the selected department name
                string concern = Message.Text; // Get the concern/message

                // Additional data
                string status = "pending";
                string studentID = "guest";
                string courseYear = "guest";

                // Insert data into the "appointment" table
                int appointmentID = InsertAppointmentRecord(fullName, email, studentID, courseYear, contactNumber, selectedDate, selectedTime, deptName, concern, status);

                if (appointmentID > 0)
                {
                    // Send an email
                    SendAppointmentConfirmationEmail(fullName, email, selectedDate, selectedTime, deptName, concern, appointmentID);

                    // Display a success message
                    SubmissionStatusSubmitted.Text = "Appointment submitted successfully.";
                    SubmissionStatusSubmitted.CssClass = "status-submitted";

                    // Clear form fields
                    FullName.Text = "";
                    Email.Text = "";
                    ContactN.Text = "";
                    time.SelectedIndex = 0; // Reset the dropdown selection
                    date.Value = "";
                    departmentChoices.SelectedIndex = 0; // Reset the dropdown selection
                    Message.Text = "";
                }
            }
        }

        private int InsertAppointmentRecord(string fullName, string email, string studentID, string courseYear, string contactNumber, string selectedDate, string selectedTime, string deptName, string concern, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO appointment ([deptName], [full_name], [email], [student_ID], [course_year], [contactNumber], [appointment_date], [appointment_time], [concern], [appointment_status]) " +
                    "VALUES (@DeptName, @FullName, @Email, @StudentID, @CourseYear, @ContactNumber, @SelectedDate, @SelectedTime, @Concern, @Status); SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DeptName", deptName);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@CourseYear", courseYear);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
                    cmd.Parameters.AddWithValue("@SelectedTime", selectedTime);
                    cmd.Parameters.AddWithValue("@Concern", concern);
                    cmd.Parameters.AddWithValue("@Status", status);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void SendAppointmentConfirmationEmail(string fullName, string email, string selectedDate, string selectedTime, string deptName, string concern, int appointmentID)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UC Gabay", "noReply@noReply.com"));
            message.To.Add(new MailboxAddress("Recipient", email));
            message.Subject = "Appointment Details";

            var builder = new BodyBuilder();

            builder.HtmlBody = $@"
        <div style='text-align: center;margin-bottom: 10px;'>
            <div>
                <img src='cid:logo-image' style='width: 100px; height: auto; margin-right: 5px; display: block; margin: 0 auto;'>
            </div>
            <div style='letter-spacing: 3px; color: #003366; font-weight: 600;'>
                GABAY
            </div>
        </div>";

            // Add additional appointment details
            builder.HtmlBody += $@"<div style='text-align: center;'><h1>You have successfully booked an appointment</h1></div>
                        <div style='text-align: center;'>
                        <p>Our team is currently verifying the availability of the chosen time and date.</p>
                        <p>Please stay connected with your email for additional updates regarding your appointment schedule.</p>
                        <p>Hello!<b> {fullName}</b>, your appointment is set. Please see the details below:</p>
                        <p><b>Appointment ID:</b> {appointmentID}</p>
                        <p><b>Appointment Date:</b> {selectedDate}</p>
                        <p><b>Appointment Time:</b> {selectedTime}</p>
                        <p><b>Department:</b> {deptName}</p>
                        <p><b>Concern:</b> {concern}</p>
                        </div>";

            var logoImage = builder.LinkedResources.Add("C:\\Users\\rodri\\source\\repos\\Gabay-Capstone42\\Gabay-Final-V2\\Resources\\Images\\UC-LOGO.png");
            logoImage.ContentId = "logo-image";
            logoImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

            message.Body = builder.ToMessageBody();

            // Send the email using MailKit
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(ConfigurationManager.AppSettings["SystemEmail"], ConfigurationManager.AppSettings["SystemEmailPass"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        //Search sa iyang appointment
        protected void searchResultsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                // No results found, show the label
                noResultsLabel.Visible = true;
            }
            else
            {
                // Results found, hide the label
                noResultsLabel.Visible = false;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(searchInput.Text, out int appointmentID))
            {
                DataTable searchResults = SearchAppointmentsByAppointmentID(appointmentID);

                searchResultsGridView.DataSource = searchResults;
                searchResultsGridView.DataBind();

                noResultsLabel.Visible = searchResults.Rows.Count == 0;
            }
            else
            {
                // Handle the case where the input is not a valid integer
                noResultsLabel.Visible = true;
            }
        }





        private DataTable SearchAppointmentsByAppointmentID(int appointmentID)
        {
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT [full_name], [appointment_status] FROM appointment WHERE [ID_appointment] = @AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(results);
                    }
                }
            }

            return results;
        }



    }
}
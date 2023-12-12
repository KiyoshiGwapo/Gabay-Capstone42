using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Student_Appointment : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateDateOptions();
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    bool hasAppointment = checkUsersAppointmentStatus(userID);
                    if (hasAppointment)
                    {
                        Response.Redirect("~/Views/Modules/Appointment/Appointment_Status.aspx");
                    }
                    retrieveStudentInfo(userID);
                }
            }
        }

        public bool checkUsersAppointmentStatus(int userID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT a.appointment_status
                         FROM appointment a
                         INNER JOIN users_table u ON a.student_ID = u.login_ID
                         WHERE u.user_ID = @userID AND (a.appointment_status != 'served' AND a.appointment_status != 'no show' AND a.appointment_status != 'rejected')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        //Populate textboxes
        public void retrieveStudentInfo(int userID)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT s.name, s.email, s.contactNumber, s.studentID, s.course_year, d.dept_name
                                 FROM student s
                                 INNER JOIN department d ON s.department_ID = d.ID_dept
                                 WHERE s.user_ID = @userID ";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID); 
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            FullName.Text = reader["name"].ToString();
                            Email.Text = reader["email"].ToString();
                            ContactN.Text = reader["contactNumber"].ToString();
                            IdNumber.Text = reader["studentID"].ToString();
                            Year.Text = reader["course_year"].ToString();
                            DepartmentName.Text = reader["dept_name"].ToString();
                        }
                    }
                }
            }
        }

        protected void ViewHistoryButton_Click(object sender, EventArgs e)
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                Response.Redirect($"AppointmentHistory.aspx?userID={userID}");
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that all fields are filled
                if (ValidateForm())
                {
                    string fullname = FullName.Text;
                    string email = Email.Text;
                    string ConNum = ContactN.Text;
                    string StudIdNum = IdNumber.Text;
                    string CourseYear = Year.Text;
                    string departmentName = DepartmentName.Text;

                    string SchedDate = date.Text;
                    string SchedTime = time.Text;
                    string Concern = Message.Text;

                    if (Session["user_ID"] != null)
                    {
                        SaveAppointmentDetails(fullname, email, ConNum, StudIdNum, CourseYear, departmentName, SchedDate, SchedTime, Concern);

                        // Show success modal
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);

                        Response.Redirect("~/Views/Modules/Appointment/Appointment_Status.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error modal
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">{ex.Message}</span>');", true);
            }
        }

        private bool ValidateForm()
        {
            // Validate that all fields are filled
            if (string.IsNullOrEmpty(FullName.Text) ||
                string.IsNullOrEmpty(Email.Text) ||
                string.IsNullOrEmpty(ContactN.Text) ||
                string.IsNullOrEmpty(IdNumber.Text) ||
                string.IsNullOrEmpty(Year.Text) ||
                string.IsNullOrEmpty(DepartmentName.Text) ||
                string.IsNullOrEmpty(date.Text) ||
                string.IsNullOrEmpty(time.Text) ||
                string.IsNullOrEmpty(Message.Text))
            {
                // Show an error message for empty fields
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Please fill in all fields.</span>');", true);

                return false;
            }

            return true;
        }

        public void SaveAppointmentDetails(string fullname, string email, string ConNum,
            string StudIdNum, string CourseYear, string DepartmentName, string SchedDate,
            string SchedTime, string Concern)
        {
            string statusSched = "pending";
            string notificationStatus = "UNREAD";
            Concern = Concern.Replace("<br>", "\n");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO appointment (deptName, full_name, email, student_ID, course_year,
                                                          contactNumber, appointment_date, appointment_time,
                                                          concern, appointment_status, Notification)
                                 VALUES (@DepartmentName, @fullname, @email, @StudIdNum, @CourseYear, @ConNum,
                                         @SchedDate, @SchedTime, @Concern, @statusSched, @Notification)";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@StudIdNum", StudIdNum);
                    cmd.Parameters.AddWithValue("@CourseYear", CourseYear);
                    cmd.Parameters.AddWithValue("@ConNum", ConNum);

                    cmd.Parameters.AddWithValue("@SchedDate", SchedDate);
                    cmd.Parameters.AddWithValue("@SchedTime", SchedTime);
                    cmd.Parameters.AddWithValue("@Concern", Concern);
                    cmd.Parameters.AddWithValue("@statusSched", statusSched);
                    cmd.Parameters.AddWithValue("@Notification", notificationStatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void UpdateDateOptions()
        {
            // Set the minimum date to today + 3 days
            date.Attributes["min"] = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
        }

        protected void date_TextChanged(object sender, EventArgs e)
        {
            string selectedDate = date.Text;
            string selectedDept = DepartmentName.Text;

            SelectedDate.Value = selectedDate;

            checkAppointmentTime(selectedDept, selectedDate);
        }

        public void checkAppointmentTime(string selectedDept, string selectedDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT appointment_time from appointment
                                 WHERE deptName = @selectedDept
                                 AND appointment_date = @selectedDate
                                 AND (appointment_status != 'rejected' AND appointment_status != 'served' AND appointment_status != 'no show')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@selectedDept", selectedDept);
                    cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are any rows in the result set
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string bookedTime = reader["appointment_time"].ToString();
                                ListItem item = time.Items.FindByValue(bookedTime);
                                if (item != null)
                                {
                                    item.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

       
    }
}
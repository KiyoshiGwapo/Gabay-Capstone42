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
using System.Data;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Student_Appointment : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the minimum date for the date input field
            date.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            if (!IsPostBack)
            {
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
                            DepartmentDropDown.Text = reader["dept_name"].ToString();
                        }
                    }
                }
            }
        }


        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string fullname = FullName.Text;
            string email = Email.Text;
            string ConNum = ContactN.Text;
            string StudIdNum = IdNumber.Text;
            string CourseYear = Year.Text;
            string DepartmentName = DepartmentDropDown.Text;

            string SchedDate = date.Value;
            string SchedTime = time.Text;
            string Concern = Message.Text;

            if (Session["user_ID"] != null)
            {
                SaveAppointmentDetails(fullname, email, ConNum, StudIdNum, CourseYear, DepartmentName, SchedDate, SchedTime, Concern);
                Response.Redirect("~/Views/Modules/Appointment/Appointment_Status.aspx");
            }
           
            
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
    }
}
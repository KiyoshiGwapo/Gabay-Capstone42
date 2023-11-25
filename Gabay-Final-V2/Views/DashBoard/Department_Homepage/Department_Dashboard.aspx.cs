using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.DashBoard.Department_Homepage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    string userName = FetchSessionStringDept(userID);

                    lblDept_name.Text = userName.ToUpper();
                }
                // Call the method to retrieve and display the user count
                UpdateUserCount();
                ActiveUserCount();
                PendingUserCount();
                ApprovedAppointmentCount();
                PendingAppointmentCount();
                DeniedAppointmentCount();
                RescheduleAppointmentCount();
            }
        }

        public string FetchSessionStringDept(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryFetchSession = "SELECT dept_name FROM department WHERE user_ID = @userID";

                using (SqlCommand cmd = new SqlCommand(queryFetchSession, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
        private void UpdateUserCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count users with role value 3
                    string query = "SELECT COUNT(*) FROM student s INNER JOIN department d ON s.department_ID = d.ID_dept WHERE d.user_ID = @userID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int userCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the userCountLabel to display the user count
                        userCountLabel.Text = userCount.ToString();
                    }
                }

            }
        }
        private void ActiveUserCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count users with role value 3
                    string query = @"SELECT COUNT(*) FROM student s
                        INNER JOIN department d ON s.department_ID = d.ID_dept 
                        INNER JOIN users_table u ON s.user_ID = u.user_ID
                        WHERE d.user_ID = @userID AND u.status = 'activated'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int userActiveCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the userCountLabel to display the user count
                        ActiveuserCountLabel.Text = userActiveCount.ToString();
                    }
                }

            }

        }
        private void PendingUserCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count users with role value 3
                    string query = @"SELECT COUNT(*) FROM student s
                        INNER JOIN department d ON s.department_ID = d.ID_dept 
                        INNER JOIN users_table u ON s.user_ID = u.user_ID
                        WHERE d.user_ID = @userID AND u.status = 'pending'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int userPendingCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the userCountLabel to display the user count
                        PendinguserCountLabel.Text = userPendingCount.ToString();
                    }
                }

            }

        }

        //Appointment Statuses
        private void ApprovedAppointmentCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count approved appointments in the department
                    string query = @"SELECT COUNT(*) FROM appointment
                             WHERE deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
                             AND appointment_status = 'approved'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int approvedAppointmentCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the ApprovedAppointmentCountLabel to display the approved appointment count
                        ApprovedAppointmentCountLabel.Text = approvedAppointmentCount.ToString();
                    }
                }
            }
        }

        private void PendingAppointmentCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count approved appointments in the department
                    string query = @"SELECT COUNT(*) FROM appointment
                             WHERE deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
                             AND appointment_status = 'pending'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int pendingAppointmentCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the ApprovedAppointmentCountLabel to display the approved appointment count
                        PendingAppointmentCountLabel.Text = pendingAppointmentCount.ToString();
                    }
                }
            }
        }

        private void DeniedAppointmentCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count approved appointments in the department
                    string query = @"SELECT COUNT(*) FROM appointment
                             WHERE deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
                             AND appointment_status = 'denied'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int deniedAppointmentCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the ApprovedAppointmentCountLabel to display the approved appointment count
                        DeniedAppointmentCountLabel.Text = deniedAppointmentCount.ToString();
                    }
                }
            }
        }

        private void RescheduleAppointmentCount()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL command to count approved appointments in the department
                    string query = @"SELECT COUNT(*) FROM appointment
                             WHERE deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
                             AND appointment_status = 'reschedule'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        int rescheduleAppointmentCount = Convert.ToInt32(command.ExecuteScalar());

                        // Set the Text property of the ApprovedAppointmentCountLabel to display the approved appointment count
                        RescheduleAppointmentCountLabel.Text = rescheduleAppointmentCount.ToString();
                    }
                }
            }
        }


        //string query = @"SELECT a.*, ur.role
        //                FROM appointment a
        //                INNER JOIN users_table u ON a.student_ID = u.login_ID
        //                INNER JOIN user_role ur ON u.role_ID = ur.role_id
        //                WHERE a.deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
        //                AND a.appointment_status = 'approved';";



    }
}
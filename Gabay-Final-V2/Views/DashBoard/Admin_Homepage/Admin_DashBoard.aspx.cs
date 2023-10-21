using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Gabay_Final_V2.Views.DashBoard.Admin_Homepage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Define the connection string as a private field
        private string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    string userName = FetchSessionStringAdmin(userID);

                    lblDept_name.Text = userName;
                    // Call the method to retrieve and display the user count
                    StudentUserCount();
                    DepartmentUserCount();
                    StudentApprovedUserCount();
                    StudentPendingUserCount();
                    BarUserCounts();
                }
            }
        }

        public string FetchSessionStringAdmin(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryFetchSession = "SELECT login_ID FROM users_table WHERE user_ID = @userID";

                using (SqlCommand cmd = new SqlCommand(queryFetchSession, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

        // Students user count
        private void StudentUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to count users with role value 3
                string query = "SELECT COUNT(*) FROM users_table WHERE role_ID = '3'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int studentuserCount = Convert.ToInt32(command.ExecuteScalar());

                    // Set the Text property of the userCountLabel to display the user count
                    StudentuserCountLabel.Text = studentuserCount.ToString();
                }
            }
        }

        private void DepartmentUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to count users with role value 3
                string query = "SELECT COUNT(*) FROM users_table WHERE role_ID = '2'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int depatmentuserCount = Convert.ToInt32(command.ExecuteScalar());

                    // Set the Text property of the userCountLabel to display the user count
                    DepatmentuserCountLabel.Text = depatmentuserCount.ToString();
                }
            }
        }
        //KATU SUD SA BAR
        private void StudentApprovedUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to count users with role value 3
                string query = @"SELECT COUNT(*) FROM users_table WHERE role_ID = '3' AND status = 'activated'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int depatmentuserCount = Convert.ToInt32(command.ExecuteScalar());

                    // Set the Text property of the userCountLabel to display the user count
                    StudentApprovedUserCountLabel.Text = depatmentuserCount.ToString();
                }
            }
        }
        private void StudentPendingUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to count users with role value 3
                string query = "SELECT COUNT(*) FROM users_table WHERE role_ID = '3' AND status = 'pending'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int depatmentuserCount = Convert.ToInt32(command.ExecuteScalar());

                    // Set the Text property of the userCountLabel to display the user count
                    StudentPendingUserCountLabel.Text = depatmentuserCount.ToString();
                }
            }
        }

        //KATUNG BAR CHART NANI


        private int GetAllStudentUserCount(string studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM student " +
                      "INNER JOIN department ON student.department_ID = department.ID_dept " +
                      "WHERE student.department_ID = @StudentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    int studentCount = Convert.ToInt32(command.ExecuteScalar());
                    return studentCount;
                }
            }
        }

        private void BarUserCounts()
        {
            BarStudentsCcsCountLabel.Value = GetAllStudentUserCount("1").ToString();
            BarStudentsEngineerUserCountLabel.Value = GetAllStudentUserCount("21").ToString();
            BarStudentsNursingUserCountLabel.Value = GetAllStudentUserCount("15").ToString();
            BarStudentsCustomUserCountLabel.Value = GetAllStudentUserCount("1016").ToString();
            BarStudentsNauticalUserCountLabel.Value = GetAllStudentUserCount("1017").ToString();
            BarStudentsMarineEUserCountLabel.Value = GetAllStudentUserCount("1018").ToString();
            BarStudentsCrimUserCountLabel.Value = GetAllStudentUserCount("1019").ToString();
            // Add more department ari
        }



    }
}
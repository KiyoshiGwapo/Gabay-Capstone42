using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                    string userName = FetchSessionStringAdmin(userID).ToUpper();

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
        // Students user count
        private void StudentUserCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SQL command to count users with role value 3
                string query = "SELECT COUNT(*) FROM users_table WHERE role_ID = '3' AND status NOT IN ('pending', 'deactivated')";
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


        private int GetDepartmentUserCount(string departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM student WHERE department_ID = @DepartmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    int departmentCount = Convert.ToInt32(command.ExecuteScalar());
                    return departmentCount;
                }
            }
        }

        private void BarUserCounts()
        {
            BarStudentsCCSUserCountLabel.Value = GetDepartmentUserCount("2024").ToString();
            BarStudentsNursingUserCountLabel.Value = GetDepartmentUserCount("2025").ToString();
            BarStudentsCriminologyUserCountLabel.Value = GetDepartmentUserCount("2027").ToString();
            BarStudentsTourismHospitalityCountLabel.Value = GetDepartmentUserCount("2028").ToString();
            BarStudentsBusinessAdministrationandAccountancyUserCountLabel.Value = GetDepartmentUserCount("2031").ToString();
            BarStudentsCustomsAdministrationUserCountLabel.Value = GetDepartmentUserCount("2032").ToString();

            BarStudentsMarineTransportationCountLabel.Value = GetDepartmentUserCount("2033").ToString();
            BarStudentsMarineEngineeringCountLabel.Value = GetDepartmentUserCount("2033").ToString();

            BarStudentsElectronicsandCommunicationEngineeringUserCountLabel.Value = GetDepartmentUserCount("0").ToString();
            BarStudentsElectricalEngineeringUserCountLabel.Value = GetDepartmentUserCount("0").ToString();
            BarStudentsMechanicalEngineeringnUserCountLabel.Value = GetDepartmentUserCount("0").ToString();

            BarStudentsIndustrialEngineeringCountLabel.Value = GetDepartmentUserCount("0").ToString();
            BarStudentsComputerEngineeringCountLabel.Value = GetDepartmentUserCount("0").ToString();

            // Add more department ari  
        }
            


    }
}
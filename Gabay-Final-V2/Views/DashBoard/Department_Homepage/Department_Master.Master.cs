using Gabay_Final_V2.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;

namespace Gabay_Final_V2.Views.DashBoard.Department_Homepage
{
    public partial class Department_Master : System.Web.UI.MasterPage
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //This is to disable caching for the dashboard to prevent backward login
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                //Database connection
                DbUtility conn = new DbUtility();
                //Setting the session ID for the user
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    string userName = conn.FetchSessionStringDept(userID);

                    lblDept_name.Text = userName;

                    FetchUnreadNotifications();
                    // Hide the badge if there are no unread notifications
                    HideBadgeIfNoUnreadNotifications();
                }
                else
                {
                    //Redirects user if login credentials is not valid
                    Response.Redirect("..\\..\\..\\Views\\Loginpages\\Department_login.aspx");

                }
            }
        }

        private void FetchUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                NotificationResult result = GetUnreadNotificationsDataTableFromDatabase(userID);

                // Display the count of unread notifications
                lblNotificationCount.Text = result.Count.ToString();

                // Bind the data to the GridView
                notificationGridView.DataSource = result.Data;
                notificationGridView.DataBind();
            }
        }


        public NotificationResult GetUnreadNotificationsDataTableFromDatabase(int userID)
        {
            NotificationResult result = new NotificationResult();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Your query to get the count of unread notifications
                    string countQuery = @"
                SELECT COUNT(*) AS UnreadCount
                FROM appointment
                WHERE Notification = 'UNREAD' AND deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
            ";

                    using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                    {
                        countCmd.Parameters.AddWithValue("@userID", userID);
                        result.Count = (int)countCmd.ExecuteScalar();
                    }

                    // Your query to get the actual notification data
                    string dataQuery = @"
                SELECT ID_appointment, appointment_date, full_name, appointment_status
                FROM appointment
                WHERE Notification = 'UNREAD' AND deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
            ";

                    using (SqlCommand dataCmd = new SqlCommand(dataQuery, conn))
                    {
                        dataCmd.Parameters.AddWithValue("@userID", userID);
                        SqlDataAdapter da = new SqlDataAdapter(dataCmd);
                        result.Data = new DataTable();
                        da.Fill(result.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them for debugging.
                Response.Write("Error: " + ex.Message);
            }

            return result;
        }



        protected void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);

                // Call a method to update the notification status in the database
                MarkNotificationsAsRead(userID);

                // Fetch and display the updated notifications
                FetchUnreadNotifications();

                // Hide the badge after marking notifications as read
                ScriptManager.RegisterStartupScript(this, GetType(), "hideBadgeScript", "hideBadge();", true);
            }
        }

        private void MarkNotificationsAsRead(int userID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    string query = @"
                UPDATE appointment
                SET Notification = 'READ'
                WHERE Notification = 'UNREAD' AND deptName = (SELECT dept_name FROM department WHERE user_ID = @userID)
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them for debugging.
                Response.Write("Error: " + ex.Message);
            }
        }

        public NotificationResult GetUnreadNotificationsDataTableFromDatabase()
        {
            NotificationResult result = new NotificationResult();

            try
            {
                string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    // Your query to get the count of unread notifications
                    string countQuery = @"
                        SELECT COUNT(*) AS UnreadCount
                        FROM AppointmentStatusHistory ash
                        LEFT JOIN appointment a ON ash.AppointmentID = a.ID_appointment
                        INNER JOIN users_table u ON a.student_ID = u.login_ID
                        WHERE ash.Notification = 'UNREAD' AND u.user_ID = @userID
                    ";

                    using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                    {
                        if (Session["user_ID"] != null)
                        {
                            int user_ID = Convert.ToInt32(Session["user_ID"]);
                            countCmd.Parameters.AddWithValue("@userID", user_ID);
                            result.Count = (int)countCmd.ExecuteScalar();
                        }
                    }

                    // Your query to get the actual notification data
                    string dataQuery = @"
            SELECT ash.AppointmentID, ash.StatusChangeDate, ash.NewStatus
            FROM AppointmentStatusHistory ash
            LEFT JOIN appointment a ON ash.AppointmentID = a.ID_appointment
            INNER JOIN users_table u ON a.student_ID = u.login_ID
            WHERE ash.Notification = 'UNREAD' AND u.user_ID = @userID
            ORDER BY ash.StatusChangeDate DESC
        ";

                    using (SqlCommand dataCmd = new SqlCommand(dataQuery, conn))
                    {
                        if (Session["user_ID"] != null)
                        {
                            int user_ID = Convert.ToInt32(Session["user_ID"]);
                            dataCmd.Parameters.AddWithValue("@userID", user_ID);
                            SqlDataAdapter da = new SqlDataAdapter(dataCmd);
                            result.Data = new DataTable();
                            da.Fill(result.Data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                // You might want to log errors to a file or a logging service.
                Console.WriteLine("Error: " + ex.Message);
            }

            return result;
        }

        public class NotificationResult
        {
            public int Count { get; set; }
            public DataTable Data { get; set; }
        }

        private void HideBadgeIfNoUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                NotificationResult result = GetUnreadNotificationsDataTableFromDatabase(userID);

                // Check if there are no unread notifications
                if (result != null && result.Data != null && result.Data.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideBadgeScript", "hideBadge();", true);
                }
            }
        }



        protected void prflBtn_Click(object sender, EventArgs e)
        {
            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Department_login.aspx");
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Department_login.aspx");
        }
    }
}
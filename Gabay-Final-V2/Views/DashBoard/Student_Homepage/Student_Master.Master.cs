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

namespace Gabay_Final_V2.Views.DashBoard.Student_Homepage
{
    public partial class Student_Master : System.Web.UI.MasterPage
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //This is to disable caching for the dashboard to prevent backward login
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //database connection
            DbUtility conn = new DbUtility();
            //Setting the session ID for the user
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);

                string userName = conn.FetchSessionStringStud(userID);

                lblStud_name.Text = userName;

                FetchUnreadNotifications();
                // Hide the badge if there are no unread notifications
                HideBadgeIfNoUnreadNotifications();
            }
            else
            {
                //Redirects user if login credentials is not valid
                Response.Redirect("..\\..\\..\\Views\\Loginpages\\Student_login.aspx");
            }
                //FetchUnreadNotifications();

        }

        private void FetchUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                DataTable dt = GetUnreadNotificationsFromDatabase(userID);

                // Bind the data to the GridView
                notificationGridView.DataSource = dt;
                notificationGridView.DataBind();
            }
        }
        private DataTable GetUnreadNotificationsFromDatabase(int userID)
        {
            DataTable notificationData = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    string query = @"
                SELECT ash.*
                FROM AppointmentStatusHistory ash
                LEFT JOIN appointment a ON ash.AppointmentID = a.ID_appointment
                INNER JOIN users_table u ON a.student_ID = u.login_ID
                WHERE ash.Notification = 'UNREAD' AND u.user_ID = @userID
                ORDER BY ash.StatusChangeDate DESC
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(notificationData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them for debugging.
                Response.Write("Error: " + ex.Message);
            }

            return notificationData;
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
                UPDATE AppointmentStatusHistory
                SET Notification = 'READ'
                FROM AppointmentStatusHistory ash
                INNER JOIN appointment a ON ash.AppointmentID = a.ID_appointment
                INNER JOIN users_table u ON a.student_ID = u.login_ID
                WHERE ash.Notification = 'UNREAD' AND u.user_ID = @userID
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
                DataTable dt = GetUnreadNotificationsFromDatabase(userID);

                // Check if there are no unread notifications
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideBadgeScript", "hideBadge();", true);
                }
            }
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Student_login.aspx");
        }

        protected void logoutLink_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Student_login.aspx");
        }
    }
}
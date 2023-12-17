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

namespace Gabay_Final_V2.Views.DashBoard.Admin_Homepage
{
    public partial class Admin_Master : System.Web.UI.MasterPage
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

                string userName = conn.FetchSessionStringAdmin(userID);

                lblDept_name.Text = "Hello! " + userName;

                FetchUnreadNotifications();
                HideBadgeIfNoUnreadNotifications();
            }
            else
            {
                //Redirects user if login credentials is not valid
                Response.Redirect("..\\..\\..\\Views\\Loginpages\\Admin_login.aspx");
            }
        }

        protected void logoutLink_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Admin_login.aspx");
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Admin_login.aspx");
        }

        private void FetchUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                int roleID = 3;
                NotificationResult result = GetUnreadNotificationsDataTableFromDatabase(userID, roleID);

                lblNotificationCount.Text = result.Count.ToString();

                notificationGridView.DataSource = result.Data;
                notificationGridView.DataBind();
            }
        }


        protected void BtnMarkAsRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    // Mark notifications as read
                    MarkNotificationsAsRead();

                    // Fetch and refresh unread notifications
                    FetchUnreadNotifications();

                    // Hide the badge if no unread notifications
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideBadgeScript", "hideBadge();", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error HAHA " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }


        [WebMethod]
        public NotificationResult GetUnreadNotificationsDataTableFromDatabase(int userID, int roleID = 3)
        {
            NotificationResult result = new NotificationResult();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    string countQuery = @"
                        SELECT COUNT(*) AS UnreadCount
                        FROM users_table
                        WHERE Notification = 'UNREAD' AND role_ID = @roleID
                    ";

                    using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                    {
                        countCmd.Parameters.AddWithValue("@roleID", roleID);
                        result.Count = (int)countCmd.ExecuteScalar();
                    }

                    string dataQuery = @"
                        SELECT u.user_ID, s.name AS UserName, u.status
                        FROM users_table u
                        INNER JOIN student s ON u.user_ID = s.user_ID
                        WHERE u.Notification = 'UNREAD' AND u.role_ID = @roleID
                        ORDER BY u.user_ID DESC
                    ";


                    using (SqlCommand dataCmd = new SqlCommand(dataQuery, conn))
                    {
                        dataCmd.Parameters.AddWithValue("@roleID", roleID);
                        SqlDataAdapter da = new SqlDataAdapter(dataCmd);
                        result.Data = new DataTable();
                        da.Fill(result.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Response.Write("Error: " + ex.Message);
            }

            return result;
        }


        public class NotificationResult
        {
            public int Count { get; set; }
            public DataTable Data { get; set; }
        }

        private bool HasUnreadNotifications(int userID)
        {
            // Check if there are any unread notifications for the user
            NotificationResult result = GetUnreadNotificationsDataTableFromDatabase(userID);

            return result.Count > 0;
        }

        private void HideBadgeIfNoUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);

                // Check if there are no unread notifications
                if (!HasUnreadNotifications(userID))
                {
                    // If there are no unread notifications, hide the badge
                    lblNotificationCount.Style.Add("display", "none");
                }
            }
        }

        private void MarkNotificationsAsRead()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    string updateQuery = @"
                UPDATE users_table
                SET Notification = 'READ'
                WHERE Notification = 'UNREAD'
            ";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while marking notifications as read: " + ex.Message;
                ShowErrorModal(errorMessage);
            }
        }


        // pakita error
        private void ShowErrorModal(string errorMessage)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
        }
    }
}
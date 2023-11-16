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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            DbUtility conn = new DbUtility();

            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);

                string userName = conn.FetchSessionStringStud(userID);

                lblStud_name.Text = userName;

                FetchUnreadNotifications();
                HideBadgeIfNoUnreadNotifications();
            }
            else
            {
                Response.Redirect("..\\..\\..\\Views\\Loginpages\\Student_login.aspx");
            }
        }

        private void FetchUnreadNotifications()
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                NotificationResult result = GetUnreadNotificationsDataTableFromDatabase(userID);

                lblNotificationCount.Text = result.Count.ToString();

                notificationGridView.DataSource = result.Data;
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
                        SELECT COUNT(*) ash UnreadCount
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
            catch /*(Exception ex)*/
            {
                //Response.Write("Error: " + ex.Message);
            }

            return notificationData;
        }

        protected void BtnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);

                MarkNotificationsAsRead(userID);

                FetchUnreadNotifications();

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
                Response.Write("Error: " + ex.Message);
            }
        }

        public NotificationResult GetUnreadNotificationsDataTableFromDatabase(int userID)
        {
            NotificationResult result = new NotificationResult();

            try
            {
                string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();

                    string countQuery = @"
                        SELECT COUNT(*) AS UnreadCount
                        FROM AppointmentStatusHistory ash
                        LEFT JOIN appointment a ON ash.AppointmentID = a.ID_appointment
                        INNER JOIN users_table u ON a.student_ID = u.login_ID
                        WHERE ash.Notification = 'UNREAD' AND u.user_ID = @userID
                    ";

                    using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                    {
                        countCmd.Parameters.AddWithValue("@userID", userID);
                        result.Count = (int)countCmd.ExecuteScalar();
                    }

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
                        dataCmd.Parameters.AddWithValue("@userID", userID);
                        SqlDataAdapter da = new SqlDataAdapter(dataCmd);
                        result.Data = new DataTable();
                        da.Fill(result.Data);
                    }
                }
            }
            catch/* (Exception ex)*/
            {
                //Console.WriteLine("Error: " + ex.Message);
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
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
            }
            else
            {
                //Redirects user if login credentials is not valid
                Response.Redirect("..\\..\\..\\Views\\Loginpages\\Student_login.aspx");
            }
                FetchUnreadNotifications();

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
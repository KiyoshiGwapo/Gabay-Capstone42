using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Gabay_Final_V2.Views.Modules.Announcement
{
    public partial class HistoryLogs : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userID"] != null)
                {
                    int userID = Convert.ToInt32(Request.QueryString["userID"]);
                    BindAppointmentHistory(userID);
                }
                else
                {
                    Response.Redirect("..\\DashBoard\\Student_Homepage\\Student_Dashboard.aspx");
                }
            }
        }

        protected void BindAppointmentHistory(int userID)
        {
            try
            {
                // Query the database to retrieve the appointment history for the student
                DataTable appointmentData = GetAppointmentHistoryFromDatabase(userID);

                if (appointmentData != null && appointmentData.Rows.Count > 0)
                {
                    GridView1.DataSource = appointmentData;
                    GridView1.DataBind();
                }
                else
                {
                    // Handle the case when no data is found
                    // You can display a message or take appropriate action.
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them for debugging.
                Response.Write("Error: " + ex.Message);
            }
        }

        private DataTable GetAppointmentHistoryFromDatabase(int userID)
        {
            DataTable appointmentData = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT a.ID_appointment, a.deptName, a.full_name, a.email, a.student_ID,
                    a.course_year, a.contactNumber, a.appointment_date, a.appointment_time,
                    a.concern, a.appointment_status
                FROM appointment AS a
                INNER JOIN users_table AS u ON a.student_ID = u.login_ID
                WHERE u.user_ID = @userID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(appointmentData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them for debugging.
                Response.Write("Error: " + ex.Message);
            }

            return appointmentData;
        }

    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Appointment_Status : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);
                    populateAppointmentLabel(userID);
                }
                else
                {
                    Response.Redirect("..\\DashBoard\\Student_Homepage\\Student_Dashboard.aspx");
                }
            }

        }
        public void populateAppointmentLabel(int userID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.ID_appointment, a.appointment_status, a.appointment_date, a.appointment_time, a.concern, a.student_ID
                        FROM appointment a
                        INNER JOIN users_table u ON a.student_ID = u.login_ID
                        WHERE u.user_ID = @userID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointmentID.Text = reader["ID_appointment"].ToString();
                            appointmentStatus.Text = reader["appointment_status"].ToString();
                            DateTime date = (DateTime)reader["appointment_date"];
                            appointmentDate.Text = date.ToString("MMMM-dd-yyyy");
                            appointmentTime.Text = reader["appointment_time"].ToString();
                            string formattedConcern = reader["concern"].ToString().Replace("\n", "<br />");
                            appointmentConcern.Text = formattedConcern;


                            string pendingIndication = @"<b>You have successfully booked an appointment.</b><br>
                                Our team is currently verifying the availability of the chosen time and date.<br> 
                                Please stay connected with your email for additional updates regarding your appointment schedule.";
                            string approvedIndication = @"<b>Your appointment is all set!</b><br>
                                The schedule of your appointment is ready please see the details below:<br> 
                                Please provide the QR code that sent to you via email.";
                            string rescheduleIndication = @"<b>Appointment Reschedule</b><br>
                                Appointment Schedule has been changed please see the details below.";

                            // Set the Indication text based on appointment status
                            string status = reader["appointment_status"].ToString();
                            if (status == "pending")
                            {
                                Indication.Text = pendingIndication;
                            }
                            else if (status == "approved")
                            {
                                Indication.Text = approvedIndication;
                            }
                            else if (status == "reschedule")
                            {
                                Indication.Text = rescheduleIndication;
                            }
                        }
                    }
                }
            }
        }

        protected void ViewHistoryButton_Click(object sender, EventArgs e)
        {
            if (Session["user_ID"] != null)
            {
                int userID = Convert.ToInt32(Session["user_ID"]);
                Response.Redirect($"HistoryLogs.aspx?userID={userID}");
            }
        }


    }
}
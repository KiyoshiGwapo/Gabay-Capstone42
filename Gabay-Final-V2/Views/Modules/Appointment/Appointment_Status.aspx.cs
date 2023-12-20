using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ZXing.QrCode;
using ZXing;
using System.Web.UI.WebControls;
using System.Web.UI;

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
                    showReschedBtns();
                }
                else
                {
                    Response.Redirect("..\\..\\LoginPages\\Student_login.aspx");
                }
            }

        }
        public void showReschedBtns()
        {
            string appointmentStatusValue = appointmentStatus.Text;
            reschedBtns.Visible = (appointmentStatusValue == "reschedule");
        }
        
        public void populateAppointmentLabel(int userID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.ID_appointment, a.appointment_status, a.appointment_date, a.appointment_time, a.concern, a.student_ID
                        FROM appointment a
                        INNER JOIN users_table u ON a.student_ID = u.login_ID
                        WHERE u.user_ID = @userID AND (a.appointment_status != 'served' AND a.appointment_status != 'no show' AND a.appointment_status != 'rejected')";

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
                            appointmentDate.Text = date.ToString("MMMM dd, yyyy");
                            appointmentTime.Text = reader["appointment_time"].ToString();
                            string formattedConcern = reader["concern"].ToString().Replace("\n", "<br />");
                            appointmentConcern.Text = formattedConcern;


                            string pendingIndication = @"<b>You have successfully booked an appointment.</b><br>
                                Our team is currently verifying the availability of the chosen time and date.<br> 
                                Please stay connected with your email for additional updates regarding your appointment schedule.";
                            string approvedIndication = @"<b>Your appointment is all set!</b><br>
                                The schedule of your appointment is ready please see the details below:<br> 
                                Please provide the QR code that sent to you via email.";
                            string rescheduleIndication = @"<b>Heads up!</b><br>
                                Your appointment date has been changed, would you like to accept this new date?";

                            // Set the Indication text based on appointment status
                            string status = reader["appointment_status"].ToString();
                            if (status == "pending")
                            {
                                Indication.Text = pendingIndication;
                                Image1.ImageUrl = "~/Resources/Images/tempIcons/database.png";
                            }
                            else if (status == "approved")
                            {
                                Indication.Text = approvedIndication;
                                Image1.ImageUrl = "~/Resources/Images/tempIcons/verified.png";
                            }
                            else if (status == "reschedule")
                            {
                                Indication.Text = rescheduleIndication;
                                Image1.ImageUrl = "~/Resources/Images/tempIcons/reschedule-icon-6.jpg";
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
                Response.Redirect($"AppointmentHistory.aspx?userID={userID}");
            }
        }

        protected void AcceptReschedBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int appointmentIDtoInt = Convert.ToInt32(appointmentID.Text);
                approveAppointment(appointmentIDtoInt);
                int userID = Convert.ToInt32(Session["user_ID"]);
                populateAppointmentLabel(userID);
                showReschedBtns();

                string successMessage = "Your schedule is set! Please check your email, we sent you the details of your appointment along with your QR code";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch  (Exception ex)
            {
                string ErrorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                   $"$('#errorMessage').text('{ErrorMessage}'); $('#errorModal').modal('show');", true);
            }
           
        }

        public void approveAppointment(int AppointmentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string getAppointmentInfoQuery = @"SELECT * FROM appointment WHERE ID_appointment = @AppointmentID";
                conn.Open();
                using (SqlCommand setCmd = new SqlCommand(getAppointmentInfoQuery, conn))
                {
                    setCmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                    using (SqlDataReader reader = setCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string appointmentID = reader["ID_appointment"].ToString();
                            string appointee = reader["full_name"].ToString();
                            string destination = GetDepartmentName(Convert.ToInt32(reader["dept_id"]));
                            DateTime date = (DateTime)reader["appointment_date"];
                            string appointmentDate = date.ToString("dd MMM, yyyy ddd");
                            string appointmentTime = reader["appointment_time"].ToString();
                            string appointeeEmail = reader["email"].ToString();

                            string TempUrl = $"https://localhost:44341/Prototype/tempPage.aspx?appointmentID={appointmentID}";

                            BarcodeWriter barcodeWriter = new BarcodeWriter();
                            barcodeWriter.Format = BarcodeFormat.QR_CODE;
                            barcodeWriter.Options = new QrCodeEncodingOptions
                            {
                                Width = 200,
                                Height = 200,
                            };
                            System.Drawing.Bitmap qrCodeBitmap = barcodeWriter.Write(TempUrl);

                            // Save the QR code as a temporary image file
                            string tempQRCodeFilePath = Server.MapPath("~/TempQRCode.png");
                            qrCodeBitmap.Save(tempQRCodeFilePath, System.Drawing.Imaging.ImageFormat.Png);

                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("UC Gabay", "noReply@noReply.com"));
                            message.To.Add(new MailboxAddress("Recipient", appointeeEmail));
                            message.Subject = "Appointment Details";

                            var builder = new BodyBuilder();

                            // Add the logo and QR code centered in the email body
                            builder.HtmlBody = $@"
                                <div style='text-align: center;margin-bottom: 10px;'>
                                    <div>
                                        <img src='cid:logo-image' style='width: 100px; height: auto; margin-right: 5px; display: block; margin: 0 auto;'>
                                    </div>
                                    <div style='letter-spacing: 3px; color: #003366; font-weight: 600;'>
                                        GABAY
                                    </div>
                                </div>
                                <div style='text-align: center;'>
                                    <img src='cid:qr-code-image' width='200' height='200'>
                                </div>";

                            // Add additional appointment details
                            builder.HtmlBody += $@"<div style='text-align: center;'><h1>Your Appointment is all set!</h1></div>
                                                <div style='text-align: center;'>
                                                <p>Hello!<b> {appointee}</b>, your appointment is set please see the details below</p>
                                                <p><b>Appointment ID:</b> {appointmentID}</p>
                                                <p><b>Schedule:</b> {appointmentDate} {appointmentTime}</p>
                                                <p><b>Destination:</b> {destination}</p>
                                                </div>";

                            var logoImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\UC-LOGO.png");
                            logoImage.ContentId = "logo-image";
                            logoImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

                            var qrCodeImage = builder.LinkedResources.Add(tempQRCodeFilePath);
                            qrCodeImage.ContentId = "qr-code-image";
                            qrCodeImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

                            message.Body = builder.ToMessageBody();

                            // Send the email using MailKit
                            using (var client = new SmtpClient())
                            {
                                client.Connect("smtp.gmail.com", 587, false);
                                client.Authenticate(ConfigurationManager.AppSettings["SystemEmail"], ConfigurationManager.AppSettings["SystemEmailPass"]);
                                client.Send(message);
                                client.Disconnect(true);
                            }

                            // Clean up (optional)
                            System.IO.File.Delete(tempQRCodeFilePath);
                        }

                        reader.Close();
                    }
                }


                string query = @"UPDATE appointment SET appointment_status = @AppointmentStats WHERE ID_appointment = @AppointmentID";
                string updateStatus = "approved";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmd.Parameters.AddWithValue("@AppointmentStats", updateStatus);
                    cmd.ExecuteNonQuery();
                }

                // Insert a record into AppointmentStatusHistory
                string queryInsertHistory = @"
                INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus)
                VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus)";
                using (SqlCommand cmdInsertHistory = new SqlCommand(queryInsertHistory, conn))
                {
                    cmdInsertHistory.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmdInsertHistory.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now);
                    cmdInsertHistory.Parameters.AddWithValue("@PreviousStatus", "current_status");
                    cmdInsertHistory.Parameters.AddWithValue("@NewStatus", "approved");
                    cmdInsertHistory.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private string GetDepartmentName(int userID)
        {
            string departmentName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                string query = "SELECT dept_name FROM department WHERE user_ID = @user_ID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@user_ID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            departmentName = reader["dept_name"].ToString();
                        }
                    }
                }
            }

            return departmentName;
        }

        protected void RejectReschedBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showRejectModal", $"$('#rejectModal').modal('show');", true);
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showRejectModal", $"$('#reschedModal').modal('show');", true);
        }

        protected void rejectAppmntCls_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showHideModal", $"$('#rejectModal').modal('hide');", true);
        }

        protected void rejectAppmntBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int appointmentIDtoInt = Convert.ToInt32(appointmentID.Text);
                rejectAppointment(appointmentIDtoInt);
                int userID = Convert.ToInt32(Session["user_ID"]);
                populateAppointmentLabel(userID);

                string successMessage = "Your appointment ticket is now closed";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                   $"$('#errorMessage').text('{ErrorMessage}'); $('#errorModal').modal('show');", true);
            }
        }

        public void rejectAppointment(int AppointmentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE appointment SET appointment_status = @AppointmentStats WHERE ID_appointment = @AppointmentID";
                conn.Open();
                string updateStatus = "rejected";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmd.Parameters.AddWithValue("@AppointmentStats", updateStatus);
                    cmd.ExecuteNonQuery();
                }

                // Insert a record into AppointmentStatusHistory
                string queryInsertHistory = @"
                INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus)
                VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus)";
                using (SqlCommand cmdInsertHistory = new SqlCommand(queryInsertHistory, conn))
                {
                    cmdInsertHistory.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmdInsertHistory.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now); // Current date and time
                    cmdInsertHistory.Parameters.AddWithValue("@PreviousStatus", "current_status"); // Replace with the actual previous status
                    cmdInsertHistory.Parameters.AddWithValue("@NewStatus", "rejected"); // New status
                    cmdInsertHistory.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
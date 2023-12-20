using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
//For Email
using System.IO;
using ZXing;
using ZXing.QrCode;
using MimeKit;
using MailKit.Net.Smtp;
//for json
using System.Configuration;
using System.Web;
using Gabay_Final_V2.Models;
using System.Web.UI.WebControls;
using System.Windows;
using static iTextSharp.text.pdf.PdfDocument;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Linq;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Manage_Appointment : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindingAppointment();
                UpdateDateOptions();
            }
        }

        public class DepartmentUser
        {
            public static event EventHandler AppointmentStatusChanged;

            public static void OnAppointmentStatusChanged(EventArgs e)
            {
                // Raise the event when the appointment status changes.
                AppointmentStatusChanged?.Invoke(null, e);
            }
        }

        private void BindingAppointment()
        {
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                DataTable dt = fetchAppointBasedOnDepartment(user_ID);

                foreach (DataRow row in dt.Rows)
                {
                    string studentID = (string)row["student_ID"];

                    if (studentID == "guest")
                    {
                        // Set "User Type" to "Guest" for guest appointments
                        row["role"] = "guest";
                    }
                }

                AppointmentView.DataSource = dt;
                AppointmentView.DataBind();
            }
        }

        public DataTable fetchAppointBasedOnDepartment(int userID)
        {
            DataTable studentTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                // this is for the db v.1.8
                //string queryFetchStudent = @"SELECT a.*, ur.role
                //                            FROM appointment a
                //                            LEFT JOIN users_table u ON a.student_ID = u.login_ID
                //                            LEFT JOIN user_role ur ON u.role_ID = ur.role_id
                //                            WHERE (a.deptName = (SELECT dept_name FROM department WHERE user_ID = @departmentUserID)
                //                            OR a.student_ID = 'guest')
                //                            AND (a.student_ID <> 'guest' OR a.deptName = (SELECT dept_name FROM department WHERE user_ID = @departmentUserID))";

                string queryFetchStudent = @"
                                            SELECT a.*, ur.role
                                            FROM appointment a
                                            LEFT JOIN users_table u ON a.student_ID = u.login_ID
                                            LEFT JOIN user_role ur ON u.role_ID = ur.role_id
                                            LEFT JOIN department d ON a.dept_id = d.ID_dept
                                            WHERE (a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID)
                                            OR a.student_ID = 'guest')
                                            AND (a.student_ID <> 'guest' OR a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID))";
                using (SqlCommand cmd = new SqlCommand(queryFetchStudent, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentUserID", userID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        studentTable.Load(reader);
                    }
                }
            }
            return studentTable;
        }

        public bool LoadAppointmentModal(int AppointmentID,out string appointmentStats)
        {
            appointmentStats = null;
            // Retrieve the User_ID from the session
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT * FROM appointment WHERE ID_appointment = @AppointmendID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmendID", AppointmentID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointmentName.Text = reader["full_name"].ToString();
                            Label1.Text = reader["ID_appointment"].ToString();
                            DateTime date = (DateTime)reader["appointment_date"];
                            AppointmentDate.Text = date.ToString("dd MMM, yyyy ddd");
                            AppointmentTime.Text = reader["appointment_time"].ToString();
                            appointmentConcern.Text = reader["concern"].ToString();
                            AppointmentStatus.Text = reader["appointment_status"].ToString();
                            appointmentStats = AppointmentStatus.Text;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        private void UpdateDateOptions()
        {
            // Set the minimum date to today
            newdate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
        }
        protected void ViewConcernModal_Click(object sender, EventArgs e)
        {
            int hiddenID = Convert.ToInt32(HiddenFieldAppointment.Value);

            if (LoadAppointmentModal(hiddenID,out string appointmentStats))
            {
                if( appointmentStats == "approved")
                {
                    approved.Attributes["class"] = "col-8 mb-2 d-grid d-none";
                    reject.Attributes["class"] = "col-4 mb-2 d-grid d-none";
                    resched.Attributes["class"] = "col-12 mb-2 d-none";
                    servedlnk.Attributes["class"] = "col-8 mb-2 d-grid";
                    noShowlnk.Attributes["class"] = "col-4 mb-2 d-grid";
                }
                else
                {
                    approved.Attributes["class"] = "col-8 mb-2 d-grid";
                    reject.Attributes["class"] = "col-4 mb-2 d-grid";
                    resched.Attributes["class"] = "col-12 mb-2";
                    servedlnk.Attributes["class"] = "col-8 mb-2 d-grid d-none";
                    noShowlnk.Attributes["class"] = "col-4 mb-2 d-grid d-none";
                }
            };
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showExampleModal", "$('#exampleModal').modal('show');", true);
        }

        protected void CloseViewModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideExampleModal", "$('#exampleModal').modal('hide');", true);
            HiddenFieldAppointment.Value = "";
        }

        protected void appointmentReschedule_Click(object sender, EventArgs e)
        {
            int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
            getCurrentSchedule(AppointmentID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showRescheduleModal", "$('#reschedModal').modal('show');", true);
        }

        public void getCurrentSchedule(int AppointmentID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT appointment_date, appointment_time, appointment_status FROM appointment WHERE ID_appointment = @AppointmentID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime date = (DateTime)reader["appointment_date"];
                            CurrentAppointmentDate.Text = date.ToString("dd MMM, yyyy ddd");
                            CurrentAppointmentTime.Text = reader["appointment_time"].ToString();
                            CurrentAppointmentStatus.Text = reader["appointment_status"].ToString();
                            newtime.SelectedValue = reader["appointment_time"].ToString();
                            DateTime date1 = (DateTime)reader["appointment_date"];
                            newdate.Text = date1.ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
        }

        protected void gobackToViewAppointment_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideExampleModal", "$('#exampleModal').modal('show');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showRescheduleModal", "$('#reschedModal').modal('hide');", true);
        }

        protected void closeReschedModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideReschedModal", "$('#reschedModal').modal('hide');", true);
            HiddenFieldAppointment.Value = "";
        }

        protected void updtSchedBtn_Click(object sender, EventArgs e)
        {
            int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
            string newTime = newtime.SelectedValue.ToString();
            string newDate = newdate.Text;
            string reschedReason = ReschedReason.Text;
            updateSchedDateTime(AppointmentID, newTime, newDate, reschedReason);
            BindingAppointment();
            string successMessage = "Schedule updated successfully.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
        }

        public void updateSchedDateTime(int AppointmentID, string newTime, string newDate, string reschedReason)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    string query = @"SELECT * FROM appointment WHERE ID_appointment = @AppointmentID";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime date = (DateTime)reader["appointment_date"];
                                string currentDate = date.ToString("dd MMM, yyyy ddd");
                                string currentTime = reader["appointment_time"].ToString();
                                string appointeeEmail = reader["email"].ToString().Trim();
                                string appointee = reader["full_name"].ToString();
                                string destination = GetDepartmentName(Convert.ToInt32(reader["dept_id"]));
                                string updateStatus = "reschedule";

                                if (newDate != currentDate || newTime != currentTime)
                                {
                                    reader.Close();

                                    string updateQuery = "UPDATE appointment SET ";
                                    if (newDate != currentDate)
                                    {
                                        updateQuery += "appointment_date = @newDate, ";
                                    }
                                    if (newTime != currentTime)
                                    {
                                        updateQuery += "appointment_time = @newTime, ";
                                    }
                                    updateQuery += "appointment_status = @newStatus WHERE ID_appointment = @AppointmentID";

                                    using (SqlCommand cmdDateTime = new SqlCommand(updateQuery, conn))
                                    {
                                        if (newDate != currentDate)
                                        {
                                            cmdDateTime.Parameters.AddWithValue("@newDate", newDate);
                                        }
                                        if (newTime != currentTime)
                                        {
                                            cmdDateTime.Parameters.AddWithValue("@newTime", newTime);
                                        }
                                        cmdDateTime.Parameters.AddWithValue("@newStatus", updateStatus);
                                        cmdDateTime.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                                        cmdDateTime.ExecuteNonQuery();
                                    }

                                    // Log the status change in the AppointmentStatusHistory table
                                    InsertStatusChangeToHistory(conn, AppointmentID, updateStatus, currentDate, currentTime);

                                    // Notify that the status has changed.
                                    DepartmentUser.OnAppointmentStatusChanged(EventArgs.Empty);
                                }

                                DateTime newdate = DateTime.Parse(newDate);
                                string formattedNewDate = newdate.ToString("dd MMM, yyyy ddd");

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
                                    <img src='cid:resched-image' width='200' height='200'>
                                </div>";

                                // Add additional appointment details
                                builder.HtmlBody += $@"<div style='text-align: center;'><h1>Heads up!</h1></div>
                                                <div style='text-align: center;'>
                                                <p>Hello!<b> {appointee}</b>, your appointment with an appointment ID: <b>{AppointmentID}</b> scheduled on <b>{currentDate} {currentTime}</b> </p>
                                                <p>has been rescheduled on <b>{formattedNewDate} {newTime}</b>,due to <b>{reschedReason}</b>, please go to the GABAY webpage to accept or reject your new appointment status</p>
                                                <p>If you have any concern question kindly visit the {destination}'s office or book another appointment</p>
                                                <p>Thank you!</p>
                                                </div>
                                                <div style='text-align: center;'>
                                                <a href='https://localhost:44341/Landing_Page/LandingPage.aspx' style='display: inline-block; padding: 10px; background-color: #007BFF; color: #fff; text-decoration: none; margin-top: 20px;'>Go to GABAY</a>
                                                </div>";

                                var logoImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\UC-LOGO.png");
                                logoImage.ContentId = "logo-image";
                                logoImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

                                var qrCodeImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\tempIcons\\reschedule-icon-6.jpg");
                                qrCodeImage.ContentId = "resched-image";
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
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch {
                string errorMessage = "Please check your internet connection";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }

        private void InsertStatusChangeToHistory(SqlConnection conn, int AppointmentID, string newStatus, string currentDate, string currentTime)
        {
            try
            {
                string insertQuery = "INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus, Notification) " +
                               "VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus, @Notification)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmd.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PreviousStatus", "current_status");
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@Notification", "UNREAD");

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                string errorMessage = "Please check your internet connection";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string newTime = newtime.SelectedValue;
            if (DateTime.TryParse(newdate.Text, out DateTime selectedDate))
            {
                // Format the date as "dd MMM, yyyy"
                string formattedDate = selectedDate.ToString("dd MMM, yyyy");

                // Save the formatted date to a variable or perform any other actions
                string savedDate = formattedDate;

                string successMessage = "Update the schedule to <b>" + HttpUtility.JavaScriptStringEncode(newTime) + "</b> and <b>" + HttpUtility.JavaScriptStringEncode(savedDate) + "</b>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showConfirmationModal",
                    $"$('#confirmationMessage').html('{successMessage}'); $('#ConfirmationModal').modal('show');", true);
            }

        }

        protected void goBacktoReschedModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showReschedModal", "$('#reschedModal').modal('show');", true);
        }

        protected void ApproveLink_Click(object sender, EventArgs e)
        {
            DateTime.TryParse(newdate.Text, out DateTime selectedDate);

            string Appointee = appointmentName.Text;
            string AppointmentID = Label1.Text;
            string appointmentDate = AppointmentDate.Text;
            string appointmentTime = AppointmentTime.Text;

            string ApproveMessage = "<b>Apointment ID:</b> " + HttpUtility.JavaScriptStringEncode(AppointmentID) + "<br />" +
                                       "<b>Appointee: </b>" + HttpUtility.JavaScriptStringEncode(Appointee) + "<br />" +
                                       "<b>Date: </b>" + HttpUtility.JavaScriptStringEncode(appointmentDate) + "<br />" +
                                       "<b>Time: </b>" + HttpUtility.JavaScriptStringEncode(appointmentTime) + "<br /> <hr />" +
                                       "Approve appointment?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showApproveMessage",
                $"$('#approveMessage').html('{ApproveMessage}'); $('#ApproveModal').modal('show');", true);
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
                approveAppointment(AppointmentID);
                BindingAppointment();
                string successMessage = "Schedule is set.";
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

        public void approveAppointment(int AppointmentID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
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
                INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus, Notification)
                VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus ,@Notification)";
                    using (SqlCommand cmdInsertHistory = new SqlCommand(queryInsertHistory, conn))
                    {
                        cmdInsertHistory.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        cmdInsertHistory.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now);
                        cmdInsertHistory.Parameters.AddWithValue("@PreviousStatus", "current_status");
                        cmdInsertHistory.Parameters.AddWithValue("@NewStatus", "approved");
                        cmdInsertHistory.Parameters.AddWithValue("@Notification", "UNREAD");
                        cmdInsertHistory.ExecuteNonQuery();
                    }

                    // Notify that the status has changed.
                    DepartmentUser.OnAppointmentStatusChanged(EventArgs.Empty);

                    conn.Close();
                }
            }
            catch
            {
                string ErrorMessage = "Please check your internet connection";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                   $"$('#errorMessage').text('{ErrorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        protected void RejectLink_Click(object sender, EventArgs e)
        {
            string rejectMessage = "Reject Appointment?";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                $"$('#confirmRejectMessage').text('{rejectMessage}'); $('#RejectModal').modal('show');", true);
        }

        protected void rejectAppointmentBtnLink_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showRejectReason", "$('#rejectModal').modal('show');", true);
        }

        protected void rejectBtn_Click(object sender, EventArgs e)
        {
            int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
            string Rejectreason = rejectReason.Text;

            try
            {
                if (string.IsNullOrEmpty(Rejectreason))
                {
                    // Display an error message for incomplete form
                    string errorMessage = "Please fill out all fields.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                        $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    return; // Stop further processing if the form is incomplete
                }
                else
                {
                    rejectAppointment(AppointmentID, Rejectreason);
                    BindingAppointment();
                    string successMessage = "Appointment updated to rejected";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                        $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
                }
            }catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                return; // Stop further processing if the form is incomplete
            }
           
        }

        public void rejectAppointment(int AppointmentID, string rejectReason)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
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
                                    <img src='cid:erro-image' width='200' height='200'>
                                </div>";

                                // Add additional appointment details
                                builder.HtmlBody += $@"<div style='text-align: center;'><h1>Something went wrong!</h1></div>
                                                <div style='text-align: center;'>
                                                <p>Hello!<b> {appointee}</b>, your appointment scheduled on <b>{appointmentDate} {appointmentTime}</b> </p>
                                                <p>that addressed to {destination} is <b>Rejected</b></p>
                                                <p> for the reason of:</p>
                                                <p><b>{rejectReason}</b></p>
                                                <p>If you have any concern question kindly visit the {destination}'s office or book another appointment</p>
                                                <p>Thank you!</p>
                                                </div>";

                                var logoImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\UC-LOGO.png");
                                logoImage.ContentId = "logo-image";
                                logoImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

                                var qrCodeImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\tempIcons\\error.png");
                                qrCodeImage.ContentId = "erro-image";
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
                            }

                            reader.Close();
                        }
                    }
                    string query = @"UPDATE appointment SET appointment_status = @AppointmentStats WHERE ID_appointment = @AppointmentID";

                    string updateStatus = "rejected";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        cmd.Parameters.AddWithValue("@AppointmentStats", updateStatus);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert a record into AppointmentStatusHistory
                    string queryInsertHistory = @"
                INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus, Notification)
                VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus, @Notification)";
                    using (SqlCommand cmdInsertHistory = new SqlCommand(queryInsertHistory, conn))
                    {
                        cmdInsertHistory.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        cmdInsertHistory.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now); // Current date and time
                        cmdInsertHistory.Parameters.AddWithValue("@PreviousStatus", "current_status"); // Replace with the actual previous status
                        cmdInsertHistory.Parameters.AddWithValue("@NewStatus", "rejected"); // New status
                        cmdInsertHistory.Parameters.AddWithValue("@Notification", "UNREAD");
                        cmdInsertHistory.ExecuteNonQuery();
                    }

                    // Notify that the status has changed.
                    DepartmentUser.OnAppointmentStatusChanged(EventArgs.Empty);



                    conn.Close();
                }
            }
            catch
            {
                string errorMessage = "Please check your internet connection";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        protected void served_Click(object sender, EventArgs e)
        {
            try
            {
                int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
                servedAppointment(AppointmentID);
                BindingAppointment();
                string successMessage = "Appointment completed!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch(Exception ex)
            {
                string errorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        public void servedAppointment(int AppointmentID)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"UPDATE appointment SET appointment_status = @AppointmentStats WHERE ID_appointment = @AppointmentID";
                conn.Open();
                string updateStatus = "served";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmd.Parameters.AddWithValue("@AppointmentStats", updateStatus);
                    cmd.ExecuteNonQuery();
                }
                string queryInsertHistory = @"
                INSERT INTO AppointmentStatusHistory (AppointmentID, StatusChangeDate, PreviousStatus, NewStatus, Notification)
                VALUES (@AppointmentID, @StatusChangeDate, @PreviousStatus, @NewStatus, @Notification)";
                using (SqlCommand cmdInsertHistory = new SqlCommand(queryInsertHistory, conn))
                {
                    cmdInsertHistory.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmdInsertHistory.Parameters.AddWithValue("@StatusChangeDate", DateTime.Now);
                    cmdInsertHistory.Parameters.AddWithValue("@PreviousStatus", "current_status");
                    cmdInsertHistory.Parameters.AddWithValue("@NewStatus", "served");
                    cmdInsertHistory.Parameters.AddWithValue("@Notification", "UNREAD");

                    cmdInsertHistory.ExecuteNonQuery();
                }


                // Notify that the status has changed.
                DepartmentUser.OnAppointmentStatusChanged(EventArgs.Empty);

                conn.Close();
            }
        }

        protected void noshow_Click(object sender, EventArgs e)
        {
            try
            {
                int AppointmentID = Convert.ToInt32(HiddenFieldAppointment.Value);
                noShowAppointment(AppointmentID);
                BindingAppointment();
                string successMessage = "No Appointee Showed up!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }

        public void noShowAppointment(int AppointmentID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"UPDATE appointment SET appointment_status = @AppointmentStats WHERE ID_appointment = @AppointmentID";
                conn.Open();
                string updateStatus = "no show";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                    cmd.Parameters.AddWithValue("@AppointmentStats", updateStatus);
                    cmd.ExecuteNonQuery();
                }
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

                // Notify that the status has changed.
                DepartmentUser.OnAppointmentStatusChanged(EventArgs.Empty);

                conn.Close();
            }
        }

        //Generate Reports
        protected void btnDownloadReports_Click(object sender, EventArgs e)
        {
            string reportType = ddlReportType.SelectedValue;

            if (reportType == "Excel")
            {
                ExportToExcel();
            }
            else if (reportType == "PDF")
            {
                ExportToPDF();
            }
        }

        private void ExportToPDF()
        {
            DataTable dt = fetchAppointBasedOnDepartment(Convert.ToInt32(Session["user_ID"]));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                // OpenDocument 
                document.Open();

                // Add the date and time at
                PdfPTable dateTimeTable = new PdfPTable(1);
                dateTimeTable.WidthPercentage = 100;
                dateTimeTable.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell dateTimeCell = new PdfPCell(new Phrase(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                dateTimeCell.Border = PdfPCell.NO_BORDER;
                dateTimeTable.AddCell(dateTimeCell);

                document.Add(dateTimeTable);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Add the logo without any box or column
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/Images/UC-LOGO.png"));
                logo.ScaleToFit(150f, 150f); // Adjust 
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Get the department name based on the user ID
                int userID = Convert.ToInt32(Session["user_ID"]);
                string departmentName = GetDepartmentName(userID);

                // Add title to the document
                Paragraph title = new Paragraph("Appointment Report", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Add the department name with styling
                Paragraph departmentNameParagraph = new Paragraph(departmentName, FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLD));
                departmentNameParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(departmentNameParagraph);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Set column widths and alignment
                float[] columnWidths = { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f }; // widths 
                PdfPTable table = new PdfPTable(columnWidths);
                table.WidthPercentage = 100; // page width

                // Add column headers to the table (excluding unwanted columns)
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != "dept_id" && column.ColumnName != "concern" && column.ColumnName != "Notification" && column.ColumnName != "contactNumber" && column.ColumnName != "role")
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(GetColumnHeader(column.ColumnName), FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                        headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.PaddingBottom = 5; // Add some bottom padding
                        table.AddCell(headerCell);
                    }
                }

                // Add data rows to the table (excluding unwanted columns)
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "dept_id" && column.ColumnName != "concern" && column.ColumnName != "Notification" && column.ColumnName != "contactNumber" && column.ColumnName != "role")
                        {
                            PdfPCell dataCell = new PdfPCell(new Phrase(GetCellData(column, row[column]), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                            dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(dataCell);
                        }
                    }
                }

                // Add the table to the document
                document.Add(table);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Create a new table for status counts
                PdfPTable statusTable = new PdfPTable(2);
                statusTable.WidthPercentage = 50; // 50% of the page width

                // Add a row for counts
                PdfPCell countCell = new PdfPCell(new Phrase("Summary Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD)));
                countCell.Colspan = 2;
                countCell.HorizontalAlignment = Element.ALIGN_CENTER;
                statusTable.AddCell(countCell);

                // Calculate counts for each status
                int servedCount = CountAppointments(dt, "served");
                int pendingCount = CountAppointments(dt, "pending");
                int approvedCount = CountAppointments(dt, "approved");
                int rescheduleCount = CountAppointments(dt, "reschedule");
                int rejectedCount = CountAppointments(dt, "rejected");

                // Add counts to the table
                statusTable.AddCell(new PdfPCell(new Phrase("All Served:", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase(servedCount.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase("All Pending:", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase(pendingCount.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase("All Approved:", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase(approvedCount.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase("All Rescheduled:", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase(rescheduleCount.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase("All Rejected", FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                statusTable.AddCell(new PdfPCell(new Phrase(rejectedCount.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                // Add the status table to the document
                document.Add(statusTable);

                document.Add(new Paragraph("\n"));


                // Add title to the document
                Paragraph by = new Paragraph("Prepared By:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK));
                by.Alignment = Element.ALIGN_RIGHT;
                document.Add(by);
                // Add the department name with styling
                Paragraph bydepartmentNameParagraph = new Paragraph(departmentName, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD));
                bydepartmentNameParagraph.Alignment = Element.ALIGN_RIGHT;
                document.Add(bydepartmentNameParagraph);

                document.Add(new Paragraph("\n"));

                // Call the method to add the footer
                AddCustomFooter(document);

                document.Close();

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=AppointmentReport.pdf");
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }

        private void AddCustomFooter(Document document)
        {
            // Set up a font with FontFamily.HELVETICA and size 11
            Font regularFont = new Font(Font.FontFamily.HELVETICA, 11);

            PdfPTable mainTable = new PdfPTable(1);
            mainTable.WidthPercentage = 100;

            // Add the first table with Contact Information and About Us
            PdfPTable contactAboutTable = new PdfPTable(2);
            contactAboutTable.WidthPercentage = 100;

            // Create a white font
            Font whiteFont = new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.BLACK);

            PdfPCell contactCell = new PdfPCell(new Phrase("Contact Information:\n\nA.C. Cortes Avenue 6000 Mandaue City Cebu\nEmail: info@uclm.edu.ph\n(032) 345 6666", whiteFont));
            PdfPCell aboutUsCell = new PdfPCell(new Phrase("About Us:\n\nGABAY is more than just a word;\nit's a beacon of support and wisdom\nthat lights the path for all of us.\nIn times of uncertainty when we\nseek direction or a helping hand,\nGABAY reminds us that we are never alone.", whiteFont));

            // Set background color
            ////contactCell.BackgroundColor = new BaseColor(55, 81, 126);
            ////aboutUsCell.BackgroundColor = new BaseColor(55, 81, 126);

            // Set border color to white
            contactCell.BorderColor = BaseColor.WHITE;
            aboutUsCell.BorderColor = BaseColor.WHITE;

            // Set text alignment to center
            contactCell.HorizontalAlignment = Element.ALIGN_LEFT;
            aboutUsCell.HorizontalAlignment = Element.ALIGN_LEFT;

            contactCell.Padding = 5f;
            aboutUsCell.Padding = 5f;

            contactAboutTable.AddCell(contactCell);
            contactAboutTable.AddCell(aboutUsCell);
            contactAboutTable.CompleteRow();

            // Add the first table to the main table
            mainTable.AddCell(contactAboutTable);

            // Add spacing between the two tables
            mainTable.AddCell(new Paragraph("\n"));

            // Add the second table with copyright text
            PdfPTable copyrightTable = new PdfPTable(1);
            copyrightTable.WidthPercentage = 100;
            PdfPCell copyrightCell = new PdfPCell(new Phrase("© Copyright Gabay. All Rights Reserved", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.WHITE)));

            // Set background color to #37517e
            copyrightCell.BackgroundColor = new BaseColor(55, 81, 126);

            // Set text alignment to center
            copyrightCell.HorizontalAlignment = Element.ALIGN_CENTER;

            // Set border color to white
            copyrightCell.BorderColor = BaseColor.WHITE;

            copyrightTable.AddCell(copyrightCell);
            copyrightTable.CompleteRow();

            // Add the second table to the main table
            mainTable.AddCell(copyrightTable);

            // Set border color to white for all cells
            foreach (PdfPCell cell in mainTable.Rows.SelectMany(row => row.GetCells()))
            {
                cell.BorderColor = BaseColor.WHITE;
            }

            // Add the main table to the document
            document.Add(mainTable);
        }

        //  get the department name based on the user ID
        private string GetDepartmentName(int userID)
        {
            string departmentName = "";
            using (SqlConnection conn = new SqlConnection(connection))
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





        // Helper method to count appointments based on status
        private int CountAppointments(DataTable dt, string status)
        {
            return dt.AsEnumerable().Count(r => r.Field<string>("appointment_status") == status);
        }

        // Helper method to get column header text
        private string GetColumnHeader(string columnName)
        {
            switch (columnName)
            {
                case "ID_appointment":
                    return "Appointment Number";
                case "full_name":
                    return "Full Name";
                case "email":
                    return "Email";
                case "student_ID":
                    return "User";
                case "course_year":
                    return "Year Level";
                case "appointment_date":
                    return "Date";
                case "appointment_time":
                    return "Time";
                case "appointment_status":
                    return "Status";
                default:
                    return columnName;
            }
        }

        // Helper method to get cell data with special formatting
        private string GetCellData(DataColumn column, object cellValue)
        {
            switch (column.ColumnName)
            {
                case "ID_appointment":
                    return cellValue.ToString();
                case "full_name":
                    return cellValue.ToString();
                case "email":
                    return cellValue.ToString();
                case "student_ID":
                    // If the value is a number, display "Student"
                    return int.TryParse(cellValue.ToString(), out _) ? "Student" : cellValue.ToString();
                case "course_year":
                    return cellValue.ToString();
                case "appointment_date":
                    // Format the date to display only the date part
                    if (DateTime.TryParse(cellValue.ToString(), out DateTime date))
                    {
                        return date.ToString("yyyy-MM-dd");
                    }
                    return cellValue.ToString();
                case "appointment_time":
                    return cellValue.ToString();
                case "appointment_status":
                    return cellValue.ToString();
                default:
                    return cellValue.ToString();
            }
        }


        private void ExportToExcel()
        {
            DataTable dt = fetchAppointBasedOnDepartment(Convert.ToInt32(Session["user_ID"]));

            // Clear the response content
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AppointmentReport.xls");
            Response.ContentType = "application/ms-excel";

            // Create a StringWriter
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a Table
            Table table = new Table();

            // Add column headers to the table (excluding unwanted columns)
            TableRow headerRow = new TableRow();
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName != "deptName" && column.ColumnName != "concern" && column.ColumnName != "Notification" && column.ColumnName != "contactNumber" && column.ColumnName != "role")
                {
                    TableCell cell = new TableCell();
                    cell.Text = GetColumnHeader(column.ColumnName);
                    headerRow.Cells.Add(cell);
                }
            }
            table.Rows.Add(headerRow);

            // Add data rows to the table (excluding unwanted columns)
            foreach (DataRow row in dt.Rows)
            {
                TableRow dataRow = new TableRow();
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != "deptName" && column.ColumnName != "concern" && column.ColumnName != "Notification" && column.ColumnName != "contactNumber" && column.ColumnName != "role")
                    {
                        TableCell cell = new TableCell();
                        cell.Text = GetCellData(column, row[column]);
                        dataRow.Cells.Add(cell);
                    }
                }
                table.Rows.Add(dataRow);
            }

            // Add a row for counts
            TableRow countRow = new TableRow();
            TableCell countCell = new TableCell();
            countCell.Text = "Counts";
            countCell.ColumnSpan = table.Rows[0].Cells.Count;
            countRow.Cells.Add(countCell);
            table.Rows.Add(countRow);

            int pendingCount = CountAppointments(dt, "pending");
            int approvedCount = CountAppointments(dt, "approved");
            int rescheduledCount = CountAppointments(dt, "reschedule");
            int servedCount = CountAppointments(dt, "served");
            int deniedCount = CountAppointments(dt, "denied");

            // Add counts to the table
            TableRow pendingCountRow = new TableRow();
            TableCell pendingCountCell = new TableCell();
            pendingCountCell.Text = $"Pending: {pendingCount}";
            pendingCountRow.Cells.Add(pendingCountCell);
            table.Rows.Add(pendingCountRow);

            TableRow approvedCountRow = new TableRow();
            TableCell approvedCountCell = new TableCell();
            approvedCountCell.Text = $"Approved: {approvedCount}";
            approvedCountRow.Cells.Add(approvedCountCell);
            table.Rows.Add(approvedCountRow);

            TableRow rescheduledCountRow = new TableRow();
            TableCell rescheduledCountCell = new TableCell();
            rescheduledCountCell.Text = $"Rescheduled: {rescheduledCount}";
            rescheduledCountRow.Cells.Add(rescheduledCountCell);
            table.Rows.Add(rescheduledCountRow);

            TableRow servedCountRow = new TableRow();
            TableCell servedCountCell = new TableCell();
            servedCountCell.Text = $"Served: {servedCount}";
            servedCountRow.Cells.Add(servedCountCell);
            table.Rows.Add(servedCountRow);

            TableRow deniedCountRow = new TableRow();
            TableCell deniedCountCell = new TableCell();
            deniedCountCell.Text = $"Denied: {deniedCount}";
            deniedCountRow.Cells.Add(deniedCountCell);
            table.Rows.Add(deniedCountRow);

            // Render the table to the StringWriter
            table.RenderControl(htw);

            // Write the content to the response
            Response.Write(sw.ToString());

            // End the response
            Response.End();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindDataTable(txtSearch.Text);
        }

        public void BindDataTable(string SearchKeyword)
        {
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                DataTable dt = SearchAppointment(SearchKeyword, user_ID);

                foreach (DataRow row in dt.Rows)
                {
                    string studentID = (string)row["student_ID"];

                    if (studentID == "guest")
                    {
                        // Set "User Type" to "Guest" for guest appointments
                        row["role"] = "guest";
                    }
                }

                AppointmentView.DataSource = dt;
                AppointmentView.DataBind();
            }
        }

        public DataTable SearchAppointment(string SearchKeyword, int user_ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                
                string query = @"SELECT a.*, ur.role
                                FROM appointment a
                                LEFT JOIN users_table u ON a.student_ID = u.login_ID
                                LEFT JOIN user_role ur ON u.role_ID = ur.role_id
                                LEFT JOIN department d ON a.dept_id = d.ID_dept
                                WHERE 
                                   (a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID)
                                            OR a.student_ID = 'guest')
                                            AND (a.student_ID <> 'guest' OR a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID))
                                    AND (
                                        ID_appointment LIKE '%' + @SearchKeyword + '%'
                                        OR appointment_status LIKE '%' + @SearchKeyword + '%'
                                        OR full_name LIKE '%' + @SearchKeyword + '%'
                                    )";

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchKeyword", SearchKeyword);
                    cmd.Parameters.AddWithValue("@departmentUserID", user_ID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        protected void displayPending_Click(object sender, EventArgs e)
        {
            string appointmentStatus = "pending";
            BindDataTableSelectedStatus(appointmentStatus);
        }

        protected void displayActive_Click(object sender, EventArgs e)
        {
            string appointmentStatus = "reschedule";
            BindDataTableSelectedStatus(appointmentStatus);
        }

        protected void displayDeactivated_Click(object sender, EventArgs e)
        {
            string appointmentStatus = "approved";
            BindDataTableSelectedStatus(appointmentStatus);
        }

        protected void displayServed_Click(object sender, EventArgs e)
        {
            string appointmentStatus = "served";
            BindDataTableSelectedStatus(appointmentStatus);
        }

        protected void displayReject_Click(object sender, EventArgs e)
        {
            string appointmentStatus = "rejected";
            BindDataTableSelectedStatus(appointmentStatus);
        }

        protected void displayAllStatus_Click(object sender, EventArgs e)
        {
            BindingAppointment();
        }

        public void BindDataTableSelectedStatus(string SelectedStatus)
        {
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                DataTable dt = SelectedAppointmentStatus(SelectedStatus, user_ID);

                foreach (DataRow row in dt.Rows)
                {
                    string studentID = (string)row["student_ID"];

                    if (studentID == "guest")
                    {
                        // Set "User Type" to "Guest" for guest appointments
                        row["role"] = "guest";
                    }
                }

                AppointmentView.DataSource = dt;
                AppointmentView.DataBind();
            }
        }

        public DataTable SelectedAppointmentStatus(string SelectedStatus, int user_ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT a.*, ur.role
                                            FROM appointment a
                                            LEFT JOIN users_table u ON a.student_ID = u.login_ID
                                            LEFT JOIN user_role ur ON u.role_ID = ur.role_id
                                            LEFT JOIN department d ON a.dept_id = d.ID_dept
                                            WHERE (a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID)
                                            OR a.student_ID = 'guest')
                                            AND (a.student_ID <> 'guest' OR a.dept_id = (SELECT ID_dept FROM department WHERE user_ID = @departmentUserID))
                                            AND appointment_status LIKE '%' + @SelectedStatus + '%'";

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedStatus", SelectedStatus);
                    cmd.Parameters.AddWithValue("@departmentUserID", user_ID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

       
    }
}
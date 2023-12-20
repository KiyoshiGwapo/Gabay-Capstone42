using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.QrCode;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Guest_Appointment : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                UpdateDateOptions();
                // Populate the department dropdown list
                ddlDept(departmentChoices);
            }
        }

        public void ddlDept(DropDownList deptDDL)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_dept, dept_name FROM department";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["dept_name"].ToString(), reader["ID_dept"].ToString());
                    deptDDL.Items.Add(item);
                }

                conn.Close();
            }
        }

        private bool IsAppointmentExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM appointment 
                                 WHERE [email] = @Email
                                 AND appointment_status NOT IN ('rejected', 'served', 'no show')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// Validate that all fields are filled
                //if (ValidateForm())
                //{
                    string email = Email.Text;

                    if (IsAppointmentExists(email))
                    {
                        // Show an error message for empty fields
                        ShowErrorModal("Appointment with this email already exists. (Appointment was Not Sent)");
                    }
                    else
                    {
                        string fullName = FullName.Text;
                        string contactNumber = ContactN.Text;
                        string selectedTime = time.SelectedValue;
                        string selectedDate = appointmentDate.Text;
                        string deptName = departmentChoices.SelectedItem.Text;
                        string concern = Message.Text;
                        string status = "pending";
                        string studentID = "guest";
                        string notificationStatus = "UNREAD";
                        string courseYear = "4";

                        int dept_id = getDeptIDbyDeptName(deptName);

                        int appointmentID = InsertAppointmentRecord(fullName, email, studentID, courseYear, contactNumber, selectedDate, selectedTime, dept_id, concern, status, notificationStatus);

                        if (appointmentID > 0)
                        {
                            string departmentName = GetDepartmenName(dept_id);
                            SendAppointmentConfirmationEmail(fullName, email, selectedDate, selectedTime, departmentName, concern, appointmentID);


                            // Clear form fields directly
                            FullName.Text = "";
                            Email.Text = "";
                            ContactN.Text = "";
                            time.SelectedIndex = 0;
                            appointmentDate.Text = "";
                            departmentChoices.SelectedIndex = 0;
                            Message.Text = "";

                            // Disable time and date
                            DisabledTimeAndDate();


                            // Show a success message
                            string submitEmailDialog = @"Appointment booked successfully!";
                            ShowSuccessModal(submitEmailDialog);
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                // Show error modal
                ShowErrorModal(ex.Message);
            }
        }
        //retrive dept_id by dept_name helper
        public int getDeptIDbyDeptName(string deptName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_dept FROM department WHERE dept_name = @DepartmentName";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", deptName);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    // Handle the case where the department name is not found
                    throw new Exception("Department not found");
                }
            }
        }

        private void ShowSuccessModal(string submitEmailDialog)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal", "$('#successModal').modal('show').find('.modal-body').html('<span style=\"color: white;\">"+submitEmailDialog+"</span>');", true);
        }

        private void ShowErrorModal(string errorMessage)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: white;\">{errorMessage}</span>');", true);
        }


        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(FullName.Text) ||
                string.IsNullOrEmpty(Email.Text) ||
                string.IsNullOrEmpty(ContactN.Text) ||
                string.IsNullOrEmpty(appointmentDate.Text) ||
                string.IsNullOrEmpty(time.SelectedValue) ||
                string.IsNullOrEmpty(departmentChoices.SelectedValue) ||
                string.IsNullOrEmpty(Message.Text))
            {
                // Show an error message for empty fields
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Please fill in all fields.</span>');", true);

                return false;
            }

            return true;
        }


        private int InsertAppointmentRecord(string fullName, string email, string studentID, string courseYear, string contactNumber, string selectedDate, string selectedTime, int dept_id, string concern, string status, string notificationStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO appointment ([dept_id], [full_name], [email], [student_ID], [course_year], [contactNumber], [appointment_date], [appointment_time], [concern], [appointment_status], [Notification]) " +
                    "VALUES (@dept_id, @FullName, @Email, @StudentID, @CourseYear, @ContactNumber, @SelectedDate, @SelectedTime, @Concern, @Status, @Notif); SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@dept_id", dept_id);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@CourseYear", courseYear);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
                    cmd.Parameters.AddWithValue("@SelectedTime", selectedTime);
                    cmd.Parameters.AddWithValue("@Concern", concern);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Notif", notificationStatus);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void SendAppointmentConfirmationEmail(string fullName, string email, string selectedDate, string selectedTime, string deptName, string concern, int appointmentID)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("UC Gabay", "noReply@noReply.com"));
                message.To.Add(new MailboxAddress("Recipient", email));
                message.Subject = "Appointment Details";

                var builder = new BodyBuilder();

                builder.HtmlBody = $@"
                <div style='text-align: center;margin-bottom: 10px;'>
                    <div>
                        <img src='cid:logo-image' style='width: 100px; height: auto; margin-right: 5px; display: block; margin: 0 auto;'>
                    </div>
                    <div style='letter-spacing: 3px; color: #003366; font-weight: 600;'>
                        GABAY
                    </div>
                </div>";

                // Add additional appointment details
                builder.HtmlBody += $@"<div style='text-align: center;'><h1>You have successfully booked an appointment</h1></div>
                    <div style='text-align: center;'>
                    <p>Our team is currently verifying the availability of the chosen time and date.</p>
                    <p>Please stay connected with your email for additional updates regarding your appointment schedule.</p>
                    <p>Hello!<b> {fullName}</b>, your appointment is set. Please see the details below:</p>
                    <p><b>Appointment ID:</b> {appointmentID}</p>
                    <p><b>Appointment Date:</b> {selectedDate}</p>
                    <p><b>Appointment Time:</b> {selectedTime}</p>
                    <p><b>Department:</b> {deptName}</p>
                    <p><b>Concern:</b> {concern}</p>
                    </div>";

                var logoImage = builder.LinkedResources.Add("C:\\Users\\quiro\\source\\repos\\Gabay-Final-V2\\Gabay-Final-V2\\Resources\\Images\\UC-LOGO.png");
                logoImage.ContentId = "logo-image";
                logoImage.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);

                message.Body = builder.ToMessageBody();

                // Send the email using MailKit
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(ConfigurationManager.AppSettings["SystemEmail"], ConfigurationManager.AppSettings["SystemEmailPass"]);
                    client.Send(message);
                    client.Disconnect(true);

                    string submitEmailDialog = @"Appointment booked successfully!";
                    // Show a success message after sending the email
                    ShowSuccessModal(submitEmailDialog);

                    // Clear form fields directly
                    FullName.Text = "";
                    Email.Text = "";
                    ContactN.Text = "";
                    time.SelectedIndex = 0;
                    appointmentDate.Text = "";
                    departmentChoices.SelectedIndex = 0;
                    Message.Text = "";

                    // Disable time and date
                    DisabledTimeAndDate();
                }
            }
            catch (Exception ex)
            {
                // Show error modal
                ShowErrorModal(ex.Message);
            }
        }

        //get dept_name by dept_id helper
        private string GetDepartmenName(int deptID)
        {
            string departmentName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                string query = "SELECT dept_name FROM department WHERE ID_dept = @dept_ID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@dept_ID", deptID);

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
        //Search sa iyang appointment
        protected void searchResultsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                // No results found, show the label
                noResultsLabel.Visible = true;
            }
            else
            {
                // Results found, hide the label
                noResultsLabel.Visible = false;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(searchInput.Text, out int appointmentID))
            {
                DataTable searchResults = SearchAppointmentsByAppointmentID(appointmentID);

                searchResultsGridView.DataSource = searchResults;
                searchResultsGridView.DataBind();

                noResultsLabel.Visible = searchResults.Rows.Count == 0;
            }
            else
            {
                // Handle the case where the input is not a valid integer
                noResultsLabel.Visible = true;
            }
        }

        private DataTable SearchAppointmentsByAppointmentID(int appointmentID)
        {
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM appointment WHERE ID_appointment = @AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                   cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    
                   using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                   {
                     adapter.Fill(results);
                   }
                }

            }

            return results;
        }

        protected void reschedBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int appointmentID = Convert.ToInt32(storeAppointmentID.Value);

                if (reschedSchedule(appointmentID, out string appointmentStats, out string appointmentDate, out string appointmentTime, out string appoiteeName))
                {
                    if (appointmentStats == "reschedule")
                    {
                        AppointmentID.Text = storeAppointmentID.Value.ToString();
                        ReschedDate.Text = appointmentDate;
                        ReschedTime.Text = appointmentTime;
                        AppointeeName.Text = appoiteeName;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showReschedule", $"$('#reschedModal').modal('show');", true);
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                   $"$('#errorMessage').text('{ErrorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        public bool reschedSchedule(int appointmentID,out string appointmentStats, out string appointmentDate, out string appointmentTime, out string apppointeeName)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT appointment_status, full_name, appointment_date, appointment_time FROM appointment WHERE ID_appointment = @AppointmentID";
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            appointmentStats = reader["appointment_status"].ToString();
                            DateTime date = (DateTime)reader["appointment_date"];
                            appointmentDate = date.ToString("dd MMM, yyyy ddd");
                            appointmentTime = reader["appointment_time"].ToString();
                            apppointeeName = reader["full_name"].ToString();
                            return true;
                        }
                        else
                        {
                            appointmentStats = null;
                            appointmentDate = null;
                            appointmentTime = null;
                            apppointeeName = null;
                            return false;
                        }
                    }
                }
            }
        }

        protected void reschedCloseBtn_Click(object sender, EventArgs e)
        {
            storeAppointmentID.Value = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", $"$('#reschedModal').modal('hide');", true);
        }

        protected void acceptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int appointmentID = Convert.ToInt32(storeAppointmentID.Value);
                approveAppointment(appointmentID);

                int.TryParse(searchInput.Text, out int searchAppointmentID);
                DataTable searchResults = SearchAppointmentsByAppointmentID(searchAppointmentID);
                searchResultsGridView.DataSource = searchResults;
                searchResultsGridView.DataBind();

                string successMessage = "Your schedule is set! Please check your email, we sent you the details of your appointment along with your QR code";
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
                            string destination = GetDepartmenName(Convert.ToInt32(reader["dept_id"]));
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
        protected void rejectBtn_Click(object sender, EventArgs e)
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
                int appointmentID = Convert.ToInt32(storeAppointmentID.Value);
                rejectAppointment(appointmentID);

                int.TryParse(searchInput.Text, out int searchAppointmentID);
                DataTable searchResults = SearchAppointmentsByAppointmentID(searchAppointmentID);
                searchResultsGridView.DataSource = searchResults;
                searchResultsGridView.DataBind();

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
            using(SqlConnection conn = new SqlConnection(connectionString))
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

       

        public string convertDeptIDtoName(string deptID)
        {
            string deptName = "";

            // Convert deptID to integer
            int convertedID = Convert.ToInt32(deptID);

            // Use using statement to automatically close connection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT dept_name FROM department WHERE ID_dept = @deptID";

                // Use using statement to automatically close command
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter to the query
                    cmd.Parameters.AddWithValue("@deptID", convertedID);

                    // Execute the query and read the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are rows returned
                        if (reader.Read())
                        {
                            // Retrieve the department name from the result
                            deptName = reader["dept_name"].ToString();
                        }
                    }
                }
            }

            return deptName;
        }

        private void UpdateDateOptions()
        {
            // Set the minimum date to today + 3 days
            appointmentDate.Attributes["min"] = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
        }

        public void DisabledTimeAndDate()
        {
            appointmentDate.Enabled = false;
            time.Enabled = false;
        }

        protected void departmentChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string departmentName = departmentChoices.SelectedValue;
            deptID.Value = departmentName;
            convertDeptIDtoName(deptID.Value);
            if (departmentName == "")
            {
                DisabledTimeAndDate();

            }
            else
            {
                appointmentDate.Enabled = true;
                time.Enabled = true;
            }
        }

        protected void date_TextChanged(object sender, EventArgs e)
        {
            string selectedDate = appointmentDate.Text;
            string selectedDept = deptID.Value;
            
            SelectedDate.Value = selectedDate;

            checkAppointmentTime(convertDeptIDtoName(selectedDept), selectedDate);
        }

        public void checkAppointmentTime(string selectedDept, string selectedDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // this is for db v.1.8
                //string query = @"SELECT appointment_time from appointment
                //                 WHERE deptName = @selectedDept
                //                 AND appointment_date = @selectedDate
                //                 AND (appointment_status != 'rejected' AND appointment_status != 'served' AND appointment_status != 'no show')";
                string query = @"SELECT appointment_time FROM appointment a
                                 INNER JOIN department d ON a.dept_id = d.ID_dept
                                 WHERE a.dept_id = (SELECT d.ID_dept FROM department WHERE dept_name = @selectedDept) 
                                 AND appointment_date = @selectedDate
                                 AND (appointment_status != 'rejected' AND appointment_status != 'served' AND appointment_status != 'no show')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@selectedDept", selectedDept);
                    cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are any rows in the result set
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string bookedTime = reader["appointment_time"].ToString();
                                ListItem item = time.Items.FindByValue(bookedTime);
                                if (item != null)
                                {
                                    item.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
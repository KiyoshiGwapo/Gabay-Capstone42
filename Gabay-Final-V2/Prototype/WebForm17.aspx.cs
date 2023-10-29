using MimeKit.Utils;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailKit.Net.Smtp;
using System.Configuration;
using ZXing;
using ZXing.QrCode;
using System.Data.SqlClient;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendEmailButton_Click(object sender, EventArgs e)
        {
          
            // Generate the QR code
            string dataFromDatabase = GetDataFromDatabase();

            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = new QrCodeEncodingOptions
            {
                Width = 200, // Set the desired width and height for the QR code
                Height = 200,
            };
            System.Drawing.Bitmap qrCodeBitmap = barcodeWriter.Write(dataFromDatabase);

            // Save the QR code as a temporary image file
            string tempQRCodeFilePath = Server.MapPath("~/TempQRCode.png");
            qrCodeBitmap.Save(tempQRCodeFilePath, System.Drawing.Imaging.ImageFormat.Png);

            // Create the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", "sender@example.com"));
            message.To.Add(new MailboxAddress("Recipient Name", "kentgerald.quiros@gmail.com"));
            message.Subject = "Your Appointment QR Code";

            var builder = new BodyBuilder();

            // Add the logo and QR code centered in the email body
            builder.HtmlBody = $@"
                                <div style='text-align: center;'>
                                    <img src='cid:logo-image' width='100' height='100'><br/>
                                    GABAY
                                </div>
                                <div style='text-align: center;'>
                                    <img src='cid:qr-code-image' width='200' height='200'>
                                </div>";

            // Add additional appointment details
            builder.HtmlBody += $@"<p>Hello, this is your appointment details:</p>
                        <p>Appointment ID: {Guid.NewGuid()}</p> <!-- Generate a unique appointment ID -->
                        <p>Schedule: {DateTime.Now:MM/dd/yyyy hh:mm tt}</p>
                        <p>Appointee name: {dataFromDatabase}</p>";

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
        private string GetDataFromDatabase()
        {
            string data = "";

            // Define your database connection string

            // Create a connection to the database
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Define your SQL query to retrieve data from the appointment table
                string query = "SELECT full_name, email, appointment_date, appointment_time, concern FROM appointment WHERE ID_appointment = 9";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the results and build a string with appointment information
                        while (reader.Read())
                        {
                            string fullName = reader["full_name"].ToString();
                            string email = reader["email"].ToString();
                            string appointmentDate = reader["appointment_date"].ToString();
                            string appointmentTime = reader["appointment_time"].ToString();
                            string concern = reader["concern"].ToString();

                            // Concatenate the data
                            data += $"Name: {fullName}\n";
                            data += $"Email: {email}\n";
                            data += $"Date: {appointmentDate}\n";
                            data += $"Time: {appointmentTime}\n";
                            data += $"Concern: {concern}\n\n";
                        }
                    }
                }
            }

            return data;
        }
    }
}
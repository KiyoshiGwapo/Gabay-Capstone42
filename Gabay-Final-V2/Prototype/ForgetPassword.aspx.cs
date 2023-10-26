using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Prototype
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve the user's email address
            string email = txtEmail.Text;

            // Find the user by email (assuming email is a unique identifier)
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Generate a password reset token and send an email with a reset link
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                string resetUrl = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/ResetPassword.aspx?userId={user.Id}&token={Uri.EscapeDataString(resetToken)}";

                // Send the reset link via email (you should implement this part)
                SendPasswordResetEmail(user.Email, resetUrl);

                // Display a message indicating that the password reset email has been sent
                lblMessage.Text = "A password reset email has been sent if the email exists in our records.";
            }
            else
            {
                lblMessage.Text = "No user found with that email address.";
            }
        }

        private void SendPasswordResetEmail(string email, string resetUrl)
        {
            // Implement the email sending logic using your preferred email library or service.
            // You can use libraries like SendGrid, SmtpClient, or any other email service.

            // Example using SmtpClient:
            var smtpClient = new SmtpClient();
            var mail = new MailMessage
            {
                Subject = "Password Reset Request",
                Body = $"To reset your password, click on the following link: {resetUrl}",
                IsBodyHtml = true
            };
            mail.To.Add(email);
            smtpClient.Send(mail);
        }
    }
}
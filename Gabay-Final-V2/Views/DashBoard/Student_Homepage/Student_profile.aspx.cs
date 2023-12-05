using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.DashBoard.Student_Homepage
{
    public partial class Student_profile : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                int deptSessionID = Convert.ToInt32(Session["user_ID"]);
                LoadStudentDetails(deptSessionID);
            }
        }

        private void LoadStudentDetails(int userId)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM student WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IDNumberLabel.Text = reader["studentID"].ToString();
                            StudentNameLabel.Text = reader["name"].ToString();
                            CourseLabel.Text = reader["course"].ToString();
                            YearLevelLabel.Text = reader["course_year"].ToString();
                            CollegeLabel.Text = reader["department_ID"].ToString();
                            ContactNumberLabel.Text = reader["contactNumber"].ToString();
                            EmailAddressLabel.Text = reader["email"].ToString();
                        }
                    }
                }
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the current, new, and confirm passwords from the input fields in the modal
                string currentPassword = currentPasswordTextBox.Text;
                string newPassword = newPasswordTextBox.Text;
                string confirmPassword = confirmPasswordTextBox.Text;

                // Check if any of the fields are empty
                if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    // Show an error message for empty fields
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Please fill in all password fields.</span>');", true);
                    return;
                }

                // Add logic to check if the current password is correct and if the new and confirm passwords match
                if (CheckCurrentPassword(currentPassword) && newPassword != currentPassword && newPassword == confirmPassword)
                {
                    // Update the password in the database
                    int userId = Convert.ToInt32(Session["user_ID"]);
                    UpdatePasswordInDatabase(userId, newPassword);

                    // Clear the textboxes
                    currentPasswordTextBox.Text = string.Empty;
                    newPasswordTextBox.Text = string.Empty;
                    confirmPasswordTextBox.Text = string.Empty;

                    // Show a success message
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
                }
                else
                {
                    // Show an error message for mismatched passwords, new password same as current password, or incorrect current password
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Password mismatch, new password should be different from the current password, or incorrect current password.</span>');", true);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">An error occurred: " + ex.Message + "</span>');", true);
            }
        }

        private void UpdatePasswordInDatabase(int userId, string newPassword)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE student SET stud_pass = @NewPassword WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE users_table SET password = @NewPassword WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private bool CheckCurrentPassword(string currentPassword)
        {
            int userId = Convert.ToInt32(Session["user_ID"]);

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT stud_pass FROM student WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string actualCurrentPassword = reader["stud_pass"].ToString();

                            // Compare the provided current password with the actual current password
                            return actualCurrentPassword == currentPassword;
                        }
                    }
                }
            }

            // Return false if the user is not found or there is an issue with the database
            return false;
        }

        //EMAIL NGA PART
        protected void UpdateEmail_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the current and new email from the input fields in the modal
                string currentEmail = currentEmailTextBox.Text;
                string newEmail = newEmailTextBox.Text;

                // Check if any of the fields are empty
                if (string.IsNullOrWhiteSpace(currentEmail) || string.IsNullOrWhiteSpace(newEmail))
                {
                    // Show an error message for empty fields
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Please fill in all email fields.</span>');", true);
                    return;
                }

                // Check if the new email is different from the current email
                if (currentEmail == newEmail)
                {
                    // Show an error message for the same current and new email
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">New email should be different from the current email.</span>');", true);
                    return;
                }

                // Add logic to check if the current email is correct and update the email in the database
                if (CheckCurrentEmail(currentEmail))
                {
                    // Update the email in the database
                    int userId = Convert.ToInt32(Session["user_ID"]);
                    UpdateEmailInDatabase(userId, newEmail);

                    // Clear the textboxes
                    currentEmailTextBox.Text = string.Empty;
                    newEmailTextBox.Text = string.Empty;

                    // Show a success message
                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
                }
                else
                {
                    // Show an error message for incorrect current email
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">Incorrect current email.</span>');", true);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", "$('#errorModal').modal('show').find('.modal-body').html('<span style=\"color: black;\">An error occurred: " + ex.Message + "</span>');", true);
            }
        }


        private bool CheckCurrentEmail(string currentEmail)
        {
            int userId = Convert.ToInt32(Session["user_ID"]);

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT email FROM student WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string actualCurrentEmail = reader["email"].ToString();

                            // Compare the provided current email with the actual current email
                            return actualCurrentEmail == currentEmail;
                        }
                    }
                }
            }

          
            return false;
        }

        private void UpdateEmailInDatabase(int userId, string newEmail)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE student SET email = @NewEmail WHERE user_ID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
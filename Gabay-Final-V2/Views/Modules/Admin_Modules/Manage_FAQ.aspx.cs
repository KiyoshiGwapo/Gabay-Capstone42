using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_FAQ : System.Web.UI.Page
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFAQs();
            }
        }

        private void LoadFAQs()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT FAQID, Question, Answer FROM FAQTable", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                FAQRepeater.DataSource = reader;
                FAQRepeater.DataBind();

                reader.Close();
            }
        }

        protected void btnAddNewFAQ_Click(object sender, EventArgs e)
        {
            try
            {
                string newQuestion = txtAddQuestion.Text.Trim();
                string newAnswer = txtAddAnswer.Text.Trim();

                // Server-side validation to ensure all fields are filled out
                if (string.IsNullOrEmpty(newQuestion) || string.IsNullOrEmpty(newAnswer))
                {
                    // Show error modal for incomplete form
                    string errorMessage = "Please fill out all fields.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                        $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    return; // Stop further processing if the form is incomplete
                }

                // Insert the new FAQ into the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO FAQTable (Question, Answer) VALUES (@Question, @Answer)", connection);
                    cmd.Parameters.AddWithValue("@Question", newQuestion);
                    cmd.Parameters.AddWithValue("@Answer", newAnswer);
                    cmd.ExecuteNonQuery();
                }

                // Reload FAQs after adding a new one
                LoadFAQs();

                // Show success modal
                string successMessage = "New FAQ added successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

                // Hide the add modal after adding
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addModalScript", "$('#addModal').modal('hide');", true);
            }
            catch (Exception ex)
            {
                // Handle any errors
                string errorMessage = "An error occurred while adding a new FAQ: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }



        protected void FAQRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int FAQID = Convert.ToInt32(e.CommandArgument); // Get the FAQID from the clicked row

                // Retrieve FAQ data from the database based on FAQID and populate edit fields
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Question, Answer FROM FAQTable WHERE FAQID = @FAQID", connection);
                    cmd.Parameters.AddWithValue("@FAQID", FAQID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string question = reader["Question"].ToString();
                        string answer = reader["Answer"].ToString();

                        // Populate the text boxes in the modal for editing
                        txtEditQuestion.Text = question;
                        txtEditAnswer.Text = answer;

                        // Store the FAQID in a hidden field within the modal for reference
                        hdEditFAQID.Value = FAQID.ToString();

                        // Show the modal dialog
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "editModalScript", "$('#editModal').modal('show');", true);
                    }

                    reader.Close();
                }
            }
            else if (e.CommandName == "Delete")
            {
                int FAQID = Convert.ToInt32(e.CommandArgument);
                // Get the FAQID from the clicked row

                // Now you can proceed with deleting the FAQ
                DeleteFAQ(FAQID);
            }
        }


        private void DeleteFAQ(int FAQID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM FAQTable WHERE FAQID = @FAQID", connection);
                cmd.Parameters.AddWithValue("@FAQID", FAQID);
                cmd.ExecuteNonQuery();
            }

            // Reload FAQs after deleting one
            LoadFAQs();
        }

        protected void btnUpdateFAQ_Click(object sender, EventArgs e)
        {
            int FAQID = Convert.ToInt32(hdEditFAQID.Value); // Get the FAQID from the hidden field
            string editedQuestion = txtEditQuestion.Text;
            string editedAnswer = txtEditAnswer.Text;

            // Update the FAQ in the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FAQTable SET Question = @Question, Answer = @Answer WHERE FAQID = @FAQID", connection);
                cmd.Parameters.AddWithValue("@Question", editedQuestion);
                cmd.Parameters.AddWithValue("@Answer", editedAnswer);
                cmd.Parameters.AddWithValue("@FAQID", FAQID);
                cmd.ExecuteNonQuery();
            }

            // Hide the modal dialog after updating
            ScriptManager.RegisterStartupScript(this, this.GetType(), "editModalScript", "$('#editModal').modal('hide');", true);
            string successMessage = "Question updated!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                   $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

            // Reload FAQs after updating
            LoadFAQs();
        }

        protected void dltAnnouceBtn_Click(object sender, EventArgs e)
        {
            int FAQID = Convert.ToInt32(hdEditFAQID.Value);
            DeleteFAQ(FAQID);
            try
            {
                DeleteFAQ(FAQID);
                string successMessage = "Question deleted successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

                LoadFAQs();
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while deleting the announcement: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_AcadCalen : System.Web.UI.Page
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUploadedFiles();
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile && fileUpload.PostedFile.ContentType == "application/pdf")
            {
                try
                {
                    // Ensure that both the file and file name are provided
                    if (!string.IsNullOrEmpty(txtFileName.Text))
                    {
                        // Get the file name and data
                        string fileName = txtFileName.Text + ".pdf"; // Appending ".pdf" to the file name
                        byte[] fileData = fileUpload.FileBytes;

                        // Insert the file data into the database
                        InsertFileData(fileName, fileData);

                        // Display a success message or perform any other actions
                        string successMessage = "Calendar Added successfully.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                            $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
                        BindUploadedFiles();
                    }
                    else
                    {
                        // Display an error message if the file name is empty
                        string errorMessage = "Please enter a file name.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                            $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message if an exception occurs during the upload
                    string errorMessage = "An error occurred while uploading the academic calendar: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                        $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }
            }
            else
            {
                // Display an error message if the uploaded file is not a PDF
                string errorMessage = "Please upload a valid PDF file.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
        }


        protected void RptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int fileIdToDelete = Convert.ToInt32(e.CommandArgument);
                    DeleteFileData(fileIdToDelete);
                    BindUploadedFiles(); // Refresh the file list
                }
                string successMessage = "Calendar deleted successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while deleting the calendar: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
           
        }

        private void BindUploadedFiles()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT FileId, FileName FROM UploadedFiles";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable filesTable = new DataTable();
                        adapter.Fill(filesTable);
                        RptFiles.DataSource = filesTable;
                        RptFiles.DataBind();
                    }
                }
            }
        }

        private void InsertFileData(string fileName, byte[] fileData)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO UploadedFiles (FileName, FileData) VALUES (@FileName, @FileData)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteFileData(int fileId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM UploadedFiles WHERE FileId = @FileId";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
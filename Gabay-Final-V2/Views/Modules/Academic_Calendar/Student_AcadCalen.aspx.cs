//using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

using static Gabay_Final_V2.Models.AcadCalen_model;

namespace Gabay_Final_V2.Views.Modules.Academic_Calendar
{
    public partial class Student_AcadCalen : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFilesToDropDownList();
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedFileId"] != null)
            {
                int fileId = (int)ViewState["SelectedFileId"];

                byte[] fileData = FetchFileDataFromDatabase(fileId);
                string fileName = FetchFileNameFromDatabase(fileId);

                if (fileData != null)
                {
                    // Set the response content type to PDF
                    Response.ContentType = "application/pdf";

                    // Set the content disposition to "inline" to open in the browser
                    Response.AddHeader("Content-Disposition", $"inline; filename={fileName}");

                    // Write the file data to the response output stream
                    Response.BinaryWrite(fileData);
                    Response.End();
                }
                else
                {
                    // Handle the case where file data is not available
                    DownloadErrorLabel.Text = "File not found.";
                }

                // Open the link in a new tab using JavaScript
                string script = "window.open('" + Request.Url.AbsoluteUri + "', '_blank');";
                ClientScript.RegisterStartupScript(this.GetType(), "openNewTab", script, true);
            }
            else
            {
                DownloadErrorLabel.Text = "Please select a file to preview.";
            }
        }





        // Helper method to determine content type based on file extension
        private string GetContentType(string fileName)
        {
            string ext = Path.GetExtension(fileName);

            switch (ext.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                // Add more cases for other file types as needed
                default:
                    return "application/octet-stream"; // Default to binary data
            }
        }
        private byte[] FetchFileDataFromDatabase(int fileId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT FileData FROM UploadedFiles WHERE FileId = @FileId";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (byte[])result;
                    }
                }
            }

            return null;
        }

        private string FetchFileNameFromDatabase(int fileId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT FileName FROM UploadedFiles WHERE Fileid = @Fileid";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Fileid", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }

        // Define a custom class to store file data
        public class FileData
        {
            public int FileId { get; set; }
            public string FileName { get; set; }
            public byte[] FileBytes { get; set; }
        }

        private List<FileData> FetchFilesDataFromDatabase()
        {
            List<FileData> filesList = new List<FileData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT FileId, FileName, FileData FROM UploadedFiles";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileData file = new FileData
                            {
                                FileId = reader.GetInt32(0),
                                FileName = reader.GetString(1),
                                FileBytes = (byte[])reader["FileData"]
                            };
                            filesList.Add(file);
                        }
                    }
                }
            }

            return filesList;
        }

        private void BindFilesToDropDownList()
        {
            List<FileData> filesList = FetchFilesDataFromDatabase();
            ddlFiles.Items.Clear(); // Clear existing items

            foreach (FileData file in filesList)
            {
                ListItem item = new ListItem(file.FileName, file.FileId.ToString());
                ddlFiles.Items.Add(item);
            }

            // Set the selected item based on ViewState
            if (ViewState["SelectedFileId"] != null)
            {
                int selectedFileId = (int)ViewState["SelectedFileId"];
                ddlFiles.SelectedValue = selectedFileId.ToString();
            }
        }


        protected void ddlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedFileId = int.Parse(ddlFiles.SelectedValue);
            ViewState["SelectedFileId"] = selectedFileId;

            // Debugging statement to display selectedFileId
            DownloadErrorLabel.Text = "Selected File ID: " + selectedFileId;

            // Fetch the selected file's data and update labels or perform other actions
            byte[] selectedFileData = FetchFileDataFromDatabase(selectedFileId);
            if (selectedFileData != null)
            {
                // Update labels or perform other actions
                DownloadErrorLabel.Text = "";
            }
            else
            {
                DownloadErrorLabel.Text = "Selected file data not found.";
            }
        }
    }
}
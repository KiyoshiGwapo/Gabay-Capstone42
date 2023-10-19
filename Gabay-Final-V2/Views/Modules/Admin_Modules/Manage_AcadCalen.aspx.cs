using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_AcadCalen : System.Web.UI.Page
    {
        private static readonly string ConnectionString = "Data Source=DESKTOP-6DAE04O\\SQLEXPRESS;Initial Catalog=gabaydb_v.1.8;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUploadedFiles();
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    // Get the file name and data
                    string fileName = !string.IsNullOrEmpty(txtFileName.Text) ? txtFileName.Text : Path.GetFileName(fileUpload.FileName);
                    byte[] fileData = fileUpload.FileBytes;

                    // Insert the file data into the database
                    InsertFileData(fileName, fileData);

                    // Display a success message or perform any other actions
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception)
                {
                    // Handle any exceptions or display an error message
                }
            }
        }

        protected void RptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int fileIdToDelete = Convert.ToInt32(e.CommandArgument);
                DeleteFileData(fileIdToDelete);
                BindUploadedFiles(); // Refresh the file list
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
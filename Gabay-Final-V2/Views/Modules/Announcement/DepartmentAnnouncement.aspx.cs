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
using Gabay_Final_V2.Models;
using System.Web.Services;

namespace Gabay_Final_V2.Views.Modules.Announcement
{
    public partial class DepartmentAnnouncement : System.Web.UI.Page
    {

        private Announcement_model announcementModel = new Announcement_model();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Load announcements when the page is first loaded
                LoadAnnouncements();
            }
        }

        // Function to load announcements into the Bootstrap table
        protected void LoadAnnouncements()
        {
            DataTable dtAnnouncements = announcementModel.GetAnnouncements(); // Use the AnnouncementModel to fetch announcements

            // Bind the DataTable to the Bootstrap table
            rptAnnouncements.DataSource = dtAnnouncements;
            rptAnnouncements.DataBind();
        }


        // Function to handle the Save button click event (Create/Update)
        protected void SaveAnnouncement(object sender, EventArgs e)
        {
            // Retrieve form values
            string title = txtTitle.Text;
            string date = txtDate.Text;
            string shortDescription = txtShortDescription.Text;
            string detailedDescription = txtDetailedDescription.Text;

            // Define the folder path for uploads
            string uploadFolderPath = Server.MapPath("~/Uploads/");

            // Check if a file is uploaded in the modal
            if (ImageFileUploadModal.HasFile)
            {
                // Get the file extension
                string fileExtension = Path.GetExtension(ImageFileUploadModal.FileName);
                fileExtension = fileExtension.ToLower();

                // Check if the file extension is allowed (jpg, jpeg, or png)
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string filePath = Path.Combine(uploadFolderPath, fileName);
                    ImageFileUploadModal.SaveAs(filePath);

                    // Define the database connection string
                    string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
                    // Insert announcement into the database along with the file path
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Announcement (Title, Date, ImagePath, ShortDescription, DetailedDescription) " +
                                       "VALUES (@Title, @Date, @ImagePath, @ShortDescription, @DetailedDescription)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Title", title);
                            command.Parameters.AddWithValue("@Date", date);
                            command.Parameters.AddWithValue("@ImagePath", "~/Uploads/" + fileName);
                            command.Parameters.AddWithValue("@ShortDescription", shortDescription);
                            command.Parameters.AddWithValue("@DetailedDescription", detailedDescription);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Clear input fields after adding announcement in the modal
                    txtTitle.Text = "";
                    txtDate.Text = "";
                    txtShortDescription.Text = "";
                    txtDetailedDescription.Text = "";

                    LoadAnnouncements();
                }
                else
                {
                    // Handle the case where the file extension is not allowed (e.g., show an error message)
                    // You can display an error message here
                }
            }
            else
            {
                // Handle the case where no file is uploaded (e.g., show an error message)
                // You can display an error message here
            }
        }


        protected void EditAnnouncement_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            int announcementID = Convert.ToInt32(editButton.CommandArgument);
            // Define the database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID", conn);
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEditTitle.Text = reader["Title"].ToString();
                    txtEditDate.Text = reader["Date"].ToString();
                    txtEditShortDescription.Text = reader["ShortDescription"].ToString();
                    txtEditDetailedDescription.Text = reader["DetailedDescription"].ToString();
                    hdnEditAnnouncementID.Value = announcementID.ToString();
                    hdnEditImagePath.Value = reader["ImagePath"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", "$('#editAnnouncementModal').modal('show');", true);
                }

                reader.Close();
            }
        }

        protected void SaveEditedAnnouncement(object sender, EventArgs e)
        {
            int announcementID;
            if (int.TryParse(hdnEditAnnouncementID.Value, out announcementID))
            {
                string title = txtEditTitle.Text;
                string date = txtEditDate.Text;
                string shortDescription = txtEditShortDescription.Text;
                string detailedDescription = txtEditDetailedDescription.Text;
                string imagePath = hdnEditImagePath.Value;

                if (ImageFileEdit.HasFile)
                {
                    string uploadFolderPath = Server.MapPath("~/Uploads/");
                    string fileExtension = Path.GetExtension(ImageFileEdit.FileName).ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                    {
                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string newFilePath = Path.Combine(uploadFolderPath, fileName);
                        ImageFileEdit.SaveAs(newFilePath);
                        imagePath = "~/Uploads/" + fileName;
                    }
                    else
                    {
                        string errorMessage = "Invalid file extension. Please upload a PNG, JPEG, or JPG file.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
                        return;
                    }
                }

                bool updated = announcementModel.UpdateAnnouncement(announcementID, title, date, imagePath, shortDescription, detailedDescription);

                if (updated)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", "$('#editAnnouncementModal').modal('hide');", true);
                    LoadAnnouncements();
                }
                else
                {
                    string errorMessage = "Update failed. Please try again.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
                }
            }
            else
            {
                string errorMessage = "Invalid Announcement ID. Please try again.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
            }
        }

        public static string GetAnnouncementDetailsByID(int announcementID)
        {
            // Call the GetAnnouncementDetails method from Announcement_model
            Announcement_model announcementModel = new Announcement_model();
            DataTable dt = announcementModel.GetAnnouncementDetails(announcementID);

            // Convert DataTable to a comma-separated string
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string result = $"{row["AnnouncementID"]},{row["Title"]},{row["Date"]},{row["ShortDescription"]},{row["DetailedDescription"]},{row["ImagePath"]}";
                return result;
            }
            else
            {
                return ""; // Return an empty string if no data found
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            int announcementID = Convert.ToInt32(deleteButton.CommandArgument);

            // Call the DeleteAnnouncement method from AnnouncementModel
            bool deleted = announcementModel.DeleteAnnouncement(announcementID);

            if (deleted)
            {
                // Announcement deleted successfully, perform necessary actions
                LoadAnnouncements();
            }
            else
            {
                // Handle the case where deleting the announcement failed
                // You can display an error message here
            }
        }

        // Function to save an uploaded image and return the image path
        private string SaveImage(FileUpload fileUpload, string uploadFolderPath)
        {
            string imagePath = string.Empty;

            if (fileUpload.HasFile)
            {
                try
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                    // Check if the file extension is allowed
                    if (fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".jpg")
                    {
                        // Create the folder if it doesn't exist
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        // Generate a unique file name for the image
                        string fileName = Guid.NewGuid().ToString() + fileExtension;

                        // Combine the folder path and file name to get the full image path
                        imagePath = Path.Combine(uploadFolderPath, fileName);

                        // Save the uploaded image
                        fileUpload.SaveAs(imagePath);
                    }
                    else
                    {
                        throw new Exception("Invalid file extension. Please upload a PNG, JPEG, or JPG file.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file upload
                    // You can log the error or display an error message
                    imagePath = string.Empty; // Set imagePath to empty if there was an error
                }
            }

            return imagePath;
        }

        // Method to get the full image path
        protected string GetImagePath(string imagePath)
        {
            return imagePath;
        }

        // Function to clear form fields after saving an announcement
        private void ClearFormFields()
        {
            txtTitle.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtShortDescription.Text = string.Empty;
            txtDetailedDescription.Text = string.Empty;
        }

    }
}
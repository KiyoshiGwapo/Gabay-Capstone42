using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Gabay_Final_V2.Views.Modules.Announcement
{
    public partial class DepartmentAnnouncement : System.Web.UI.Page
    {
        // Define the database connection string
        string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AnnouncementID, Title, ImagePath, CONVERT(VARCHAR(10), Date, 120) AS Date, ShortDescription, DetailedDescription FROM Announcement", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the DataTable to the Bootstrap table
                rptAnnouncements.DataSource = dt;
                rptAnnouncements.DataBind();
            }
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
                }
            }
            else
            {
                // Handle the case where no file is uploaded (e.g., show an error message)
            }
        }


        // Function to handle the Edit button click event
        protected void EditAnnouncement_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button editButton = (Button)sender;

            // Extract the AnnouncementID from the command argument
            int announcementID = Convert.ToInt32(editButton.CommandArgument);

            // Query the database to fetch the announcement details
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID", conn);
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Populate the edit modal controls with fetched data
                    txtEditTitle.Text = reader["Title"].ToString();
                    txtEditDate.Text = reader["Date"].ToString();
                    txtEditShortDescription.Text = reader["ShortDescription"].ToString();
                    txtEditDetailedDescription.Text = reader["DetailedDescription"].ToString();
                    hdnEditAnnouncementID.Value = announcementID.ToString();
                    hdnEditImagePath.Value = reader["ImagePath"].ToString();

                    // Show the edit modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", "$('#editAnnouncementModal').modal('show');", true);
                }

                reader.Close();
            }
        }

        // Function to handle the Save button click event in the edit modal
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
                    // A new image is uploaded, update the image
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
                        // Handle the case where the file extension is not allowed
                        string errorMessage = "Invalid file extension. Please upload a PNG, JPEG, or JPG file.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
                        return; // Exit the function, don't proceed with the update
                    }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Announcement SET Title = @Title, Date = @Date, ShortDescription = @ShortDescription, DetailedDescription = @DetailedDescription, ImagePath = @ImagePath WHERE AnnouncementID = @AnnouncementID";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@ShortDescription", shortDescription);
                        command.Parameters.AddWithValue("@DetailedDescription", detailedDescription);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Successful update, hide the edit modal using client-side JavaScript
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", "$('#editAnnouncementModal').modal('hide');", true);

                            // Reload announcements
                            LoadAnnouncements();
                        }
                        else
                        {
                            // Handle the case where the update was not successful
                            string errorMessage = "Update failed. Please try again.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
                        }
                    }
                }
            }
            else
            {
                // Parsing failed, handle the error (e.g., show an error message)
                string errorMessage = "Invalid Announcement ID. Please try again.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "editAnnouncementModal", $"showErrorToast('{errorMessage}');", true);
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


        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            // Retrieve the button that triggered the event
            Button deleteButton = (Button)sender;

            // Retrieve the AnnouncementID from the CommandArgument
            int announcementID = Convert.ToInt32(deleteButton.CommandArgument);

            // Delete the announcement from the database using the AnnouncementID
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@AnnouncementID", announcementID);
                    command.ExecuteNonQuery();
                }
            }

            // Show a success pop-up (toast) notification using ScriptManager
            ScriptManager.RegisterStartupScript(this, GetType(), "deleteSuccess", "showSuccessToast('Announcement deleted successfully!');", true);

            // Bind the announcements again to refresh the table
            LoadAnnouncements();
        }


    }
}
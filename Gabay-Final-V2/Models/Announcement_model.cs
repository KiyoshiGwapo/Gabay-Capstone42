using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Gabay_Final_V2.Models
{
    public class Announcement_model
    {

        // Define the database connection string from your configuration
        private string connStr = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        public DataTable GetAnnouncements()
        {
            string query = "SELECT AnnouncementID, Title, Date, ImagePath, ShortDescription, DetailedDescription FROM Announcement";

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
        }

        public bool AddAnnouncement(string title, string date, string imagePath, string shortDescription, string detailedDescription)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "INSERT INTO Announcement (Title, Date, ImagePath, ShortDescription, DetailedDescription) " +
                                   "VALUES (@Title, @Date, @ImagePath, @ShortDescription, @DetailedDescription)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);
                        command.Parameters.AddWithValue("@ShortDescription", shortDescription);
                        command.Parameters.AddWithValue("@DetailedDescription", detailedDescription);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions here, log them, or throw further
                return false;
            }
        }

        // Method to save an uploaded image and return the image path
        public string SaveImage(HttpPostedFile imageFile, string uploadFolderPath)
        {
            string imagePath = string.Empty;

            try
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

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
                        imageFile.SaveAs(filePath);

                        imagePath = "~/Uploads/" + fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle any exceptions that may occur during file upload
                // You can log the error or throw further
            }

            return imagePath;
        }

        protected string GetImagePath(string imagePath)
        {
            return imagePath;
        }


        public bool UpdateAnnouncement(int announcementID, string title, string date, string imagePath, string shortDescription, string detailedDescription)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "UPDATE Announcement SET Title = @Title, Date = @Date, ShortDescription = @ShortDescription, DetailedDescription = @DetailedDescription, ImagePath = @ImagePath WHERE AnnouncementID = @AnnouncementID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@ShortDescription", shortDescription);
                        command.Parameters.AddWithValue("@DetailedDescription", detailedDescription);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions here, log them, or throw further
                return false;
            }
        }

        public bool DeleteAnnouncement(int announcementID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "DELETE FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions here, log them, or throw further
                return false;
            }
        }

        //Katung error samok kaayu 
        // Add a new method to your Announcement_model class for fetching announcement details by ID
        // Method to fetch announcement details by ID
        public DataTable GetAnnouncementDetails(int announcementID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = "SELECT AnnouncementID, Title, Date, ImagePath, ShortDescription, DetailedDescription FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AnnouncementID", announcementID);
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exceptions here, log them, or throw further
                return null; // Return null if there's an error
            }
        }


    }
}
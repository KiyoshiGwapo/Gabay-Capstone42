﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_Announcement : System.Web.UI.Page
    {
        //private Announcement_model announcementModel = new Announcement_model();
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load announcements when the page is first loaded
                LoadAnnouncements();
                UpdateDateOptions();
            }
        }

        // Function to load announcements into the Bootstrap table
        //to load announcement data
        public void LoadAnnouncements()
        {
            DataTable dataTable = GetAnnouncements();

            AnnouncementList.DataSource = dataTable;
            AnnouncementList.DataBind();
        }
        public DataTable GetAnnouncements()
        {
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                // Modify the SQL query to include a WHERE clause
                string query = "SELECT * FROM Announcement WHERE User_ID = @user_ID";

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user_ID", user_ID);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    return dt;
                }
            }
            else
            {
                return new DataTable();
            }
        }

        private void UpdateDateOptions()
        {
            // Set the minimum date to today
            addDatebx.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            Datebx.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Function to handle the Save button click event (Create/Update)
        //to save announcement data
        protected void SaveAnnouncement_Click(object sender, EventArgs e)
        {
            string title = addTitlebx.Text;
            string date = addDatebx.Text;
            string shortDescript = addShrtbx.Text;
            string detailedDescript = addDtldbx.Text;
            string startTim = addStartTime.Text;
            string endTim = addEndTime.Text;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(startTim) || string.IsNullOrWhiteSpace(endTim) ||
                string.IsNullOrWhiteSpace(shortDescript) || string.IsNullOrWhiteSpace(detailedDescript))
            {
                // Show validation modal
                string errorMessage = "Please fill in all the fields.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                return; // Stop further processing
            }

            if (addFilebx.HasFile)
            {
                try
                {
                    HttpPostedFile postedFile = addFilebx.PostedFile;
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    if (Session["user_ID"] != null)
                    {
                        int user_ID = Convert.ToInt32(Session["user_ID"]);
                        AddData(user_ID, title, date, bytes, shortDescript, detailedDescript, startTim, endTim);
                    }
                    string successMessage = "Announcement Added successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                        $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

                    LoadAnnouncements();
                    clearAddModalInputs();
                }
                catch (Exception ex)
                {
                    string errorMessage = "An error occurred while deleting the announcement: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                        $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }

            }
        }
        public void AddData(int user_ID, string Title, string Date, byte[] imgFile, string shortDescription, string DetailedDescription, string StartTime, string EndTime)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"INSERT INTO Announcement (User_ID, Title, Date, ImagePath, ShortDescription, DetailedDescription, StartTime ,EndTime)
                                 VALUES (@user_ID, @Title, @Date, @imgFile, @shortDescript, @DetailedDescript, @StartTim, @EndTim)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", user_ID);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    SqlParameter imgParam = new SqlParameter("@imgFile", SqlDbType.VarBinary);
                    imgParam.Value = imgFile;
                    cmd.Parameters.Add(imgParam);
                    cmd.Parameters.AddWithValue("@shortDescript", shortDescription);
                    cmd.Parameters.AddWithValue("@DetailedDescript", DetailedDescription);
                    cmd.Parameters.AddWithValue("@StartTim", StartTime);
                    cmd.Parameters.AddWithValue("@EndTim", EndTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //clear out textbox after adding
        public void clearAddModalInputs()
        {
            addTitlebx.Text = "";
            addDatebx.Text = "";
            addShrtbx.Text = "";
            addDtldbx.Text = "";
            addStartTime.Text = "";
            addEndTime.Text = "";
        }

        //Function to handle the Delete button click event (Delete)
        //to delete announcement data
        protected void dltAnnouceBtn_Click(object sender, EventArgs e)
        {
            int hiddenID = Convert.ToInt32(HidAnnouncementID.Value);
            try
            {
                DeleteAnnouncement(hiddenID);
                string successMessage = "Announcement deleted successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

                LoadAnnouncements();
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while deleting the announcement: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);

            }
        }
        public void DeleteAnnouncement(int AnnouncementID)
        {
            // First, retrieve the User_ID from the session
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Modify the query to include a WHERE clause to ensure the user can only delete their own announcements
                    string query = @"DELETE FROM Announcement WHERE AnnouncementID = @AnnouncementID AND User_ID = @user_ID";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                        cmd.Parameters.AddWithValue("@user_ID", user_ID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                // Handle the case when user_ID is not available in the session
                // You can throw an exception, show an error message, or take appropriate action.
                throw new Exception("User_ID not available in the session.");
            }
        }

        //Function to handle the retrieve Data in the gridview
        //to retrieve announcement data from gridview and put it in the edit modal
        public void LoadAnnouncementInfo(int AnnouncementID)
        {
            // Retrieve the User_ID from the session
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    string query = @"SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID AND User_ID = @user_ID";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                        cmd.Parameters.AddWithValue("@user_ID", user_ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Titlebx.Text = reader["Title"].ToString();
                                DateTime date;
                                if (DateTime.TryParse(reader["Date"].ToString(), out date))
                                {
                                    Datebx.Text = date.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    // Handle parsing error or set a default value
                                    Datebx.Text = string.Empty;
                                }
                                ShortDescbx.Text = reader["ShortDescription"].ToString();
                                DtlDescBx.Text = reader["DetailedDescription"].ToString();

                                // Format StartTime and EndTime using a custom format string
                                StartTimebx.Text = FormatTime(reader["StartTime"]);
                                EndTimebx.Text = FormatTime(reader["EndTime"]);
                            }
                        }
                    }
                }
            }
            else
            {
                // Handle the case when user_ID is not available in the session
                // You can throw an exception, show an error message, or take appropriate action.
                throw new Exception("User_ID not available in the session.");
            }
        }

        private string FormatTime(object timeObject)
        {
            if (timeObject != null && timeObject != DBNull.Value)
            {
                // Assuming timeObject is a TimeSpan or DateTime
                DateTime time = Convert.ToDateTime(timeObject);
                return time.ToString("hh:mm:ss tt"); // Include AM/PM designator
            }
            else
            {
                return string.Empty;
            }
        }


        protected void gridviewEdit_Click(object sender, EventArgs e)
        {
            int hiddenID = Convert.ToInt32(HidAnnouncementID.Value);
            LoadAnnouncementInfo(hiddenID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#toEditModal').modal('show');", true);
        }
        protected void closeEditModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#toEditModal').modal('hide');", true);
        }

        //Function to handle the Edit button click event (Edit/Update)
        //to update the announcement data
        public void updtAnnoucementList(int AnnouncementID)
        {
            // Retrieve the User_ID from the session
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID AND User_ID = @user_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                        cmd.Parameters.AddWithValue("@user_ID", user_ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string currentTitle = Titlebx.Text;
                                string currentDate = Datebx.Text;
                                string currentShortDesc = ShortDescbx.Text;
                                string currentDetailedDesc = DtlDescBx.Text;
                                string currentStartTime = StartTimebx.Text;
                                string currentEndTime = EndTimebx.Text;

                                // Fetch the existing ImagePath from the database record
                                byte[] existingImage = (byte[])reader["ImagePath"];

                                reader.Close();

                                // Check if a new image is uploaded
                                if (Imgbx.HasFile)
                                {
                                    // Handle the uploaded image and convert it to a byte array
                                    byte[] newImage = GetByteArrayFromImage(Imgbx.FileBytes);

                                    // Update the record with the new image data
                                    string updateQuery = @"UPDATE Announcement
                                       SET Title = @newTitle,
                                           Date = @newDate,
                                           ShortDescription = @newShortDesc,
                                           DetailedDescription = @newDetailedDesc,
                                           ImagePath = @newImage,
                                           StartTime = @newStartTime,
                                           EndTime = @newEndTime
                                       WHERE AnnouncementID = @AnnouncementID AND User_ID = @user_ID";

                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@newTitle", currentTitle);
                                        updateCmd.Parameters.AddWithValue("@newDate", currentDate);
                                        updateCmd.Parameters.AddWithValue("@newShortDesc", currentShortDesc);
                                        updateCmd.Parameters.AddWithValue("@newDetailedDesc", currentDetailedDesc);
                                        updateCmd.Parameters.AddWithValue("@newImage", newImage);
                                        updateCmd.Parameters.AddWithValue("@newStartTime", currentStartTime); // Corrected parameter name
                                        updateCmd.Parameters.AddWithValue("@newEndTime", currentEndTime); // Corrected parameter name
                                        updateCmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                                        updateCmd.Parameters.AddWithValue("@user_ID", user_ID);

                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // No new image uploaded, so update other fields only
                                    string updateQuery = @"UPDATE Announcement
                                       SET Title = @newTitle,
                                           Date = @newDate,
                                           ShortDescription = @newShortDesc,
                                           DetailedDescription = @newDetailedDesc,
                                           StartTime = @newStartTime,
                                           EndTime = @newEndTime
                                       WHERE AnnouncementID = @AnnouncementID AND User_ID = @user_ID";

                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@newTitle", currentTitle);
                                        updateCmd.Parameters.AddWithValue("@newDate", currentDate);
                                        updateCmd.Parameters.AddWithValue("@newShortDesc", currentShortDesc);
                                        updateCmd.Parameters.AddWithValue("@newDetailedDesc", currentDetailedDesc);
                                        updateCmd.Parameters.AddWithValue("@newStartTime", currentStartTime); // Corrected parameter name
                                        updateCmd.Parameters.AddWithValue("@newEndTime", currentEndTime); // Added new parameter for EndTime
                                        updateCmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                                        updateCmd.Parameters.AddWithValue("@user_ID", user_ID);

                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Handle the case when user_ID is not available in the session
                // You can throw an exception, show an error message, or take appropriate action.
                throw new Exception("User_ID not available in the session.");
            }
        }

        // Helper function to convert a byte array from an image file
        private byte[] GetByteArrayFromImage(byte[] imageBytes)
        {
            // Add any additional processing if needed (e.g., resizing, compression)
            return imageBytes;
        }
        protected void updtAnnouncement_Click(object sender, EventArgs e)
        {
            int hiddenID = Convert.ToInt32(HidAnnouncementID.Value);
            try
            {
                updtAnnoucementList(hiddenID);
                string successMessage = "Announcement updated successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);

                LoadAnnouncements();
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
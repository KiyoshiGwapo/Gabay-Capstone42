using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        string connection = "Data Source=DESKTOP-6DAE04O\\SQLEXPRESS;Initial Catalog=gabaydb_v.1.8;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadAnnouncements();
            }
            
        }
        //to load announcement data
        public void LoadAnnouncements()
        {
            DataTable dataTable = GetAnnouncements();

            AnnouncementList.DataSource = dataTable;
            AnnouncementList.DataBind();
        }
        public DataTable GetAnnouncements()
        {
            string query = @"SELECT * FROM Announcement";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }

        }
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
            catch(Exception ex)
            {
                string errorMessage = "An error occurred while deleting the announcement: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);

            }
        }
        public void DeleteAnnouncement(int AnnoucementID)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"DELETE FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@AnnouncementID", AnnoucementID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //to retrieve announcement data from gridview and put it in the edit modal
        public void LoadAnnouncementInfo(int AnnoucementID)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SElECT * FROM Announcement WHERE AnnouncementID = @AnnoucementID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AnnoucementID", AnnoucementID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Titlebx.Text = reader["Title"].ToString();
                            DateTime date = (DateTime)reader["Date"];
                            Datebx.Text = date.ToString("yyyy-MM-dd");
                            ShortDescbx.Text = reader["ShortDescription"].ToString();
                            DtlDescBx.Text = reader["DetailedDescription"].ToString();
                        }
                    }
                }
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
        //to update the announcement data
        public void updtAnnoucementList(int AnnouncementID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentTitle = Titlebx.Text;
                            string currentDate = Datebx.Text;
                            string currentShortDesc = ShortDescbx.Text;
                            string currentDetailedDesc = DtlDescBx.Text;

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
                                                   ImagePath = @newImage
                                               WHERE AnnouncementID = @AnnouncementID";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@newTitle", currentTitle);
                                    updateCmd.Parameters.AddWithValue("@newDate", currentDate);
                                    updateCmd.Parameters.AddWithValue("@newShortDesc", currentShortDesc);
                                    updateCmd.Parameters.AddWithValue("@newDetailedDesc", currentDetailedDesc);
                                    updateCmd.Parameters.AddWithValue("@newImage", newImage);
                                    updateCmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);

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
                                                   DetailedDescription = @newDetailedDesc
                                               WHERE AnnouncementID = @AnnouncementID";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@newTitle", currentTitle);
                                    updateCmd.Parameters.AddWithValue("@newDate", currentDate);
                                    updateCmd.Parameters.AddWithValue("@newShortDesc", currentShortDesc);
                                    updateCmd.Parameters.AddWithValue("@newDetailedDesc", currentDetailedDesc);
                                    updateCmd.Parameters.AddWithValue("@AnnouncementID", AnnouncementID);

                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                conn.Close();
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
        //to save announcement data
        protected void SaveAnnouncement_Click(object sender, EventArgs e)
        {
            string title = addTitlebx.Text;
            string date = addDatebx.Text;
            string shortDescript = addShrtbx.Text;
            string detailedDescript = addDtldbx.Text;

            if (addFilebx.HasFile)
            {
                try
                {
                    HttpPostedFile postedFile = addFilebx.PostedFile;
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                    AddData(title, date, bytes, shortDescript, detailedDescript);
                    string successMessage = "Announcement updated successfully.";
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
        public void AddData(string Title, string Date,byte[] imgFile, string shortDescription, string DetailedDescription)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"INSERT INTO Announcement (Title, Date, ImagePath, ShortDescription, DetailedDescription)
                                 VALUES (@Title, @Date, @imgFile, @shortDescript, @DetailedDescript)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    SqlParameter imgParam = new SqlParameter("@imgFile", SqlDbType.VarBinary);
                    imgParam.Value = imgFile;
                    cmd.Parameters.Add(imgParam);
                    cmd.Parameters.AddWithValue("@shortDescript", shortDescription);
                    cmd.Parameters.AddWithValue("@DetailedDescript", DetailedDescription);
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

        }
    }
}
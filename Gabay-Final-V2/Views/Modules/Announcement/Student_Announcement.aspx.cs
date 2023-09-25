using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Gabay_Final_V2.Views.Modules.Announcement
{
    public partial class Student_Announcement : System.Web.UI.Page
    {
        // Define the database connection string
        string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load announcements when the page is first loaded
            LoadAnnouncements();
        }

        protected void LoadAnnouncements()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AnnouncementID, Title, ImagePath, CONVERT(VARCHAR(10), Date, 120) AS Date, ShortDescription, DetailedDescription FROM Announcement", conn);

                // Create a SqlDataAdapter to fetch the data
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Create a DataTable to store the data
                DataTable dt = new DataTable();

                // Fill the DataTable with data from the database
                da.Fill(dt);

                // Bind the DataTable to the Repeater control
                rptAnnouncements.DataSource = dt;
                rptAnnouncements.DataBind();
            }
        }

        // Event handler for the "Learn More" button click
        protected void ShowAnnouncementDetails(object sender, EventArgs e)
        {
            Button btnLearnMore = (Button)sender;
            int announcementID = Convert.ToInt32(btnLearnMore.CommandArgument);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Title, ImagePath, CONVERT(VARCHAR(10), Date, 120) AS Date, ShortDescription, DetailedDescription FROM Announcement WHERE AnnouncementID = @AnnouncementID", conn);
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Find modal controls
                    Label modalTitle = (Label)FindControl("modalTitle" + announcementID);
                    Image modalImage = (Image)FindControl("modalImage" + announcementID);
                    Label modalDate = (Label)FindControl("modalDate" + announcementID);
                    Label modalShortDescription = (Label)FindControl("modalShortDescription" + announcementID);
                    Label modalDetailedDescription = (Label)FindControl("modalDetailedDescription" + announcementID);

                    // Populate the modal with announcement details
                    modalTitle.Text = reader["Title"].ToString();
                    modalImage.ImageUrl = reader["ImagePath"].ToString();
                    modalDate.Text = "Date: " + reader["Date"].ToString();
                    modalShortDescription.Text = "Short Description: " + reader["ShortDescription"].ToString();
                    modalDetailedDescription.Text = "Detailed Description: " + reader["DetailedDescription"].ToString();

                    // Show the modal using JavaScript
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal(" + announcementID + ");", true);
                }

                reader.Close();
            }
        }

    }
}
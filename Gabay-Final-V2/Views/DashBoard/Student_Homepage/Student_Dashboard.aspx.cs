using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Data.SqlTypes;

namespace Gabay_Final_V2.Views.DashBoard.Student_Homepage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAnnouncements();
            }
        }
        public DataTable GetAnnouncements()
        {
            if (Session["user_ID"] != null)
            {
                int user_ID = Convert.ToInt32(Session["user_ID"]);
                string searchTerm = txtSearch.Text.Trim(); // Get the search term
                string filterDateText = calFilterDate.Text; // Get the date as a string

                // Parse the filter date from the input element
                if (DateTime.TryParse(filterDateText, out DateTime filterDate))
                {
                    // The filterDate contains a valid date
                }
                else
                {
                    // Set filterDate to DateTime.MinValue if parsing fails
                    filterDate = DateTime.MinValue;
                }

                string query = @"SELECT A.*
             FROM Announcement A
             LEFT JOIN department D ON A.User_ID = D.user_ID
             LEFT JOIN student S ON D.ID_dept = S.department_ID
             WHERE (S.user_ID = @user_ID OR A.User_ID = 1)
               AND (A.Title LIKE '%' + @searchTerm + '%' OR @searchTerm = '')
               AND (A.Date = @filterDate OR @filterDate IS NULL)";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_ID", user_ID);
                    command.Parameters.AddWithValue("@searchTerm", searchTerm);
                    command.Parameters.AddWithValue("@filterDate", filterDate == DateTime.MinValue ? (object)DBNull.Value : filterDate);

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                    return dt;
                }
            }
            else
            {
                return null;
            }
        }

        protected void LoadAnnouncements()
        {

            DataTable dt = GetAnnouncements();

            rptAnnouncements.DataSource = dt;
            rptAnnouncements.DataBind();
        }

        protected void learnMoreBtn_Click(object sender, EventArgs e)
        {
            int announcementID = Convert.ToInt32(AnnouncementIDHolder.Value);

            fetchAnnouncementDetails(announcementID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDetailedModal", "$('#dtldModal').modal('show');", true);

        }
        public void fetchAnnouncementDetails(int announcementID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AnnouncementID", announcementID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dtldTitle.Text = reader["Title"].ToString();
                            DateTime date = (DateTime)reader["Date"];
                            dtldDate.Text = date.ToString("yyyy-MM-dd");
                            dtldDescrp.Text = reader["DetailedDescription"].ToString();

                            // ari ipa gwas katung time2x
                            DateTime startTime = (DateTime)reader["StartTime"];
                            DateTime endTime = (DateTime)reader["EndTime"];

                            dtldStartTime.Text = startTime.ToString("hh:mm tt");
                            dtldEndTime.Text = endTime.ToString("hh:mm tt");

                            byte[] imageBytes = reader["ImagePath"] as byte[];
                            if (imageBytes != null)
                            {
                                string base64Image = Convert.ToBase64String(imageBytes);
                                dtldimgPlaceholder.ImageUrl = "data:Image/png;base64," + base64Image;
                            }
                        }
                    }
                }
            }
        }



        protected void dtldModalClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDetailedModal", "$('#dtldModal').modal('hide');", true);
        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAnnouncements();
        }



        protected void txtFilterDate_TextChanged(object sender, EventArgs e)
        {
            LoadAnnouncements();
        }
    }
}
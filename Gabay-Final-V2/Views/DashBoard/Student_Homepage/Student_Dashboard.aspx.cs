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
                string query = @"SELECT A.*
                     FROM Announcement A
                     INNER JOIN department D ON A.User_ID = D.user_ID
                     INNER JOIN student S ON D.ID_dept = S.department_ID
                     WHERE S.user_ID = @user_ID";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_ID", user_ID);

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
            int announcementID = Convert.ToInt32(HiddenField1.Value);

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
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gabay_Final_V2.Models;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        private string connStr = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               LoadAnnouncements();
            }
        }
        public DataTable GetAnnouncements()
        {
            string query = "SELECT * FROM Announcement";

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                return dt;
            }
        }
        protected void LoadAnnouncements()
        {
            Announcement_model announcementModel = new Announcement_model();
            DataTable dt = announcementModel.GetAnnouncements();

            rptAnnouncements.DataSource = dt;
            rptAnnouncements.DataBind();
        }
    }
}
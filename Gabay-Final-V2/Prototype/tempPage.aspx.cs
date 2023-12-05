using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Prototype
{
    public partial class tempPage : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["appointmentID"] != null)
                {
                    int appointmentID = Convert.ToInt32(Request.QueryString["appointmentID"]);

                    loadDetails(appointmentID);
                }
            }
        }

        public void loadDetails(int appointmentID)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"SELECT * FROM appointment WHERE ID_appointment = @AppointmentID";

                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            appointID.Text = reader["ID_appointment"].ToString();
                            Name.Text = reader["full_name"].ToString();
                            Concern.Text = reader["concern"].ToString();
                            DateTime date = (DateTime)reader["appointment_date"];
                            Date.Text = date.ToString("dd MMM, yyyy ddd");
                            Time.Text = reader["appointment_time"].ToString();
                            Concern.Text = reader["concern"].ToString();
                        }
                    }
                }
            }
        }
    }
}
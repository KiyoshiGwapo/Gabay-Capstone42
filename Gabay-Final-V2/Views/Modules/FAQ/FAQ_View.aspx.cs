using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Gabay_Final_V2.Views.Modules.FAQ
{
    public partial class FAQ_View : System.Web.UI.Page
    {
        private string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve all FAQs from the database
                BindFAQs();
            }
        }

        private void BindFAQs()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT FAQID, Question, Answer FROM FAQTable", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                FAQRepeater.DataSource = reader;
                FAQRepeater.DataBind();

                reader.Close();
            }
        }


        private void DisplayFirstFAQ()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Question, Answer FROM FAQTable", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string question = reader["Question"].ToString();
                    string answer = reader["Answer"].ToString();

                    // Populate the placeholders with the retrieved data
                    faqQuestion.InnerText = question;
                    faqAnswer.InnerText = answer;
                }

                reader.Close();
            }
        }


    }
}
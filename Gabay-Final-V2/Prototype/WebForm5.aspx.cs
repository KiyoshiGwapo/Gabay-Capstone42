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
    public partial class WebForm5 : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateDropDownList();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string textAppend = TextBox1.Text; // Get the content of TextBox1
            if (string.IsNullOrEmpty(textAppend))
            {
                Label1.Text = "No Inputs";
            }
            else
            {
                // Append the content of TextBox1 to TextBox2 with a newline
                Label1.Text = "";
                TextBox2.Text += textAppend + Environment.NewLine;
                TextBox1.Text = string.Empty;
                PopulateDropDownList();
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string collectedText = TextBox2.Text;

            // Split the collected text by newline characters and join with commas
            string formattedData = string.Join(",", collectedText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

            // Insert the formatted data into the database
            addData(formattedData);

            // Clear TextBox2
            TextBox2.Text = "";

            // Optionally, you can provide feedback to the user
            Label1.Text = "Data added to the database.";
        }

        private void addData (string data)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO sampleData (data) VALUES (@data)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        protected void PopulateDropDownList()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "SELECT data FROM sampleData WHERE id = 2"; // Adjust the query as needed

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string data = reader["data"].ToString();
                    string[] dataArray = data.Split(',');

                    // Add items to the DropDownList
                    foreach (string item in dataArray)
                    {
                        DropDownList1.Items.Add(new ListItem(item));
                    }
                }

                reader.Close();
            }
        }
    }
}
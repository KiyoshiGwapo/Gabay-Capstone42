using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindStudentData("");
            DisplayDT();
        }

        public void DisplayDT()
        {
            studentList.DataSource = BindDT();
            studentList.DataBind();
        }

        public DataTable BindDT()
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT * FROM example_data";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int personID = Convert.ToInt32(hidPersonID.Value);

            // Delete the record
            DeleteRow(personID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
            // Refresh the GridView
            DisplayDT();
            
        }

        public void DeleteRow(int personID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "DELETE example_data WHERE ex_ID = @personID";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@personID", personID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string SearchText = txtSearch.Text.Trim();
            BindStudentData(SearchText);
        }


        // ApproveRecord

        public void ApproveRecord(int personID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "UPDATE example_data SET Status = 'Approved' WHERE ex_ID = @personID";

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@personID", personID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        public void BindStudentData(string searchQuery)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "SELECT * FROM example_data WHERE Name LIKE @searchQuery";
                conn.Open();
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        studentList.DataSource = dt;
                        studentList.DataBind();
                    }
                }
            }
        }
    }
}

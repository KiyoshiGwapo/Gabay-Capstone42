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
    public partial class WebForm6 : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "SELECT * FROM department WHERE ID_dept = 2024";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            deptName.Text = read["dept_name"].ToString();
                            deptHead.Text = read["dept_head"].ToString();
                            deptDesc.Text = read["dept_description"].ToString();
                            string formatCourse = read["courses"].ToString();
                            string[] items = formatCourse.Split(',');
                            string formattedCourse = string.Join("<br />", items);
                            courses.Text = formattedCourse;
                            offHrs.Text = read["office_hour"].ToString();

                            emailbx.Text = read["email"].ToString();
                            conNum.Text = read["contactNumber"].ToString();
                        }
                    }
                }
            }

        }
    }
}
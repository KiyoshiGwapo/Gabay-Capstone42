using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.Modules.Department_Info
{
    public partial class Student_deptInfo : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            int studentSessionID = Convert.ToInt32(Session["user_ID"]);
            LoadData(studentSessionID);
        }
        public void LoadData(int studentSessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
            
                string query = @"SELECT D.dept_name, D.dept_head, D.dept_description, D.contactNumber, D.email, D.courses, D.office_hour
                                 FROM department D
                                 INNER JOIN student S ON D.ID_dept = S.department_ID
                                 WHERE S.user_ID = @studentSessionID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studentSessionID", studentSessionID);

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
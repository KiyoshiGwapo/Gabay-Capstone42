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
    public partial class Guest_deptInfo : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Department> departments = new List<Department>();

                // Replace with your database connection code
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string query = @"SELECT ID_dept, dept_name, dept_head, dept_description,
                                     contactNumber, email, courses, office_hour FROM department";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            DepartmentID = Convert.ToInt32(reader["ID_dept"]),
                            DepartmentName = reader["dept_name"].ToString(),
                            DepartmentHead = reader["dept_head"].ToString(),
                            DepartmentDescription = reader["dept_description"].ToString(),
                            DepartmentCourses = reader["courses"].ToString(),
                            DepartmentOffHours = reader["office_hour"].ToString(),
                            DepartmentContactNumber = reader["contactNumber"].ToString(),
                            DepartmentEmail = reader["email"].ToString(),
                        };
                        departments.Add(department);
                    }
                }

                // Bind the data to the repeater control
                departmentRepeater.DataSource = departments;
                departmentRepeater.DataBind();
            }
        }
        public class Department
        {
            public int DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string DepartmentHead { get; set; }
            public string DepartmentDescription { get; set; }
            public string DepartmentCourses { get; set; }
            public string DepartmentOffHours { get; set; }
            public string DepartmentContactNumber { get; set; }
            public string DepartmentEmail { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Gabay_Final_V2.Views.Modules.Department_Info
{
    public partial class Guest_deptInfo : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load department data into the DropDownList
                LoadDepartments();
            }
        }

        private void LoadDepartments()
        {
            List<Department> departments = new List<Department>();

            // Replace with your database connection code
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT ID_dept, dept_name, dept_head, dept_description, courses, office_hour, contactNumber, email FROM department";
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
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

            // Bind the data to the DropDownList
            ddlDepartments.DataSource = departments;
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentID";
            ddlDepartments.DataBind();

            // Add a default item
            ddlDepartments.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Department Below:", ""));
        }

        protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle the selected department change
            string selectedDepartmentId = ddlDepartments.SelectedValue;

            // Fetch department details based on the selected ID
            Department selectedDepartment = GetDepartmentDetails(selectedDepartmentId);

            // Set the text values directly in the front-end labels
            lblDepartmentHead.InnerText = selectedDepartment?.DepartmentHead ?? "N/A";
            lblDepartmentDescription.InnerText = selectedDepartment?.DepartmentDescription ?? "N/A";
            lblDepartmentCourses.InnerText = selectedDepartment?.DepartmentCourses ?? "N/A";
            lblDepartmentOffHours.InnerText = selectedDepartment?.DepartmentOffHours ?? "N/A";
            lblDepartmentContactNumber.InnerText = selectedDepartment?.DepartmentContactNumber ?? "N/A";
            lblDepartmentEmail.InnerText = selectedDepartment?.DepartmentEmail ?? "N/A";
        }



        private Department GetDepartmentDetails(string departmentId)
        {
            // Fetch department details based on the selected ID
            // Use parameterized query to avoid SQL injection
            string query = "SELECT * FROM department WHERE ID_dept = @DepartmentID";

            // Replace with your database connection code
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the department details in the Department object
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
                        return department;
                    }
                }
            }

            return null; // Return null if no department details are found
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
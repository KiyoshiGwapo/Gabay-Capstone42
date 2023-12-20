using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;

namespace Gabay_Final_V2.Views.RegistrationPages
{
	public partial class Student_Reg : System.Web.UI.Page
	{
		string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				DbUtility conn = new DbUtility();
				conn.ddlDept(departmentChoices);

			   
			}
		}
		protected void regBtn_Click(object sender, EventArgs e)
		{
			string fullName = name.Text;
			string studAddress = address.Text;
			string contactNumber = contact.Text;
			string studBOD = DOB.Text;
			string studEmail = email.Text;
			string studentNumber = idNumber.Text;
			string Studpassword = password.Text;
			int departmentID = Convert.ToInt32(departmentChoices.SelectedValue);
			string course = courseList.SelectedValue;
			string courseYear = courseYearChoices.SelectedItem.Text;

			if (existingStudent(studentNumber))
			{
                idNumExist.Attributes["class"] = "alert alert-danger";
               
            }
			else
			{
                idNumExist.Attributes["class"] = "alert alert-danger d-none";
                addStudent(departmentID, fullName, studAddress, contactNumber, studBOD, course, courseYear, studentNumber, Studpassword, studEmail);
				Response.Redirect("Pending_page.aspx");
			}
		}

        public bool existingStudent(string studID)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{
                conn.Open();
                string checkDeptQuery = @"SELECT COUNT(*) FROM student WHERE studentID = @studID";
                using (SqlCommand checkUserCmd = new SqlCommand(checkDeptQuery, conn))
                {
                    checkUserCmd.Parameters.AddWithValue("@studID", studID);
                    int existUser = Convert.ToInt32(checkUserCmd.ExecuteScalar());

                    return existUser > 0;
                }
            }
        }

		public void addStudent(int deptID, string studName, string studAddress, string studCN, string studBOD, string course, string studCY, string studID, string studPass, string studEmail)
		{
			using (SqlConnection conn = new SqlConnection(connection))
			{
				conn.Open();
				string userStatus = "pending";
				string roleType = "student";
				string Notif = "UNREAD";
				int roleID;

				string query = @"INSERT INTO student (department_ID, name, address, contactNumber, DOB, course, course_year, studentID, stud_pass, email) " +
							   "VALUES (@deptID, @studName, @studAddress, @studCN, @studBOD, @course, @studCY, @studID, @studPass, @studEmail)";

				string roleQuery = @"SELECT role_id FROM user_role WHERE role = @roleType";

				string userQuery = @"INSERT INTO users_table (role_ID, login_ID, password, status, Notification)
									 VALUES (@role_ID, @login_ID, @password, @userStatus, @Notification)";

				string updateDeptQuery = @"UPDATE student SET user_ID = (SELECT user_ID FROM users_table WHERE login_ID = @studID)
										   WHERE studentID = @studID";


				//string checkUserQuery = @"SELECT COUNT(*) FROM users_table WHERE login_ID = @studID";


				using (SqlCommand roleCmd = new SqlCommand(roleQuery, conn))
				{
					roleCmd.Parameters.AddWithValue("@roleType", roleType);
					roleID = Convert.ToInt32(roleCmd.ExecuteScalar());
				}



				//using (SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, conn))
				//{
				//	checkUserCmd.Parameters.AddWithValue("@studID", studID);
				//	int existUser = Convert.ToInt32(checkUserCmd.ExecuteScalar());

				//	if (existUser > 0)
				//	{
				//		return;
				//	}
				//}

				using (SqlCommand insertCmd = new SqlCommand(userQuery, conn))
				{
					insertCmd.Parameters.AddWithValue("@role_ID", roleID);
					insertCmd.Parameters.AddWithValue("@login_ID", studID);
					insertCmd.Parameters.AddWithValue("@password", studPass);
					insertCmd.Parameters.AddWithValue("@userStatus", userStatus);
					insertCmd.Parameters.AddWithValue("@Notification", Notif);

					insertCmd.ExecuteNonQuery();
				}

				using (SqlCommand studCmd = new SqlCommand(query, conn))
				{
					studCmd.Parameters.AddWithValue("@deptID", deptID);
					studCmd.Parameters.AddWithValue("@studName", studName);
					studCmd.Parameters.AddWithValue("@studAddress", studAddress);
					studCmd.Parameters.AddWithValue("@studCN", studCN);
					studCmd.Parameters.AddWithValue("@studBOD", studBOD);
					studCmd.Parameters.AddWithValue("@course", course);
					studCmd.Parameters.AddWithValue("@studCY", studCY);
					studCmd.Parameters.AddWithValue("@studID", studID);
					studCmd.Parameters.AddWithValue("@studPass", studPass);
					studCmd.Parameters.AddWithValue("@studEmail", studEmail);

					studCmd.ExecuteNonQuery();
				}

				using (SqlCommand updateCmd = new SqlCommand(updateDeptQuery, conn))
				{
					updateCmd.Parameters.AddWithValue("@loginID", studID);
					updateCmd.Parameters.AddWithValue("@studID", studID);

					updateCmd.ExecuteNonQuery();
				}

				conn.Close();

			}
		}

		protected void departmentChoices_SelectedIndexChanged(object sender, EventArgs e)
		{
			DbUtility conn = new DbUtility();
			string selectedDeptID = departmentChoices.SelectedValue;

			courseList.ClearSelection();

			if (!string.IsNullOrEmpty(selectedDeptID) )
			{
				conn.ddlCourse(courseList, selectedDeptID);
				
			}
		}
	}
}
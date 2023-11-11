using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Gabay_Final_V2.Views.DashBoard.Department_Homepage
{
    public partial class Department_profile : System.Web.UI.Page
    {
        // Property to hold the list of file paths
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int deptSessionID = Convert.ToInt32(Session["user_ID"]);
                loadGeneralInfo(deptSessionID);
                loadCredentials(deptSessionID);
                BindUploadedFiles(deptSessionID);

            }

        }
        public void loadGeneralInfo(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT dept_name, dept_head, dept_description, contactNumber, email,
                                 courses, office_hour FROM department WHERE user_ID = @user_ID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            deptName.Text = reader["dept_name"].ToString();
                            deptHead.Text = reader["dept_head"].ToString();
                            deptDesc.Text = reader["dept_description"].ToString();
                            string formatCourse = reader["courses"].ToString();
                            string[] items = formatCourse.Split(',');
                            string formattedCourse = string.Join("<br>", items);
                            courses.Text = formattedCourse;
                            offHrs.Text = reader["office_hour"].ToString();

                            emailbx.Text = reader["email"].ToString();
                            conNum.Text = reader["contactNumber"].ToString();
                        }
                    }
                }
            }
        }
        public void loadCredentials(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT * FROM department WHERE user_ID = @user_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("user_ID", sessionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            m_deptName.Text = reader["dept_name"].ToString();

                            lgnIDBx.Text = reader["departmentID"].ToString();
                            pssBx.Text = reader["dept_pass"].ToString();

                            deptHeadBx.Text = reader["dept_head"].ToString();
                            deptDescription.Text = reader["dept_description"].ToString();
                            deptNameBx.Text = reader["dept_name"].ToString();
                            string formatCourse = reader["courses"].ToString();
                            //string[] items = formatCourse.Split(',');
                            //string formattedCourse = string.Join("\n", items);
                            formatCourse = formatCourse.Replace(",", Environment.NewLine);
                            CoursesAppended.Text = formatCourse;
                            officeHour.Text = reader["office_hour"].ToString();

                            emailTxtBx.Text = reader["email"].ToString();
                            contactNumber.Text = reader["contactNumber"].ToString();
                        }
                    }
                }
            }
        }

        protected void updtBtnLgn_Click(object sender, EventArgs e)
        {
            int deptSessionID = Convert.ToInt32(Session["user_ID"]);
            try
            {
                updateLgn(deptSessionID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModalScript", "openSuccessModal();", true);
                loadGeneralInfo(deptSessionID);
                loadCredentials(deptSessionID);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setErrorMessageScript", $"document.querySelector('.modal-body').innerHTML = '{message}';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openErrorModalScript", "openErrorrModal();", true);
            }
        }
        public void updateLgn(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT departmentID, dept_pass FROM department WHERE user_ID = @user_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentLgnID = lgnIDBx.Text;
                            string currentPass = pssBx.Text;

                            if (currentLgnID != reader["departmentID"].ToString() || currentPass != reader["dept_pass"].ToString())
                            {
                                reader.Close();
                                string deptUpdateQuery = @"UPDATE department 
                                                          SET departmentID = @newDeptID, 
                                                              dept_pass = @newDeptPass 
                                                          WHERE user_ID = @user_ID";

                                using (SqlCommand deptUpdateCmd = new SqlCommand(deptUpdateQuery, conn))
                                {
                                    deptUpdateCmd.Parameters.AddWithValue("@newDeptID", currentLgnID);
                                    deptUpdateCmd.Parameters.AddWithValue("@newDeptPass", currentPass);
                                    deptUpdateCmd.Parameters.AddWithValue("@user_ID", sessionID);

                                    deptUpdateCmd.ExecuteNonQuery();
                                }

                                // Update the users_table if necessary
                                string usersUpdateQuery = @"UPDATE users_table 
                                                            SET login_ID = @newDeptID, 
                                                                password = @newDeptPass 
                                                            WHERE user_ID = @user_ID";

                                using (SqlCommand usersUpdateCmd = new SqlCommand(usersUpdateQuery, conn))
                                {
                                    usersUpdateCmd.Parameters.AddWithValue("@newDeptID", currentLgnID);
                                    usersUpdateCmd.Parameters.AddWithValue("@newDeptPass", currentPass);
                                    usersUpdateCmd.Parameters.AddWithValue("@user_ID", sessionID);

                                    usersUpdateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                conn.Close();
            }
        }

        protected void updBtnDeptInfo_Click(object sender, EventArgs e)
        {
            int deptSessionID = Convert.ToInt32(Session["user_ID"]);
            try
            {
                updtDeptInfo(deptSessionID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModalScript", "openSuccessModal();", true);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setErrorMessageScript", $"document.querySelector('.modal-body').innerHTML = '{message}';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openErrorModalScript", "openErrorrModal();", true);
            }
            loadGeneralInfo(deptSessionID);
            loadCredentials(deptSessionID);
        }
        public void updtDeptInfo(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT dept_description, dept_name, dept_head, office_hour
                                 FROM department WHERE user_ID = @user_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentDscrpt = deptDescription.Text;
                            string currentDeptNm = deptNameBx.Text;
                            string currentDeptDn = deptHeadBx.Text;
                            string currentOffHrs = officeHour.Text;

                            if (currentDscrpt != reader["dept_description"].ToString() ||
                                currentDeptNm != reader["dept_name"].ToString() ||
                                currentDeptDn != reader["dept_head"].ToString() ||
                                currentOffHrs != reader["office_hour"].ToString())
                            {
                                reader.Close();
                                string deptUpdateQuery = @"UPDATE department 
                                                          SET dept_description = @newDept_descrpt,
                                                              dept_name = @newDept_Name, 
                                                              dept_head = @newDept_Head, 
                                                              office_hour = @newOffHours
                                                          WHERE user_ID = @user_ID";

                                using (SqlCommand deptUpdateCmd = new SqlCommand(deptUpdateQuery, conn))
                                {
                                    deptUpdateCmd.Parameters.AddWithValue("@newDept_descrpt", currentDscrpt);
                                    deptUpdateCmd.Parameters.AddWithValue("@newDept_Name", currentDeptNm);
                                    deptUpdateCmd.Parameters.AddWithValue("@newDept_Head", currentDeptDn);
                                    deptUpdateCmd.Parameters.AddWithValue("@newOffHours", currentOffHrs);
                                    deptUpdateCmd.Parameters.AddWithValue("@user_ID", sessionID);

                                    deptUpdateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                conn.Close();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string courseAppend = coursesInput.Text;
            if (string.IsNullOrEmpty(courseAppend))
            {
                noInput.Text = "No inputs";
            }
            else
            {
                noInput.Text = string.Empty;

                // Check if the TextBox is not empty
                if (!string.IsNullOrEmpty(CoursesAppended.Text))
                {
                    // Append a newline character before the new course
                    courseAppend = Environment.NewLine + courseAppend;
                }

                // Append the new course
                CoursesAppended.Text += courseAppend;
                coursesInput.Text = string.Empty;

                // Register a client script to update the UpdatePanel after server-side code execution
                ScriptManager.RegisterStartupScript(this, this.GetType(), "updatePanelScript", "updatePanelFunction();", true);
            }

        }
        protected void updBtnCoursesModal_Click(object sender, EventArgs e)
        {
            int deptSessionID = Convert.ToInt32(Session["user_ID"]);
            string collectedCourses = CoursesAppended.Text;

            string coursesFormatted = string.Join(",", collectedCourses.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            try
            {
                updtDeptCourse(deptSessionID, coursesFormatted);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModalScript", "openSuccessModal();", true);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setErrorMessageScript", $"document.querySelector('.modal-body').innerHTML = '{message}';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openErrorModalScript", "openErrorrModal();", true);
            }
            loadGeneralInfo(deptSessionID);
            loadCredentials(deptSessionID);
        }
        public void updtDeptCourse(int sessionID, string coursesData)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"UPDATE department SET courses = @Courses WHERE user_ID = @sessionID";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Courses", coursesData);
                    cmd.Parameters.AddWithValue("@sessionID", sessionID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        protected void updtBtnContactInfo_Click(object sender, EventArgs e)
        {
            int deptSessionID = Convert.ToInt32(Session["user_ID"]);
            try
            {
                updtContactInfo(deptSessionID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "openSuccessModalScript", "openSuccessModal();", true);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setErrorMessageScript", $"document.querySelector('.modal-body').innerHTML = '{message}';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openErrorModalScript", "openErrorrModal();", true);
            }
            loadGeneralInfo(deptSessionID);
            loadCredentials(deptSessionID);
        }
        public void updtContactInfo(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT contactNumber, email
                                 FROM department WHERE user_ID = @user_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentConNum = contactNumber.Text;
                            string currentDeptEmail = emailTxtBx.Text;


                            if (currentConNum != reader["contactNumber"].ToString() || currentDeptEmail != reader["email"].ToString())

                            {
                                reader.Close();
                                string deptUpdateQuery = @"UPDATE department 
                                                          SET contactNumber = @newConNum,
                                                              email = @newEmail 
                                                          WHERE user_ID = @user_ID";

                                using (SqlCommand deptUpdateCmd = new SqlCommand(deptUpdateQuery, conn))
                                {
                                    deptUpdateCmd.Parameters.AddWithValue("@newConNum", currentConNum);
                                    deptUpdateCmd.Parameters.AddWithValue("@newEmail", currentDeptEmail);
                                    deptUpdateCmd.Parameters.AddWithValue("@user_ID", sessionID);

                                    deptUpdateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                conn.Close();
            }
        }

        //PARA SA KATUNG FILES NANI

        private string GetLoggedInUserID()
        {

            return Session["user_ID"]?.ToString();
        }

        // Modified btnUpload_Click method
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    int sessionID = Convert.ToInt32(Session["user_ID"]);
                    string fileName = fileUpload.FileName;

                    UpdateFileData(sessionID, fileName);

                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception)
                {
                    // Handle any exceptions or display an error message
                }
            }
        }


        private void UpdateFileData(int sessionID, string fileName)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"UPDATE department SET file_path = @filePath WHERE user_ID = @user_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID); // Use int parameter
                    cmd.Parameters.AddWithValue("@filePath", fileName);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        // Inside your Page_Load or wherever it's appropriate
        private void BindUploadedFiles(int sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT file_path FROM department WHERE user_ID = @user_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_ID", sessionID);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable filesTable = new DataTable();
                        adapter.Fill(filesTable);

                        // Ensure that the 'FilePath' column is present
                        if (!filesTable.Columns.Contains("FilePath"))
                        {
                            filesTable.Columns.Add("FilePath", typeof(string));
                        }

                        // Assuming that the file path in your database includes the file name
                        filesTable.Columns.Add("FileName", typeof(string));
                        foreach (DataRow row in filesTable.Rows)
                        {
                            // Check if the 'file_path' column is present
                            if (filesTable.Columns.Contains("file_path"))
                            {
                                object filePathObj = row["file_path"];
                                if (filePathObj != null && filePathObj != DBNull.Value)
                                {
                                    string filePath = filePathObj.ToString();
                                    if (!string.IsNullOrEmpty(filePath))
                                    {
                                        row["FileName"] = Path.GetFileName(filePath);
                                    }
                                    else
                                    {
                                        // Handle the case where the file path is empty
                                        row["FileName"] = "No File";
                                    }
                                }
                                else
                                {
                                    // Handle the case where the file path is DBNull.Value or null
                                    row["FileName"] = "No File";
                                }
                            }
                            else
                            {
                                // Handle the case where the 'file_path' column is not present
                                // You can set a default value for the 'FileName' column in this case
                                row["FileName"] = "No File";
                            }
                        }


                        RptFiles.DataSource = filesTable;
                        RptFiles.DataBind();
                    }
                }
            }
        }


        protected void DownloadFile(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string filePath = btn.CommandArgument;

            // Check if the file path is not empty
            if (!string.IsNullOrEmpty(filePath))
            {
                // Retrieve the file data (byte array) from the database
                byte[] fileData = GetFileDataFromDatabase(filePath);

                if (fileData != null)
                {
                    // Set response headers for file download
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(filePath)}");
                    Response.BinaryWrite(fileData);
                    Response.End();
                }
                else
                {
                    // Handle the case where file data is not retrieved from the database
                    // You can display an error message or log the issue
                }
            }
            else
            {
                // Handle the case where the file path is empty
                // You can display an error message or log the issue
            }
        }

        private byte[] GetFileDataFromDatabase(string filePath)
        {
            byte[] fileData = null;

            // Retrieve file data from the database based on the provided file path
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT file_path FROM department WHERE user_ID = @User_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User_ID", filePath);
                    object result = cmd.ExecuteScalar();
                    {
                        if (result != null && result != DBNull.Value)
                        {
                            return (byte[])result;
                        }
                    }
                }
            }

            return fileData;
        }




        protected void RptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Handle the item command event here, if needed
        }





    }
}
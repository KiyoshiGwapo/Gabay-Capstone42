﻿using System;
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
                BindFilesToDropDownList();

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

        // Adjust the event handler for BtnUpload_Click
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    // Get the file name and data
                    string fileName = !string.IsNullOrEmpty(txtFileName.Text) ? txtFileName.Text : Path.GetFileName(fileUpload.FileName);
                    byte[] fileData = fileUpload.FileBytes;

                    // Get the user_ID from the session
                    int deptSessionID = Convert.ToInt32(Session["user_ID"]);

                    // Insert the file data into the database along with user_ID
                    InsertFileData(fileName, fileData, deptSessionID);

                    // Display a success message or perform any other actions
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    // You can replace this with actual logging mechanism in your application
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw; // Rethrow the exception to see the full stack trace in the browser
                }
            }
        }



        // Modify the UpdateFileData method
        private void InsertFileData(string fileName, byte[] fileData, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string insertQuery = "INSERT INTO DepartmentFiles (FileName, FileData, user_ID) VALUES (@FileName, @FileData, @user_ID)";

                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@user_ID", userId);
                    command.ExecuteNonQuery();
                }
            }
        }




        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedFileId"] != null)
            {
                int fileId = (int)ViewState["SelectedFileId"];

                byte[] fileData = FetchFileDataFromDatabase(fileId);
                string fileName = FetchFileNameFromDatabase(fileId);

                if (fileData != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", $"inline; filename={fileName}");
                    Response.BinaryWrite(fileData);
                    Response.End();
                }
                else
                {
                    DownloadErrorLabel.Text = "File not found.";
                }

                string script = "window.open('" + Request.Url.AbsoluteUri + "', '_blank');";
                ClientScript.RegisterStartupScript(this.GetType(), "openNewTab", script, true);
            }
            else
            {
                DownloadErrorLabel.Text = "Please select a file to preview.";
            }
        }

        private byte[] FetchFileDataFromDatabase(int fileId)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = "SELECT FileData FROM DepartmentFiles WHERE FileId = @FileId";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (byte[])result;
                    }
                }
            }

            return null;
        }

        private string FetchFileNameFromDatabase(int fileId)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = "SELECT FileName FROM DepartmentFiles WHERE Fileid = @Fileid";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@Fileid", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }

        public class FileData
        {
            public int FileId { get; set; }
            public string FileName { get; set; }
            public byte[] FileBytes { get; set; }
        }

        public List<FileData> FetchFilesDataFromDatabase()
        {
            List<FileData> filesList = new List<FileData>();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Modify the query based on your requirements
                string query = "SELECT FileId, FileName, FileData FROM DepartmentFiles";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileData file = new FileData
                            {
                                FileId = reader.GetInt32(0),
                                FileName = reader.GetString(1),
                                FileBytes = (byte[])reader["FileData"]
                            };
                            filesList.Add(file);
                        }
                    }
                }
            }

            return filesList;
        }

        // Modify the BindFilesToDropDownList method
        private void BindFilesToDropDownList()
        {
            List<FileData> filesList = FetchFilesDataFromDatabase();

            ddlFiles.Items.Clear();

            foreach (FileData file in filesList)
            {
                ListItem item = new ListItem(file.FileName, file.FileId.ToString());
                ddlFiles.Items.Add(item);
            }

            if (ViewState["SelectedFileId"] != null)
            {
                int selectedFileId = (int)ViewState["SelectedFileId"];
                ddlFiles.SelectedValue = selectedFileId.ToString();
            }
        }



        // Adjust the SelectedIndexChanged event for ddlFiles
        protected void ddlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedFileId = int.Parse(ddlFiles.SelectedValue);
            ViewState["SelectedFileId"] = selectedFileId;


            DownloadErrorLabel.Text = "Selected File ID: " + selectedFileId;

            byte[] selectedFileData = FetchFileDataFromDatabase(selectedFileId);
            if (selectedFileData != null)
            {
                DownloadErrorLabel.Text = "";
            }
            else
            {
                DownloadErrorLabel.Text = "Selected file data not found.";
            }
        }


    
        protected void RptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Handle the item command event here, if needed
        }





    }
}
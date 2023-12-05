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

namespace Gabay_Final_V2.Views.Modules.Department_Info
{
    public partial class Student_deptInfo : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            int studentSessionID = Convert.ToInt32(Session["user_ID"]);
            LoadData(studentSessionID);
            if (!IsPostBack)
            {
                // Bind dropdown only on initial load
                BindFilesToDropDownList(studentSessionID);
            }

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

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedFileId"] != null)
            {
                int fileId = (int)ViewState["SelectedFileId"];

                // Add the studentSessionID parameter here
                int studentSessionID = Convert.ToInt32(Session["user_ID"]);

                byte[] fileData = FetchFileDataFromDatabase(fileId);
                string fileName = FetchFileNameFromDatabase(fileId, studentSessionID);  // Include studentSessionID parameter

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

        private string FetchFileNameFromDatabase(int fileId, int studentSessionID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = @"SELECT FileName 
                               FROM DepartmentFiles 
                               WHERE FileId = @FileId 
                               AND department_ID IN (SELECT department_ID FROM student WHERE user_ID = @studentSessionID)";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);
                    command.Parameters.AddWithValue("@studentSessionID", studentSessionID);

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



        // Modify the BindFilesToDropDownList method
        private void BindFilesToDropDownList(int studentSessionID)
        {
            List<FileData> filesList = FetchFilesDataFromDatabase(studentSessionID);

            // Add an empty item as the default in the DropDownList
            ddlFiles.Items.Clear();
            ddlFiles.Items.Add(new ListItem("Select Here", ""));

            foreach (FileData file in filesList)
            {
                ListItem item = new ListItem(file.FileName, file.FileId.ToString());
                ddlFiles.Items.Add(item);
            }

            if (ViewState["SelectedFileId"] != null)
            {
                int selectedFileId = (int)ViewState["SelectedFileId"];

                // Check if the selected file is in the DropDownList items
                if (ddlFiles.Items.FindByValue(selectedFileId.ToString()) != null)
                {
                    ddlFiles.SelectedValue = selectedFileId.ToString();
                }
                else
                {
                    // If the selected file is not in the items, set the selected value to the first item (empty/default)
                    ddlFiles.SelectedIndex = 0;
                }
            }
        }

        private List<FileData> FetchFilesDataFromDatabase(int studentSessionID)
        {
            List<FileData> filesList = new List<FileData>();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = @"SELECT FileId, FileName, FileData 
                               FROM DepartmentFiles 
                               WHERE department_ID IN (SELECT department_ID FROM student WHERE user_ID = @studentSessionID)";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@studentSessionID", studentSessionID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileData file = new FileData
                            {
                                FileId = Convert.ToInt32(reader["FileId"]),
                                FileName = reader["FileName"].ToString(),
                                FileBytes = reader["FileData"] as byte[]
                            };

                            filesList.Add(file);
                        }
                    }
                }
            }

            return filesList;
        }



        // Adjust the SelectedIndexChanged event for ddlFiles
        protected void ddlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Initialize a variable to store the selectedFileId
            int selectedFileId;

            // Check if the selected value is a valid integer
            if (int.TryParse(ddlFiles.SelectedValue, out selectedFileId))
            {
                // Successfully parsed, update ViewState
                ViewState["SelectedFileId"] = selectedFileId;
                DownloadErrorLabel.Text = "Selected File ID: " + selectedFileId;

                // Fetch and display the selected file data (if needed)
                byte[] selectedFileData = FetchFileDataFromDatabase(selectedFileId);
                if (selectedFileData != null)
                {
                    DownloadErrorLabel.Text = "";
                }
                else
                {
                    // Handle the case where the selected value is not a valid integer
                    DownloadErrorLabel.Text = "Invalid selection. Please select a valid file.";
                }
                // Add debugging statements here

                Selected.Text = "<- To view this File click this Button";
            }
            else
            {
                // Handle the case where the selected value is not a valid integer
                DownloadErrorLabel.Text = "Invalid selection. Please select a valid file.";
            }
        }



    }
}
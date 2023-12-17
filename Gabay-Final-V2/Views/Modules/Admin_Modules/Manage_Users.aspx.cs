using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_Users : System.Web.UI.Page
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initial data load or any other initialization
                SetGridViewVisibility();

            }
        }

        //para close sa modal
        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGridViewVisibility();
            BindGridView(ddlFilter.SelectedValue);
        }

        protected void EditModalClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDetailedModal", "$('#editPasswordModal').modal('hide');", true);
        }

        protected void DeleteModalClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDeleteModal", "$('#confirmDeleteModal').modal('hide');", true);
        }


        private void BindGridView(string filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";
                // Adjust the query based on your database schema and the selected filter
                if (filter == "Students")
                {
                    query = "SELECT s.ID_student as ID, s.user_ID, s.name as Name, s.email as Email, d.dept_name as StudentDepartment " +
                            "FROM student s " +
                            "INNER JOIN department d ON s.department_ID = d.ID_dept";
                    GridViewStudents.Visible = true;
                    GridViewDepartments.Visible = false;
                }
                else if (filter == "Departments")
                {
                    query = "SELECT ID_dept as ID, dept_head as DepartmentHead, dept_name as DepartmentName, email as Email FROM department";
                    GridViewStudents.Visible = false;
                    GridViewDepartments.Visible = true;
                }


                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind data to the appropriate GridView
                    if (filter == "Students")
                    {
                        GridViewStudents.DataSource = dt;
                        GridViewStudents.DataBind();
                    }
                    else if (filter == "Departments")
                    {
                        GridViewDepartments.DataSource = dt;
                        GridViewDepartments.DataBind();
                    }
                }
            }
        }

        private void SetGridViewVisibility()
        {
            string filter = ddlFilter.SelectedValue;
            GridViewStudents.Visible = (filter == "Students");
            GridViewDepartments.Visible = (filter == "Departments");
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected row index from the hidden field
                int rowIndex = Convert.ToInt32(hfSelectedRowIndex.Value);

                // Determine which GridView is currently active (Students or Departments)
                GridView gridView;
                if (ddlFilter.SelectedValue == "Students")
                {
                    gridView = GridViewStudents;
                }
                else if (ddlFilter.SelectedValue == "Departments")
                {
                    gridView = GridViewDepartments;
                }
                else
                {
                    // Handle the case where no filter is selected
                    return;
                }

                // Make sure the rowIndex is within bounds
                if (rowIndex >= 0 && rowIndex < gridView.Rows.Count)
                {
                    int idToDelete = Convert.ToInt32(gridView.DataKeys[rowIndex].Value);

                    // Perform the delete operation based on your database schema
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = "";
                        if (ddlFilter.SelectedValue == "Students")
                        {
                            deleteQuery = "DELETE FROM student WHERE ID_student = @ID";
                        }
                        else if (ddlFilter.SelectedValue == "Departments")
                        {
                            deleteQuery = "DELETE FROM department WHERE ID_dept = @ID";
                        }

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ID", idToDelete);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Rebind the GridView to reflect the changes
                    BindGridView(ddlFilter.SelectedValue);

                    // Optionally, show a success message or perform other actions after delete
                }

                // Hide the modal after delete
                ScriptManager.RegisterStartupScript(this, GetType(), "hideModal", "$('#confirmDeleteModal').modal('hide');", true);

                // Optionally, show a success message or perform other actions after delete
                string successMessage = "User Deleted successfully.";
                ShowSuccessModal(successMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while deleting the user: " + ex.Message;
                ShowErrorModal(errorMessage);
            }

        }



        //EDIT PASSWORD
        protected void btnConfirmEditPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected row index from the hidden field
                int rowIndex = Convert.ToInt32(hfSelectedRowIndex.Value);

                // Determine which GridView is currently active (Students or Departments)
                GridView gridView;
                if (ddlFilter.SelectedValue == "Students")
                {
                    gridView = GridViewStudents;
                }
                else if (ddlFilter.SelectedValue == "Departments")
                {
                    gridView = GridViewDepartments;
                }
                else
                {
                    // Handle the case where no filter is selected
                    return;
                }

                // Make sure the rowIndex is within bounds
                if (rowIndex >= 0 && rowIndex < gridView.Rows.Count)
                {
                    int idToEdit = Convert.ToInt32(gridView.DataKeys[rowIndex].Value);

                    // Get the new password from the input field in the modal
                    string newPassword = txtNewPassword.Text;

                    // Perform the password change operation based on your database schema
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string updateQuery = "";
                        string updateUserQuery = "";
                        if (ddlFilter.SelectedValue == "Students")
                        {
                            updateQuery = "UPDATE student SET stud_pass = @Password WHERE ID_student = @ID";
                            updateUserQuery = "UPDATE users_table SET password = @Password WHERE user_ID IN (SELECT user_ID FROM student WHERE ID_student = @ID)";
                        }
                        else if (ddlFilter.SelectedValue == "Departments")
                        {
                            updateQuery = "UPDATE department SET dept_pass = @Password WHERE ID_dept = @ID";
                            updateUserQuery = "UPDATE users_table SET password = @Password WHERE user_ID IN (SELECT user_ID FROM department WHERE ID_dept = @ID)";
                        }

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ID", idToEdit);
                            cmd.Parameters.AddWithValue("@Password", newPassword);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(updateUserQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ID", idToEdit);
                            cmd.Parameters.AddWithValue("@Password", newPassword);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // Rebind the GridView to reflect the changes
                    BindGridView(ddlFilter.SelectedValue);

                    // Optionally, show a success message or perform other actions after password change
                }

                // Hide the modal after password change
                ScriptManager.RegisterStartupScript(this, GetType(), "hideEditPasswordModal", "$('#editPasswordModal').modal('hide');", true);

                // Optionally, show a success message or perform other actions after password change
                string successMessage = "Password updated successfully.";
                ShowSuccessModal(successMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while updating the password: " + ex.Message;
                ShowErrorModal(errorMessage);
            }

        }

        private void ShowSuccessModal(string successMessage)
        {
            // Show a success message
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessModal",
                $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
        }

        private void ShowErrorModal(string errorMessage)
        {
            // Show an error message
            ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal",
                $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
        }
        //Generate Reports

        protected void btnDownloadReports_Click(object sender, EventArgs e)
        {
            // Determine which GridView is currently active (Students or Departments)
            GridView gridView;
            if (ddlFilter.SelectedValue == "Students")
            {
                gridView = GridViewStudents;
            }
            else if (ddlFilter.SelectedValue == "Departments")
            {
                gridView = GridViewDepartments;
            }
            else
            {
                // Handle the case where no filter is selected
                return;
            }

            // Create a DataTable to hold the data from the GridView
            DataTable dt = new DataTable();

            // Add columns to the DataTable based on the GridView header
            foreach (DataControlField field in gridView.Columns)
            {
                // Exclude the "Actions" column
                if (field.HeaderText != "Actions")
                {
                    dt.Columns.Add(field.HeaderText);
                }
            }

            foreach (GridViewRow row in gridView.Rows)
            {
                DataRow dr = dt.NewRow();

                // Iterate through the cells in the row
                foreach (TableCell cell in row.Cells)
                {
                    DataControlField field = gridView.Columns[row.Cells.GetCellIndex(cell)];

                    // Exclude the "Actions" column
                    if (field.HeaderText != "Actions")
                    {
                        // Add the cell text to the DataRow
                        dr[field.HeaderText] = cell.Text;
                    }
                }

                // Add the DataRow to the DataTable
                dt.Rows.Add(dr);
            }

            // Determine the selected report type (Excel or PDF)
            string reportType = ddlReportType.SelectedValue;

            // Call the appropriate method to export the DataTable
            if (reportType == "Excel")
            {
                ExportToExcel(dt);
            }
            else if (reportType == "PDF")
            {
                // Call the method to export to PDF (implement this method)
                ExportToPDF(dt, ddlFilter.SelectedItem.Text);
            }
        }

        private void ExportToExcel(DataTable dt)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportedData.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // Create a form to contain the grid
                    Table table = new Table();
                    table.GridLines = GridLines.Both;

                    // Add the header row to the table
                    TableRow headerRow = new TableRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // Exclude the "Department" and "Actions" columns
                        if (dt.Columns[i].ColumnName != "Department" && dt.Columns[i].ColumnName != "Actions")
                        {
                            TableCell cell = new TableCell();
                            cell.Text = dt.Columns[i].ColumnName;
                            headerRow.Cells.Add(cell);
                        }
                    }
                    table.Rows.Add(headerRow);

                    // Add each data row to the table
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Exclude the "Department" and "Actions" columns
                            if (dt.Columns[j].ColumnName != "Department" && dt.Columns[j].ColumnName != "Actions")
                            {
                                TableCell cell = new TableCell();
                                cell.Text = dt.Rows[i][j].ToString();
                                row.Cells.Add(cell);
                            }
                        }
                        table.Rows.Add(row);
                    }

                    // Render the table to the HTMLTextWriter
                    table.RenderControl(htw);

                    // Write the HTML to the response stream
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
        }
        private void ExportToPDF(DataTable dt, string accountType)
        {
            Document document = new Document();

            // Provide the path where you want to save the PDF file
            string filePath = Server.MapPath("~/ExportedData.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                // Add the date and time at the top right corner
                PdfPTable dateTimeTable = new PdfPTable(1);
                dateTimeTable.WidthPercentage = 100;
                dateTimeTable.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell dateTimeCell = new PdfPCell(new Phrase(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new Font(Font.FontFamily.HELVETICA, 10)));
                dateTimeCell.Border = PdfPCell.NO_BORDER;
                dateTimeTable.AddCell(dateTimeCell);

                document.Add(dateTimeTable);

                document.Add(new Paragraph("\n"));

                // Add the logo or picture to the top right corner
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/Images/UC-LOGO.png"));
                logo.ScaleToFit(150f, 150f); // Adjust 
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);

                // Add spacing
                document.Add(new Paragraph("\n"));

                // Add title to the document
                Paragraph des = new Paragraph("University of Cebu – Lapu-Lapu and Mandaue Campus (UCLM)", new Font(Font.FontFamily.HELVETICA, 9));
                des.Font.Color = BaseColor.BLACK;
                des.Alignment = Element.ALIGN_CENTER;
                document.Add(des);

                // Add spacing
                document.Add(new Paragraph("\n"));
                // Add title to the document with selected account type
                Paragraph title = new Paragraph($"List of {accountType}", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLUE));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // Add spacing between title and table
                document.Add(new Paragraph("\n"));

                // Add table to the document
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100; // Table width is set to 100% of the page width

                // Add columns to the table
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != "Actions" && column.ColumnName != "Department")
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                        cell.BackgroundColor = new BaseColor(91, 192, 222); // Header row background color
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                // Add data rows to the table
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "Actions" && column.ColumnName != "Department")
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), new Font(Font.FontFamily.HELVETICA, 10)));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                    }
                }
                document.Add(table);

                // Add spacing between title and table
                document.Add(new Paragraph("\n"));

                // Create a new table for total students
                PdfPTable totalStudentsTable = new PdfPTable(1);
                totalStudentsTable.WidthPercentage = 100;
                totalStudentsTable.HorizontalAlignment = Element.ALIGN_CENTER;

                // Add a cell for the total students count
                PdfPCell totalStudentsCell = new PdfPCell(new Phrase($"Total: {dt.Rows.Count}", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
                totalStudentsCell.Border = PdfPCell.NO_BORDER;
                totalStudentsTable.AddCell(totalStudentsCell);

                // Add the total students table to the document
                document.Add(totalStudentsTable);

                // Add title to the document
                Paragraph by = new Paragraph("Prepared by:", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK));
                by.Alignment = Element.ALIGN_RIGHT;
                document.Add(by);

                // Add title to the document
                Paragraph admin = new Paragraph("______________", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK));
                admin.Alignment = Element.ALIGN_RIGHT;
                document.Add(admin);
                // Add spacing between title and table
                document.Add(new Paragraph("\n"));

                // Call the method to add the footer
                AddCustomFooter(document);

                document.Close();
            }

            // Provide the file for download
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ExportedData.pdf");
            Response.TransmitFile(filePath);
            Response.End();
        }

        private void AddCustomFooter(Document document)
        {
            Paragraph footer = new Paragraph();

            string footerHtml = @"
            <div style='background-color: cyan; font-size: 10px; position: fixed; width: 100%; text-align:center; color: #333;' >
                <div style='padding: 5px; '>
                    <b>Contact Information:</b><br>
                    A.C. Cortes Avenue 6000 Mandaue City Cebu<br>
                    Email: info@uclm.edu.ph<br>
                    (032) 345 6666<br>
                </div>
                <div style='padding: 5x; '>
                    <b>Quick Links:</b><br>
                    <a href='https://www.universityofcebu.net/p/university-of-cebu-lapu-lapu-and.html'>Website</a><br>
                    <a href='https://www.facebook.com/search/top?q=uclm%20college%20of%20computer%20studies'>Facebook</a><br>
                </div>
                <div style='padding: 5x;'>
                    <b>ABOUT US:</b><br>
                    GABAY is more than just a word;<br> 
                    it's a beacon of support and wisdom <br>
                    that lights the path for all of us. <br>
                    In times of uncertainty,<br> 
                    when we seek direction or a helping hand, <br>
                    GABAY reminds us that we are never alone. <br>
                </div>
                <div style='clear: both;'></div>
            </div>
        </div>
    ";

            // Use HTMLWorker to parse HTML and add it to the paragraph
            using (var sr = new StringReader(footerHtml))
            {
                List<IElement> elements = HTMLWorker.ParseToList(sr, null, null);

                foreach (var element in elements)
                {
                    footer.Add(element);
                }
            }

            // Add the footer paragraph to the document
            document.Add(footer);
        }





    }
}
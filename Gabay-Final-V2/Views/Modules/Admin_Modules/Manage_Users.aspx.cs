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

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGridViewVisibility();
            BindGridView(ddlFilter.SelectedValue);
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
                    query = "SELECT s.ID_student as ID, s.name as Name, s.email as Email, d.dept_name as StudentDepartment " +
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
        }

        //EDIT PASSWORD

        protected void btnConfirmEditPassword_Click(object sender, EventArgs e)
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
                    if (ddlFilter.SelectedValue == "Students")
                    {
                        updateQuery = "UPDATE student SET stud_pass = @Password WHERE ID_student = @ID";
                    }
                    else if (ddlFilter.SelectedValue == "Departments")
                    {
                        updateQuery = "UPDATE department SET dept_pass = @Password WHERE ID_dept = @ID";
                    }

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
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
                ExportToPDF(dt);
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


        private void ExportToPDF(DataTable dt)
        {
            Document document = new Document();

            // Provide the path where you want to save the PDF file
            string filePath = Server.MapPath("~/ExportedData.pdf");

            // Create a PdfWriter instance to write to the document
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();

            // Add a table to the document
            PdfPTable table = new PdfPTable(dt.Columns.Count);

            // Add columns to the table
            foreach (DataColumn column in dt.Columns)
            {
                // Exclude the "Actions" column
                if (column.ColumnName != "Actions" && column.ColumnName != "Department")
                {
                    table.AddCell(new Phrase(column.ColumnName));
                }
            }

            // Add rows to the table
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    // Exclude the "Actions" column
                    if (column.ColumnName != "Actions" && column.ColumnName != "Department")
                    {
                        table.AddCell(new Phrase(row[column].ToString()));
                    }
                }
            }

            document.Add(table);

            document.Close();

            // Provide the file for download
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=ExportedData.pdf");
            Response.TransmitFile(filePath);
            Response.End();
        }



    }
}

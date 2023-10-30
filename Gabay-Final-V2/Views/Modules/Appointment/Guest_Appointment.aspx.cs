using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Guest_Appointment : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the minimum date for the date input field
            date.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

            if (!IsPostBack)
            {
                // Populate the department dropdown list
                ddlDept(departmentChoices);
            }
        }

        public void ddlDept(DropDownList deptDDL)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_dept, dept_name FROM department";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem item = new ListItem(reader["dept_name"].ToString(), reader["ID_dept"].ToString());
                    deptDDL.Items.Add(item);
                }

                conn.Close();
            }
        }

        private bool IsAppointmentExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM appointment WHERE [email] = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Get the values from the form fields
            string email = Email.Text;

            // Check if an appointment with the given email already exists
            if (IsAppointmentExists(email))
            {
                SubmitStatusNotSubmitted.Text = "Appointment with this email already exists. (Appointment was Not Sended)";
                SubmitStatusNotSubmitted.CssClass = "status-not-submitted";
            }
            else
            {
                // The appointment doesn't exist; proceed with insertion
                string fullName = FullName.Text;
                string contactNumber = ContactN.Text;
                string selectedTime = time.SelectedValue;
                string selectedDate = date.Value;
                string deptName = departmentChoices.SelectedItem.Text; // Get the selected department name
                string concern = Message.Text; // Get the concern/message

                // Additional data
                string status = "pending";
                string studentID = "guest";
                string courseYear = "guest";

                // Insert data into the "appointment" table
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO appointment ([deptName], [full_name], [email], [student_ID], [course_year], [contactNumber], [appointment_date], [appointment_time], [concern], [appointment_status]) " +
                        "VALUES (@DeptName, @FullName, @Email, @StudentID, @CourseYear, @ContactNumber, @SelectedDate, @SelectedTime, @Concern, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DeptName", deptName);
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        cmd.Parameters.AddWithValue("@CourseYear", courseYear);
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
                        cmd.Parameters.AddWithValue("@SelectedTime", selectedTime);
                        cmd.Parameters.AddWithValue("@Concern", concern);
                        cmd.Parameters.AddWithValue("@Status", status);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                    // Display a success message
                    SubmissionStatusSubmitted.Text = "Appointment submitted successfully.";
                    SubmissionStatusSubmitted.CssClass = "status-submitted";

                    // Clear form fields
                    FullName.Text = "";
                    Email.Text = "";
                    ContactN.Text = "";
                    time.SelectedIndex = 0; // Reset the dropdown selection
                    date.Value = "";
                    departmentChoices.SelectedIndex = 0; // Reset the dropdown selection
                    Message.Text = "";
                }
            }
        }


        //Search sa iyang appointment
        protected void searchResultsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                // No results found, show the label
                noResultsLabel.Visible = true;
            }
            else
            {
                // Results found, hide the label
                noResultsLabel.Visible = false;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Get the email address entered for the search
            string searchEmail = searchInput.Text;

            // Search for appointments with the given email address
            DataTable searchResults = SearchAppointmentsByEmail(searchEmail);

            // Bind the search results to the GridView
            searchResultsGridView.DataSource = searchResults;
            searchResultsGridView.DataBind();

            // Check if there are no search results and update the visibility of the label
            noResultsLabel.Visible = searchResults.Rows.Count == 0;
        }




        private DataTable SearchAppointmentsByEmail(string email)
        {
            // Implement the logic to query the database and retrieve search results
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT [full_name], [appointment_status] FROM appointment WHERE [email] = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(results);
                    }
                }
            }

            return results;
        }


    }
}
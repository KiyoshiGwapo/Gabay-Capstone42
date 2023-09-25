using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Gabay_Final_V2.Views.Modules.CampusInfo
{
    public partial class Student_CampusInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAccordionData();
            }
        }

        private void BindAccordionData()
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            string query = "SELECT id, Title, Content FROM Campus_Information";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

                rptAccordion.DataSource = dt;
                rptAccordion.DataBind();
            }
        }

        public class AccordionItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int id { get; set; }
        }


        // Add this JavaScript function to handle the Edit button click and display the appropriate content in the modal
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string script = @"
            function editAccordionItem(itemid, title) {
                // Assuming you have a hidden field with ID 'hdnAccordionIndex' to store the item ID for the update
                document.getElementById('hdnAccordionIndex').value = itemid;
                // Set the title in the title input field
                document.getElementById('<%= txtNewTitle.ClientID %>').value = title;

                // Fetch the content from the accordion item and set it in the modal textarea
                var accordionItem = document.querySelector(`[data-item-id='${itemid}']`);
                var content = accordionItem.querySelector('.description').innerText.trim();
                document.getElementById('<%= txtNewContent.ClientID %>').value = content;
            }
        ";

            ScriptManager.RegisterStartupScript(this, GetType(), "EditAccordionItemScript", script, true);

        }

        protected void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            // Get the values from the modal input fields
            string newTitle = txtNewTitle.Text;
            string newContent = txtNewContent.Text;

            // Insert the new record into the database
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Campus_Information (Title, Content) VALUES (@Title, @Content)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", newTitle);
                    command.Parameters.AddWithValue("@Content", newContent);


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Refresh the page to update the accordion with the modified content
                        Response.Redirect(Request.RawUrl);
                        // New record inserted successfully
                        Response.Write("<script>alert('New record added successfully.');</script>");
                        // You can update the accordion or perform any other actions here to reflect the new record on the front-end.
                    }
                    else
                    {
                        // Refresh the page to update the accordion with the modified content
                        Response.Redirect(Request.RawUrl);
                        // New record insertion failed
                        Response.Write("<script>alert('Failed to add new record. Please try again later.');</script>");
                    }
                }
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Get the edited title and description from the modal inputs
            string editedTitle = txtNewTitle.Text;
            string editedContent = txtNewContent.Text;

            // Get the ID of the campus information to update (from the hidden field)
            if (int.TryParse(hdnAccordionIndex.Value, out int campusInfoId))
            {
                // Update the content in the database
                SaveChanges(campusInfoId, editedTitle, editedContent);

                // Refresh the page to update the accordion with the modified content
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                // Handle the case where 'hdnAccordionIndex.Value' cannot be parsed to an integer
                // You may want to show an error message or perform error handling
            }
        }

        public void SaveChanges(int index, string title, string content)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";

                // Update the database with the edited title, description, and the corresponding accordion index
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Assuming you have a primary key 'Id' column in the 'Campus_Information' table
                    // Use this Id to identify which accordion item to update
                    int campusInfoId = index; // Replace this with the appropriate value from the front-end

                    // Update the 'Title' and 'Content' columns for the specified accordion item
                    string updateQuery = "UPDATE Campus_Information SET Title = @Title, Content = @Content WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Content", content);
                        command.Parameters.AddWithValue("@id", campusInfoId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Content updated successfully
                            // You can show a success message or perform any other actions
                            Response.Write("<script>alert('Content updated successfully.');</script>");
                        }
                        else
                        {
                            // Content update failed
                            // You can show an error message or perform any other error handling
                            Response.Write("<script>alert('Content update failed. Please try again later.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception if any and show an error message or perform error handling
                Response.Write("<script>alert('Error updating content: " + ex.Message + "');</script>");
            }
        }

        protected void btnDeleteContent_Click(object sender, EventArgs e)
        {

            // Get the ID of the campus information to update (from the hidden field)
            if (int.TryParse(hdnAccordionIndex.Value, out int campusInfoId))
            {
                // Update the content in the database
                DeleteContent(campusInfoId);
                // Refresh the page to update the accordion with the modified content
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                // Handle the case where 'hdnAccordionIndex.Value' cannot be parsed to an integer
                // You may want to show an error message or perform error handling
            }
        }
        public void DeleteContent(int index)
        {
            string connectionString = "Data Source=LAPTOP-35UJ0LOL\\SQLEXPRESS;Initial Catalog=gabay_v.1.8;Integrated Security=True";
            string deleteQuery = "DELETE FROM Campus_Information WHERE id = @id";
            int campusInfoId = index;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", campusInfoId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Content delete failed');</script>");
                    }
                }
            }
        }


    }
}
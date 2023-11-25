using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_Chatbot : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDT();
            }
        }

        public void BindDT()
        {
            Chatbot_model conn = new Chatbot_model();
            ScriptTable.DataSource = conn.dt();
            ScriptTable.DataBind();
        }

        protected void scriptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string Keywords = this.KeywordTextArea.Text.ToLower();
                string Script = this.ScriptTextArea.Text;

                if (!string.IsNullOrEmpty(Script) && !string.IsNullOrEmpty(Keywords))
                {
                    Chatbot_model conn = new Chatbot_model();
                    conn.AddResponse(Script, Keywords);

                    ScriptTextArea.Text = string.Empty;
                    KeywordTextArea.Text = string.Empty;

                    BindDT();
                }

                string successMessage = "Script Added successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while adding the script: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        protected void ScriptTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the selected row index
            int rowIndex = e.NewEditIndex;

            // Get the data from the selected row
            GridViewRow selectedRow = ScriptTable.Rows[rowIndex];
            Label scriptContentLabel = selectedRow.FindControl("ScriptContentLabel") as Label;
            Label keywordsLabel = selectedRow.FindControl("KeywordsLabel") as Label;

            // Populate the TextBox controls with the data
            ScriptTextArea.Text = scriptContentLabel.Text;
            KeywordTextArea.Text = keywordsLabel.Text;

            // Hide the Save button and show the Update and Cancel buttons
            saveBtnCont.Attributes["class"] = "d-grid d-none";
            updtBtnCont.Attributes["class"] = "d-flex justify-content-evenly";

            ScriptTable.EditIndex = -1;
        }

        protected void updtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected row index
                int rowIndex = ScriptTable.EditIndex;

                // Check if the selected row index is valid
                if (rowIndex >= 0 && rowIndex < ScriptTable.Rows.Count)
                {
                    // Get the controls within the selected row
                    GridViewRow selectedRow = ScriptTable.Rows[rowIndex];
                    Label scriptContentLabel = selectedRow.FindControl("ScriptContentLabel") as Label;
                    Label keywordsLabel = selectedRow.FindControl("KeywordsLabel") as Label;

                    // Update the data in the GridView row
                    scriptContentLabel.Text = ScriptTextArea.Text;
                    keywordsLabel.Text = KeywordTextArea.Text;

                    // Save the updated data to the database
                    int scriptId = Convert.ToInt32(ScriptTable.DataKeys[rowIndex].Value);
                    string updatedScript = ScriptTextArea.Text;
                    string updatedKeywords = KeywordTextArea.Text;

                    Chatbot_model conn = new Chatbot_model();
                    conn.UpdateResponse(scriptId, updatedScript, updatedKeywords);

                    // Reset the TextBox controls
                    ScriptTextArea.Text = string.Empty;
                    KeywordTextArea.Text = string.Empty;

                    // Hide the Update and Cancel buttons, and show the Save button
                    saveBtnCont.Attributes["class"] = "d-grid";
                    updtBtnCont.Attributes["class"] = "d-flex justify-content-evenly d-none";

                    // Cancel the editing operation of the GridView
                    ScriptTable.EditIndex = -1;

                    // Refresh the GridView with updated data
                    BindDT();

                    string successMessage = "Script Updated successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                        $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while updating the script: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }

        protected void cnclBtn_Click(object sender, EventArgs e)
        {
            ScriptTextArea.Text = string.Empty;
            KeywordTextArea.Text = string.Empty;

            saveBtnCont.Attributes["class"] = "d-grid";
            updtBtnCont.Attributes["class"] = "d-flex justify-content-evenly d-none";

            ScriptTable.EditIndex = -1;
            BindDT();
        }

        protected void ScriptTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int scriptID = Convert.ToInt32(ScriptTable.DataKeys[e.RowIndex].Value);

                Chatbot_model conn = new Chatbot_model();
                conn.DeleteResponse(scriptID);

                BindDT();

                string successMessage = "Script Deleted successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                    $"$('#successMessage').text('{successMessage}'); $('#successModal').modal('show');", true);
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while deleting the script: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                    $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
            }
            
        }
    }
}
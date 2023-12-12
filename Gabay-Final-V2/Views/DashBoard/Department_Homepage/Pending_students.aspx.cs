using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.DashBoard.Department_Homepage
{
    
    public partial class WebForm2 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reBindPendingTable();
            }
        }

        private void reBindPendingTable()
        {
            try
            {
                DbUtility conn = new DbUtility();

                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);
                    DataTable dt = conn.displayPendingStudents(userID);

                    pending_table.DataSource = dt;
                    pending_table.DataBind();
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private void reBindActiveTable()
        {
            try
            {
                DbUtility conn = new DbUtility();

                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);
                    DataTable dt = conn.displayActiveStudents(userID);

                    active_table.DataSource = dt;
                    active_table.DataBind();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void reBindDeactiveTable()
        {
            try
            {
                DbUtility conn = new DbUtility();

                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);
                    DataTable dt = conn.displayDeactivedStudents(userID);

                    deactivated_table.DataSource = dt;
                    deactivated_table.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DbUtility conn = new DbUtility();

                string searchCriteria = txtSearch.Text;
                string studentStatusCriteria = hidStatus.Value;

                if (Session["user_ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["user_ID"]);

                    DataTable dt = conn.searchStudents(userID, searchCriteria, studentStatusCriteria);

                    if (studentStatusCriteria == "pending")
                    {
                        pending_table.DataSource = dt;
                        pending_table.DataBind();

                    }
                    else if (studentStatusCriteria == "activated")
                    {
                        active_table.DataSource = dt;
                        active_table.DataBind();
                    }
                    else if (studentStatusCriteria == "deactivated")
                    {
                        deactivated_table.DataSource = dt;
                        deactivated_table.DataBind();
                    }
                    else
                    {
                        pending_table.DataSource = dt;
                        pending_table.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtility conn = new DbUtility();

                string studID = hidPersonID.Value;

                conn.updateStudentStatusApproved(studID);
                var StudInfo = conn.getStudEmailInfo(studID);

                string StudEmail = StudInfo.Item1;
                string StudName = StudInfo.Item2;

                conn.emailApprovedAccount(StudEmail, StudName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$('#ApproveSuccessModal').modal('show');", true);
                reBindPendingTable();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void displayPending_Click(object sender, EventArgs e)
        {
            pending_table.Visible = true;
            active_table.Visible = false;
            deactivated_table.Visible = false;
            reBindPendingTable();
            hidStatus.Value = "pending";
        }

        protected void displayActive_Click(object sender, EventArgs e)
        {
            pending_table.Visible = false;
            active_table.Visible = true;
            deactivated_table.Visible = false;
            reBindActiveTable();
            hidStatus.Value = "activated";
        }

        protected void deactiveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtility conn = new DbUtility();

                string studID = hidPersonID.Value;

                conn.updateStudentStatusDeactivate(studID);
                var StudInfo = conn.getStudEmailInfo(studID);

                string StudEmail = StudInfo.Item1;
                string StudName = StudInfo.Item2;

                conn.emailDeactivateAccount(StudEmail, StudName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$('#DeactiveSuccessModal').modal('show');", true);
                reBindActiveTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void displayDeactivated_Click(object sender, EventArgs e)
        {
            pending_table.Visible = false;
            active_table.Visible = false;
            deactivated_table.Visible = true;
            reBindDeactiveTable();
            hidStatus.Value = "deactivated";
        }
    }
}
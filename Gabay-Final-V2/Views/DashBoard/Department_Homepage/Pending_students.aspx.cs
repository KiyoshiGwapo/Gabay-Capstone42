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
            reBindPendingTable();
        }

        private void reBindPendingTable()
        {
            DbUtility conn = new DbUtility();

            int userID = Convert.ToInt32(Session["user_ID"]);
            DataTable dt = conn.displayPendingStudents(userID);

            pending_table.DataSource = dt;
            pending_table.DataBind();
        }

        

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DbUtility conn = new DbUtility();

            string searchCriteria = txtSearch.Text;
            int userID = Convert.ToInt32(Session["user_ID"]);

            DataTable dt = conn.searchStudents(userID, searchCriteria);

            pending_table.DataSource = dt;
            pending_table.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DbUtility conn = new DbUtility();

            string studID = hidPersonID.Value;

            conn.updateStudentStatus(studID);
            var StudInfo = conn.getStudEmailInfo(studID);

            string StudEmail = StudInfo.Item1;
            string StudName = StudInfo.Item2;

            conn.emailApprovedAccount(StudEmail, StudName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$('#successModal').modal('show');", true);
            reBindPendingTable();
            
        }
    }
}
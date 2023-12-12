using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.Modules.Admin_Modules
{
    public partial class Manage_Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void ClearInputs()
        {
            loginID.Text = string.Empty;
            loginPass.Text = string.Empty;
            DeptName.Text = string.Empty;
            deptHead.Text = string.Empty;
            deptDesc.Text = string.Empty;
            deptCN.Text = string.Empty;
            deptEmail.Text = string.Empty;
            deptHours.Text = string.Empty;
        }

        protected void addDeptBtn_Click(object sender, EventArgs e)
        {
            string deptLogin = loginID.Text;
            string deptPass = loginPass.Text;
            string deptName = DeptName.Text;
            string dept_Head = deptHead.Text;
            string dept_Desc = deptDesc.Text;
            string dept_CN = deptCN.Text;
            string dept_Email = deptEmail.Text;
            string dept_Hour = deptHours.Text;

            try
            {
                DbUtility conn = new DbUtility();

                bool existUsername = false;

                conn.addDept(deptName, deptLogin, deptPass, dept_Head, dept_Desc, dept_CN, dept_Email, dept_Hour, existUsername);

                string message = "Department added successfully.";
                ShowModalSuccess(message);
                ClearInputs();
            }
            catch (Exception ex) {

                string message = ex.Message;
                ShowModalError(message);
                ClearInputs();
            }
            
        }

        private void ShowModalSuccess(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSuccessModal",
                $"$('#successMessage').text('{message}'); $('#successModal').modal('show');", true);
        }

        private void ShowModalError(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorModal",
                        $"$('#errorMessage').text('{message}'); $('#errorModal').modal('show');", true);
        }
    }
}
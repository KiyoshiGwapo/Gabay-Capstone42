using Gabay_Final_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.RegistrationPages
{
    public partial class Student_Reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DbUtility conn = new DbUtility();
                conn.ddlDept(departmentChoices);
            }
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            string fullName = name.Text;
            string studAddress = address.Text;
            string contactNumber = contact.Text;
            string studBOD = DOB.Text;
            string studEmail = email.Text;
            string studentNumber = idNumber.Text;
            string Studpassword = password.Text;
            int departmentID = Convert.ToInt32(departmentChoices.SelectedValue);
            string course = courseList.SelectedValue;
            string courseYear = courseYearChoices.SelectedItem.Text;

            DbUtility conn = new DbUtility();

            conn.addStudent(departmentID, fullName, studAddress, contactNumber, studBOD, course, courseYear, studentNumber, Studpassword, studEmail);

            Response.Redirect("Pending_page.aspx");
        }

        protected void departmentChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbUtility conn = new DbUtility();
            string selectedDeptID = departmentChoices.SelectedValue;

            courseList.ClearSelection();

            if(!string.IsNullOrEmpty(selectedDeptID) )
            {
                conn.ddlCourse(courseList, selectedDeptID);
            }
        }
    }
}
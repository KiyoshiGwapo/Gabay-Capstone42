using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.LoginPages
{
    public partial class Guest_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gst_lgnBtn_Click(object sender, EventArgs e)
        {
            string guestName = guestNameBx.Text;

            if(string.IsNullOrEmpty(guestName) )
            {
                errorDiv.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                errorDiv.Attributes["class"] = "alert alert-danger d-none";
                Response.Redirect("..\\DashBoard\\Guest_Homepage\\Guest_Dashboard.aspx");
            }
        }
    }
}
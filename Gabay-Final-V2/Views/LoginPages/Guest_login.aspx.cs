using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.LoginPages
{
	public partial class Guest_login : System.Web.UI.Page
	{
		string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void gst_lgnBtn_Click(object sender, EventArgs e)
		{
			string guestName = guestNameBx.Text;

			if (string.IsNullOrEmpty(guestName))
			{
				errorDiv.Attributes["class"] = "alert alert-danger";
			}
			else
			{
				errorDiv.Attributes["class"] = "alert alert-danger d-none";

				// Set the guest name in a session variable
				Session["GuestName"] = guestName;
				Session["role_ID"] = 4;
				Response.Redirect("..\\DashBoard\\Guest_Homepage\\Guest_Dashboard.aspx");
			}
		}
	}
}
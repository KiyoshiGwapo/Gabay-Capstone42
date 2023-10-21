using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gabay_Final_V2.Models;

namespace Gabay_Final_V2.Views.DashBoard.Guest_Homepage
{
    public partial class Guest_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This is to disable caching for the dashboard to prevent backward login
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            // Database connection
            DbUtility conn = new DbUtility();

            // Set the guestName from the text boxs
            string guestName = guestNameBx.Text;

            if (!string.IsNullOrEmpty(guestName))
            {
                // Fetch the guest's name and display it
                guestNameBx.Text = guestName;
            }
            else
            {
                //Redirects user if login credentials is not valid
                //Response.Redirect("..\\..\\..\\Views\\Loginpages\\Guest_login.aspx");
            }
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Guest_login.aspx");
        }

        protected void logoutLink_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("..\\..\\..\\Views\\Loginpages\\Guest_login.aspx");
        }
    }
}
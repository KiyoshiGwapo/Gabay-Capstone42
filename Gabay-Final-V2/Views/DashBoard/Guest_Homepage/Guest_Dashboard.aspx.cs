using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.DashBoard.Guest_Homepage
{
    public partial class Guest_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (Session["GuestName"] != null)
            {
                GuestName.Text = Session["GuestName"].ToString().ToUpper();
            }
            else
            {
                Response.Redirect("~/Views/Loginpages/Guest_login.aspx");
            }
        }
    }
}
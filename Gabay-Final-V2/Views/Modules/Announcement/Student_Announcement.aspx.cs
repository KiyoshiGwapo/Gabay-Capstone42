using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Gabay_Final_V2.Models;

namespace Gabay_Final_V2.Views.Modules.Announcement
{
    public partial class Student_Announcement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAnnouncements();
            }
        }

        protected void LoadAnnouncements()
        {
            Announcement_model announcementModel = new Announcement_model();
            DataTable dt = announcementModel.GetAnnouncements();

            rptAnnouncements.DataSource = dt;
            rptAnnouncements.DataBind();
        }

        // Event handler for the "Learn More" button click
        protected void ShowAnnouncementDetails(object sender, EventArgs e)
        {
            Button btnLearnMore = (Button)sender;
            int announcementID = Convert.ToInt32(btnLearnMore.CommandArgument);

            Announcement_model announcementModel = new Announcement_model();
            DataTable dt = announcementModel.GetAnnouncementDetails(announcementID);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Find modal controls
                Label modalTitle = (Label)FindControl("modalTitle" + announcementID);
                Image modalImage = (Image)FindControl("modalImage" + announcementID);
                Label modalDate = (Label)FindControl("modalDate" + announcementID);
                Label modalShortDescription = (Label)FindControl("modalShortDescription" + announcementID);
                Label modalDetailedDescription = (Label)FindControl("modalDetailedDescription" + announcementID);

                // Populate the modal with announcement details
                modalTitle.Text = dt.Rows[0]["Title"].ToString();
                modalImage.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
                modalDate.Text = "Date: " + dt.Rows[0]["Date"].ToString();
                modalShortDescription.Text = "Short Description: " + dt.Rows[0]["ShortDescription"].ToString();
                modalDetailedDescription.Text = "Detailed Description: " + dt.Rows[0]["DetailedDescription"].ToString();

                // Show the modal using JavaScript
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal(" + announcementID + ");", true);
            }
        }

    }
}
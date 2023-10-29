using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Views.Modules.Appointment
{
    public partial class Guest_Appointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the minimum date for the date input field
            selectedDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
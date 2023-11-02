using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gabay_Final_V2.Models
{
    public class DepartmentUser
    {
        public static event EventHandler AppointmentStatusChanged;

        public static void OnAppointmentStatusChanged(EventArgs e)
        {
            // Raise the event when the appointment status changes.
            AppointmentStatusChanged?.Invoke(null, e);
        }
    }
}
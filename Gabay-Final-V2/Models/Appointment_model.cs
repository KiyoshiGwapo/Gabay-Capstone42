using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Gabay_Final_V2.Models
{
    public class Appointment_model
    {
        //connection string
        private string connStr = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        internal void uploadFile(string filename, byte[] fileData)
        {
            throw new NotImplementedException();
        }

        //ari i butang ang mga mo handle sa database
    }
}
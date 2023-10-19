using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Gabay_Final_V2.Models
{
    public class DashboardDepartment
    {
        private static string conn = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Gabay_Final_V2.Models
{
    public class AcadCalen_model
    {
        //Database handling sa Academic Calendar need pa ni i fix kay dli mo fetch 
        //pero mo gana ang sa drop down
        //I add pa ang sa admin na side
        string connection = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        
    }
}
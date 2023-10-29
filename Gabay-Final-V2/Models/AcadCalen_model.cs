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
        // Make connectionString private
        private string connectionString = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        public class FileData
        {
            public int FileId { get; set; }
            public string FileName { get; set; }
            public byte[] FileBytes { get; set; }
        }

        public List<FileData> FetchFilesDataFromDatabase()
        {
            List<FileData> filesList = new List<FileData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT FileId, FileName, FileData FROM UploadedFiles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileData file = new FileData
                            {
                                FileId = reader.GetInt32(0),
                                FileName = reader.GetString(1),
                                FileBytes = (byte[])reader["FileData"]
                            };
                            filesList.Add(file);
                        }
                    }
                }
            }

            return filesList;
        }
    }
}
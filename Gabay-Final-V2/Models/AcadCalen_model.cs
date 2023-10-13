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

        public class AcademicFile
        {
            public int FileId { get; set; }
            public string FileName { get; set; }
            public byte[] FileData { get; set; }
        }

        public void uploadFile(string fileName, byte[] fileData)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"INSERT INTO UploadedFiles (FileName, FileData) VALUES (@fileName, @fileData)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@fileData", fileData);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable dt()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = @"SELECT FileId, FileName FROM UploadedFiles";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dataTable);
                conn.Close();
            }
            return dataTable;


        }
        public void updateUpldFile(int fileID, string fileName, byte[] fileData)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"UPDATE UploadedFiles SET FileName = @fileName, FileData = @fileData WHERE FileId = @fileID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Parameters.AddWithValue("@fileData", fileData);
                    cmd.Parameters.AddWithValue("@fileID", fileID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void deleteUpldFile(int fileID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {

                conn.Open();
                string query = "DELETE FROM UploadedFiles WHERE FileId = @fileID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fileID", fileID);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void fetchData(int fileID)
        {
            //string fileName = null;
            //byte[] fileData = null;

            //using (SqlConnection conn = new SqlConnection(connection))
            //{
            //    string query = @"SELECT FileName, FileData FROM UploadedFiles WHERE FileId = @fileID";
            //}
        }



        public List<AcademicFile> FetchFilesDataFromDatabase()
        {
            List<AcademicFile> filesList = new List<AcademicFile>();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT FileId, FileName, FileData FROM UploadedFiles";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AcademicFile file = new AcademicFile
                            {
                                FileId = reader.GetInt32(0),
                                FileName = reader.GetString(1),
                                FileData = (byte[])reader["FileData"]
                            };
                            filesList.Add(file);
                        }
                    }
                }
            }
            return filesList;
        }
        public byte[] FetchFileDataFromDatabase(int fileId)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = "SELECT FileData FROM UploadedFiles WHERE FileId = @FileId";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (byte[])result;
                    }
                }
            }

            return null;
        }
        public string FetchFileNameFromDatabase(int fileId)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string selectQuery = "SELECT FileName FROM UploadedFiles WHERE FileId = @FileId";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }
    }
}
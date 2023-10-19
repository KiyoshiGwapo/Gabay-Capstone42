using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Gabay_Final_V2.Prototype
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        string connection = "Data Source=DESKTOP-6DAE04O\\SQLEXPRESS;Initial Catalog=gabaydb_v.1.8;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSampleData();
        }

        public void LoadSampleData()
        {
            DataTable dataTable = GetSampleData();

            AnnouncementList.DataSource = dataTable;
            AnnouncementList.DataBind();
        }
        public DataTable GetSampleData()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT * FROM example_data";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public void AddData(string Name, byte[] imgFile, string Address)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"INSERT INTO example_data (Name, Address, ImgPath)
                                 VALUES (@Name, @Address, @ImgPath)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    SqlParameter imgParam = new SqlParameter("@ImgPath", SqlDbType.VarBinary);
                    imgParam.Value = imgFile;
                    cmd.Parameters.Add(imgParam);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void SaveAnnouncement_Click(object sender, EventArgs e)
        {
            string addName = addNamebx.Text;
            string addAddress = addAddressbx.Text;

            if (addFilebx.HasFile)
            {
                HttpPostedFile postedFile = addFilebx.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);

                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                AddData(addName, bytes, addAddress);
                LoadSampleData();
            }
        }

        protected void gridviewEdit_Click(object sender, EventArgs e)
        {
            int hiddenID = Convert.ToInt32(HidAnnouncementID.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModal", "$('#toEditModal').modal('show');", true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Gabay_Final_V2.Models
{
    public class Appointment_model
    {
        // Connection string
        private string connStr = ConfigurationManager.ConnectionStrings["Gabaydb"].ConnectionString;

        // methods and properties for handling the database can go here

        public DataTable GetAppointments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                string query = "SELECT ID_appointment, IdNumber, department_ID, full_name, Year, ContactNumber, Email, SelectedDate, SelectedTime, Status FROM Appointments";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetAppointmentsByStatus(string status)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                string query = "SELECT * FROM Appointments WHERE Status = @Status";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool DeleteAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                string query = "DELETE FROM Appointments WHERE ID_appointment = @AppointmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Returns true if at least one row was deleted
                }
            }
        }



        public string GetMessageForAppointment(int appointmentID)
        {
            string message = null;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Message FROM Appointments WHERE ID_appointment = @AppointmentID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            message = reader["Message"].ToString();
                        }
                    }
                }
            }

            return message;
        }
    }
}



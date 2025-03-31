using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Administrator_Functionalities.Models;
using System.Configuration;
using System.Windows.Forms;

namespace Administrator_Functionalities.DataAccessLayer
{
    public class ClassroomDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetRooms()
        {
            List<string> languages = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Room_No FROM Classroom";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    languages.Add(reader["Room_No"].ToString());
                }
                reader.Close();
            }
            return languages;
        }

        public List<Models.Classroom> GetAllRooms()
        {
            List<Models.Classroom> room = new List<Models.Classroom>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Room_No, Capacity FROM Classroom ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        room.Add(new Models.Classroom
                        {
                            roomNo = reader["Room_No"].ToString(),
                            capacity = Convert.ToInt32(reader["Capacity"]),
                        });
                    }
                }
                return room;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Classroom>();
            }
        }

        public bool InsertRoom(Models.Classroom room)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Classroom (Room_No, Capacity) " +
                                   "values (@RoomNo, @Capacity); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@RoomNo", room.roomNo);
                    command.Parameters.AddWithValue("@Capacity", room.capacity);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while inserting the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateRoom(string roomNo, int capacity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Classroom SET Capacity = @capacity " +
                                   "WHERE Room_No = @RoomNo ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@RoomNo", roomNo);
                    command.Parameters.AddWithValue("@capacity", capacity);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while updating the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteRoom(string roomNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Classroom WHERE Room_No = @RoomNo ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@RoomNo", roomNo);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while deleting the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}

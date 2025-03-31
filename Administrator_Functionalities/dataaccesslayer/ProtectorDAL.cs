using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Administrator_Functionalities.DataAccessLayer
{
    public class ProtectorDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Models.Protector> GetAllProtectors()
        {
            List<Models.Protector> prot = new List<Models.Protector>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Student_ID, Protector_Name, Protector_Phone FROM Protector ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        prot.Add(new Models.Protector
                        {
                            studentID = reader["Student_ID"].ToString(),
                            protectorName = reader["Protector_Name"].ToString(),
                            protectorPhone = reader["Protector_Phone"].ToString(),
                        });
                    }
                }
                return prot;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Protector>();
            }
        }

        public bool InsertProtector(Models.Protector prot)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Protector (Student_ID, Protector_Name, Protector_Phone) " +
                                   "values (@StdID, @ProtectorName, @ProtectorPhone); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@StdID", prot.studentID);
                    command.Parameters.AddWithValue("@ProtectorName", prot.protectorName);
                    command.Parameters.AddWithValue("@ProtectorPhone", prot.protectorPhone);

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

        public bool UpdateProtector(string stdID, string protName, string protPhone)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Protector SET Protector_Name = @ProtectorName, Protector_Phone = @ProtectorPhone " +
                                   "WHERE Student_ID = @StdID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@StdID", stdID);
                    command.Parameters.AddWithValue("@ProtectorName", protName);
                    command.Parameters.AddWithValue("@ProtectorPhone", protPhone);

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

        public bool DeleteProtector(string stdID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Protector WHERE Student_ID = @stdID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@stdID", stdID);

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

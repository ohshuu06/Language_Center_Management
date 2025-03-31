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
    public class TeachesDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Models.Teaches> GetAllSchedules()
        {
            List<Models.Teaches> teas = new List<Models.Teaches>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Teacher_ID, Course_ID FROM Teaches ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        teas.Add(new Models.Teaches
                        {
                            teacherID = reader["Teacher_ID"].ToString(),
                            courseID = reader["Course_ID"].ToString(),
                        });
                    }
                }
                return teas;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Teaches>();
            }
        }

        public bool InsertTeaches(Models.Teaches teas)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Teaches (Teacher_ID, Course_ID) " +
                                   "values (@teacherID, @courseID); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@teacherID", teas.teacherID);
                    command.Parameters.AddWithValue("@courseID", teas.courseID);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while inserting the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateTeaches(Models.Teaches teas, string oldTeacherID, string oldCourseID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Delete the old record
                    string deleteQuery = "DELETE FROM Teaches WHERE Course_ID = @oldCourseID AND Teacher_ID = @oldTeacherID ";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@oldCourseID", oldCourseID);
                        deleteCmd.Parameters.AddWithValue("@oldTeacherID", oldTeacherID);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return false;
                        }
                    }

                    // Step 2: Insert the new record with updated values
                    string insertQuery = "INSERT INTO Teaches (Teacher_ID, Course_ID) VALUES (@teacherID, @courseID)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@courseID", teas.courseID);
                        insertCmd.Parameters.AddWithValue("@teacherID", teas.teacherID);
                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return false;
                        }
                    }
                    connection.Close();
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while updating the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteTeaches(string courseID, string teacherID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Teaches WHERE Course_ID = @courseID AND Teacher_ID = @teacherID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@courseID", courseID);
                    command.Parameters.AddWithValue("@teacherID", teacherID);

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

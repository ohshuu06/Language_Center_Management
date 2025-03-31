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
    public class TakesDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetStudents()
        {
            List<string> std = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Student_ID FROM Takes";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    std.Add(reader["Student_ID"].ToString());
                }
                reader.Close();
            }
            return std;
        }

        public List<string> GetCourses()
        {
            List<string> cour = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Course_ID FROM Takes";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cour.Add(reader["Course_ID"].ToString());
                }
                reader.Close();
            }
            return cour;
        }
        public List<Models.Takes> GetAllTakes()
        {
            List<Models.Takes> taks = new List<Models.Takes>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Student_ID, Course_ID FROM Takes ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        taks.Add(new Models.Takes
                        {
                            studentID = reader["Student_ID"].ToString(),
                            courseID = reader["Course_ID"].ToString(),
                        });
                    }
                }
                return taks;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Takes>();
            }
        }

        public bool InsertTakes(Models.Takes taks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Takes (Student_ID, Course_ID) " +
                                   "values (@studentID, @courseID); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@studentID", taks.studentID);
                    command.Parameters.AddWithValue("@courseID", taks.courseID);

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

        public bool UpdateTakes(Models.Takes taks, string oldCourseID, string oldStudentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Delete the old record
                    string deleteQuery = "DELETE FROM Takes WHERE Course_ID = @oldCourseID AND Student_ID = @oldStudentID";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@oldCourseID", oldCourseID);
                        deleteCmd.Parameters.AddWithValue("@oldStudentID", oldStudentID);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return false;
                        }
                    }

                    // Step 2: Insert the new record with updated values
                    string insertQuery = "INSERT INTO Takes (Student_ID, Course_ID) VALUES (@studentID, @courseID)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@studentID", taks.studentID);
                        insertCmd.Parameters.AddWithValue("@courseID", taks.courseID);
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
            catch (Exception)
            {
                MessageBox.Show("An error occurred while updating the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteTakes(string courseID, string studentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Takes WHERE Course_ID = @courseID AND Student_ID = @studentID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@courseID", courseID);
                    command.Parameters.AddWithValue("@studentID", studentID);

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

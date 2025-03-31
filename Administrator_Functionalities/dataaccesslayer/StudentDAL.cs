using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_Functionalities.Models;
using Microsoft.Data.SqlClient;

namespace Administrator_Functionalities.DataAccessLayer
{
    public class StudentDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetStudents()
        {
            List<string> std = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Student_ID FROM Student";
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

        private string GenerateStudentID()
        {
            string newID = "STD1"; // Default for first student
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Student_ID FROM Student ORDER BY Student_ID DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastID = result.ToString(); // Example: "STD003"
                    int lastNumber = int.Parse(lastID.Substring(3)); // Extract the number (003 -> 3)
                    newID = "STD00" + (lastNumber + 1); // Increment and format new ID
                }
            }
            return newID;
        }

        public List<Models.Student> GetAllStudents()
        {
            List<Models.Student> std = new List<Models.Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Student_ID, Student_Name, Student_Phone, Student_Email, Student_DOB, Register_Language FROM Student ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        std.Add(new Models.Student { 
                            studentID = reader["Student_ID"].ToString(),
                            studentName = reader["Student_Name"].ToString(),
                            studentPhone = reader["Student_Phone"].ToString(),
                            studentEmail = reader["Student_Email"].ToString(),
                            studentDOB = Convert.ToDateTime(reader["Student_DOB"]),
                            registeredLanguage = reader["Register_Language"].ToString()
                        });
                    }
                }
                return std;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Student>();
            }
        }

        public List<Models.Student> SearchStudent(string searchTerm)
        {
            List<Models.Student> std = new List<Models.Student>();

            string query = "SELECT * FROM Student WHERE Student_ID LIKE @search OR Student_Name LIKE @search";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    std.Add(new Models.Student
                    {
                        studentID = reader["Student_ID"].ToString(),
                        studentName = reader["Student_Name"].ToString(),
                        studentPhone = reader["Student_Phone"].ToString(),
                        studentEmail = reader["Student_Email"].ToString(),
                        studentDOB = Convert.ToDateTime(reader["Student_DOB"]),
                        registeredLanguage = reader["Register_Language"].ToString()
                    });
                }
            }
            return std;
        }

        public bool InsertStudent(Models.Student std)
        {
            string generateID = GenerateStudentID();
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Student (Student_ID, Student_Name, Student_Phone, Student_Email, Student_DOB, Register_Language) " +
                                   "values (@Student_ID, @Student_Name, @Student_Phone, @Student_Email, @Student_DOB, @Register_Language); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@Student_ID", generateID);
                    command.Parameters.AddWithValue("@Student_Name", std.studentName);
                    command.Parameters.AddWithValue("@Student_Phone", std.studentPhone);
                    command.Parameters.AddWithValue("@Student_Email", std.studentEmail);
                    command.Parameters.AddWithValue("@Student_DOB", std.studentDOB);
                    command.Parameters.AddWithValue("@Register_Language", std.registeredLanguage);

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

        public bool UpdateStudent(Models.Student std, string stdID)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Student SET Student_Name = @Name, Student_Phone = @Phone, " +
                                   "Student_Email = @Email, Register_Language = @Language, Student_DOB = @DOB " +
                                   "WHERE Student_ID = @StudentID";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                   
                    command.Parameters.AddWithValue("@StudentID", stdID);
                    command.Parameters.AddWithValue("@Name", std.studentName);
                    command.Parameters.AddWithValue("@Phone", std.studentPhone);
                    command.Parameters.AddWithValue("@Email", std.studentEmail);
                    command.Parameters.AddWithValue("@DOB", std.studentDOB);
                    command.Parameters.AddWithValue("@Language", std.registeredLanguage);

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

        public bool DeleteStudent(string stdID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Student WHERE Student_ID = @StudentID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@StudentID", stdID);

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

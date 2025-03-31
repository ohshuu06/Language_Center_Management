using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Administrator_Functionalities.Models;
using System.Windows.Forms;

namespace Administrator_Functionalities.DataAccessLayer
{
    public class TeacherDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetTeachers()
        {
            List<string> tea = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Teacher_ID FROM Teacher";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tea.Add(reader["Teacher_ID"].ToString());
                }
                reader.Close();
            }
            return tea;
        }

        private string GenarateTeacherID()
        {
            string newID = "TEA1"; // Default for first student
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Teacher_ID FROM Teacher ORDER BY Teacher_ID DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastID = result.ToString(); // Example: "STD003"
                    int lastNumber = int.Parse(lastID.Substring(3)); // Extract the number (003 -> 3)
                    newID = "TEA00" + (lastNumber + 1); // Increment and format new ID
                }
            }
            return newID;
        }

        public List<Models.Teacher> GetAllTeachers()
        {
            List<Models.Teacher> tea = new List<Models.Teacher>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Teacher_ID, Teacher_Name, Teacher_Phone, Teacher_Email, Qualification_Language FROM Teacher ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        tea.Add(new Models.Teacher
                        {
                            teacherID = reader["Teacher_ID"].ToString(),
                            teacherName = reader["Teacher_Name"].ToString(),
                            teacherPhone = reader["Teacher_Phone"].ToString(),
                            teacherEmail = reader["Teacher_Email"].ToString(),
                            quailificationLanguage = reader["Qualification_Language"].ToString()
                        });
                    }
                }
                return tea;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Teacher>();
            }
        }

        public bool InsertTeacher(Models.Teacher tea)
        {
            string generateID = GenarateTeacherID();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Teacher (Teacher_ID, Teacher_Name, Teacher_Phone, Teacher_Email, Qualification_Language) " +
                                   "values (@TeacherID, @TeacherName, @TeacherPhone, @TeacherEmail, @QualificationLanguage); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@TeacherID", generateID);
                    command.Parameters.AddWithValue("@TeacherName", tea.teacherName);
                    command.Parameters.AddWithValue("@TeacherPhone", tea.teacherPhone);
                    command.Parameters.AddWithValue("@TeacherEmail", tea.teacherEmail);
                    command.Parameters.AddWithValue("@QualificationLanguage", tea.quailificationLanguage);

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

        public bool UpdateTeacher(Models.Teacher tea, string teaID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Teacher SET Teacher_Name = @Name, Teacher_Phone = @Phone, " +
                                   "Teacher_Email = @Email, Qualification_Language = @Language " +
                                   "WHERE Teacher_ID = @TeacherID";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@TeacherID", teaID);
                    command.Parameters.AddWithValue("@Name", tea.teacherName);
                    command.Parameters.AddWithValue("@Phone", tea.teacherPhone);
                    command.Parameters.AddWithValue("@Email", tea.teacherEmail);
                    command.Parameters.AddWithValue("@Language", tea.quailificationLanguage);

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

        public bool DeleteTeacher(string teaID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Teacher WHERE Teacher_ID = @TeacherID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@TeacherID", teaID);

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

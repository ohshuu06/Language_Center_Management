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
    public class CourseDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetCourses()
        {
            List<string> cour = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Course_ID FROM Course";
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

        private string GenerateCourseID()
        {
            string newID = "CS1"; // Default for first student
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Course_ID FROM Course ORDER BY Course_ID DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string lastID = result.ToString(); // Example: "STD003"
                    int lastNumber = int.Parse(lastID.Substring(3)); // Extract the number (003 -> 3)
                    newID = "CS00" + (lastNumber + 1); // Increment and format new ID
                }
            }
            return newID;
        }

        public List<Models.Course> GetAllCourses()
        {
            List<Models.Course> cour = new List<Models.Course>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Course_ID, Course_Name, Language_Name FROM Course ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cour.Add(new Models.Course
                        {
                            courseID = reader["Course_ID"].ToString(),
                            courseName = reader["Course_Name"].ToString(),
                            languageName = reader["Language_Name"].ToString(),
                        });
                    }
                }
                return cour;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Course>();
            }
        }

        public bool InsertCourse(Models.Course cour)
        {
            try
            {
                string generateCourID = GenerateCourseID();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Course (Course_ID, Course_Name, Language_Name) " +
                                   "values (@CourseID, @CourseName, @LanguageName); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@CourseID", generateCourID);
                    command.Parameters.AddWithValue("@CourseName", cour.courseName);
                    command.Parameters.AddWithValue("@LanguageName", cour.languageName);

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

        public bool UpdateCourse(string courID, string courName, string languageName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Course SET Course_Name = @CourseName, Language_Name = @LanguageName " +
                                   "WHERE Course_ID = @CourseID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@CourseID", courID);
                    command.Parameters.AddWithValue("@CourseName", courName);
                    command.Parameters.AddWithValue("@LanguageName", languageName);

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

        public bool DeleteCourse(string courID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Course WHERE Course_ID = @CourseID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@CourseID", courID);

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

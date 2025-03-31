using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_Functionalities.Models;
using Microsoft.Data.SqlClient;

namespace Administrator_Functionalities.DataAccessLayer
{
    public class Course_ScheduleDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataTable GetCourseName()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT cs.Course_ID, c.Course_Name, c.Language_Name FROM Course c, Course_Schedule cs " +
                                   "WHERE c.Course_ID = cs.Course_ID ";
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);    
                    
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR: {ex}");
            }
        }

        public List<string> GetCourses()
        {
            List<string> cour = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Course_ID FROM Course_Schedule";
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

        public List<Models.Course_Schedule> GetAllCourses()
        {
            List<Models.Course_Schedule> cs = new List<Models.Course_Schedule>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Course_ID, Starting_Date, Ending_Date, Room_No FROM Course_Schedule ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cs.Add(new Models.Course_Schedule
                        {
                            courseID = reader["Course_ID"].ToString(),
                            startingDate = Convert.ToDateTime(reader["Starting_Date"]),
                            endingDate = Convert.ToDateTime(reader["Ending_Date"]),
                            roomNo = reader["Room_No"].ToString()
                        });
                    }
                }
                return cs;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Course_Schedule>();
            }
        }

        public bool InsertCourseSchedule(Models.Course_Schedule cs)
        {
            try
            {                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Course_Schedule (Course_ID, Starting_Date, Ending_Date, Room_No) " +
                                   "values (@courseID, @startingDate, @endingDate, @roomNo); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@courseID", cs.courseID);
                    command.Parameters.AddWithValue("@startingDate", cs.startingDate);

                    /*DateTime endingDate = cs.startingDate.AddDays(21);*/
                    command.Parameters.AddWithValue("@endingDate", cs.endingDate);
                    command.Parameters.AddWithValue("@roomNo", cs.roomNo);

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

        public bool UpdateCourseSchedule(string courID, Models.Course_Schedule cs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Course_Schedule SET Starting_Date = @startingDate, Ending_Date = @endindDate, Room_No = @roomNo " +
                                   "WHERE Course_ID = @CourseID ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@CourseID", courID);
                    command.Parameters.AddWithValue("@startingDate", cs.startingDate);
                    command.Parameters.AddWithValue("@endindDate", cs.endingDate);
                    command.Parameters.AddWithValue("@roomNo", cs.roomNo);

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
                MessageBox.Show("An error occurred while updating the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteCourseSchedule(string courID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Course_Schedule WHERE Course_ID = @CourseID ";

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

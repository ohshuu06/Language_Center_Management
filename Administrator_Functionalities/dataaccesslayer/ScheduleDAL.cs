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
    public class ScheduleDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Models.Schedule> GetAllSchedules()
        {
            List<Models.Schedule> scd = new List<Models.Schedule>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Course_ID, DayofWeek, Starting_Time FROM Schedule ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        scd.Add(new Models.Schedule
                        {
                            dayofWeek = reader["DayofWeek"].ToString(),
                            startingTime = Convert.ToDateTime(reader["Starting_Time"]).TimeOfDay,
                            courseID = reader["Course_ID"].ToString(),
                        });
                    }
                }
                return scd;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Schedule>();
            }
        }

        public bool InsertSchedule(Models.Schedule scd)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Schedule (Course_ID, DayofWeek, Starting_Time) " +
                                   "values (@courseID, @dayofWeek, @strTime); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@courseID", scd.courseID);
                    command.Parameters.AddWithValue("@dayofWeek", scd.dayofWeek);
                    command.Parameters.AddWithValue("@strTime", scd.startingTime.ToString(@"hh\:mm"));

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

        public bool UpdateSchedule(Models.Schedule scd, string oldCourseID, string oldDayofWeek, TimeSpan oldStartingTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Delete the old record
                    string deleteQuery = "DELETE FROM Schedule WHERE Course_ID = @oldCourseID AND DayofWeek = @oldDayOfWeek AND Starting_Time = @oldStartTime";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@oldCourseID", oldCourseID);
                        deleteCmd.Parameters.AddWithValue("@oldDayOfWeek", oldDayofWeek);
                        deleteCmd.Parameters.AddWithValue("@oldStartTime", oldStartingTime);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return false;
                        }
                    }

                    // Step 2: Insert the new record with updated values
                    string insertQuery = "INSERT INTO Schedule (Course_ID, DayofWeek, Starting_Time) VALUES (@newCourseID, @newDayOfWeek, @newStartTime)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@newCourseID", scd.courseID);
                        insertCmd.Parameters.AddWithValue("@newDayOfWeek", scd.dayofWeek);
                        insertCmd.Parameters.AddWithValue("@newStartTime", scd.startingTime.ToString(@"hh\:mm"));
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

        public bool DeleteSchedule(string courseID, string dayofWeek, TimeSpan startingTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Schedule WHERE Course_ID = @courseID AND DayofWeek = @dayofWeek AND Starting_Time = @startingTime ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                        
                    command.Parameters.AddWithValue("@courseID", courseID);
                    command.Parameters.AddWithValue("@dayofWeek", dayofWeek);
                    command.Parameters.AddWithValue("@startingTime", startingTime);

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

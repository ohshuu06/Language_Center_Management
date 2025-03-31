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
    public class Schedule_TimeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Models.Schedule_Time> GetAllTimes()
        {
            List<Models.Schedule_Time> st = new List<Models.Schedule_Time>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT DayofWeek, Starting_Time, Ending_Time FROM Schedule_Time ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        st.Add(new Models.Schedule_Time
                        {
                            dayofWeek = reader["DayofWeek"].ToString(),
                            startingTime = Convert.ToDateTime(reader["Starting_Time"]).TimeOfDay,
                            endingTime = Convert.ToDateTime(reader["Ending_Time"]).TimeOfDay,
                        });
                    }
                }
                return st;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Schedule_Time>();
            }
        }

        public bool InsertTime(Models.Schedule_Time st)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Schedule_Time (DayofWeek, Starting_Time, Ending_Time) " +
                                   "values (@dayofWeek, @strTime, @endTime); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@dayofWeek", st.dayofWeek);
                    command.Parameters.AddWithValue("@strTime", st.startingTime.ToString(@"hh\:mm"));

                    /*TimeSpan endingTime = st.startingTime.Add(new TimeSpan(3, 0, 0));*/
                    command.Parameters.AddWithValue("@endTime", st.endingTime.ToString(@"hh\:mm"));

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

        public bool UpdateTime(Models.Schedule_Time st, string oldDayofWeek, TimeSpan oldStartingTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Delete the old record
                    string deleteQuery = "DELETE FROM Schedule_Time WHERE DayofWeek = @oldDayOfWeek AND Starting_Time = @oldStartTime";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@oldDayOfWeek", oldDayofWeek);
                        deleteCmd.Parameters.AddWithValue("@oldStartTime", oldStartingTime);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if(rowsAffected == 0)
                        {
                            return false;
                        }
                    }

                    // Step 2: Insert the new record with updated values
                    string insertQuery = "INSERT INTO Schedule_Time (DayofWeek, Starting_Time, Ending_Time) VALUES (@newDayOfWeek, @newStartTime, @newEndTime)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@newDayOfWeek", st.dayofWeek);
                        insertCmd.Parameters.AddWithValue("@newStartTime", st.startingTime);

                        /*TimeSpan endingTime = st.startingTime.Add(new TimeSpan(3, 0, 0));*/
                        insertCmd.Parameters.AddWithValue("@newEndTime", st.endingTime);
                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if(rowsAffected == 0)
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

        public bool DeleteTime(string dayofWeek, TimeSpan startingTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Schedule_Time WHERE DayofWeek = @dayofWeek AND Starting_Time = @startingTime ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

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

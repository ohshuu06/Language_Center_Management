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
    public class AssignmentDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<string> GetAssignment()
        {
            List<string> ass = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Assignment_Code FROM Assignment";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ass.Add(reader["Assignment_Code"].ToString());
                }
                reader.Close();
            }
            return ass;
        }

        public List<Models.Assignment> GetAllAssignment()
        {
            List<Models.Assignment> ass = new List<Models.Assignment>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Assignment_Code, Description FROM Assignment ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ass.Add(new Models.Assignment
                        {
                            assignmentCode = reader["Assignment_Code"].ToString(),
                            assignmentDescription = reader["Description"].ToString(),
                        });
                    }
                }
                return ass;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Assignment>();
            }
        }

        public bool InsertAssignment(Models.Assignment ass)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Assignment (Assignment_Code, Description) " +
                                   "values (@AssignmentCode, @Description); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@AssignmentCode", ass.assignmentCode);
                    command.Parameters.AddWithValue("@Description", ass.assignmentDescription);

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

        public bool UpdateAssignment(string assCode, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Language SET Description = @description " +
                                   "WHERE Assignment_Code = @assignmentCode ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@assignmentCode", assCode);
                    command.Parameters.AddWithValue("@description", description);

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

        public bool DeleteAssignment(string assCode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Assignment WHERE Assignment_Code = @assignmentCode ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@assignmentCode", assCode);

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

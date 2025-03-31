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
    public class GradeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Models.Grades> GetAllGrades()
        {
            List<Models.Grades> gra = new List<Models.Grades>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Assignment_Code, Student_ID, Course_ID, Assignment_Date, Assignment_Grade FROM Grades ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        gra.Add(new Models.Grades
                        {
                            assignmentCode = reader["Assignment_Code"].ToString(),
                            courseID = reader["Course_ID"].ToString(),
                            studentID = reader["Student_ID"].ToString(),
                            assignmentDate = Convert.ToDateTime(reader["Assignment_Date"]),
                            grade = Convert.ToDecimal(reader["Assignment_Grade"])
                        });
                    }
                }
                return gra;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while fetching the record. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Models.Grades>();
            }
        }

        public bool InsertGrade(Models.Grades gra)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "insert into Grades (Student_ID, Course_ID, Assignment_Code, Assignment_Date, Assignment_Grade) " +
                                   "values (@studentID, @courseID, @assignmentCode, @assignmentDate, @assignmentGrade); ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@studentID", gra.studentID);
                    command.Parameters.AddWithValue("@courseID", gra.courseID);
                    command.Parameters.AddWithValue("@assignmentCode", gra.assignmentCode);
                    command.Parameters.AddWithValue("@assignmentDate", gra.assignmentDate);
                    command.Parameters.AddWithValue("@assignmentGrade", gra.grade);

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

        public bool UpdateGrade(Models.Grades gra, string oldStudentID, string oldCourseID, string oldAssignmentCode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Delete the old record
                    string deleteQuery = "DELETE FROM Grades WHERE Student_ID = @oldStdID AND Course_ID = @oldCourID AND Assignment_Code = @oldAssCode";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@oldStdID", oldStudentID);
                        deleteCmd.Parameters.AddWithValue("@oldCourID", oldCourseID);
                        deleteCmd.Parameters.AddWithValue("@oldAssCode", oldAssignmentCode);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            return false;
                        }
                    }

                    // Step 2: Insert the new record with updated values
                    string insertQuery = "INSERT INTO Grades (Student_ID, Course_ID, Assignment_Code, Assignment_Date, Assignment_Grade) VALUES (@studentID, @courseID, @assignmentCode, @assignmentDate, @assignmentGrade)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@studentID", gra.studentID);
                        insertCmd.Parameters.AddWithValue("@courseID", gra.courseID);
                        insertCmd.Parameters.AddWithValue("@assignmentCode", gra.assignmentCode);
                        insertCmd.Parameters.AddWithValue("@assignmentDate", gra.assignmentDate);
                        insertCmd.Parameters.AddWithValue("@assignmentGrade", gra.grade);

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

        public bool DeleteGrade(string studentID, string courseID, string assignmentCode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Grades WHERE Student_ID = @stdID AND Course_ID = @courID AND Assignment_Code = @assCode ";

                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@stdID", studentID);
                    command.Parameters.AddWithValue("@courID", courseID);
                    command.Parameters.AddWithValue("@assCode", assignmentCode);

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

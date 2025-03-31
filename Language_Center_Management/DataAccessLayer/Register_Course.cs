using Language_Center_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Language_Center_Management.DataAccessLayer
{
    public class Register_Course
    {
        private string connectionString = "data source=LAPTOP-OHSHUU;initial catalog=Language_Center_Management2;integrated security=True;";

        public Student ValidateUser(string username, string password)
        {
            Student student = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Đổi tên cột thành Student_Username và Student_Password
                string query = "SELECT * FROM Student WHERE Student_Username = @Username AND Student_Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        Student_ID = reader["Student_ID"].ToString(),
                        Student_Name = reader["Student_Name"].ToString(),
                        Student_Email = reader["Student_Email"].ToString()
                    };
                }
                con.Close();
            }
            return student;
        }
        public Teacher ValidateTeacher(string username, string password)
        {
            Teacher teacher = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Teacher WHERE Teacher_Username = @Username AND Teacher_Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    teacher = new Teacher
                    {
                        Teacher_ID = reader["Teacher_ID"].ToString(),
                        Teacher_Name = reader["Teacher_Name"].ToString(),
                        Teacher_Email = reader["Teacher_Email"].ToString()
                    };
                }
                con.Close();
            }
            return teacher;
        }
        public Protector ValidateProtector(string phone, string studentId)
        {
            Protector protector = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Protector WHERE Protector_Phone = @Phone AND Student_ID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    protector = new Protector
                    {
                        Student_ID = reader["Student_ID"].ToString(),
                        Protector_Name = reader["Protector_Name"].ToString(),
                        Protector_Phone = reader["Protector_Phone"].ToString()
                    };
                }
                con.Close();
            }
            return protector;
        }

        public bool RegisterCourse(string studentId, string courseId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Kiểm tra xem sinh viên đã đăng ký khóa học chưa
                string checkQuery = "SELECT COUNT(*) FROM Takes WHERE Student_ID = @StudentID AND Course_ID = @CourseID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@StudentID", studentId);
                checkCmd.Parameters.AddWithValue("@CourseID", courseId);

                con.Open();
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return false; // Đã đăng ký
                }

                // Thêm mới vào bảng Takes
                string insertQuery = "INSERT INTO Takes (Student_ID, Course_ID) VALUES (@StudentID, @CourseID)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@StudentID", studentId);
                insertCmd.Parameters.AddWithValue("@CourseID", courseId);

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0; // Trả về true nếu thêm thành công
            }
        }
        public List<CourseSchedule> GetRegisteredCourses(string studentId)
        {
            List<CourseSchedule> registeredCourses = new List<CourseSchedule>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT cs.Course_ID, cs.Starting_Date, cs.Ending_Date, cs.Room_No
                    FROM Course_Schedule cs
                    JOIN Takes t ON cs.Course_ID = t.Course_ID
                    WHERE t.Student_ID = @StudentID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    registeredCourses.Add(new CourseSchedule
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Starting_Date = Convert.ToDateTime(reader["Starting_Date"]),
                        Ending_Date = Convert.ToDateTime(reader["Ending_Date"]),
                        Room_No = reader["Room_No"].ToString()
                    });
                }
            }
            return registeredCourses;
        }

        public bool HasCompletedPrerequisites(string studentId, string courseId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT p.Preq_ID
                FROM Prerequisites p
                WHERE p.Course_ID = @CourseID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> prerequisiteCourses = new List<string>();

                while (reader.Read())
                {
                    prerequisiteCourses.Add(reader["Preq_ID"].ToString());
                }
                con.Close();

                // Kiểm tra xem sinh viên đã hoàn thành tất cả các môn tiên quyết chưa
                foreach (string preqId in prerequisiteCourses)
                {
                    string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Takes
                    WHERE Student_ID = @StudentID AND Course_ID = @PreqID";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@StudentID", studentId);
                    checkCmd.Parameters.AddWithValue("@PreqID", preqId);
                    con.Open();

                    int count = (int)checkCmd.ExecuteScalar();
                    con.Close();

                    if (count == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public List<Classroom> GetClassrooms()
        {
            List<Classroom> classrooms = new List<Classroom>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Classroom";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classrooms.Add(new Classroom
                    {
                        Room_No = reader["Room_No"].ToString(),
                        Capacity = (int)reader["Classroom_Name"]
                    });
                }
                con.Close();
            }
            return classrooms;
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Course";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Course_Name = reader["Course_Name"].ToString()
                    });
                }
                con.Close();
            }
            return courses;
        }
              

        public List<CourseSchedule> GetCourseSchedules()
        {
            List<CourseSchedule> schedules = new List<CourseSchedule>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"select cs.Course_ID,c.Course_Name,c.Language_Name,cs.Starting_Date, cs.Ending_Date
                                from Course_Schedule cs
                                join Course c on c.Course_ID=cs.Course_ID ";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    schedules.Add(new CourseSchedule
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Course_name = reader["Course_name"].ToString(),
                        Language_Name = reader["Language_Name"].ToString(),
                        Starting_Date = Convert.ToDateTime(reader["Starting_Date"]),
                        Ending_Date = Convert.ToDateTime(reader["Ending_Date"]),
                    });
                }
                con.Close();
            }
            return schedules;
        }
       
        public List<CourseSchedule> GetRegisteredCourseSchedules(string studentId)
        {
            List<CourseSchedule> schedules = new List<CourseSchedule>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT cs.Course_ID, cs.Starting_Date, cs.Ending_Date, st.DayOfWeek, st.Starting_Time, st.Ending_Time, cs.Room_No
                FROM Course_Schedule cs
                JOIN Course c ON cs.Course_ID = c.Course_ID
                JOIN Takes t ON cs.Course_ID = t.Course_ID
                JOIN Schedule s ON cs.Course_ID = s.Course_ID
                JOIN Schedule_Time st ON s.DayofWeek = st.DayOfWeek AND s.Starting_Time = st.Starting_Time
                WHERE t.Student_ID = @StudentID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    schedules.Add(new CourseSchedule
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Starting_Date = Convert.ToDateTime(reader["Starting_Date"]),
                        Ending_Date = Convert.ToDateTime(reader["Ending_Date"]),
                        DayOfWeek = reader["DayOfWeek"].ToString(),
                        Starting_Time = reader["Starting_Time"].ToString(),
                        Ending_Time = reader["Ending_Time"].ToString(),
                        Room_No = reader["Room_No"].ToString()
                    });
                }
                con.Close();
            }
            return schedules;
        }

        public List<CourseSchedule> GetTeachingSchedule(string teacherId)
        {
            List<CourseSchedule> schedules = new List<CourseSchedule>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT cs.Course_ID, cs.Starting_Date, cs.Ending_Date, st.DayOfWeek, st.Starting_Time, st.Ending_Time, cs.Room_No
                FROM Course_Schedule cs
                JOIN Course c ON cs.Course_ID = c.Course_ID
                JOIN Teaches t ON cs.Course_ID = t.Course_ID
                JOIN Schedule s ON cs.Course_ID = s.Course_ID
                JOIN Schedule_Time st ON s.DayofWeek = st.DayOfWeek AND s.Starting_Time = st.Starting_Time
                WHERE t.Teacher_ID = @TeacherID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherID", teacherId);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        schedules.Add(new CourseSchedule
                        {
                            Course_ID = reader["Course_ID"].ToString(),
                            Starting_Date = Convert.ToDateTime(reader["Starting_Date"]),
                            Ending_Date = Convert.ToDateTime(reader["Ending_Date"]),
                            DayOfWeek = reader["DayOfWeek"].ToString(),
                            Starting_Time = reader["Starting_Time"].ToString(),
                            Ending_Time = reader["Ending_Time"].ToString(),
                            Room_No = reader["Room_No"].ToString()
                        });
                    }
                }
            }

            return schedules;
        }
        public List<Grade> GetAllGradesByStudent(string studentId)
        {
            List<Grade> grades = new List<Grade>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                                SELECT g.Course_ID,  g.Assignment_Code, g.Assignment_Date, g.Assignment_Grade
                                FROM Grades g
                                INNER JOIN Assignment a ON g.Assignment_Code = a.Assignment_Code
                                INNER JOIN Takes t ON g.Course_ID = t.Course_ID AND g.Student_ID = t.Student_ID
                                INNER JOIN Course c ON g.Course_ID = c.Course_ID
                                WHERE g.Student_ID = @StudentID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Grade grade = new Grade
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Assignment_Code = reader["Assignment_Code"].ToString(),
                        Assignment_Date = Convert.ToDateTime(reader["Assignment_Date"]),
                        Assignment_Grade = Convert.ToDecimal(reader["Assignment_Grade"])
                    };
                    grades.Add(grade);
                }
                con.Close();
            }
            return grades;
        }
        public List<Grade> GetAllGradesByTeacher(string teacherId)
        {
            List<Grade> grades = new List<Grade>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                        SELECT DISTINCT g.Course_ID, g.Student_ID, g.Assignment_Code, g.Assignment_Date, g.Assignment_Grade 
                        FROM Grades g
                        JOIN Teaches t ON t.Course_ID = g.Course_ID
                        JOIN Student s ON g.Student_ID = s.Student_ID
                        WHERE t.Teacher_ID = @TeacherID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Grade grade = new Grade
                    {
                        Course_ID = reader["Course_ID"].ToString(),
                        Student_ID = reader["Student_ID"].ToString(),
                        Assignment_Code = reader["Assignment_Code"].ToString(),
                        Assignment_Date = Convert.ToDateTime(reader["Assignment_Date"]),
                        Assignment_Grade = Convert.ToDecimal(reader["Assignment_Grade"])
                    };
                    grades.Add(grade);
                }
                con.Close();
            }
            return grades;
        }

    }
}
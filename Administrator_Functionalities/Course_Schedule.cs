using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_Functionalities.DataAccessLayer;

namespace Administrator_Functionalities
{
    public partial class Course_Schedule : Form
    {
        private Course_ScheduleDAL csDAL;
        private CourseDAL courDAL;
        private ClassroomDAL roomDAL;
        public Course_Schedule()
        {
            InitializeComponent();
            /*dateTimePicker2.Enabled = false;*/

            csDAL = new Course_ScheduleDAL();
            courDAL = new CourseDAL();
            roomDAL = new ClassroomDAL();

            PopulateComboBox();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void PopulateComboBox()
        {
            List<string> cour = courDAL.GetCourses();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(cour.ToArray());

            List<string> room = roomDAL.GetRooms();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(room.ToArray());
        }

        private void Course_Schedule_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = csDAL.GetAllCourses();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedItem = row.Cells["courseID"].Value.ToString();
                comboBox2.SelectedItem = row.Cells["roomNo"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["startingDate"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["endingDate"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Models.Course_Schedule newCS = new Models.Course_Schedule
            {
                courseID = comboBox1.SelectedItem.ToString(),
                roomNo = comboBox2.SelectedItem.ToString(),
                startingDate = dateTimePicker1.Value,
                endingDate = dateTimePicker2.Value,
            };

            var result = csDAL.InsertCourseSchedule(newCS);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = csDAL.GetAllCourses();
            }
            else
            {
                MessageBox.Show("Error inserting new schedule for a course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Models.Course_Schedule newCS = new Models.Course_Schedule
            {
                roomNo = comboBox2.SelectedItem.ToString(),
                startingDate = dateTimePicker1.Value,
                endingDate = dateTimePicker2.Value,
            };

            var result = csDAL.UpdateCourseSchedule(comboBox1.SelectedItem.ToString(), newCS);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = csDAL.GetAllCourses();
            }
            else
            {
                MessageBox.Show("Error updating schedule for a course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = csDAL.DeleteCourseSchedule(comboBox1.SelectedItem.ToString());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = csDAL.GetAllCourses();
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Error deleting schedule for a course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

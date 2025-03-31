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
    public partial class Teaches : Form
    {
        private TeachesDAL teasDAL;
        private TeacherDAL teaDAL;
        private Course_ScheduleDAL csDAL;

        public Teaches()
        {
            InitializeComponent();
            teasDAL =new TeachesDAL();
            teaDAL  = new TeacherDAL();
            csDAL =  new Course_ScheduleDAL();

            PopulateComboBox();
            comboBox1.SelectedIndex = 0;
        }

        private bool ValidateInput(string courseID)
        {
            if (string.IsNullOrWhiteSpace(courseID))
            {
                MessageBox.Show("Course cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PopulateComboBox()
        {
            List<string> lang = teaDAL.GetTeachers();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(lang.ToArray());
        }

        private void Teaches_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = teasDAL.GetAllSchedules();
            dataGridView2.DataSource = csDAL.GetCourseName();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Course_ID"].Value.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedItem = row.Cells["teacherID"].Value.ToString();              
                textBox1.Text = row.Cells["courseID"].Value.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text.Trim()))
            {
                return;
            }

            Models.Teaches newTeaches = new Models.Teaches
            {
                teacherID = comboBox1.SelectedItem.ToString(),
                courseID = textBox1.Text.ToString(),
            };

            var result = teasDAL.InsertTeaches(newTeaches);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teasDAL.GetAllSchedules();
            }
            else
            {
                MessageBox.Show("Error inserting new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text.Trim()))
            {
                return;
            }
            string oldTeacherID = dataGridView1.SelectedRows[0].Cells["teacherID"].Value.ToString();
            string oldCourseID = dataGridView1.SelectedRows[0].Cells["courseID"].Value.ToString();

            Models.Teaches newTeaches = new Models.Teaches
            {
                teacherID = comboBox1.SelectedItem.ToString(),
                courseID = textBox1.Text.ToString()
            };

            var result = teasDAL.UpdateTeaches(newTeaches, oldTeacherID, oldCourseID);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teasDAL.GetAllSchedules();
            }
            else
            {
                MessageBox.Show("Error updating new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please specify a course", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = teasDAL.DeleteTeaches(textBox1.Text.ToString(), comboBox1.SelectedItem.ToString());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teasDAL.GetAllSchedules();
            }
            else
            {
                MessageBox.Show("Error deleting new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

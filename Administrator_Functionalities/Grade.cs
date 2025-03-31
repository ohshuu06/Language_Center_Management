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
    public partial class Grade : Form
    {
        private AssignmentDAL assDAL;
        private StudentDAL stdDAL;
        private Course_ScheduleDAL csDAL;
        private GradeDAL graDAL;
        private TakesDAL takDAL;

        public Grade()
        {
            InitializeComponent();
            assDAL = new AssignmentDAL();
            stdDAL = new StudentDAL();
            csDAL = new Course_ScheduleDAL();
            graDAL = new GradeDAL();
            takDAL = new TakesDAL();

            PopulateCombobox();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private bool ValidateInput(decimal? grade)
        {            
            if (!grade.HasValue)
            {
                numericUpDown1.Value = 0;
                return true;
            }
            return true;
        }

        private void PopulateCombobox()
        {
            List<string> ass = assDAL.GetAssignment();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ass.ToArray());

            List<string> std = takDAL.GetStudents();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(std.ToArray());

            List<string> cour = takDAL.GetCourses();
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(cour.ToArray());
        }

        private void Grade_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = graDAL.GetAllGrades();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedItem = row.Cells["assignmentCode"].Value.ToString();
                comboBox2.SelectedItem = row.Cells["studentID"].Value.ToString();
                comboBox3.SelectedItem = row.Cells["courseID"].Value.ToString();                
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["assignmentDate"].Value);
                numericUpDown1.Value = Convert.ToDecimal(row.Cells["grade"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(numericUpDown1.Value))
            {
                return;
            }

            Models.Grades newGrade = new Models.Grades
            {
                assignmentCode = comboBox1.SelectedItem.ToString(),
                studentID = comboBox2.SelectedItem.ToString(),
                courseID = comboBox3.SelectedItem.ToString(),
                assignmentDate = dateTimePicker1.Value,
                grade = numericUpDown1.Value
            };

            var result = graDAL.InsertGrade(newGrade);

            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = graDAL.GetAllGrades();
            }
            else
            {
                MessageBox.Show("Error inserting new grade.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(numericUpDown1.Value))
            {
                return;
            }
            string oldCourseID = dataGridView1.SelectedRows[0].Cells["courseID"].Value.ToString();
            string oldStudentID = dataGridView1.SelectedRows[0].Cells["studentID"].Value.ToString();
            string oldAssignmentCode = dataGridView1.SelectedRows[0].Cells["assignmentCode"].Value.ToString();

            Models.Grades newGrade = new Models.Grades
            {
                assignmentCode = comboBox1.SelectedItem.ToString(),
                studentID = comboBox2.SelectedItem.ToString(),
                courseID = comboBox3.SelectedItem.ToString(),
                assignmentDate = dateTimePicker1.Value,
                grade = numericUpDown1.Value
            };

            var result = graDAL.UpdateGrade(newGrade, oldStudentID, oldCourseID, oldAssignmentCode);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = graDAL.GetAllGrades();
            }
            else
            {
                MessageBox.Show("Error updating a new grade.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = graDAL.DeleteGrade(comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), comboBox1.SelectedItem.ToString());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = graDAL.GetAllGrades();
            }
            else
            {
                MessageBox.Show("Error deleting a grade.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

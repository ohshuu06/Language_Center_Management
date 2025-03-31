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
    public partial class Takes : Form
    {
        private StudentDAL stdDAL;
        private CourseDAL courDAL;
        private TakesDAL takDAL;

        public Takes()
        {
            InitializeComponent();
            stdDAL = new StudentDAL();
            courDAL = new CourseDAL();
            takDAL =new TakesDAL();

            PopulateComboBox();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void PopulateComboBox()
        {
            List<string> std = stdDAL.GetStudents();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(std.ToArray());

            List<string> cour = courDAL.GetCourses();
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(cour.ToArray());
        }

        private void Takes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = takDAL.GetAllTakes();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
       
                comboBox2.SelectedItem = row.Cells["studentID"].Value.ToString();
                comboBox3.SelectedItem = row.Cells["courseID"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Models.Takes newTakes = new Models.Takes
            {
                studentID = comboBox2.SelectedItem.ToString(),
                courseID = comboBox3.SelectedItem.ToString(),
            };

            var result = takDAL.InsertTakes(newTakes);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = takDAL.GetAllTakes();
            }
            else
            {
                MessageBox.Show("Error inserting new takes for student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string oldCourseID = dataGridView1.SelectedRows[0].Cells["courseID"].Value.ToString();
            string oldStudentID = dataGridView1.SelectedRows[0].Cells["studentID"].Value.ToString();

            Models.Takes newTakes = new Models.Takes
            {
                studentID = comboBox2.SelectedItem.ToString(),
                courseID = comboBox3.SelectedItem.ToString(),
            };

            var result = takDAL.UpdateTakes(newTakes, oldCourseID, oldStudentID);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = takDAL.GetAllTakes();
            }
            else
            {
                MessageBox.Show("Error updating a new take for student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = takDAL.DeleteTakes(comboBox3.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = takDAL.GetAllTakes();
            }
            else
            {
                MessageBox.Show("Error deleting a take of student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

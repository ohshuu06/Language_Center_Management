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
    public partial class Course : Form
    {
        private CourseDAL courDAL;
        private LanguageDAL langDAL;
        public Course()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            courDAL = new CourseDAL();
            langDAL = new LanguageDAL();
            PopulateCombobox();
            comboBox1.SelectedIndex = 0;
        }

        private bool ValidateInput(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Course name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PopulateCombobox()
        {
            List<string> lang = langDAL.GetLanguages();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(lang.ToArray());
        }

        private void Course_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = courDAL.GetAllCourses();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["courseID"].Value.ToString();
                textBox2.Text = row.Cells["courseName"].Value.ToString();         
                comboBox1.SelectedItem = row.Cells["languageName"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox2.Text.Trim()))
            {
                return;
            }

            Models.Course newCourse = new Models.Course
            {
                courseID = textBox1.Text,
                courseName = textBox2.Text,
                languageName = comboBox1.SelectedItem.ToString()
            };

            var result = courDAL.InsertCourse(newCourse);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = courDAL.GetAllCourses();
            }
            else
            {
                MessageBox.Show("Error inserting course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox2.Text.Trim()))
            {
                return;
            }

            var result = courDAL.UpdateCourse(textBox1.Text.Trim(), textBox2.Text.Trim(), comboBox1.SelectedItem.ToString());
            if (result)
            {
                MessageBox.Show("Update Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = courDAL.GetAllCourses();
            }
            else
            {
                MessageBox.Show("Error updating course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox2.Text.Trim()))
            {
                return;
            }

            var result = courDAL.DeleteCourse(textBox1.Text.Trim());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = courDAL.GetAllCourses();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

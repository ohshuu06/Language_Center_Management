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
    public partial class Teacher : Form
    {
        private TeacherDAL teaDAL;
        private LanguageDAL langDAL;

        public Teacher()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            teaDAL = new TeacherDAL();
            langDAL = new LanguageDAL();
            PopulateComboBox();
            comboBox1.SelectedIndex = 0;
        }

        private bool ValidateInput(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Teacher name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Phone cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (phone.Length != 10)
            {
                MessageBox.Show("Phone number must be exactly 10 digits!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PopulateComboBox()
        {
            List<string> lang = langDAL.GetLanguages();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(lang.ToArray());
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = teaDAL.GetAllTeachers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Models.Teacher newTeacher = new Models.Teacher
            {
                teacherName = textBox2.Text,
                teacherPhone = textBox3.Text,
                teacherEmail = textBox4.Text,
                quailificationLanguage = comboBox1.SelectedItem.ToString()
            };

            if (!ValidateInput(textBox2.Text, textBox3.Text, textBox4.Text))
            {
                return;
            }

            var result = teaDAL.InsertTeacher(newTeacher);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teaDAL.GetAllTeachers();
            }
            else
            {
                MessageBox.Show("Error inserting teacher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string teaID = textBox1.Text;
            if (!ValidateInput(textBox2.Text, textBox3.Text, textBox4.Text))
            {
                return;
            }

            Models.Teacher newTeacher = new Models.Teacher
            {
                teacherName = textBox2.Text,
                teacherPhone = textBox3.Text,
                teacherEmail = textBox4.Text,               
                quailificationLanguage = comboBox1.SelectedItem.ToString()
            };

            var result = teaDAL.UpdateTeacher(newTeacher, teaID);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teaDAL.GetAllTeachers();
            }
            else
            {
                MessageBox.Show("Error updating teacher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a teacher to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string teaID = textBox1.Text;

            var result = teaDAL.DeleteTeacher(teaID);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = teaDAL.GetAllTeachers();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting teacher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["teacherID"].Value.ToString();
                textBox2.Text = row.Cells["teacherName"].Value.ToString();
                textBox3.Text = row.Cells["teacherPhone"].Value.ToString();
                textBox4.Text = row.Cells["teacherEmail"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["quailificationLanguage"].Value.ToString();             
            }
        }
    }
}

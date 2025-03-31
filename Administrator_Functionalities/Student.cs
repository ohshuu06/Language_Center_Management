using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Administrator_Functionalities.DataAccessLayer;
using Administrator_Functionalities.Models;

namespace Administrator_Functionalities
{
    public partial class Student : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private StudentDAL stdDAL;
        private LanguageDAL LangDAL;
        public Student()
        {
            InitializeComponent();  
            textBox1.Enabled = false;
            stdDAL = new StudentDAL();
            LangDAL = new LanguageDAL();
            PopulateComboBox();
            comboBox1.SelectedIndex = 0;
        }
        private bool ValidateInput(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Student name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(phone.Length != 10)
            {
                MessageBox.Show("Phone number must be exactly 10 digits!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void PopulateComboBox()
        {
            List<string> lang= LangDAL.GetLanguages();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(lang.ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = stdDAL.GetAllStudents();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Models.Student newStudent = new Models.Student
            {
                studentName = textBox2.Text,
                studentPhone = textBox3.Text,
                studentEmail = textBox4.Text,
                studentDOB = dateTimePicker1.Value, // DateTimePicker selected date
                registeredLanguage = comboBox1.SelectedItem.ToString()
            };

            if(!ValidateInput(textBox2.Text, textBox3.Text))
            {
                return;
            }

            var result = stdDAL.InsertStudent(newStudent);

            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stdDAL.GetAllStudents();
            }
            else
            {
                MessageBox.Show("Error inserting student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string studentID = textBox1.Text;
            Models.Student newStudent = new Models.Student
            {
                studentName = textBox2.Text,
                studentPhone = textBox3.Text,
                studentEmail = textBox4.Text,
                studentDOB = dateTimePicker1.Value, // DateTimePicker selected date
                registeredLanguage = comboBox1.SelectedItem.ToString()
            };

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a student to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = stdDAL.UpdateStudent(newStudent,studentID);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stdDAL.GetAllStudents();
            }
            else
            {
                MessageBox.Show("Error updating student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {             
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["studentID"].Value.ToString();
                textBox2.Text = row.Cells["studentName"].Value.ToString();
                textBox3.Text = row.Cells["studentPhone"].Value.ToString();
                textBox4.Text = row.Cells["studentEmail"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["registeredLanguage"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["studentDOB"].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stdID = textBox1.Text;

            var result = stdDAL.DeleteStudent(stdID);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stdDAL.GetAllStudents();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error delete student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
    }
}

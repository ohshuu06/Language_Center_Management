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
    public partial class Protector : Form
    {
        private ProtectorDAL protDAL;
        private StudentDAL stdDAL;
        public Protector()
        {
            InitializeComponent();
            protDAL = new ProtectorDAL();
            stdDAL = new StudentDAL();
        }

        private bool ValidateInput(string name, string phone, string stdID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Protector name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Protector phone number cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(stdID))
            {
                MessageBox.Show("Student ID cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (phone.Length != 10)
            {
                MessageBox.Show("Phone number must be exactly 10 digits!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void Protector_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = protDAL.GetAllProtectors();
            dataGridView2.DataSource = stdDAL.GetAllStudents();

            dataGridView2.Columns["studentPhone"].Visible = false;
            dataGridView2.Columns["studentEmail"].Visible = false;
            dataGridView2.Columns["studentDOB"].Visible = false;
            dataGridView2.Columns["registeredLanguage"].Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = stdDAL.SearchStudent(textBox3.Text);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox3.Text = row.Cells["studentID"].Value.ToString();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["protectorName"].Value.ToString();
                textBox2.Text = row.Cells["protectorPhone"].Value.ToString();
                textBox3.Text = row.Cells["studentID"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!ValidateInput(textBox1.Text, textBox2.Text, textBox3.Text))
            {
                return;
            }

            Models.Protector newProtector = new Models.Protector
            {
                protectorName = textBox1.Text,
                protectorPhone = textBox2.Text,
                studentID = textBox3.Text
            };

            var result = protDAL.InsertProtector(newProtector);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = protDAL.GetAllProtectors();
            }
            else
            {
                MessageBox.Show("Error inserting new protector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text, textBox2.Text, textBox3.Text))
            {
                return;
            }

            var result = protDAL.UpdateProtector(textBox3.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim());
            if (result)
            {
                MessageBox.Show("Update Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = protDAL.GetAllProtectors();
            }
            else
            {
                MessageBox.Show("Error updating a protector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                MessageBox.Show("Student ID cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = protDAL.DeleteProtector(textBox3.Text.Trim());
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = protDAL.GetAllProtectors();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting a protector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

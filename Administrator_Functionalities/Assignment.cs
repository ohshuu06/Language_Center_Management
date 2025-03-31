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
    public partial class Assignment : Form
    {
        private AssignmentDAL assDAL;
        public Assignment()
        {
            InitializeComponent();
            assDAL = new AssignmentDAL();
        }

        private bool ValidateInput(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Assignment code cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(description))
            {
                richTextBox1.Text = "No description given";
                return true;
            }
            return true;
        }

        private void Assignment_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = assDAL.GetAllAssignment();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["assignmentCode"].Value.ToString();
                richTextBox1.Text = row.Cells["assignmentDescription"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text, richTextBox1.Text))
            {
                return;
            }

            Models.Assignment newAssignment = new Models.Assignment
            {
                assignmentCode = textBox1.Text,
                assignmentDescription = richTextBox1.Text,
            };

            var result = assDAL.InsertAssignment(newAssignment);

            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = assDAL.GetAllAssignment();
            }
            else
            {
                MessageBox.Show("Error inserting assignment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text, richTextBox1.Text))
            {
                return;
            }

            var result = assDAL.UpdateAssignment(textBox1.Text, richTextBox1.Text.Trim());
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = assDAL.GetAllAssignment();
            }
            else
            {
                MessageBox.Show("Error updating a assignment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a assignment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string assCode = textBox1.Text;

            var result = assDAL.DeleteAssignment(assCode);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = assDAL.GetAllAssignment();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting an assignment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
    }
}

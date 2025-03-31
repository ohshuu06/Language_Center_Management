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
    public partial class Language : Form
    {
        private LanguageDAL langDAL;

        public Language()
        {
            InitializeComponent();
            langDAL = new LanguageDAL();
        }

        private bool ValidateInput(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Language name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(description))
            {
                richTextBox1.Text = "No description given";
                return true;
            }
            return true;
        }

        private void Language_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = langDAL.GetAllLanguages();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["languageName"].Value.ToString();
                richTextBox1.Text = row.Cells["description"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text, richTextBox1.Text))
            {
                return;
            }

            Models.Language newLanguage = new Models.Language
            {
                languageName = textBox1.Text,
                description = richTextBox1.Text,
            };           

            var result = langDAL.InsertLanguage(newLanguage);

            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = langDAL.GetAllLanguages();
            }
            else
            {
                MessageBox.Show("Error inserting language.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!ValidateInput(textBox1.Text, richTextBox1.Text))
            {
                return;
            }

            var result = langDAL.UpdateLanguage(textBox1.Text, richTextBox1.Text.Trim());
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = langDAL.GetAllLanguages();
            }
            else
            {
                MessageBox.Show("Error updating language.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a language to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stdID = textBox1.Text;

            var result = langDAL.DeleteStudent(stdID);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = langDAL.GetAllLanguages();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting language.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
    }
}

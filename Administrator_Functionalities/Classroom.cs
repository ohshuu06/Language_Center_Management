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
    public partial class Classroom : Form
    {
        private ClassroomDAL roomDAL;
        public Classroom()
        {
            InitializeComponent();
            roomDAL = new ClassroomDAL();
        }

        private bool ValidateInput(string roomNo, int? capacity)
        {
            if (string.IsNullOrWhiteSpace(roomNo))
            {
                MessageBox.Show("Room number cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!capacity.HasValue)
            {
                numericUpDown1.Value = 0;
                return true;
            }
            return true;
        }

        private void Classroom_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = roomDAL.GetAllRooms();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["roomNo"].Value.ToString();
                numericUpDown1.Text = row.Cells["capacity"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (!ValidateInput(textBox1.Text, (int)numericUpDown1.Value))
            {
                return;
            }

            Models.Classroom newRoom = new Models.Classroom
            {
                roomNo = textBox1.Text,
                capacity = (int)numericUpDown1.Value,
            };

            var result = roomDAL.InsertRoom(newRoom);

            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = roomDAL.GetAllRooms();
            }
            else
            {
                MessageBox.Show("Error inserting room.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text, (int)numericUpDown1.Value))
            {
                return;
            }

            var result = roomDAL.UpdateRoom(textBox1.Text, (int)numericUpDown1.Value);

            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = roomDAL.GetAllRooms();
            }
            else
            {
                MessageBox.Show("Error updating room.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a room to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var result = roomDAL.DeleteRoom(textBox1.Text);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = roomDAL.GetAllRooms();
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Error deleting room.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
    }
}

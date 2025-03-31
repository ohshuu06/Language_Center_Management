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
    public partial class Schedule_Time : Form
    {
        private Schedule_TimeDAL stDAL;
        public Schedule_Time()
        {
            InitializeComponent();
            /*dateTimePicker2.Enabled =false;*/
            stDAL = new Schedule_TimeDAL();

            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true; 

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;

            comboBox1.Items.AddRange(new string[]
            {
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
            });
            comboBox1.SelectedIndex = 0;

        }

        private void Schedule_Time_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = stDAL.GetAllTimes();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedItem = row.Cells["dayofWeek"].Value.ToString();
                dateTimePicker1.Value = DateTime.Today.Add((TimeSpan)row.Cells["startingTime"].Value);
                dateTimePicker2.Value = DateTime.Today.Add((TimeSpan)row.Cells["endingTime"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Models.Schedule_Time newTime = new Models.Schedule_Time
            {
                dayofWeek = comboBox1.SelectedItem.ToString(),
                startingTime = dateTimePicker1.Value.TimeOfDay,
                endingTime= dateTimePicker2.Value.TimeOfDay,
            };

            var result = stDAL.InsertTime(newTime);
            if (result)
            {
                MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stDAL.GetAllTimes();
            }
            else
            {
                MessageBox.Show("Error inserting new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimeSpan oldStartTime = (TimeSpan)dataGridView1.SelectedRows[0].Cells["startingTime"].Value;

            Models.Schedule_Time newTime = new Models.Schedule_Time
            {
                dayofWeek = comboBox1.SelectedItem.ToString(),
                startingTime = dateTimePicker1.Value.TimeOfDay,
                endingTime = dateTimePicker2.Value.TimeOfDay,
            };

            var result = stDAL.UpdateTime(newTime, comboBox1.SelectedItem.ToString(), oldStartTime);
            if (result)
            {
                MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stDAL.GetAllTimes();
            }
            else
            {
                MessageBox.Show("Error updating a new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = stDAL.DeleteTime(comboBox1.SelectedItem.ToString(), dateTimePicker1.Value.TimeOfDay);
            if (result)
            {
                MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = stDAL.GetAllTimes();
            }
            else
            {
                MessageBox.Show("Error deleting a schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

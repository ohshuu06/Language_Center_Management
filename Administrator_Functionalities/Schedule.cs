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
    public partial class Schedule : Form
    {
        private ScheduleDAL scdDAL;
        private Course_ScheduleDAL csDAL;
        private Schedule_TimeDAL scDAL;

        public Schedule()
        {
            InitializeComponent();
            scdDAL = new ScheduleDAL();
            csDAL = new Course_ScheduleDAL();
            scDAL = new Schedule_TimeDAL();

            PopulateComboBox();
            comboBox1.SelectedIndex = 0;         
        }

        private bool ValidateInput(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                MessageBox.Show("Schedule cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }        
            return true;
        }

        private void PopulateComboBox()
        {
            List<string> lang = csDAL.GetCourses();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(lang.ToArray());
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = scdDAL.GetAllSchedules();
            dataGridView2.DataSource = scDAL.GetAllTimes();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                string dayofweek = row.Cells["dayofWeek"].Value.ToString();
                TimeSpan startingTime = (TimeSpan)row.Cells["startingTime"].Value;

                textBox1.Text = $"{dayofweek} AT {startingTime}";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedItem = row.Cells["courseID"].Value.ToString();
                string dayofweek = row.Cells["dayofWeek"].Value.ToString();
                TimeSpan startingTime = (TimeSpan)row.Cells["startingTime"].Value;
                textBox1.Text = $"{dayofweek} AT {startingTime}";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text.Trim()))
            {
                return;
            }

            string scheduleText = textBox1.Text;
            string[] parts = scheduleText.Split(new string[] { " AT " }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string dayOfWeek = parts[0].Trim(); // "Friday"
                TimeSpan startingTime = Convert.ToDateTime(parts[1]).TimeOfDay; // "09:30:00"

                Models.Schedule newSchedule = new Models.Schedule
                {
                    dayofWeek = dayOfWeek,
                    startingTime = startingTime,
                    courseID = comboBox1.SelectedItem.ToString(),
                };

                var result = scdDAL.InsertSchedule(newSchedule);
                if (result)
                {
                    MessageBox.Show("Insert Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = scdDAL.GetAllSchedules();
                }
                else
                {
                    MessageBox.Show("Error inserting new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(textBox1.Text.Trim()))
            {
                return;
            }
            TimeSpan oldStartTime = (TimeSpan)dataGridView1.SelectedRows[0].Cells["startingTime"].Value;
            string oldDayofWeek = dataGridView1.SelectedRows[0].Cells["dayofWeek"].Value.ToString();

            string scheduleText = textBox1.Text;
            string[] parts = scheduleText.Split(new string[] { " AT " }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string dayOfWeek = parts[0].Trim(); // "Friday"
                TimeSpan startingTime = Convert.ToDateTime(parts[1]).TimeOfDay; // "09:30:00"

                Models.Schedule newSchedule = new Models.Schedule
                {
                    dayofWeek = dayOfWeek,
                    startingTime = startingTime,
                    courseID = comboBox1.SelectedItem.ToString(),
                };

                var result = scdDAL.UpdateSchedule(newSchedule, comboBox1.SelectedItem.ToString(), oldDayofWeek, oldStartTime );
                if (result)
                {
                    MessageBox.Show("Update Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = scdDAL.GetAllSchedules();
                }
                else
                {
                    MessageBox.Show("Error updating new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please specify a day to delete", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string scheduleText = textBox1.Text;
            string[] parts = scheduleText.Split(new string[] { " AT " }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                string dayOfWeek = parts[0].Trim(); // "Friday"
                TimeSpan startingTime = Convert.ToDateTime(parts[1]).TimeOfDay; // "09:30:00"

                Models.Schedule newSchedule = new Models.Schedule
                {
                    dayofWeek = dayOfWeek,
                    startingTime = startingTime,
                    courseID = comboBox1.SelectedItem.ToString(),
                };

                var result = scdDAL.DeleteSchedule(comboBox1.SelectedItem.ToString(), dayOfWeek, startingTime);
                if (result)
                {
                    MessageBox.Show("Delete Successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = scdDAL.GetAllSchedules();
                }
                else
                {
                    MessageBox.Show("Error deleting new schedule.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

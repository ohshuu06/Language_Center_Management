using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administrator_Functionalities
{
    public partial class Login : Form
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminUsername = textBox1.Text.Trim();
            string adminPassword = textBox2.Text.Trim();

            if(adminUsername == AdminUsername && adminPassword == AdminPassword)
            {
                MessageBox.Show("Welcome, Administrator");
                Menu menuForm = new Menu();
                menuForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username and password is incorrect");
            }
        }
    }
}

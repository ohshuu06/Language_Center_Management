using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administrator_Functionalities
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            LoadFormNames();
        }

        private void LoadFormNames()
        {
            var formTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Form)))
                .Select(t => t.Name)
                .ToList();

            comboBox1.DataSource = formTypes;
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedFormName = comboBox1.SelectedItem.ToString();
                OpenForm(selectedFormName);
            }
        }
        private void OpenForm(string formName)
        {
            Type formType = Assembly.GetExecutingAssembly().GetType("Administrator_Functionalities." + formName);
            if (formType != null)
            {
                Form formInstance = (Form)Activator.CreateInstance(formType);
                formInstance.Show();
            }
            else
            {
                MessageBox.Show("Form not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

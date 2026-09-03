using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtuserName.Text =="admin" && txtPassword.Text =="admin")
            {
                Main mn = new Main();
                mn.Show();
            }
            else if (txtuserName.Text == "admin" || txtPassword.Text == "1234");
                    MessageBox.Show("Invalid user name or password");

        }
    }
}

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
    public partial class Main : Form
    {
        public DataAccess Da { get; set; }
        
        private Form AdminloginForm;

        private Doctor docForm;
        private Nurse nurForm;
        private Patient patForm;
        private Appointment apnForm;
        public Main()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (patForm == null || patForm.IsDisposed)
                patForm = new Patient(this);

            patForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nurForm == null || nurForm.IsDisposed)
                nurForm = new Nurse(this);
            nurForm.Show();
            this.Hide();

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (docForm == null || docForm.IsDisposed)
                docForm = new Doctor(this);

            docForm.Show();
            this.Hide();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnApn_Click(object sender, EventArgs e)
        {
            if (apnForm == null || apnForm.IsDisposed)
                apnForm = new Appointment(this);

            apnForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Adminlogin AdminloginForm = new Adminlogin();
            AdminloginForm.Show();
            this.Hide();
        }
        private void btnMain_Click(object sender, EventArgs e)
        {
            Adminlogin AdminloginForm = new Adminlogin();
            AdminloginForm.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Appointment : Form
    {
        public DataAccess Da { get; set; }
        string apnID = null;

        private Form mainForm;
        public Appointment(Form Main)
        {
            try
            {
                InitializeComponent();
                this.Da = new DataAccess();
                this.mainForm = Main;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Patient form: " + ex.Message);
            }
        }
        private void PopulateGridView(string sql = "SELECT *  FROM [Hospital].[dbo].[Appointment];")
        {
            try
            {
                var ds = this.Da.ExecuteQuery(sql);

                this.dgview.AutoGenerateColumns = false;
                this.dgview.DataSource = ds.Tables[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }

        }

        public Appointment()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Nurse_Load(object sender, EventArgs e)
        {

        }

        private void Appoinment_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.Show();
            this.Hide();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!IsValidToInsert())
                    return;
                var aid = this.txtAID.Text;
                var ptname = this.txtPatientName.Text;
                var dcname = this.txtDoctorName.Text;
                var appointdate = this.txtAppointDate.Text;
                var status = this.txtStatus.Text;

                var sql = "INSERT INTO [Hospital].[dbo].[Appointment] (AId, PatientName , DoctorName, AppointDate, Status) " +
                          "VALUES (" + txtAID + ", '" + PatientName + "', " + DoctorName + ", '" + AppointDate + "', '" + Status + "');";
                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Inserted Successfully.");
                   
                    PopulateGridView();
                }
                else
                {
                    MessageBox.Show("Data Insertion Failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Insert button: " + ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                    if (!IsValidToUpdate(apnID)) // Check if the input is valid
                    return; // If not valid, return early\


                var aid = this.txtAID.Text;
                var ptname = this.txtPatientName.Text;
                var dcname = this.txtDoctorName.Text;
                var appointdate = this.txtAppointDate.Text;
                var status = this.txtStatus.Text;

                if (string.IsNullOrEmpty(aid))
                {
                    MessageBox.Show("Please select a record to Update.");
                    return;
                }
                var sql = "UPDATE [Hospital].[dbo].[Appointment] SET " +
                          "PatientName='" + ptname + "', " +
                          "DoctorName='" + dcname + "', " +
                          "AppointDate='" + appointdate + "', " +
                          "Status='" + status + "' " +
                          "WHERE AID=" + aid + ";";

                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Updated Successfully.");
                    
                    PopulateGridView();
                    txtAID = null;
                }
                else
                {
                    MessageBox.Show("Data Updation Failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Update button: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(apnID))
                {
                    MessageBox.Show("Please select a record to delete.");
                    return;
                }

                var title = this.dgview.CurrentRow.Cells["colName"].Value.ToString();
                DialogResult res = MessageBox.Show("Are you sure to remove " + title + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return;
                var sql = "DELETE FROM [Hospital].[dbo].[Appointment] WHERE Id=" + apnID + ";";

                var count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show("Data Deleted Successfully.");
                   // ClearAll();
                    PopulateGridView();
                    apnID = null;
                }
                else
                {
                    MessageBox.Show("Data Deletion Failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Delete button: " + ex.Message);
            }
        }

        private bool IsValidToUpdate(string id)
        {
            // Example validation: check if id is not null or empty
            return !string.IsNullOrEmpty(id);
        }
    }
}

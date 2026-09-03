using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class Patient : Form
    {
        public DataAccess Da { get; set; }
        string patientID = null;

        private Form mainForm;

        public Patient(Form Main)
        {
            try
            {
                InitializeComponent();
                this.Da = new DataAccess();
                PopulateGridView();
                this.mainForm = Main;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Patient form: " + ex.Message);
            }
        }
        private void PopulateGridView(string sql = "SELECT * FROM [Hospital].[dbo].[Patient];")
        {
            try
            {
                var ds = this.Da.ExecuteQuery(sql);

                this.dgView.AutoGenerateColumns = false;
                this.dgView.DataSource = ds.Tables[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToInsert()) 
                    return; 

                var name = this.txtPatientName.Text;
                var age = this.txtAge.Text;
                var gender = this.txtGender.Text;
                var address = this.txtAddress.Text;
                var patientID = this.txtPatientID.Text;

                var sql = "INSERT INTO [Hospital].[dbo].[Patient] (Id, Name, Age, Gender, Address) " +
                          "VALUES (" + patientID + ", '" + name + "', " + age + ", '" + gender + "', '" + address + "');";
                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Inserted Successfully.");
                    ClearAll();
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


        private bool IsValidToInsert()
        {
            if (String.IsNullOrEmpty(this.txtPatientName.Text) ||
                String.IsNullOrEmpty(this.txtAge.Text) ||
                String.IsNullOrEmpty(this.txtGender.Text) ||
                String.IsNullOrEmpty(this.txtAddress.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return false;
            }

           

            if (!int.TryParse(txtPatientID.Text, out int number))
            {
                MessageBox.Show("Please enter a valid 4-digit Patient ID.");
                return false;
            }

            return true; // Return true if all validations pass
        }

        private bool IsValidToUpdate()
        {
            if (String.IsNullOrEmpty(this.txtPatientName.Text) ||
                String.IsNullOrEmpty(this.txtAge.Text) ||
                String.IsNullOrEmpty(this.txtGender.Text) ||
                String.IsNullOrEmpty(this.txtAddress.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return false;
            }
           
            return true; // Return true if all validations pass
        }

        private void ClearAll()
        {
            this.txtPatientName.Clear();
            this.txtAge.Clear();
            this.txtGender.Clear();
            this.txtAddress.Clear();
            this.txtPatientID.Clear();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Patient_Load(object sender, EventArgs e)
        {

        }

        

        private void btnPatient_Click(object sender, EventArgs e)
        {
            Patient patientForm = new Patient(this); 
            patientForm.Show();
        }

        private void Patient_Load_1(object sender, EventArgs e)
        {



        }

        private void dgView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgView.CurrentRow == null) return;
            
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToUpdate()) // Check if the input is valid
                    return; // If not valid, return early\


                var name = this.txtPatientName.Text;
                var age = this.txtAge.Text;
                var gender = this.txtGender.Text;
                var address = this.txtAddress.Text;
                var patientID = this.txtPatientID.Text;
                //var doctorID = this.txtDoctorID.Text;

                if (string.IsNullOrEmpty(patientID))
                {
                    MessageBox.Show("Please select a record to Update.");
                    return;
                }
                var sql = "UPDATE [Hospital].[dbo].[Doctor] SET Name='" + name + "',Age='" + age + "', Gender='" +gender + "', Address='" + address + "' PatientId=" + patientID + ";";

                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Updated Successfully.");
                    ClearAll();
                    PopulateGridView();
                    patientID = null;
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
                if (string.IsNullOrEmpty(patientID))
                {
                    MessageBox.Show("Please select a record to delete.");
                    return;
                }

                var title = this.dgView.CurrentRow.Cells["Name"].Value.ToString();
                DialogResult res = MessageBox.Show("Are you sure to remove " + title + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return;
                var sql = "DELETE FROM [Hospital].[dbo].[Patient] WHERE Id=" + patientID + ";";

                var count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show("Data Deleted Successfully.");
                    ClearAll();
                    patientID = null;
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

        

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Hide();
        }
    }
}       

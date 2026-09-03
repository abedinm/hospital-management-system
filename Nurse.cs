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
    public partial class Nurse : Form
    {
        private DataAccess Da { get; set; }

        string NurseID = null;
        private Form mainForm;
        public Nurse(Form Main)
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
                MessageBox.Show("Error in Doctor form: " + ex.Message);
            }
        }
        private void PopulateGridView(string sql = "SELECT *  FROM [Hospital].[dbo].[Nurse];")
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
        private void txtDoctorID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Hide();
        }

        private void Nurse_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // if (!IsValidToInsert()) // Check if the input is valid
                return; // If not valid, return early

                var id = this.txtNurseID.Text;
                var name = this.txtNurseName.Text;
                var phone = this.txtPhone.Text;
                var department = this.txtDepartment.Text;


                var sql = "INSERT INTO [Hospital].[dbo].[Nurse] (ID,Name,Phone,Department) " +
                    "VALUES (" + id + ", '" + name + "','" + phone + "', '" + department + "');";
                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Inserted Successfully.");

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
            if (String.IsNullOrEmpty(this.txtNurseName.Text) ||
                String.IsNullOrEmpty(this.txtPhone.Text) ||
                String.IsNullOrEmpty(this.txtDepartment.Text) ||
                String.IsNullOrEmpty(this.txtNurseID.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return false;
            }

            if (!long.TryParse(this.txtPhone.Text, out long n) || this.txtPhone.Text.Length != 11)
            {
                MessageBox.Show("Please enter a valid 11-digit phone number.");
                return false;
            }

            if (!int.TryParse(txtNurseID.Text, out int number))
            {
                MessageBox.Show("Please enter a valid 4-digit Doctor ID.");
                return false;
            }

            return true; // Return true if all validations pass
        }
        private bool IsValidToUpdate()
        {
            if (String.IsNullOrEmpty(this.txtNurseName.Text) ||
               String.IsNullOrEmpty(this.txtNurseID.Text) ||
               String.IsNullOrEmpty(this.txtPhone.Text) ||
               String.IsNullOrEmpty(this.txtDepartment.Text))
                String.IsNullOrEmpty(this.txtDepartment.Text);
            {
                MessageBox.Show("Please fill all the fields.");
                return false;
            }
            if (!long.TryParse(this.txtPhone.Text, out long n) || this.txtPhone.Text.Length != 11)
            {
                MessageBox.Show("Please enter a valid 11-digit phone number.");
                return false;
            }
            return true; // Return true if all validations pass
        }
        private void ClearAll()
        {
            this.txtNurseID.Clear();
            this.txtNurseName.Clear();
            this.txtPhone.Clear();
            this.txtDepartment.Clear();
            this.txtNurseID.Clear();
        }
        private void dgview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            this.txtNurseID.Text = this.dgview.CurrentRow.Cells["ID"].Value.ToString();
            this.txtNurseName.Text = this.dgview.CurrentRow.Cells["Name"].Value.ToString();
            this.txtPhone.Text = this.dgview.CurrentRow.Cells["Phone"].Value.ToString();
            this.txtDepartment.Text = this.dgview.CurrentRow.Cells["Department"].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToUpdate()) // Check if the input is valid
                    return; // If not valid, return early\


                var id = this.txtNurseID.Text;
                var name = this.txtNurseName.Text;
                var phone = this.txtPhone.Text;
                var department = this.txtDepartment.Text;
                //var doctorID = this.txtDoctorID.Text;

                if (string.IsNullOrEmpty(NurseID))
                {
                    MessageBox.Show("Please select a record to Update.");
                    return;
                }
                var sql = "UPDATE [Hospital].[dbo].[Nurse] SET ID='" + id + "', Name='" + name + "', Phone='" + phone + "', Department='" + department + "' WHERE Id=" + NurseID + ";";

                var count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data Updated Successfully.");
                    ClearAll();
                    PopulateGridView();
                    NurseID = null;
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
                if (string.IsNullOrEmpty(NurseID))
                {
                    MessageBox.Show("Please select a record to delete.");
                    return;
                }

                var title = this.dgview.CurrentRow.Cells["Name"].Value.ToString();
                DialogResult res = MessageBox.Show("Are you sure to remove " + title + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No)
                    return;
                var sql = "DELETE FROM [Hospital].[dbo].[Nurse] WHERE Id=" + NurseID + ";";

                var count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show("Data Deleted Successfully.");
                    ClearAll();
                    PopulateGridView();
                    NurseID = null;
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
        private void Nurse_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Show();
            this.Hide();
        }
    }
}
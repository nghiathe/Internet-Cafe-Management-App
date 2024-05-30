using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquannet.Model
{
    public partial class ComSelect : Form
    {
        public ComSelect()
        {
            InitializeComponent();
            LoadComboBox(cboZone, "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True", "Select ZoneName From Zone");
            LoadComboBox(cboStaff, "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True", "Select LastName From Employee");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadComboBox(ComboBox comboBox, string connectionString, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox.Items.Add(reader[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        private void cboCom_Click(object sender, EventArgs e)
        {
            cboCom.Items.Clear();
            LoadComboBox(cboCom, "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True", "Select ComputerName From Computer as c inner join Zone as z on c.ZoneID = z.ZoneID where ZoneName = N'" + cboZone.Text + "'");
        }

        private void btnConfirmAddFood_Click(object sender, EventArgs e)
        {
            
            
        }

        private int GetComputerID(string ComputerName)
        {
            int ComputerID;
            string connectionString = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select ComputerID From Computer as c inner join UsageSession as us on c.ComputerID = us.ComputerID where ComputerName= N'"+ComputerName+"'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    ComputerID = Convert.ToInt32(result);

                }
            }
            return ComputerID;
        }
        private int GetBllingID(int ComputerID)
        {
            int BillingID;
            string connectionString = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select BillingID from UsageSession where ComputerID = @ComID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@ComID", ComputerID);
                    object result = cmd.ExecuteScalar();
                    BillingID = Convert.ToInt32(result);

                }
            }
            return BillingID;
        }

    }
}

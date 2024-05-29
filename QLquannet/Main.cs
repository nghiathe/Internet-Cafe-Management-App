using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquannet
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //string imagePath = "C:\\Users\\theng\\Downloads\\imgbtl\\banhmiruoc.jpg"; // Đường dẫn tới tệp ảnh
            //byte[] imageBytes = ImageToByteArray(imagePath);

            //SaveImageToDatabase("Bánh mì ruốc", 10000, 6000, 200, 3, imageBytes);

            Console.WriteLine("Image saved to database successfully.");
        }
        public void loadform(object Form)
        {
            if(this.pnlMain.Controls.Count > 0)
            {
                this.pnlMain.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(f);
            this.pnlMain.Tag = f;
            f.Show();
        }

        private void btnComputer_Click(object sender, EventArgs e)
        {
            btnComputer.BackColor = Color.Gray;
            btnFood.BackColor = Color.FromArgb(24, 30, 54);
            LoadFormCon(new frmComputer());
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            loadform(new Food());
            btnFood.BackColor = Color.Gray;
            btnComputer.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void bthLogout_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            btnBill.BackColor = Color.Gray;
            btnComputer.BackColor = Color.FromArgb(24, 30, 54);
            btnFood.BackColor = Color.FromArgb(24, 30, 54);
            LoadFormCon(new frmHoaDon());
        }
        private Form curentChildForm;
        private void LoadFormCon (Form childForm)
        {
            if(curentChildForm != null)
            {
                curentChildForm.Close();
            }
            curentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public static byte[] ImageToByteArray(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg); 
                    return ms.ToArray();
                }
            }
        }
        public static void SaveImageToDatabase(string productName, decimal productPrice, decimal productIntakePrice, int Inventory, int CategoryID,  byte[] imageBytes)
        {
            string connectionString = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Food (FoodName, Price, IntakePrice, Inventory, CategoryID, Image) VALUES (@Name, @Price, @IntakePrice, @Inventory, @CategoryID, @Image)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
               
                    cmd.Parameters.AddWithValue("@Name", productName);
                    cmd.Parameters.AddWithValue("@Price", productPrice);
                    cmd.Parameters.AddWithValue("@IntakePrice", productIntakePrice);
                    cmd.Parameters.AddWithValue("@Inventory", Inventory);
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@Image", imageBytes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;


namespace QLquannet
{
    public partial class Food : Form
    {

        public Food()
        {
            InitializeComponent();

        }
        private void Food_Load(object sender, EventArgs e)
        {
            AddCategory();
            ProductPanel.Controls.Clear();
            LoadProduct();
        }
        private void AddCategory()
        {
            string Strconn = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";
            SqlConnection conn = new SqlConnection(Strconn);
            string qry = "Select CategoryName from Category";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Button b = new Button();
                    b.BackColor = Color.White;
                    b.Size = new Size(90, 50);
                    b.Text = row["CategoryName"].ToString();

                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b =(Button)sender;
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItem(string id, String name, String cat, String price, Image pimage)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(id)
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["ID"].Value) == wdg.id)
                    {
                        item.Cells["Qty"].Value = int.Parse(item.Cells["Qty"].Value.ToString()) + 1;
                        item.Cells["Amount"].Value = int.Parse(item.Cells["Qty"].Value.ToString()) * double.Parse(item.Cells["Price"].Value.ToString());
                        return;
                    }

                }
                dataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                Gettotal();
            };
        }

        private void LoadProduct()
        {
            string Strconn = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";
            SqlConnection conn = new SqlConnection(Strconn);
            string qry = "Select * from  Food as f inner join Category as c on f.CategoryID = c.CategoryID";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["Image"];
                byte[] imagebytearray = imagearray;

                AddItem(item["FoodID"].ToString(), item["FoodName"].ToString(), item["CategoryName"].ToString(), item["Price"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void txtSearchFood_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearchFood.Text.Trim().ToLower());
            }
        }

        private void txtSearchFood_Click(object sender, EventArgs e)
        {
            txtSearchFood.SelectAll();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                count++;
                r.Cells[0].Value = count;
            }
        }

        
        private void Gettotal()
        {
            double total = 0;
            lbTongtien.Text = "";

            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells["Amount"].Value == null)
                {
                    continue; // Bỏ qua nếu ô không có giá trị
                }
                total += double.Parse(r.Cells["Amount"].Value.ToString());
                
            }

            lbTongtien.Text = total.ToString("N2");
        }



    }
}

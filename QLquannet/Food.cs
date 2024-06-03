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
using DTO;


namespace QLquannet
{
    public partial class Food : Form
    {
        private FoodDAL foodDAL;

        public Food()
        {
            InitializeComponent();
            foodDAL = new FoodDAL();
        }

        private void Food_Load(object sender, EventArgs e)
        {
            AddCategory();
            ProductPanel.Controls.Clear();
            LoadProduct();
            LoadComboBox(cboZone, "Select ZoneName From Zone");
        }

        private void AddCategory()
        {
            DataTable dt = foodDAL.GetCategories();
            foreach (DataRow row in dt.Rows)
            {
                Button b = new Button
                {
                    BackColor = Color.White,
                    Size = new Size(90, 50),
                    Text = row["CategoryName"].ToString()
                };

                b.Click += new EventHandler(b_Click);
                CategoryPanel.Controls.Add(b);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (ucProduct pro in ProductPanel.Controls)
            {
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItem(FoodDTO food)
        {
            var w = new ucProduct()
            {
                PName = food.FoodName,
                PPrice = food.Price.ToString(),
                PCategory = food.CategoryName,
                PImage = Image.FromStream(new MemoryStream(food.Image)),
                id = food.FoodID
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
            };
        }

        private void LoadProduct()
        {
            DataTable dt = foodDAL.GetAllFoods();
            foreach (DataRow item in dt.Rows)
            {
                FoodDTO food = new FoodDTO
                {
                    FoodID = Convert.ToInt32(item["FoodID"]),
                    FoodName = item["FoodName"].ToString(),
                    CategoryName = item["CategoryName"].ToString(),
                    Price = Convert.ToDecimal(item["Price"]),
                    Image = (byte[])item["Image"]
                };
                AddItem(food);
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

        private void GetTotal()
        {
            double total = 0;
            lbTongtien.Text = "";

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells["Amount"].Value == null)
                {
                    continue;
                }
                total += double.Parse(r.Cells["Amount"].Value.ToString());
            }

            lbTongtien.Text = total.ToString("N2");
        }

        private decimal TinhTongTien(DataGridView datagridView)
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in datagridView.Rows)
            {
                if (row.IsNewRow) continue;
                var cellValue = row.Cells["Amount"].Value;

                if (decimal.TryParse(cellValue.ToString(), out decimal amount))
                {
                    totalAmount += amount;
                }
            }

            return totalAmount;
        }

        private void UpdateTongTien(DataGridView datagridView, Label lnTongtien)
        {
            decimal totalAmount = TinhTongTien(datagridView);
            lnTongtien.Text = totalAmount.ToString("N2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FoodModel.AddFood AF = new FoodModel.AddFood();
            AF.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FoodModel.EditFood EF = new FoodModel.EditFood();
            EF.Show();
        }

        private void cboZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCom.Text = "";
        }

        private void LoadComboBox(ComboBox comboBox, string query)
        {
            DataTable dt = foodDAL.LoadComboBoxData(query);
            foreach (DataRow row in dt.Rows)
            {
                comboBox.Items.Add(row[0].ToString());
            }
        }

        private void cboCom_Click(object sender, EventArgs e)
        {
            cboCom.Items.Clear();
            LoadComboBox(cboCom, $"Select ComputerName From Computer as c inner join Zone as z on c.ZoneID = z.ZoneID where ZoneName = N'{cboZone.Text}'");
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Model.AddCategory AC = new Model.AddCategory();
            AC.Show();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTongTien(dataGridView1, lbTongtien);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            lbTongtien.Text = "0.00";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int computerID = foodDAL.GetComputerID(cboCom.Text);
            int billingID = foodDAL.GetBillingID(computerID);

            if (cboCom.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Bạn chưa chọn máy!");
            }
            else if (billingID == -1)
            {
                MessageBox.Show("Máy chưa được bật!");
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int foodID = Convert.ToInt32(row.Cells["ID"].Value);
                        int count = Convert.ToInt32(row.Cells["Qty"].Value);
                        decimal cost = Convert.ToDecimal(row.Cells["Amount"].Value);
                        foodDAL.SaveFoodDetails(billingID, foodID, count, cost);
                    }

                    MessageBox.Show("Dữ liệu đã được lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}


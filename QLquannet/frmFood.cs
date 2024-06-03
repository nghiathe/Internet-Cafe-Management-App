using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace QLquannet
{
    public partial class frmFood : Form
    {

        public frmFood()
        {
            InitializeComponent();
        }

        private void Food_Load(object sender, EventArgs e)
        {
            LoadCategories();
            ProductPanel.Controls.Clear();
            LoadMenu();
            //LoadProduct();
            LoadComboBox(cboZone, "Select ZoneName From Zone");
        }



        //private void AddItem(DTO.Food food)
        //{
        //    var w = new ucProduct()
        //    {
        //        PName = food.FoodName,
        //        PPrice = food.Price.ToString(),
        //        PCategory = food.CategoryName,
        //        PImage = Image.FromStream(new MemoryStream(food.Image)),
        //        id = food.FoodID
        //    };

        //    ProductPanel.Controls.Add(w);

        //    w.onSelect += (ss, ee) =>
        //    {
        //        var wdg = (ucProduct)ss;

        //        foreach (DataGridViewRow item in dgvFoodList.Rows)
        //        {
        //            if (Convert.ToInt32(item.Cells["ID"].Value) == wdg.id)
        //            {
        //                item.Cells["Qty"].Value = int.Parse(item.Cells["Qty"].Value.ToString()) + 1;
        //                item.Cells["Amount"].Value = decimal.Parse(item.Cells["Qty"].Value.ToString()) * decimal.Parse(item.Cells["Price"].Value.ToString());
        //                return;
        //            }
        //        }
        //        dgvFoodList.Rows.Add(new object[] { 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
        //    };
        //}

        //private void LoadProduct()
        //{
        //    DataTable dt = FoodDAL.Instance.GetAllFoods();
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        DTO.Food food = new DTO.Food
        //        {
        //            FoodID = Convert.ToInt32(item["FoodID"]),
        //            FoodName = item["FoodName"].ToString(),
        //            CategoryName = item["CategoryName"].ToString(),
        //            Price = Convert.ToDecimal(item["Price"]),
        //            Image = (byte[])item["Image"]
        //        };
        //        AddItem(food);
        //    }
        //}
        private void LoadCategories()
        {
            DataTable dt = FoodDAL.Instance.GetCategories();
            foreach (DataRow row in dt.Rows)
            {
                Button b = new Button
                {
                    BackColor = Color.White,
                    Size = new Size(80, 50),
                    Text = row["CategoryName"].ToString()
                };

                b.Click += new EventHandler(btn_Click);
                CategoryPanel.Controls.Add(b);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            LoadMenu(btn.Text);
        }

        private void LoadMenu(string categoryName = null)
        {
            ProductPanel.Controls.Clear();
            ProductPanel.SuspendLayout();

            List<FoodOnMenu> foodls = FoodOnMenuDAL.Instance.LoadMenu(categoryName);
            foreach (FoodOnMenu food in foodls)
            {
                ucProduct ucproduct = new ucProduct
                {
                    id = food.FoodID,
                    PName = food.FoodName,
                    PPrice = food.Price.ToString(),
                    PImage = food.FoodImage
                };

                ProductPanel.Controls.Add(ucproduct);
            }
            ProductPanel.ResumeLayout();
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
            foreach (DataGridViewRow r in dgvFoodList.Rows)
            {
                count++;
                r.Cells[0].Value = count;
            }
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
            FoodModel.frmAddFood AF = new FoodModel.frmAddFood();
            AF.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FoodModel.frmEditFood EF = new FoodModel.frmEditFood();
            EF.Show();
        }

        private void cboZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCom.Text = "";
        }

        private void LoadComboBox(ComboBox comboBox, string query)
        {
            DataTable dt = FoodDAL.Instance.LoadComboBoxData(query);
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
            Model.frmAddCategory AC = new Model.frmAddCategory();
            AC.Show();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTongTien(dgvFoodList, lbTongtien);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvFoodList.Rows.Clear();
            //LoadProduct();
            lbTongtien.Text = "0.00";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int computerID = FoodDAL.Instance.GetComputerIDByName(cboCom.Text);
            int billingID = FoodDAL.Instance.GetBillingIDByComID(computerID);

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
                    foreach (DataGridViewRow row in dgvFoodList.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int foodID = Convert.ToInt32(row.Cells["ID"].Value);
                        int count = Convert.ToInt32(row.Cells["Qty"].Value);
                        decimal cost = Convert.ToDecimal(row.Cells["Amount"].Value);
                        FoodDAL.Instance.SaveFoodDetails(billingID, foodID, count, cost);
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


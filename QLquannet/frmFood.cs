using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
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
            LoadcboZone();
            //cboCom.DataSource = null;
            //LoadComboBox(cboZone, "Select ZoneName From Zone");
        }

        private void LoadcboZone()
        {
            cboZone.DataSource = ZoneDAL.Instance.getZones();
            cboZone.DisplayMember = "ZoneName";
            cboZone.ValueMember = "ZoneID";
            cboZone.SelectedIndex = -1;
        }
        private void LoadcboCom(byte zoneid)
        {
            cboCom.DataSource = ComputerDAL.Instance.GetComs(zoneid);
            cboCom.DisplayMember = "ComputerName";
            cboCom.ValueMember = "ComputerID";
            cboCom.SelectedIndex = -1;
        }
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

            List<Food> foodls = FoodDAL.Instance.LoadMenu(categoryName);
            foreach (Food food in foodls)
            {
                ucProduct ucproduct = new ucProduct
                {
                    id = food.FoodID,
                    PName = food.FoodName,
                    PPrice = food.Price.ToString(),
                    PImage = food.Image
                };

                // Add click event handler
                ucproduct.PictureBoxClick += UcProduct_Clicked;

                ProductPanel.Controls.Add(ucproduct);
            }
            ProductPanel.ResumeLayout();
        }

        private void UcProduct_Clicked(object sender, EventArgs e)
        {
            ucProduct clickedProduct = sender as ucProduct;
            bool productFound = false;
            foreach (DataGridViewRow item in dgvFoodList.Rows)
            {
                if (Convert.ToInt32(item.Cells["ID"].Value) == clickedProduct.id)
                {
                    int quantity = int.Parse(item.Cells["Qty"].Value.ToString()) + 1;
                    item.Cells["Qty"].Value = quantity;
                    item.Cells["Amount"].Value = quantity * decimal.Parse(item.Cells["Price"].Value.ToString());
                    productFound = true;
                    break;
                }
            }
            if (!productFound)
            {
                dgvFoodList.Rows.Add(new object[] { 0, clickedProduct.id, clickedProduct.PName, 1, clickedProduct.PPrice, clickedProduct.PPrice });
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

        private void dgvFoodList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow r in dgvFoodList.Rows)
            {
                count++;
                r.Cells[0].Value = count;
            }
        }

        //private decimal TinhTongTien(DataGridView datagridView)
        //{
        //    decimal totalAmount = 0;

        //    foreach (DataGridViewRow row in datagridView.Rows)
        //    {
        //        if (row.IsNewRow) continue;
        //        var cellValue = row.Cells["Amount"].Value;

        //        if (decimal.TryParse(cellValue.ToString(), out decimal amount))
        //        {
        //            totalAmount += amount;
        //        }
        //    }

        //    return totalAmount;
        //}

        //private void UpdateTongTien(DataGridView datagridView, Label lnTongtien)
        //{
        //    decimal totalAmount = TinhTongTien(datagridView);
        //    lnTongtien.Text = totalAmount.ToString("N2");
        //}

        private void LoadFoodDetail(byte comid)
        {
            List<Food> foods = FoodDAL.Instance.GetFoodDetail(comid);

            dgvFoodList.Rows.Clear(); // Clear existing rows (optional)

            foreach (Food food in foods)
            {
                DataGridViewRow row = new DataGridViewRow();

                foreach (PropertyInfo property in food.GetType().GetProperties())
                {
                    object value = property.GetValue(food);
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                }

                dgvFoodList.Rows.Add(row);
            }
        }



        //        foreach (DataGridViewRow item in dgvFoodList.Rows)
        //            {
        //                if (Convert.ToInt32(item.Cells["ID"].Value) == clickedProduct.id)
        //                {
        //                    int quantity = int.Parse(item.Cells["Qty"].Value.ToString()) + 1;
        //        item.Cells["Qty"].Value = quantity;
        //                    item.Cells["Amount"].Value = quantity* decimal.Parse(item.Cells["Price"].Value.ToString());
        //        productFound = true;
        //                    break;
        //                }
        //}

        private void btnThem_Click(object sender, EventArgs e)
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
            if (cboZone.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)cboZone.SelectedItem;
                LoadcboCom((byte)selectedRow["ZoneID"]);
            }
        }

        private void cboCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCom.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)cboCom.SelectedItem;
                LoadFoodDetail((byte)selectedRow["ComputerID"]);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Model.frmAddCategory AC = new Model.frmAddCategory();
            AC.Show();
        }

        //private void dgvFoodList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    UpdateTongTien(dgvFoodList, lbTongtien);
        //}

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvFoodList.Rows.Clear();
            LoadMenu();
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


using DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLquannet.FoodModel
{

    public partial class frmEditFood : Form
    {
        public int CatID;
        public string CatName;
        public int FoodID;
        public string imagePath;
        public byte[] imageBytes;
        private EditFoodDAL editFoodDAL;
        public frmEditFood()
        {
            InitializeComponent();
            editFoodDAL = new EditFoodDAL();
        }
        private void EditFood_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvFood.DataSource = editFoodDAL.GetAllFood();
            dgvFood.Columns[6].Width = 200;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFood.Rows[e.RowIndex];
                FoodID = Convert.ToInt32(row.Cells["FoodID"].Value.ToString());
                txtFoodName.Text = row.Cells["FoodName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtIntakePrice.Text = row.Cells["IntakePrice"].Value.ToString();
                txtInventory.Text = row.Cells["Inventory"].Value.ToString();
                CatID = Convert.ToInt32(row.Cells["CategoryID"].Value.ToString());
                CheckCboID();
                // Lấy ảnh từ cơ sở dữ liệu
                imageBytes = (byte[])row.Cells["Image"].Value;
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    //using (var ms = new System.IO.MemoryStream(imageBytes))
                    //{
                    //    picFood.Image = Image.FromStream(ms);
                    //}\
                    ImageProcess.ByteArrayToImage(imageBytes);
                }
                else
                {
                    picFood.Image = null;
                }
            }
        }



        private void btnDlt_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFood.SelectedRows[0];
                int rowId = Convert.ToInt32(selectedRow.Cells["FoodID"].Value);

                editFoodDAL.DeleteFood(rowId);
                dgvFood.Rows.Remove(selectedRow);
                MessageBox.Show("Dòng đã được xóa từ cơ sở dữ liệu và DataGridView.");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count > 0)
            {

                //DTO.Food food = new DTO.Food
                //{
                //    FoodID = Convert.ToInt32(FoodID),
                //    FoodName = txtFoodName.Text,
                //    Price = Convert.ToDecimal(txtPrice.Text),
                //    IntakePrice = Convert.ToDecimal(txtIntakePrice.Text),
                //    Inventory = Convert.ToInt32(txtInventory.Text),
                //    //CategoryID = CatID,
                //    Image = imageBytes
                //};

                //editFoodDAL.UpdateFood(food);
                //LoadDataGridView();
                MessageBox.Show("Cập nhật món ăn thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    if (imagePath != null)
                    {
                        picFood.Image = Image.FromFile(imagePath);
                        imageBytes = ImageToByteArray(imagePath);
                    }
                }
            }

        }

        private void CheckCboID()
        {
            CatName = editFoodDAL.GetCategoryName(CatID);
            cboCategory.Text = CatName;
        }

        public static byte[] ImageToByteArray(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    return ms.ToArray();
                }
            }
        }
    }

}

using DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLquannet.FoodModel
{
    public partial class frmAddFood : Form
    {
        public string imagePath;
        public string CatID;
        private AddFoodDAL addFoodDAL;
        public frmAddFood()
        {
            InitializeComponent();
            addFoodDAL = new AddFoodDAL();
        }
        private void AddFood_Load(object sender, EventArgs e)
        {
            LoadComboBox(cboCategory, "SELECT CategoryName FROM Category");
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
                }
            }
        }

        private void btnConfirmAddFood_Click(object sender, EventArgs e)
        {

            byte[] imageBytes = ImageProcess.ImageToByteArray(imagePath);
            CheckCboID();

            //Food food = new Food
            //{
            //    FoodName = txtFoodName.Text,
            //    Price = Convert.ToDecimal(txtPrice.Text),
            //    IntakePrice = Convert.ToDecimal(txtIntakePrice.Text),
            //    Inventory = Convert.ToInt32(txtInventory.Text),
            //    CategoryID = Convert.ToInt32(CatID),
            //    Image = imageBytes
            //};

            //addFoodDAL.SaveFood(food);
            //MessageBox.Show("Thêm món ăn thành công!");
            //this.Close();

        }

        private void LoadComboBox(ComboBox comboBox, string query)
        {
            DataTable dt = addFoodDAL.GetCategories();
            foreach (DataRow row in dt.Rows)
            {
                comboBox.Items.Add(row["CategoryName"].ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckCboID()
        {
            CatID = addFoodDAL.GetCategoryID(cboCategory.SelectedItem.ToString()).ToString();
        }
    }
}

using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquannet.Model
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void btnCnfirmAddCat_Click(object sender, EventArgs e)
        {
            string categoryName = txtCat.Text;
            AddCategoryDAL addCategoryDAL = new AddCategoryDAL();

            bool isAdded = addCategoryDAL.AddCategory(categoryName);
            if (isAdded)
            {
                MessageBox.Show("Thêm loại đồ ăn thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể thêm loại đồ ăn!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquannet
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void txtusername_Click(object sender, EventArgs e)
        {
            txtusername.SelectAll();
        }

        private void txtpassword_Click(object sender, EventArgs e)
        {
            txtpassword.SelectAll();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            btnlogin.ForeColor = Color.Blue;
            this.Hide();
            Main main = new Main();
            main.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

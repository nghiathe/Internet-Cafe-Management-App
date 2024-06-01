using DAL;
using DTO;
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
    public partial class frmLogin : Form
    {
        Login lg = new Login();
        public frmLogin()
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
            lg.username = txtusername.Text;
            lg.password = txtpassword.Text;
            string rs = (string)LoginDAL.Instance.Login(lg);
            if(rs != null)
            {
                Employee.emId = (byte)LoginDAL.Instance.GetEmployeeId(lg);
                Employee.fullName = rs;
                btnlogin.ForeColor = Color.Blue;
                this.Hide();
                Main main = new Main();
                main.Show();
            
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}

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

        }
        private void loadform(object Form)
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

            LoadFormCon(new frmComputer());
            ChangeColorMainBtn(btnComputer, null);
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            loadform(new Food());
            ChangeColorMainBtn(btnFood, null);
        }
        
        private void btnBill_Click(object sender, EventArgs e)
        {
            LoadFormCon(new frmBilling());
            ChangeColorMainBtn(btnBill, null);
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
        void ChangeColorMainBtn(object sender, EventArgs e)
        {
            foreach (Control c in pnlMenubar.Controls)
            {
                c.BackColor = Color.FromArgb(24, 30, 54);
            }
            Control cl = (Control)sender;
            cl.BackColor = Color.Gray;

        }
    }
}

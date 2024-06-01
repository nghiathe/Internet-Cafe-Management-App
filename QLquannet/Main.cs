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

        bool drag = false;
        Point start_point = new Point();

        private void pnlMovable_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void pnlMovable_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);            }
        }

        private void pnlMovable_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
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
        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormCon(new frmMaintainance());
            ChangeColorMainBtn(btnMaintain, null);
        }
        private void loadform(object Form)
        {
            if (this.pnlMain.Controls.Count > 0)
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
        private Form curentChildForm;
        void LoadFormCon (Form childForm)
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

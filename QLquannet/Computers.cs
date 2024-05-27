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
    
    public partial class Computers : Form
    {
        public static string zone ="";
        public Computers()
        {
            InitializeComponent();
        }
        private void loadformcomputer(object Form)
        {
            if (this.pnlZoneComputer.Controls.Count > 0)
            {
                this.pnlZoneComputer.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pnlZoneComputer.Controls.Add(f);
            this.pnlZoneComputer.Tag = f;
            f.Show();
        }

        private void btnZone1_Click(object sender, EventArgs e)
        {
            zone = "zone1";
            loadformcomputer(new Zone1());
            btnZone1.BackColor = Color.FromArgb(0, 126, 249);
            btnZone2.BackColor = Color.FromArgb(46, 51, 73);
            btnZone3.BackColor = Color.FromArgb(46, 51, 73);
            btnZone4.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnInfor_Click(object sender, EventArgs e)
        {
            
        }

        private void btnZone2_Click(object sender, EventArgs e)
        {
            zone = "zone2";
            loadformcomputer(new Zone1());
            btnZone2.BackColor = Color.FromArgb(0, 126, 249);
            btnZone1.BackColor = Color.FromArgb(46, 51, 73);
            btnZone3.BackColor = Color.FromArgb(46, 51, 73);
            btnZone4.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnZone3_Click(object sender, EventArgs e)
        {
            zone = "zone3";
            loadformcomputer(new Zone1());
            btnZone3.BackColor = Color.FromArgb(0, 126, 249);
            btnZone2.BackColor = Color.FromArgb(46, 51, 73);
            btnZone1.BackColor = Color.FromArgb(46, 51, 73);
            btnZone4.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnZone4_Click(object sender, EventArgs e)
        {
            zone = "zone4";
            loadformcomputer(new Zone1());
            btnZone4.BackColor = Color.FromArgb(0, 126, 249);
            btnZone2.BackColor = Color.FromArgb(46, 51, 73);
            btnZone3.BackColor = Color.FromArgb(46, 51, 73);
            btnZone1.BackColor = Color.FromArgb(46, 51, 73);
        }
    }
}

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
    public partial class Zone1 : Form
    {
        public Zone1()
        {
            InitializeComponent();
        }

        
        private void btnMay10_Click(object sender, EventArgs e)
        {

        }

        private void Zone1_Load(object sender, EventArgs e)
        {
            if(Computers.zone == "zone1")
            {
                lbZonename.Text = "Zone 1";
            }
            if (Computers.zone == "zone2")
            {
                lbZonename.Text = "Zone 2";
            }
            if (Computers.zone == "zone3")
            {
                lbZonename.Text = "Zone 3";
            }
            if (Computers.zone == "zone4")
            {
                lbZonename.Text = "Zone 4";
            }
        }
    }
}

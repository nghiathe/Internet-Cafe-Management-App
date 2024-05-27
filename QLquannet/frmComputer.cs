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
    public partial class frmComputer : Form
    {
        #region ---------- Code cua HungTuLenh 
        Computer com = new Computer();
        byte zid;
        public frmComputer()
        {
            InitializeComponent();

        }

        private void btnZone1_Click(object sender, EventArgs e)
        {
            LoadZone(1);
            lblZone.Text = "Zone1";
            ComputerZone.zoneId = 1;
        }

        private void btnZone2_Click(object sender, EventArgs e)
        {
            LoadZone(2);
            lblZone.Text = "Zone2";
            ComputerZone.zoneId = 2;

        }

        private void btnZone3_Click(object sender, EventArgs e)
        {
            LoadZone(3);
            lblZone.Text = "Zone3";
            ComputerZone.zoneId = 3;

        }

        private void btnZone4_Click(object sender, EventArgs e)
        {
            LoadZone(4);
            lblZone.Text = "Zone4";
            ComputerZone.zoneId = 4;

        }

        void LoadZone(byte zoneid )
        {
            flpCom.Controls.Clear();
            List<Computer> listCom = ComputerDAL.Instance.loadCom(zoneid);
            int online = 0;
            int offline = 0;
            foreach (Computer com in listCom)
            {
                Button btn = new Button()
                {
                    Width = ComputerDAL.ComWidth,
                    Height = ComputerDAL.ComHeight,
                };
                
                btn.Click += btn_Click;

                btn.Tag = com;
                
                switch (com.ComStatus)
                {
                    case 0:
                        btn.BackColor = Color.LightGray;
                        btn.Text = com.ComName + Environment.NewLine + "Offline";
                        offline++;
                        break;
                        
                    case 1:
                        btn.BackColor = Color.Aqua;
                        btn.Text = com.ComName + Environment.NewLine + "Online";
                        online++;
                        break;
                    /*default:
                        btn.BackColor = Color.OrangeRed;
                        btn.Text = com.ComName + Environment.NewLine + "Error";
                        break;*/
                }
                
                txtAvailable.Text = offline.ToString();
                txtUsing.Text = online.ToString();
                flpCom.Controls.Add(btn);

            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            LoadZone(ComputerZone.zoneId);
            Button btn = (Button)sender;
            Computer com = btn.Tag as Computer;

            gbMay.Text = com.ComName;
            txtPrice.Text = com.PricePh.ToString();
            if (com.STime.HasValue)
            {
                tpStime.Value = com.STime.Value;
            }
            else
            {
                tpStime.Value = DateTime.Now;
            }
            TimeSpan duration = DateTime.Now - tpStime.Value;
            string formatDuration = string.Format("{0:D2}:{1:D2}", (int)duration.TotalHours, duration.Minutes);
            txtTime.Text = formatDuration;

            int hours = int.Parse(formatDuration.Split(':')[0]);
            int minutes = int.Parse(formatDuration.Split(':')[1]);
            decimal tamTinh = (hours + (minutes / 60.0m)) * com.PricePh;

            txtTamtinh.Text = Math.Round(tamTinh, 2).ToString();

            zid = com.ComId;
            if(com.ComStatus == 0)
            {
                txtTT.Text = "Offline";
            }
            else
            {
                txtTT.Text = "Online";
            }

        }
        private void btnBatmay_Click(object sender, EventArgs e)
        {
            if (txtTT.Text == "Online")
            {
                MessageBox.Show("Máy đang online!");
            }
            else if (txtTT.Text == "Error")
            {
                MessageBox.Show("Máy đang bảo trì!");
            }
            else if (gbMay.Text == "")
            {
                MessageBox.Show("Chưa chọn máy!");
            }
            else
            {
                ComputerDAL.Instance.Online(zid);
                LoadZone(ComputerZone.zoneId);
            }
        }
        #endregion

    }
}

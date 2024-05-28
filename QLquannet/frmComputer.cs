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
        byte cid;
        public frmComputer()
        {
            InitializeComponent();
            LoadZone(1);

        }

        #region Event
        private void btnZone1_Click(object sender, EventArgs e)
        {
            LoadZone(1);

        }

        private void btnZone2_Click(object sender, EventArgs e)
        {
            LoadZone(2);

        }

        private void btnZone3_Click(object sender, EventArgs e)
        {
            LoadZone(3);

        }

        private void btnZone4_Click(object sender, EventArgs e)
        {
            LoadZone(4);

        }

        private void btn_Click(object sender, EventArgs e)
        {
            LoadZone(ComputerZone.zoneId);

            cid = ((sender as Button).Tag as Computer).ComId;
            LoadUsageSession(cid);
            LoadFoodDetail(cid);

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
                ComputerDAL.Instance.Online(cid);
                LoadZone(ComputerZone.zoneId);
            }
        }
        #endregion

        #region Method
        void LoadZone(byte zoneid)
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

                gbZone.Text = com.ZoneName;
                txtPrice.Text = com.PricePh.ToString();
                txtAvailable.Text = offline.ToString();
                txtUsing.Text = online.ToString();
                txtCPU.Text = com.CpuModel;
                txtGPU.Text = com.Gpumodel;
                txtHDD.Text = com.HddModel;
                txtSSD.Text = com.SsdModel;
                txtMouse.Text = com.MouseModel;
                txtKey.Text = com.KeyboardModel;
                txtMonitor.Text = com.MonitorModel;

                ComputerZone.zoneId = zoneid;

                flpCom.Controls.Add(btn);
                
            }
        }
        
        void LoadUsageSession(byte comid)
        {
            UsageSession us = UsageSessionDAL.Instance.GetUsageSessionDetails(comid);

            gbMay.Text = us.ComName;
            if (us.STime.HasValue)
            {
                tpStime.Value = us.STime.Value;
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
            decimal tamTinh = (hours + (minutes / 60.0m)) * decimal.Parse(txtPrice.Text);
            txtTamtinh.Text = Math.Round(tamTinh, 2).ToString();

            if (us.ComStatus == 0)
            {
                txtTT.Text = "Offline";
            }
            else
            {
                txtTT.Text = "Online";
            }
        }
        void LoadFoodDetail(byte comid)
        {
            lvFood.Items.Clear();
            List<FoodPerCom> fl = FoodPerComDAL.Instance.GetFoodDetail(comid);
            decimal fcost = 0m;
            foreach (FoodPerCom f in fl)
            {
                ListViewItem lvi = new ListViewItem(f.FoodName.ToString());
                lvi.SubItems.Add(f.Count.ToString());
                lvi.SubItems.Add(f.Price.ToString());
                lvi.SubItems.Add(f.Cost.ToString());
                fcost += f.Cost;
                lvFood.Items.Add(lvi);
            }
            txtFcost.Text = fcost.ToString();
        }
        #endregion

        #endregion

    }
}

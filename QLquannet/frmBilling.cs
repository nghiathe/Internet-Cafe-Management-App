using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLquannet
{
    public partial class frmBilling : Form
    {
        public frmBilling()
        {
            InitializeComponent();
            LoadDate();
            ShowBill(tpBd.Value, tpKt.Value);
        }
        void LoadDate()
        {
            DateTime today = DateTime.Now;
            tpBd.Value = new DateTime(today.Year, today.Month, 1);
            tpKt.Value = tpBd.Value.AddMonths(1).AddDays(-1);
        }
        void ShowBill(DateTime ngaybd, DateTime ngaykt)
        {
            lvBill.Items.Clear();
            List<BillingHis> lb = BillingDAL.Instance.loadBillList(ngaybd, ngaykt);
            decimal income = 0;
            decimal outcome = 0;
            foreach (BillingHis b in lb)
            {
                ListViewItem lvi = new ListViewItem(b.BDate.ToString());
                lvi.SubItems.Add(b.EmName.ToString());
                if (b.BType == 1)
                {
                    lvi.SubItems.Add("Thu");
                }
                else
                {
                    lvi.SubItems.Add("Chi");
                }
                
                lvi.SubItems.Add(b.SCost.ToString());
                lvi.SubItems.Add(b.MCost.ToString());
                lvi.SubItems.Add(b.FCost.ToString());
                lvi.SubItems.Add(b.Amount.ToString());
                if(b.BType == 1)
                {
                    income += b.Amount;
                }
                else
                { 
                    outcome += b.Amount; 
                }
                
                lvBill.Items.Add(lvi);
            }
            CultureInfo ct = new CultureInfo("vi-VN");
            txtIn.Text = income.ToString("c", ct);
            txtOut.Text = outcome.ToString("c", ct);
            txtProfit.Text = (income - outcome).ToString("c", ct);
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
        }
    }
}

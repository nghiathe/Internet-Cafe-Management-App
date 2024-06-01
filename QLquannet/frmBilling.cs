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
        decimal profit = 0;

        CultureInfo ct = new CultureInfo("vi-VN");
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
            tpKt.Value = tpBd.Value.AddMonths(1);
            tpTs.Value = new DateTime(today.Year, today.Month, 1);
            tpTe.Value = tpTs.Value.AddMonths(-1);
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
                if (b.BType == 1)
                {
                    income += b.Amount;
                }
                else
                {
                    outcome += b.Amount;
                }

                lvBill.Items.Add(lvi);
            }
            profit = income - outcome;
            txtIn.Text = income.ToString("c", ct);
            txtOut.Text = outcome.ToString("c", ct);
            txtProfit.Text = (profit).ToString("c", ct);

        }
   

        private void rbKhoang_CheckedChanged(object sender, EventArgs e)
        {
            if(rbKhoang.Checked)
            {
                gbKhoang.Enabled = true;
                ShowBill(tpBd.Value, tpKt.Value);
            }
            else
            {
                gbKhoang.Enabled = false;
            }
        }

        private void tpKt_ValueChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
        }

        private void tpBd_ValueChanged(object sender, EventArgs e)
        {
            ShowBill(tpBd.Value, tpKt.Value);
            
        }

        private void rbHai_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHai.Checked)
            {
                gbHai.Enabled = true;
            }
            else
            {
                gbHai.Enabled = false;
            }
        }
        private void tpTs_ValueChanged(object sender, EventArgs e)
        {
            DateTime fday = new DateTime(tpTs.Value.Year, tpTs.Value.Month, 1);
            DateTime lday = fday.AddMonths(1);
            ShowBill(fday, lday);
            txtProfitMonth.Text = (profit).ToString("c", ct);
        }

        private void tpTe_ValueChanged(object sender, EventArgs e)
        {
            DateTime fday = new DateTime(tpTe.Value.Year, tpTe.Value.Month, 1);
            DateTime lday = fday.AddMonths(1);
            ShowBill(fday, lday);
            txtProfitOtherMonth.Text = (profit).ToString("c", ct);
        }
    }
}

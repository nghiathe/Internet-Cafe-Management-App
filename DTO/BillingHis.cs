using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    #region ---------- Code cua HungTuLenh 
    public class BillingHis
    {
        public BillingHis(DateTime bdate, string emname, byte btype, decimal scost, decimal mcost, decimal fcost,  decimal amount)
        {
            this.BDate = bdate;
            this.EmName = emname;
            this.BType = btype;
            this.SCost = scost;
            this.MCost = mcost;
            this.FCost = fcost;
            this.Amount = amount;
        }

        public BillingHis(DataRow row)
        {
            this.BDate = (DateTime)row["billingdate"];
            this.EmName = row["employeename"].ToString();
            this.BType = (byte)row["billingtype"];
            this.SCost = (decimal)row["sessioncost"];
            this.MCost = (decimal)row["maintainancecost"];
            this.FCost = (decimal)row["foodcost"];
            this.Amount = (decimal)row["amount"];
        }
        DateTime bDate;
        string emName;
        byte bType;
        decimal sCost;
        decimal mCost;
        decimal fCost;
        decimal amount;

        public DateTime BDate { get => bDate; set => bDate = value; }
        public string EmName { get => emName; set => emName = value; }
        public byte BType { get => bType; set => bType = value; }
        public decimal SCost { get => sCost; set => sCost = value; }
        public decimal MCost { get => mCost; set => mCost = value; }
        public decimal FCost { get => fCost; set => fCost = value; }
        public decimal Amount { get => amount; set => amount = value; }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsageSession
    {
        public UsageSession(int billid, string comname, byte comstatus, DateTime? stime)
        {
            this.BillId = billid;
            this.ComName = comname;
            this.ComStatus = comstatus;
            this.STime = stime;
        }
        public UsageSession(DataRow row)
        { 
            this.ComName = row["computername"].ToString();
            this.ComStatus = (byte)row["computerstatus"];
            var thoigianTemp = row["starttime"];
            if (thoigianTemp.ToString() != "")
            {
                this.STime = (DateTime?)thoigianTemp;
            }
            var billidTemp = row["billingid"];
            if (billidTemp.ToString() != "")
            {
                this.BillId = (int)billidTemp;
            }
        }

        public UsageSession() { }


        private int billId;
        private string comName;
        private byte comStatus;
        private DateTime? sTime;

        public string ComName { get => comName; set => comName = value; }
        public int BillId { get => billId; set => billId = value; }
        public byte ComStatus { get => comStatus; set => comStatus = value; }
        public DateTime? STime { get => sTime; set => sTime = value; }
    }
}

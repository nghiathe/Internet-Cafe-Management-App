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
        public UsageSession(byte comid, string comname, byte comstatus, DateTime? stime)
        {
            this.ComId = comid;
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
        }

        public UsageSession() { }


        private byte comId;
        private string comName;
        private byte comStatus;
        private DateTime? sTime;

        public string ComName { get => comName; set => comName = value; }
        public byte ComId { get => comId; set => comId = value; }
        public byte ComStatus { get => comStatus; set => comStatus = value; }
        public DateTime? STime { get => sTime; set => sTime = value; }
    }
}

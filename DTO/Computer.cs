using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{   
    #region ---------- Code cua HungTuLenh 
    public class Computer
    {
        
        public Computer(byte comid, string comname, byte zoneid, byte comstatus, decimal priceph, DateTime? stime)
        {
            this.ComId = comid;
            this.ComName = comname;
            this.ZoneId = zoneid;
            this.ComStatus = comstatus;
            this.PricePh = priceph;
            this.STime = stime;
            

        }
        public Computer (DataRow row)
        {
            //this.ComId = (byte)row["computerid"];
            this.ComName = row["computername"].ToString();
            this.ZoneId = (byte)row["zoneid"];
            this.ComStatus = (byte)row["computerstatus"];
            this.PricePh = (decimal)row["priceperhour"];
            var thoigianTemp = row["starttime"];
            if (thoigianTemp.ToString() != "")
            {
                this.STime = (DateTime?)thoigianTemp;
            }
        }

        public Computer() { }
        

        private byte comId;
        private string comName;
        private byte zoneId;
        private byte comStatus;
        private decimal pricePh;
        private DateTime? sTime;

        public string ComName { get => comName; set => comName = value; }
        public byte ComId { get => comId; set => comId = value; }
        public byte ZoneId { get => zoneId; set => zoneId = value; }
        public byte ComStatus { get => comStatus; set => comStatus = value; }
        public decimal PricePh { get => pricePh; set => pricePh = value; }
        public DateTime? STime { get => sTime; set => sTime = value; }
        
    }
    public static class ComputerZone
    {
        public static byte zoneId { get; set; }
    }
    #endregion
}

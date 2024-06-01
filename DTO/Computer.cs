using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Computer
    {

        public Computer(byte comid, string comname, byte comstatus) 
        { 
            this.ComId = comid;
            this.ComName = comname;
            this.ComStatus = comstatus;
        }
        public Computer(DataRow row)
        {
            this.ComId = (byte)row["computerid"];
            this.ComName = row["computername"].ToString(); ;
            this.ComStatus = (byte)row["computerstatus"]; ;

        }

        private byte comId;
        private string comName;
        private byte comStatus;

        public byte ComId { get => comId; set => comId = value; }
        public string ComName { get => comName; set => comName = value; }
        public byte ComStatus { get => comStatus; set => comStatus = value; }
    }
}

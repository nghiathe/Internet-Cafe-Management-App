using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ComputerDAL
    {
        #region ---------- Code cua HungTuLenh 
        private static ComputerDAL instance;

        public static ComputerDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComputerDAL();
                }
                return ComputerDAL.instance;
            }
            private set { ComputerDAL.instance = value; }
        }

        private ComputerDAL() { }

        public static int ComWidth = 150;
        public static int ComHeight = 100;


        public List<Computer> loadCom(byte zoneid)
        {
            List<Computer> lc = new List<Computer>();
            string query = "GetComputerDetailsByZone @zoneid";
            DataTable dt = Database.Instance.ExecuteQuery(query , new object[] {zoneid});
            foreach (DataRow dr in dt.Rows)
            {
                Computer com = new Computer(dr);
                lc.Add(com);
            }
            return lc;
        }

        
        #endregion
    }
}

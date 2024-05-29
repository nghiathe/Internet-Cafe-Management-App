using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsageSessionDAL
    {
        private static UsageSessionDAL instance;

        public static UsageSessionDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UsageSessionDAL();
                }
                return UsageSessionDAL.instance;
            }
            private set { UsageSessionDAL.instance = value; }
        }

        private UsageSessionDAL() { }

        public UsageSession GetUsageSessionDetails(byte comid)
        {
            DataTable dt = Database.Instance.ExecuteQuery("GetUsageSessionDetails @comid", new object[] { comid });
            DataRow row = dt.Rows[0];
            return new UsageSession(row);

        }
    }
}

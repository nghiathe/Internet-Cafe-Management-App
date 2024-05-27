using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillingDAL
    {
        #region ---------- Code cua HungTuLenh 
        private static BillingDAL instance;

        public static BillingDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillingDAL();
                }
                return BillingDAL.instance;
            }
            private set { BillingDAL.instance = value; }
        }
        private BillingDAL() { }

        public List<BillingHis> loadBillList(DateTime ngaybd, DateTime ngaykt)
        {
            List<BillingHis> bl = new List<BillingHis>();
            string query = "GetBillByDate @ngaybd , @ngaykt";
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { ngaybd, ngaykt });
            foreach (DataRow dr in dt.Rows)
            {
                BillingHis hd = new BillingHis(dr);
                bl.Add(hd);
            }
            return bl;
        }

        #endregion
    }
}

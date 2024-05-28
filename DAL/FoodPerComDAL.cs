using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FoodPerComDAL
    {
        private static FoodPerComDAL instance;

        public static FoodPerComDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodPerComDAL();
                }
                return FoodPerComDAL.instance;
            }
            private set { FoodPerComDAL.instance = value; }
        }

        private FoodPerComDAL() { }

        public List<FoodPerCom> GetFoodDetail(byte comid)
        {
            List<FoodPerCom> fl = new List<FoodPerCom>();

            string query = "GetFoodDetailsByComputerID @ComputerID";
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] {comid});

            foreach (DataRow dr in dt.Rows)
            {
                FoodPerCom f = new FoodPerCom(dr);
                fl.Add(f);
            }

            return fl;
        }
    }
}

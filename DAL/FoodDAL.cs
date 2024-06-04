using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class FoodDAL
    {
        private static FoodDAL instance;

        public static FoodDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodDAL();
                }
                return FoodDAL.instance;
            }
            private set { FoodDAL.instance = value; }
        }

        private FoodDAL() { }
        public List<Food> GetFoodDetail(byte comid)
        {
            List<Food> fl = new List<Food>();

            string query = "GetFoodDetailsByComputerID @ComputerID";
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { comid });

            foreach (DataRow dr in dt.Rows)
            {
                Food f = new Food(dr);
                fl.Add(f);
            }

            return fl;
        }

        public List<Food> LoadMenu(string categoryName = null)
        {
            StringBuilder builder = new StringBuilder();
            List<Food> productls = new List<Food>();
            builder.Append(@"SELECT f.foodid, f.categoryid, f.foodname, fd.count, f.price, fd.cost, 
                            f.image FROM food f left join fooddetail fd on fd.foodid = f.foodid 
                            join category c on c.categoryid = f.categoryid");

            DataTable dt;
            string query = builder.ToString();

            if (!string.IsNullOrEmpty(categoryName))
            {
                builder.Append(" WHERE categoryname = @categoryName");
                query = builder.ToString();
                dt = Database.Instance.ExecuteQuery(query, new object[] { categoryName });
            }
            else
            {
                dt = Database.Instance.ExecuteQuery(query);
            }

            foreach (DataRow dr in dt.Rows)
            {
                Food f = new Food(dr);
                productls.Add(f);
            }

            return productls;
        }
        public DataTable GetCategories()
        {
            string query = "SELECT CategoryName FROM Category";
            return Database.Instance.ExecuteQuery(query);
        }

        public int GetComputerIDByName(string ComputerName)
        {
            string query = "SELECT c.ComputerID FROM Computer AS c INNER JOIN UsageSession AS us ON c.ComputerID = us.ComputerID WHERE c.ComputerName = @ComputerName";
            return Convert.ToInt32(Database.Instance.ExecuteScalar(query, new object[] { ComputerName }));
        }

        public int GetBillingIDByComID(int ComputerID)
        {
            string query = "SELECT BillingID, EndTime FROM UsageSession WHERE ComputerID = @ComID";
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { ComputerID });

            if (dt.Rows.Count > 0)
            {
                object endTime = dt.Rows[0]["EndTime"];
                if (endTime == DBNull.Value)
                    return Convert.ToInt32(dt.Rows[0]["BillingID"]);
            }
            return -1;
        }

        public void SaveFoodDetails(int BillingID, int FoodID, int Count, decimal Cost)
        {
            string query = "INSERT INTO FoodDetail (BillingID, FoodID, Count, Cost) VALUES ( @BillingID , @FoodID , @Count , @Cost )";
            Database.Instance.ExecuteNonQuery(query, new object[] { BillingID, FoodID, Count, Cost });
        }

        public DataTable LoadComboBoxData(string query)
        {
            return Database.Instance.ExecuteQuery(query);
        }

    }

}

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
            builder.Append(@"SELECT f.foodid, f.categoryid, f.foodname, 0 AS count, f.price, 0 AS cost, f.image
                            FROM food f JOIN category c ON c.categoryid = f.categoryid");

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

        public int GetUncheckBillingID(byte comId)
        {
            string query = "SELECT BillingID FROM UsageSession WHERE ComputerID = @ComID and endtime is null";
            object result = Database.Instance.ExecuteScalar(query, new object[] { comId });

            if (result == null || result == DBNull.Value)
            {
                return -1; // Trả về -1 nếu không có kết quả hoặc kết quả là null
            }

            return (int)result;
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

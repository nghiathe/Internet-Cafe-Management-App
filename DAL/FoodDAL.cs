using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class FoodDAL
    {


        private string connectionString = ConnectionConstants.DefaultConnection;


        public DataTable GetCategories()
        {
            string query = "SELECT CategoryName FROM Category";
            return Database.Instance.ExecuteQuery(query);
        }

        public DataTable GetAllFoods()
        {
            string query = "SELECT * FROM Food AS f INNER JOIN Category AS c ON f.CategoryID = c.CategoryID";
            return Database.Instance.ExecuteQuery(query);
        }

        public int GetComputerID(string ComputerName)
        {
            string query = "SELECT c.ComputerID FROM Computer AS c INNER JOIN UsageSession AS us ON c.ComputerID = us.ComputerID WHERE c.ComputerName = @ComputerName";
            return Convert.ToInt32(Database.Instance.ExecuteScalar(query, new object[] {  ComputerName }));
        }

        public int GetBillingID(int ComputerID)
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
            string query = "INSERT INTO FoodDetail (BillingID, FoodID, Count, Cost) VALUES (@BillingID, @FoodID, @Count, @Cost)";
            Database.Instance.ExecuteNonQuery(query, new object[] { BillingID, FoodID, Count, Cost });
        }

        public DataTable LoadComboBoxData(string query)
        {
            return Database.Instance.ExecuteQuery(query);
        }
    }
}

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
        private string connectionString = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";

        public DataTable GetCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select CategoryName from Category";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllFoods()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select * from Food as f inner join Category as c on f.CategoryID = c.CategoryID";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int GetComputerID(string ComputerName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select c.ComputerID From Computer as c inner join UsageSession as us on c.ComputerID = us.ComputerID where c.ComputerName= @ComputerName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ComputerName", ComputerName);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetBillingID(int ComputerID)
        {
            object endTime = null;
            int billID = -1; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT BillingID, EndTime FROM UsageSession WHERE ComputerID = @ComID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ComID", ComputerID);
                conn.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        endTime = r["EndTime"];
                        billID = Convert.ToInt32(r["BillingID"]);
                    }
                }
            }

            if (endTime == DBNull.Value)
            {
                return billID;
            }
            else
            {
                return -1;
            }
        }

        public void SaveFoodDetails(int BillingID, int FoodID, int Count, decimal Cost)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FoodDetail (BillingID, FoodID, Count, Cost) VALUES (@BillingID, @FoodID, @Count, @Cost)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BillingID", BillingID);
                cmd.Parameters.AddWithValue("@FoodID", FoodID);
                cmd.Parameters.AddWithValue("@Count", Count);
                cmd.Parameters.AddWithValue("@Cost", Cost);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable LoadComboBoxData(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}

using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddFoodDAL
    {
        private string connectionString = "Data Source=LAPTOP-KKNF42CS\\SQLEXPRESS;Initial Catalog=QLyCafeInternet;Integrated Security=True";

        public DataTable GetCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryName, CategoryID FROM Category";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void SaveFood(FoodDTO food)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Food (FoodName, Price, IntakePrice, Inventory, CategoryID, Image) VALUES (@Name, @Price, @IntakePrice, @Inventory, @CategoryID, @Image)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", food.FoodName);
                    cmd.Parameters.AddWithValue("@Price", food.Price);
                    cmd.Parameters.AddWithValue("@IntakePrice", food.IntakePrice);
                    cmd.Parameters.AddWithValue("@Inventory", food.Inventory);
                    cmd.Parameters.AddWithValue("@CategoryID", food.CategoryID);
                    cmd.Parameters.AddWithValue("@Image", food.Image);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int GetCategoryID(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID FROM Category WHERE CategoryName = @CatName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CatName", categoryName);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}

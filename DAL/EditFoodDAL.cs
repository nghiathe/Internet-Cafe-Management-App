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
    public class EditFoodDAL
    {
        private string connectionString = "Data Source=DESKTOP-N234E7R\\SQLEXPRESS01;Initial Catalog=Qlquannet;Integrated Security=True";

        public DataTable GetAllFood()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void DeleteFood(int foodID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Food WHERE FoodID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", foodID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFood(FoodDTO food)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Food SET FoodName = @Name, Price = @Price, IntakePrice = @IntakePrice, Inventory = @Inventory, Image = @Image WHERE FoodID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", food.FoodName);
                    cmd.Parameters.AddWithValue("@Price", food.Price);
                    cmd.Parameters.AddWithValue("@IntakePrice", food.IntakePrice);
                    cmd.Parameters.AddWithValue("@Inventory", food.Inventory);
                    cmd.Parameters.AddWithValue("@Image", food.Image);
                    cmd.Parameters.AddWithValue("@ID", food.FoodID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GetCategoryName(int categoryID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryName FROM Category WHERE CategoryID = @CatID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CatID", categoryID);
                    conn.Open();
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}

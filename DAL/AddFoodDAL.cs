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

        Database database = Database.Instance;

        private string connectionString = ConnectionConstants.DefaultConnection;


        public DataTable GetCategories()
        {
            string query = "SELECT CategoryName, CategoryID FROM Category";
            return database.ExecuteQuery(query);
        }

        public void SaveFood(FoodDTO food)
        {
            string query = "INSERT INTO Food (FoodName, Price, IntakePrice, Inventory, CategoryID, Image) VALUES ( @Name , @Price , @IntakePrice , @Inventory , @CategoryID , @Image )";
            database.ExecuteNonQuery(query, new object[] { food.FoodName, food.Price, food.IntakePrice, food.Inventory, food.CategoryID, food.Image });
        }

        public int GetCategoryID(string categoryName)
        {
            string query = "SELECT CategoryID FROM Category WHERE CategoryName = @CatName ";
            return Convert.ToInt32(database.ExecuteScalar(query, new object[] { categoryName }));
        }
    }
}

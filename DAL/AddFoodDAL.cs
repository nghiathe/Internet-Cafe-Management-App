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
        public DataTable GetCategories()
        {
            string query = "SELECT CategoryName, CategoryID FROM Category";
            return Database.Instance.ExecuteQuery(query);
        }

        public void SaveFood(Food food)
        {
            string query = "INSERT INTO Food (FoodName, Price, IntakePrice, Inventory, CategoryID, Image) VALUES ( @Name , @Price , @IntakePrice , @Inventory , @CategoryID , @Image )";
            database.ExecuteNonQuery(query, new object[] { food.FoodName, food.Price, food.IntakePrice, food.Inventory, food.CategoryID, food.Image });
            string query = "INSERT INTO Food (FoodName, Price, IntakePrice, Inventory, CategoryID, Image) VALUES ( @Name , @Price , @IntakePrice , @Inventory , @CategoryID , @Image )";
            Database.Instance.ExecuteNonQuery(query, new object[] { food.FoodName, food.Price, food.IntakePrice, food.Inventory, food.CategoryID, food.Image });
        }

        public int GetCategoryID(string categoryName)
        {
            string query = "SELECT CategoryID FROM Category WHERE CategoryName = @CatName ";
            return Convert.ToInt32(database.ExecuteScalar(query, new object[] { categoryName }));
            string query = "SELECT CategoryID FROM Category WHERE CategoryName = @CatName ";
            return Convert.ToInt32(Database.Instance.ExecuteScalar(query, new object[] { categoryName }));
        }
    }
}

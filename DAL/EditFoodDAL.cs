using DTO;
using System.Data;

namespace DAL
{
    public class EditFoodDAL
    {
        public DataTable GetAllFood()
        {
            string query = "SELECT * FROM Food";
            return Database.Instance.ExecuteQuery(query);
        }

        public void DeleteFood(int foodID)
        {
            string query = "DELETE FROM Food WHERE FoodID = @ID";
            Database.Instance.ExecuteNonQuery(query, new object[] { foodID });
        }

        public void UpdateFood(Food food)
        {
            string query = "UPDATE Food SET FoodName = @Name , Price = @Price , IntakePrice = @IntakePrice , Inventory = @Inventory , Image = @Image WHERE FoodID = @ID";
            Database.Instance.ExecuteNonQuery(query, new object[] { food.FoodName, food.Price, food.IntakePrice, food.Inventory, food.Image, food.FoodID });
        }

        public string GetCategoryName(int categoryID)
        {
            string query = "SELECT CategoryName FROM Category WHERE CategoryID = @CatID";
            return Database.Instance.ExecuteScalar(query, new object[] { categoryID }).ToString();
        }
    }
}

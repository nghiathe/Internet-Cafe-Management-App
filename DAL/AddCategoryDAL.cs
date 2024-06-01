using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddCategoryDAL
    {
        public bool AddCategory(string categoryName)
        {
            string query = "INSERT INTO Category (CategoryName) VALUES (N'"+categoryName+"')";
            

            int result = Database.Instance.ExecuteNonQuery(query, new object[] { categoryName });

            // If one row is affected, the insertion was successful
            return result > 0;
        }
    }
}

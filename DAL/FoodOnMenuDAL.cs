using DTO;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class FoodOnMenuDAL
    {
        private static FoodOnMenuDAL instance;

        public static FoodOnMenuDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodOnMenuDAL();
                }
                return instance;
            }
            private set { FoodOnMenuDAL.instance = value; }
        }
        public FoodOnMenuDAL() { }
        public List<FoodOnMenu> LoadMenu(string categoryName = null)
        {
            StringBuilder builder = new StringBuilder();
            List<FoodOnMenu> productls = new List<FoodOnMenu>();
            builder.Append(@"SELECT f.foodid, f.foodname, f.price, f.categoryid, 
                            f.image FROM food f join category c on f.categoryid = c.categoryid");

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
                FoodOnMenu a = new FoodOnMenu(dr);
                productls.Add(a);
            }

            return productls;
        }

    }

}


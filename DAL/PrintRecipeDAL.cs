using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PrintRecipeDAL
    {
        public DataTable GetAllBillings()
        {
            string query = "SELECT * FROM Billing";
            return Database.Instance.ExecuteQuery(query);
        }

    }
}

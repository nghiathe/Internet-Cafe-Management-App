using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FoodDTO
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string CategoryName { get; set; }
        public decimal IntakePrice { get; set; }
        public int Inventory { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
    }

    public class CategoryDTO
    {
        public string CategoryName { get; set; }
    }
}

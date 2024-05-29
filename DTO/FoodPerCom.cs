using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FoodPerCom
    {
        public FoodPerCom(string fname,int c, decimal pr, decimal co = 0m)
        {
            this.FoodName = fname;
            this.Count = c;
            this.Price = pr;
            this.Cost = co;
        }
        public FoodPerCom(DataRow row)
        {
            this.FoodName = row["foodname"].ToString();
            this.Count = (int)row["count"];
            this.Price = (decimal)row["price"];
            this.Cost = (decimal)row["cost"];
        }

        private decimal cost;
        private decimal price;
        private int count;
        private string foodName;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public decimal Price { get => price; set => price = value; }
        public decimal Cost { get => cost; set => cost = value; }
    }
}

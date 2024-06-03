using System.Data;
using System.IO;
using System.Drawing;

namespace DTO
{
    public class Food
    {
        private int foodID;
        private string categoryName;
        private decimal intakePrice;
        private int inventory;
        private int categoryID;
        private string foodName;
        private decimal price;
        private byte[] image;
        private int count;
        private decimal cost;
        public int FoodID { get => foodID; set => foodID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public decimal IntakePrice { get => intakePrice; set => intakePrice = value; }
        public int Inventory { get => inventory; set => inventory = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public decimal Price { get => price; set => price = value; }
        public byte[] Image { get => image; set => image = value; }
        public int Count { get => count; set => count = value; }
        public decimal Cost { get => cost; set => cost = value; }
    }

    public class FoodOnCom
    {
        public FoodOnCom(DataRow row)
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
    public class FoodOnMenu
    {
        int foodID;
        string foodName;
        decimal price;
        Image foodImage;

        public FoodOnMenu(DataRow row)
        {
            this.FoodID = (byte)row["foodid"];
            this.FoodName = row["foodname"].ToString();
            this.Price = (decimal)row["price"];
            this.FoodImage = Image.FromStream(new MemoryStream((byte[])row["image"]));
        }
        public int FoodID { get => foodID; set => foodID = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public decimal Price { get => price; set => price = value; }
        public Image FoodImage { get => foodImage; set => foodImage = value; }

    }
    public class Category
    {
        private int categoryID;
        private string categoryName;
        public Category(DataRow row)
        {
            this.CategoryID = (int)row["categoryid"];
            this.CategoryName = row["categoryname"].ToString();
        }

        public int CategoryID { get => categoryID; set => categoryID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
    }
}

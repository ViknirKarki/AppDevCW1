using System;
namespace AppDevCW1.Data
{
    public class Orders
    {
        public List<Coffees> CoffeesOrderList { get; set; }
        public List<AddIns> AddInsList { get; set; }
        public int TotalPrice { get; set; }
        public int Discount { get; set; }
        public string CustomerPhNum { get; set; }
        public DateTime OrderDate { get; set; }
        public int FinalPrice { get; set; }
    }
}

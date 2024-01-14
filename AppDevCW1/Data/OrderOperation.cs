using System;
using System.Diagnostics;
using System.Text.Json;


namespace AppDevCW1.Data
{
    public class OrderOperation
    {
        public Orders orderInstance;

        public OrderOperation()
        {
            orderInstance = new();
            orderInstance.AddInsList = new List<AddIns>();
            orderInstance.CoffeesOrderList = new List<Coffees>();
            orderInstance.TotalPrice = 0;
        }


        public List<Orders> GetAllOrders()
        {
            string ordersFilePath = Func.GetAppOrdersFilePath();
            if (!File.Exists(ordersFilePath))
            {
                return new List<Orders>();
            }

            var json = File.ReadAllText(ordersFilePath);

            return JsonSerializer.Deserialize<List<Orders>>(json);
        }

        public int getOrderCount(string CustomerPhNum)
        {
            List<Orders> orders = GetAllOrders();
            int count = 0;
            foreach (var order in orders)
            {
                if (order.CustomerPhNum.Equals(CustomerPhNum))
                {
                    count++;
                }
            }
            return count;
        }

        public List<Orders> GetUniqueOrdersForCustomer(string CustomerPhNum)
        {
            List<Orders> allOrders = GetAllOrders();
            var uniqueOrders = allOrders
                .Where(o => o.CustomerPhNum == CustomerPhNum)
                .GroupBy(o => o.OrderDate)
                .Select(group => group.First())
                .ToList();

            return uniqueOrders;
        }

        public bool checkDiscountAvailable(string CustomerPhNum)
        {
            List<Orders> orders = GetUniqueOrdersForCustomer(CustomerPhNum);

            DateTime currentDate = DateTime.Now;
            DateTime thresholdDate = currentDate.AddDays(-30);

            int orderCountForMonth = 0;

            foreach (var order in orders)
            {
                if (orderInstance.OrderDate >= thresholdDate)
                {
                    orderCountForMonth++;
                }

            }

            if (orderCountForMonth >= 22)
                return true;
            return false;
        }
        public int GetCoffeesWithHighestPrice()
        {
            List<Coffees> Coffees = orderInstance.CoffeesOrderList;
            if (Coffees == null || Coffees.Count == 0)
            {
                return 0;
            }

            var CoffeesWithHighestPrice = Coffees.OrderByDescending(c => c.CoffeesPrice).FirstOrDefault();

            return CoffeesWithHighestPrice.CoffeesPrice;
        }


        public void SaveAllOrders()
        {
            List<Orders> orderList = GetAllOrders();
            orderInstance.OrderDate = DateTime.Now;
            orderList.Add(orderInstance);
            orderInstance = new Orders();
            orderInstance.AddInsList = new List<AddIns>();
            orderInstance.CoffeesOrderList = new List<Coffees>();
            string appOrdersFilePath = Func.GetAppOrdersFilePath();
            var json = JsonSerializer.Serialize(orderList);
            File.WriteAllText(appOrdersFilePath, json);
            Debug.WriteLine(appOrdersFilePath);

        }
    }
}
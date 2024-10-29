using Delivery.Domain.Entities;

namespace Delivery.Domain.Seeder.Seeds.Demo;

public class DemoHelper
{
    private static readonly Random MyRandom = new Random();
    private const int Count = 10;

    public IEnumerable<Order> GetOrders()
    {
        var orders = new List<Order>();
        for (int i = 0; i < Count; i++)
        {
            orders.Add(new Order()
            {
                OrderId = Guid.NewGuid(),
                District = RandomDistrict(),
                Weight = RandomWeight(),
                OrderDate = RandomDate(),
            });
        }
        return orders;
    }
   
    private static string RandomDistrict()
    {
        string[] districts = ["ленинский", "советский","центральный","коминтерновский"];
        return districts[MyRandom.Next(districts.Length)];
    }
    
    private static float RandomWeight()
    {
        const float maxWeight = 10f;
        return (float)(MyRandom.NextDouble() * maxWeight);
    }
    
    private static DateTime RandomDate()
    {
        var startDate = new DateTime(2024,10,1);
        var endDate = new DateTime(2024, 10, 29);
        
        return startDate.AddDays(MyRandom.Next((endDate - startDate).Days)).AddHours(MyRandom.Next(0, 24)).AddMinutes(MyRandom.Next(0, 60)).ToUniversalTime();
    }
}
namespace CTraderMVC.Models
{
    public class View
    {
        public class ZonesViewModel
        {
            public DateTime? TimeS { get; set; }

            public string ZoneType { get; set; }

            public bool ZoneStatus { get; set; }
        }

        public class Orders
        {
            public int orderId { get; set; }

            public int zoneId { get; set; }

            public string zoneType { get; set; }

            public string symbol { get; set; }

            public double entryPrice { get; set; }

            public DateTime entryTime { get; set; }

            public double stopLoss { get; set; }

            public double takeProfit { get; set; }

            public int pips { get; set; }
        }
    }
}

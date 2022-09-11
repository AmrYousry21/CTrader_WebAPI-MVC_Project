using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Web_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetALLZones()
        {
            string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";

            List<Models.Orders> orders = new List<Models.Orders>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        var order = new Models.Orders
                        {
                            orderId = reader.GetInt32(0),
                            zoneId = reader.GetInt32(1),
                            zoneType = reader.GetString(2),
                            symbol = reader.GetString(3),
                            entryPrice = reader.GetDouble(4),
                            entryTime = reader.GetDateTime(5),
                            stopLoss = reader.GetDouble(6),
                            takeProfit = reader.GetDouble(7),
                            pips = reader.GetInt32(8),

                        };

                        orders.Add(order);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Always call the Close method when you have finished using the DataReader object.
                reader.Close();

            }

            return Ok(orders);
        }
    }
}

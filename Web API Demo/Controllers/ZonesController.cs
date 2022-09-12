using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Web_API_Demo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace CTraderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ZonesController : ControllerBase
    {

        private readonly IConfiguration Configuration;

        public ZonesController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        


        [HttpGet]
        [Authorize]
        public IActionResult GetALLZones()
        {
            string connectionString = this.Configuration.GetConnectionString("MyConn");
            List<Zones> zones = new List<Zones>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Indecision_Candles", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        var zone = new Zones
                        {
                            ID = reader.GetInt32(0),
                            TimeS = reader.GetDateTime(1),
                            Zonetype = reader.GetString(2),
                            ZoneStatus = reader.GetBoolean(3)
                        };

                        zones.Add(zone);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Always call the Close method when you have finished using the DataReader object.
                reader.Close();

            }

            return Ok(zones);
        }

        [HttpPost]
        public void AddNewZone([FromBody] Zones zone)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConn");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string commandText = $"INSERT INTO indecision_candles (TimeS, ZoneType, ZoneStatus) VALUES ('{DateTime.Now}', '{zone.Zonetype}', {(zone.ZoneStatus ? 1 : 0)})";
                SqlCommand command = new SqlCommand(commandText, conn);

                conn.Open();

                command.ExecuteNonQuery();

                conn.Close();

            }
        }

        [HttpDelete("{id}")]

        public void Delete(int id)
        {

            string connectionString = this.Configuration.GetConnectionString("MyConn");
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string commandText = "DELETE FROM indecision_candles WHERE ID = @id";

                SqlCommand command = new SqlCommand(commandText, conn);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                conn.Open();

                command.ExecuteNonQuery();

                conn.Close();



            }
        }

        [HttpPut("{id}")]

        public void Update(int id, [FromBody] Zones zone)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConn");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string commandText = $"UPDATE indecision_candles SET Zonetype = '{ zone.Zonetype }', ZoneStatus = { (zone.ZoneStatus ? 1 : 0) } WHERE ID = { id }";

                SqlCommand command = new SqlCommand(commandText, conn);

                conn.Open();

                command.ExecuteNonQuery();

                conn.Close();
            }
        }
        [HttpGet("{ID}")]
        public IActionResult GetZone(int ID)
        {
            Zones zone = new Zones();
            string connectionString = this.Configuration.GetConnectionString("MyConn");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                

                string commandText = "SELECT * FROM indecision_candles WHERE ID = @id";
                conn.Open();
                SqlCommand command = new SqlCommand(commandText, conn);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = ID;

                SqlDataReader reader = command.ExecuteReader();

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        zone = new Zones
                        {
                            ID = reader.GetInt32(0),
                            TimeS = reader.GetDateTime(1),
                            Zonetype = reader.GetString(2),
                            ZoneStatus = reader.GetBoolean(3)
                        };
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Always call the Close method when you have finished using the DataReader object.
                reader.Close();

            }

            return Ok(zone);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace CTraderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ZonesController : ControllerBase
    {
       
        string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";

        [HttpGet]
        public string GetALLZones() 
        {
            
           using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM indecision_candles" , connectionString);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                    conn.Close();
                }

                else
                {
                    return "No data found";
                    conn.Close();
                }
                
            }

        }

        [HttpGet("{id}")]
        public string  GetZoneStatus(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM indecision_candles WHERE ZoneStatus = " + id, connectionString);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                    conn.Close();
                }

                else
                {
                    return "No data found";
                    conn.Close();
                }

            }
        }

        [HttpPut]
        public void AddNewZone()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "AddNewZone",
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                
            }
        }

        [HttpDelete("{id}")]

        public string  Delete(int id)
        {
            return "";
        }

    }
}

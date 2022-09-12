using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API_Demo.Models;

namespace Web_API_Demo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public AuthenticationController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        
      
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] User user)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConn");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE UserName = '{user.UserName}' AND Password = '{user.Password}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                User userDb = null;

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        userDb = new User
                        {
                            UserName = reader.GetString(1),
                        };

                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                if (userDb is null)
                {
                    return Unauthorized();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                      {
                        new Claim(ClaimTypes.Name, userDb.UserName)
                      }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public void InsertNewUser([FromBody] User user)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConn");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES ('{user.UserName}','{user.Password}')", conn);
                command.ExecuteNonQuery();
            }
               
        }
    }
}

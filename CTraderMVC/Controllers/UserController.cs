using CTraderMVC.Models;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestSharp;
using System.Net;

namespace CTraderMVC.Controllers
{
    public class UserController : Controller
    {
        string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";
        private readonly RestClient _client;

        public UserController()
        {
            _client = new RestClient();
        }

        //public List<User> OnGet()
        //{
        //    List<User> userIds = new List<User>();
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand("SELECT * FROM Users", conn);
        //        conn.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        // Check if the DataReader has any row.
        //        if (reader.HasRows)
        //        {
        //            // Obtain a row from the query result.
        //            while (reader.Read())
        //            {
        //                var userAccount = new User
        //                {
        //                    PersonId = reader.GetInt32(0),
        //                    UserName = reader.GetString(1),
        //                    Password = reader.GetString(2)
        //                };

        //                userIds.Add(userAccount);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("No rows found.");
        //        }
        //        // Always call the Close method when you have finished using the DataReader object.
        //        reader.Close();
        //    }
        //    return userIds;
        //}

        
        public ActionResult Login(User user)
        {
            var request = new RestRequest("https://localhost:7064/api/Authentication", Method.Post);
            request.AddBody(user);
            var result = _client.Execute(request);
            

            if (result.StatusCode == HttpStatusCode.OK) 
            {
                return RedirectToAction("Zones", "ZonesOrders");
            }

            else 
            {
                return RedirectToAction("Home", "User");
            }
        }

        // GET: Users
        public ActionResult Home()
        {
            return View();
        }
    }
}

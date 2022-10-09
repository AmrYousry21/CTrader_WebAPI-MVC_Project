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

        public ActionResult LoginPage() 
        {
            return View();
        }
        public ActionResult Login(User user)
        {
            var request = new RestRequest("https://ctraderwebapi20221009122731.azurewebsites.net/api/Authentication/login", Method.Post);
            request.AddBody(user);
            var result = _client.Execute(request);


            if (result.StatusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("Token", result.Content);
                return RedirectToAction("Zones", "Zone");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("Token", "");

            return RedirectToAction("Index", "Home");
        }

        // GET: Users

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult InsertNewUser(User user)
        {
            var request = new RestRequest("https://ctraderwebapi20221009122731.azurewebsites.net/api/Authentication/register", Method.Post);
            request.AddBody(user);
            var result = _client.Execute(request);

            return RedirectToAction("LoginPage");
        }
    }
}

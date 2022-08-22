using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;



namespace WebAppMVC.Controllers
{
    public class UserController : Controller
    {
       

        Uri baseAddress = new Uri("http://localhost:14927/api/Home");
        HttpClient client;

        public UserController(Uri baseAddress, HttpClient client)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }   

        public IActionResult Index()
        {
            List <ZonesViewModel> zones = new List<ZonesViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode) 
            {
                string data = response.Content.ReadAsStringAsync().Result;
                zones = JsonConvert.DeserializeObject<List<ZonesViewModel>>(data);
            }
            return View(zones);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
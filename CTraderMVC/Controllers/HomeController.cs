using CTraderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using RestSharp;
using CTraderMVC.Models;

namespace CTraderMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new RestClient();
        }

        public IActionResult Index()
        {
            var request = new RestRequest("https://localhost:7064/api/Zones", Method.Get);
            var result = _client.Execute(request);
            List<ZonesViewModel> zones = JsonConvert.DeserializeObject<List<ZonesViewModel>>(result.Content);

            return View(zones);
        }

        public IActionResult Orders()
        {
            var request = new RestRequest("https://localhost:7064/api/Order", Method.Get);
            var result = _client.Execute(request);
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(result.Content);

            return View(orders);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
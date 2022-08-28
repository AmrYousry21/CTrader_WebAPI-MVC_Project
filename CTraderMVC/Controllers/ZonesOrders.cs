using CTraderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using RestSharp;
using CTraderMVC.Models;
using static CTraderMVC.Controllers.User;

namespace CTraderMVC.Controllers
{
    
    public class ZonesOrders : Controller
    {
        private readonly ILogger<ZonesOrders> _logger;
        private readonly RestClient _client;
        
        
        
        public ZonesOrders(ILogger<ZonesOrders> logger)
        {
            _logger = logger;
            _client = new RestClient();
        }

        public IActionResult Zones()
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
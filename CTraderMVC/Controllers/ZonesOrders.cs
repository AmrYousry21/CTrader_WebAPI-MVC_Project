using CTraderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using RestSharp;
using CTraderMVC.Models;

namespace CTraderMVC.Controllers
{
    
    public class ZonesOrders : Controller
    {
        private readonly ILogger<ZonesOrders> _logger;
        private readonly RestClient _client;

        UserState user = UserState.GetUserState();

        public ZonesOrders(ILogger<ZonesOrders> logger)
        {
            _logger = logger;
            _client = new RestClient();
        }

        public IActionResult Zones()
        {
            List<ZonesViewModel> zones = new List<ZonesViewModel>();

           
            if (user is not null && user.isLoggedIn)
            {
                var request = new RestRequest("https://localhost:7064/api/Zones", Method.Get);
                var result = _client.Execute(request);
                 zones = JsonConvert.DeserializeObject<List<ZonesViewModel>>(result.Content);
            }

            return View(zones.Any() ? zones : null);
        }

        public IActionResult Orders()
        {
            List<Order> orders = new List<Order>();
            
            
            if (user is not null && user.isLoggedIn) 
            {
                var request = new RestRequest("https://localhost:7064/api/Order", Method.Get);
                var result = _client.Execute(request);
                orders = JsonConvert.DeserializeObject<List<Order>>(result.Content);
            }

            return View(orders.Any() ? orders :null);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
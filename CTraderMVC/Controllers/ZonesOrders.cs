using CTraderMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using RestSharp;
using CTraderMVC.Models;
using System.ComponentModel;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Net;

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
            List<Zones> zones = new List<Zones>();

            var request = new RestRequest("https://localhost:7064/api/Zones", Method.Get);
           
            request.AddHeader("Authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYW1yeW91c3J5IiwibmJmIjoxNjYyODU3MzUwLCJleHAiOjE2NjI4NTc5NTAsImlhdCI6MTY2Mjg1NzM1MH0.65oo2iUaOJqLF9EiDx_U3x1eDbJiZbnWQwRQU1a5LKM");
            var result = _client.Execute(request);
            zones = JsonConvert.DeserializeObject<List<Zones>>(result.Content);

            return View(zones);
        }

        public IActionResult Orders()
        {
            List<Order> orders = new List<Order>();

            var request = new RestRequest("https://localhost:7064/api/Order", Method.Get);
            
            request.AddHeader($"Authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYW1yeW91c3J5IiwibmJmIjoxNjYyODU3MzUwLCJleHAiOjE2NjI4NTc5NTAsImlhdCI6MTY2Mjg1NzM1MH0.65oo2iUaOJqLF9EiDx_U3x1eDbJiZbnWQwRQU1a5LKM");
            var result = _client.Execute(request);
            orders = JsonConvert.DeserializeObject<List<Order>>(result.Content);

            return View(orders);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Delete(int id)
        {
            var request = new RestRequest("https://localhost:7064/api/Zones/" + id, Method.Delete);
            var result = _client.Execute(request);

            return RedirectToAction("Zones", Zones);
        }

        public ActionResult ViewZone(int ID)
        {
            Zones zone = new Zones();
            var request = new RestRequest("https://localhost:7064/api/Zones/" + ID, Method.Get);
            var result = _client.Execute(request);
            zone = JsonConvert.DeserializeObject<Zones>(result.Content);

            return View(zone);
        }
        public ActionResult Update(int id)
        {
            Zones zone = new Zones();

            var request = new RestRequest("https://localhost:7064/api/Zones/" + id, Method.Get);

            var result = _client.Execute(request);

            return View("Update", zone);
        }

        [HttpPost]
        public ActionResult SaveZoneChanges(Zones model)
        {
            if (model is not null)
            {
                var request = new RestRequest("https://localhost:7064/api/Zones/" + model.ID, Method.Put).AddBody(model);

                _client.Execute(request);
            }

            return RedirectToAction("Zones");
        }

        public ActionResult CreateZone()
        {
            return View();
        }

        public ActionResult InsertNewZone(Zones model)
        {
            if (model is not null)
            {
                var request = new RestRequest("https://localhost:7064/api/Zones/", Method.Put).AddBody(model);

                _client.Execute(request);
            }

            return RedirectToAction("Zones");
        }
    }
}
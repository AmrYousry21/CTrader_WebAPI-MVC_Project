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

    public class Zone : Controller
    {
        private readonly ILogger<Zone> _logger;
        private readonly RestClient _client;

        public Zone(ILogger<Zone> logger)
        {
            _logger = logger;
            _client = new RestClient();
        }

        public IActionResult Zones()
        {
            List<Zones> zones = new List<Zones>();

            var request = new RestRequest("https://localhost:7064/api/Zones", Method.Get);
            var token = HttpContext.Session.GetString("Token") ?? "";
            var tokenWrapper = "Bearer " + token.Replace("\"", "");
            request.AddHeader("Authorization", tokenWrapper);
            var result = _client.Execute(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                zones = JsonConvert.DeserializeObject<List<Zones>>(result.Content);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(zones);
        }

        public IActionResult Orders()
        {
            List<Order> orders = new List<Order>();

            var request = new RestRequest("https://localhost:7064/api/Order", Method.Get);
            var token = HttpContext.Session.GetString("Token") ?? "";
            var tokenWrapper = "Bearer " + token.Replace("\"", "");
            request.AddHeader("Authorization", tokenWrapper);
            var result = _client.Execute(request);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                orders = JsonConvert.DeserializeObject<List<Order>>(result.Content);
            }
            else if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "Home");
            }


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
            var token = HttpContext.Session.GetString("Token");
            var tokenWrapper = "Bearer " + token.Replace("\"", "");
            request.AddHeader("Authorization", tokenWrapper);
            var result = _client.Execute(request);

            return RedirectToAction("Zones", Zones);
        }

        public ActionResult ViewZone(int ID)
        {
            Zones zone = new Zones();
            var request = new RestRequest("https://localhost:7064/api/Zones/" + ID, Method.Get);
            var token = HttpContext.Session.GetString("Token");
            var tokenWrapper = "Bearer " + token.Replace("\"", "");
            request.AddHeader("Authorization", tokenWrapper);
            var result = _client.Execute(request);
            zone = JsonConvert.DeserializeObject<Zones>(result.Content);

            return View(zone);
        }
        public ActionResult Update(int id)
        {
            var request = new RestRequest("https://localhost:7064/api/Zones/" + id, Method.Get);
            var token = HttpContext.Session.GetString("Token");
            var tokenWrapper = "Bearer " + token.Replace("\"", "");
            request.AddHeader("Authorization", tokenWrapper);
            var result = _client.Execute(request);

            var zone = JsonConvert.DeserializeObject<Zones>(result.Content);
            return View("Update", zone);
        }

        [HttpPost]
        public ActionResult SaveZoneChanges(Zones model)
        {
            if (model is not null)
            {
                // Initialize PUT Request
                var request = new RestRequest("https://localhost:7064/api/Zones/" + model.ID, Method.Put);

                // Retrieve Token From Session
                var token = HttpContext.Session.GetString("Token");
                var tokenWrapper = "Bearer " + token.Replace("\"", "");

                // Attach Token and Body
                request.AddHeader("Authorization", tokenWrapper);
                request.AddBody(model);

                // Execute Request
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
                var request = new RestRequest("https://localhost:7064/api/Zones/", Method.Post).AddBody(model);

                _client.Execute(request);
            }

            return RedirectToAction("Zones");
        }


    }
}
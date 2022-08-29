﻿using CTraderMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CTraderMVC.Controllers
{
    public class UserController : Controller
    {
        public List<User> OnGet()
        {
            string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";


            List<User> userIds = new List<User>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        var userAccount = new User
                        {
                            PersonId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2)
                        };

                        userIds.Add(userAccount);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Always call the Close method when you have finished using the DataReader object.
                reader.Close();
            }
            return userIds;
        }

        [HttpPost]
        public ActionResult Login(UserInput input)
        {
            User userToLogin = null;

            List<User> users = OnGet();
            foreach (User user in users)
            {
                if (user.UserName == input.UserNameInput && user.Password == input.UserPasswordInput)
                {
                    userToLogin = user;
                    break;
                }
            }

            if (userToLogin is not null)
            {
                UserState.PostUserState(userToLogin.UserName);

                return RedirectToAction("Zones", "ZonesOrders");
            }
            if (userToLogin is not null) 
            {
                UserState.PostUserState(userToLogin.UserName);

                return RedirectToAction("Orders", "ZonesOrders");
            }
            else
            {
                return View("Home");
            }
        }

        // GET: Users
        public ActionResult Home()
        {
            return View();
        }
    }
}

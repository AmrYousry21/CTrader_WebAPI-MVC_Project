using CTraderMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
namespace CTraderMVC.Controllers
{
    public class User : Controller
    {
        
        public class UserModel 
        {
            public bool Credential = false;
            public List<Users> OnGet() 
            {
                string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";
                

                List<Users> userIds = new List<Users>();
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
                            var userAccount = new Users
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

            public bool OnPost() 
            {
                UserInput input = new UserInput();
                

                foreach (Users user in OnGet()) 
                {
                    if(user.UserName == input.UserNameInput && user.Password == input.UserPasswordInput) 
                    {
                        Credential = true;
                        return Credential;
                    }

                    else 
                    {
                        return Credential;
                    }
                }
                return Credential;
            }
        }

        // GET: Users
        public ActionResult Home()
        {
            return View();
        }

      
    }
}

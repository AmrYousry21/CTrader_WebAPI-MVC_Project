using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTraderMVC.Models
{
    public class UserInput
    {
        [BindProperty]
        [Display(Name ="User Name")]
        public string UserNameInput { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        public string UserPasswordInput { get; set; }
    }
}

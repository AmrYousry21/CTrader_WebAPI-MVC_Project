using Google.Apis.Admin.Directory.directory_v1.Data;
using System.ComponentModel.DataAnnotations;

namespace CTraderMVC.Models
{
    public class User
    {
        public int PersonId { get; set; }
        [Required]
        [Display(Name= "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

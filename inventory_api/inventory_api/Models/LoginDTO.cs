using System.ComponentModel.DataAnnotations;

namespace inventory_api.Models
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace inventory_api.Models
{
    public class UserRegister
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }    
    }
}

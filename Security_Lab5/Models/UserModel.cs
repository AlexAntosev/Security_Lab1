using System.ComponentModel.DataAnnotations;

namespace Security_Lab5.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Token { get; set; }
        
        public string CreditCard { get; set; }
    }
}
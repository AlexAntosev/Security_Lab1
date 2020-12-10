using System;
using System.ComponentModel.DataAnnotations;

namespace Security_Lab5.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        
        public string PasswordSalt { get; set; }
        
        public string CreditCardHash { get; set; }
        
        public string CreditCardSalt { get; set; }
    }
}
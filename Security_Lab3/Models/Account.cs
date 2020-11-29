using System;

namespace Security_Lab3.Models
{
    public class Account
    {
        public string Id { get; set; }
        
        public long Money { get; set; }
        
        public DateTime DeletionTime { get; set; }
    }
}
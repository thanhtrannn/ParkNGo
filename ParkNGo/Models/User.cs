using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string SecretQuestion1 { get; set; }
        public string SecretAnswer1 { get; set; }
        public string SecretQuestion2 { get; set; }
        public string SecretAnswer2 { get; set; }
        public string DisplayUrl { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public int? PaymentId { get; set; }
        public int? PropertyId { get; set; }
    }
}

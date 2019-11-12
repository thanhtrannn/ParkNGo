using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Expiration { get; set; }
        public string Ccv { get; set; }
    }
}

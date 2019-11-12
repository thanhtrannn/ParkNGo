using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyOwner { get; set; }
        public string PurchaserId { get; set; }
        public string TimeSlot { get; set; }
        public double Amount { get; set; }
        public int PaymentId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}

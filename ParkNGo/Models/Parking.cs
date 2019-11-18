using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class Parking
    {
        public int ParkingId { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public byte[] Image { get; set; }
        public double? Rating { get; set; }
        public int? CommentsId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string IntersectionFrom { get; set; }
        public string IntersectionTo { get; set; }
        public double? LongTo { get; set; }
        public double? LatTo { get; set; }
        public TimeSpan? HoursFrom { get; set; }
        public TimeSpan? HoursTo { get; set; }
        public decimal? Cost { get; set; }
        public string Rate { get; set; }
        public string Additional { get; set; }
    }
}

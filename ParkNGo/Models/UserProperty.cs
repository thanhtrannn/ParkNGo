using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class UserProperty
    {
        public int PropertyId { get; set; }
        public string Username { get; set; }
        public string PropAddress { get; set; }
        public string PropCity { get; set; }
        public string PropProvinceId { get; set; }
        public string PropPostalCode { get; set; }
        public byte[] ImageUrl { get; set; }
        public string AvailabilityId { get; set; }
        public double? Rating { get; set; }
        public int? CommentsId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}

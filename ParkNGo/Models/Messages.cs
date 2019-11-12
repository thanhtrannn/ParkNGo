using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class Messages
    {
        public int MessageId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public string Attachments { get; set; }
    }
}

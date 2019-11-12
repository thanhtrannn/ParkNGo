using System;
using System.Collections.Generic;

namespace ParkNGo.Models
{
    public partial class CommentRating
    {
        public int CommentId { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }
    }
}

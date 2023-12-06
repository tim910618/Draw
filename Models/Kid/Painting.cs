
using System;

namespace backend.Models
{
    public class Painting
    {
        public Guid painting_id { get; set; }
        public Guid kid_id { get; set; }
        public string picture { get; set; }
        public string result { get; set; }
        public DateTime create_time { get; set; }
    }
}
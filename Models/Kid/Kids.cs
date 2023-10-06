
using System;

namespace backend.Models{
    public class Kids{
        public string email {get;set;}
        public Guid kid_id{get;set;}
        public string name{get;set;}
        public DateTime birth{get;set;}
        public bool gender{get;set;}
        public string image{get;set;}
    }
}
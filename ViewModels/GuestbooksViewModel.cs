using System;

namespace backend.ViewModels
{
    public class GuestbooksViewModel
    {
        public string Id{get;set;}
        public string Name{get;set;}
        public string Content{get;set;}
        public string CreateTime{get;set;}
        public string Reply{get;set;}
        public string ReplyTime{get;set;}
    }
}
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace backend.util
{
    public class appSettings
    {
        public string jwt_secret {get;set;}
        public string db {get;set;}
        public string hash_key { get; set; }
        public string UploadPath{get;set;}
    }
}
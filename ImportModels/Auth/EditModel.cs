
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace backend.ImportModels.Register
{
    public class EditModel
    {
        public string name{get;set;}
        public string phone{get;set;}
        public string password{get;set;}
        public IFormFile image{get;set;}
    }
}
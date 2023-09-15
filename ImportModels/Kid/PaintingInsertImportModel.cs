
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace backend.ImportModels{
    
    public class PaintingInsertImportModel{
        public string kid_id{get;set;}
        public IFormFile picture{get;set;}
        public string result{get;set;}
    }
}
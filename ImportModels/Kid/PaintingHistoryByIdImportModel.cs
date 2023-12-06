
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace backend.ImportModels
{
    public class PaintingHistoryByIdImportModel
    {
        public string painting_id { get; set; }
    }
}
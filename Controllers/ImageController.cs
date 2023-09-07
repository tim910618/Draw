using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



using backend.ViewModels;
using System.IO;

namespace backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly ILogger<ImageController> _logger;
    public ImageController(ILogger<ImageController> logger)
    {
      _logger = logger;
    }

    // GET api/Image?FileName={FileName}
    [HttpGet]
    public IActionResult Image(string FileName)
    {
      try
      {
        string ResultExtension = string.Empty;
        switch(Path.GetExtension(FileName))
        {
          case ".jpg":
          case ".jpeg":
              ResultExtension = "image/jpeg";
              break;
          case ".png":
              ResultExtension = "image/png";
              break;
          default:
              break;
        }
        Byte[] b = System.IO.File.ReadAllBytes(@$"./{FileName}");
        return File(b, ResultExtension);
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<string>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }


  }
}

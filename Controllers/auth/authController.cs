using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using backend.ViewModels;
using backend.Middleware.jwt;

namespace backend.Controllers.auth
{
  [ApiController]
  [Route("api/[controller]")]

  public class authController : ControllerBase
  {

    private readonly ILogger<authController> _logger;
    private readonly IUserService _service;
    public authController(ILogger<authController> logger, IUserService service)
    {
      _logger = logger;
      _service = service;
    }  

    [HttpPost]
    [Route("login")]
    public IActionResult login([FromBody] AuthenticateRequest model)
    {
      try
      {
        AuthenticateResponse Result = _service.Authenticate(model);
        if (Result == null)
        {
          return BadRequest(new ResultViewModel<AuthenticateResponse>
          {
            isSuccess = false,
            message = "帳號或密碼輸入錯誤",
            Result = null,
          });
        }
        else
        {
          return Ok(new ResultViewModel<AuthenticateResponse>
          {
            isSuccess = true,
            message = string.Empty,
            Result = Result,
          });
        }
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<AuthenticateResponse>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }
  }
}
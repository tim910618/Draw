
using System;
using backend.ImportModels.Register;
using backend.Middleware.jwt;
using backend.Middleware.jwt_t;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers.Ch09
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsController:ControllerBase
    {
        private readonly ILogger<ParentsController> _logger;
        private readonly IUserService_T _service;
        public ParentsController(ILogger<ParentsController> logger,IUserService_T service)
        {
            _logger=logger;
            _service=service;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] loginViewModel model)
        {
            try
            {
                AuthenticateResponse Result=_service.Authenticate(model);
                if(Result == null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="帳號密碼輸入錯誤",
                        Result=null,
                    });
                }
                else
                {
                    return Ok(new ResultViewModel<AuthenticateResponse>
                    {
                        isSuccess=true,
                        message=string.Empty,
                        Result=Result,
                    });
                }
            }
            catch(Exception e)
            {
                return NotFound(new ResultViewModel<string>
                {
                    isSuccess=false,
                    message=e.Message.ToString(),
                    Result=null,
                });
            }
        }
    }
}


using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;
using System.Linq;
using System.IO;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScaleController : ControllerBase
    {
        private readonly ILogger<GuestbooksController> _logger;
        private readonly KidService _service;
        public ScaleController(ILogger<GuestbooksController> logger, KidService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region 填寫量表資料
        [HttpPost]
        [Route("WriteScale")]
        public IActionResult WriteScale()
        {
            try
            {
                List<KidViewModel> Result = _service.GetDataList();
                return Ok(new ResultViewModel<List<KidViewModel>>
                {
                    isSuccess = false,
                    message = string.Empty,
                    Result = Result
                });
            }
            catch (Exception e)
            {
                return NotFound(new ResultViewModel<string>
                {
                    isSuccess = false,
                    message = e.Message.ToString(),
                    Result = null
                });
            }
        }
        #endregion
    }
}
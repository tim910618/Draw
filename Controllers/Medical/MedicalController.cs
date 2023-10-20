

using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalController : ControllerBase
    {
        private readonly ILogger<MedicalController> _logger;
        private readonly GuestbooksService _service;
        public MedicalController(ILogger<MedicalController> logger, GuestbooksService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region 全部
        [HttpGet]
        [Route("List")]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ResultViewModel<List<GuestbooksViewModel>>
                {
                    isSuccess = false,
                    message = string.Empty,
                    Result = _service.GetDataList()
                });
            }
            catch (Exception e)
            {
                return NotFound(new ResultViewModel<List<GuestbooksViewModel>>
                {
                    isSuccess = false,
                    message = e.Message.ToString(),
                    Result = null
                });
            }
        }
        #endregion

        #region 單筆
        [HttpGet]
        public ActionResult Get(string id)
        {
            try
            {
                return Ok(new ResultViewModel<GuestbooksViewModel>
                {
                    isSuccess = false,
                    message = string.Empty,
                    Result = _service.GetDataById(id)
                });
            }
            catch (Exception e)
            {
                return NotFound(new ResultViewModel<GuestbooksViewModel>
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
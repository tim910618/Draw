

using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;
using System.IO;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaintingController : ControllerBase
    {
        private readonly ILogger<GuestbooksController> _logger;
        private readonly PaintingService _service;
        public PaintingController(ILogger<GuestbooksController> logger, PaintingService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region 新增
        [HttpPost]
        public IActionResult Insert([FromForm] PaintingInsertImportModel model)
        {
            try
            {
                string fileExtension = Path.GetExtension(model.pic.FileName);
                if (fileExtension.ToLower() == ".csv")
                {
                    _service.Insert(model);
                    return Ok(new ResultViewModel<GuestbooksViewModel>
                    {
                        isSuccess = false,
                        message = "新增成功",
                        Result = null
                    });
                }
                else
                {
                    return BadRequest(new ResultViewModel<GuestbooksViewModel>
                    {
                        isSuccess = false,
                        message = "檔案型態錯誤",
                        Result = null
                    });
                }
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
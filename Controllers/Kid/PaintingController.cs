

using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using backend.util;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaintingController : ControllerBase
    {
        private readonly ILogger<GuestbooksController> _logger;
        private readonly PaintingService _paintingservice;
        public PaintingController(ILogger<GuestbooksController> logger, PaintingService paintingservice)
        {
            this._logger = logger;
            this._paintingservice = paintingservice;
        }

        #region 上傳圖片
        [HttpPost]
        public IActionResult Insert([FromForm] PaintingInsertImportModel model)
        {
            try
            {
                string fileExtension = Path.GetExtension(model.picture.FileName);
                if (IsImageFile(fileExtension))
                {
                    _paintingservice.Insert(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "新增成功",
                        Result = null
                    });
                }
                else
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "檔案型態錯誤",
                        Result = null
                    });
                }
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
        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(fileExtension.ToLower());
        }
    }
}
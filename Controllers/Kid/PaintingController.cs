

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
        private readonly PaintingService _service;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;
        public PaintingController(ILogger<GuestbooksController> logger, PaintingService service, IHttpContextAccessor HttpContextAccessor)
        {
            this._logger = logger;
            this._service = service;
            this._HttpContextAccessor = HttpContextAccessor ??
                throw new ArgumentNullException(nameof(HttpContextAccessor));

            // 解析Token
            tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);

            // 抓取Token的PayLoad
            var PayLoad = TokenEnCode.GetPayLoad();

            // 抓取PayLoad中的account
            this._AccountNumber = PayLoad is null ? "" : PayLoad["email"].ToString();
        }

        #region 上傳圖片
        [HttpPost]
        public IActionResult Insert([FromForm] PaintingInsertImportModel model)
        {
            try
            {
                if (_AccountNumber != "")
                {
                    string fileExtension = Path.GetExtension(model.picture.FileName);
                    if (IsImageFile(fileExtension))
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
                else
                {
                    return BadRequest(new ResultViewModel<GuestbooksViewModel>
                        {
                            isSuccess = false,
                            message = "權限不足",
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
        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(fileExtension.ToLower());
        }
    }
}
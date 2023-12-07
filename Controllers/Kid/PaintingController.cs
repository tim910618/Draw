

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
        [Route("Insert")]
        public IActionResult Insert([FromForm] PaintingInsertImportModel model)
        {
            try
            {
                string fileExtension = Path.GetExtension(model.picture.FileName);
                if (IsImageFile(fileExtension))
                {
                    KidViewModelID result = _paintingservice.Insert(model);
                    return Ok(new ResultViewModel<KidViewModelID>
                    {
                        isSuccess = true,
                        message = "新增成功",
                        Result = result
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
        #region 歷史紀錄(ALL)
        [HttpPost]
        [Route("History")]
        public IActionResult History([FromBody] PaintingHistoryImportModel model)
        {
            try
            {
                List<KidHistoryViewModel> Result = _paintingservice.History(model);
                return Ok(new ResultViewModel<List<KidHistoryViewModel>>
                {
                    isSuccess = true,
                    message = "查詢成功",
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
        #region 歷史紀錄(One)
        [HttpPost]
        [Route("HistoryOne")]
        public IActionResult HistoryOne([FromBody] PaintingHistoryByIdImportModel model)
        {
            try
            {
                KidHistoryOneViewModels Result = _paintingservice.HistoryById(model);
                return Ok(new ResultViewModel<KidHistoryOneViewModels>
                {
                    isSuccess = true,
                    message = "查詢成功",
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
        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(fileExtension.ToLower());
        }
    }
}
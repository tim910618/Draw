

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
    public class KidController : ControllerBase
    {
        private readonly ILogger<GuestbooksController> _logger;
        private readonly KidService _service;
        public KidController(ILogger<GuestbooksController> logger, KidService service)
        {
            this._logger = logger;
            this._service = service;
        }
        public class ResultViewModelWithImage : ResultViewModel<KidViewModel>
        {
            public string ImageBase64 { get; set; }
        }

        #region 顯示全部小孩資料
        [HttpGet]
        [Route("AllKid")]
        public IActionResult Kids()
        {
            try
            {
                List<KidViewModel> Result = _service.GetDataList();
                return Ok(new ResultViewModel<List<KidViewModel>>
                {
                    isSuccess = true,
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
        #region 顯示單個小孩資料
        [HttpGet]
        [Route("OnlyKid")]

        public IActionResult Kid(KidOnlyModel Data)
        {
            try
            {
                KidViewModel Result = _service.GetDataByKid_Id(Data.kid_id);
                /*string ResultExtension = string.Empty;
                Byte[] b = System.IO.File.ReadAllBytes(@$"./{Result.image}");
                switch (Path.GetExtension(Result.image))
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
                return File(b, ResultExtension);*/
                Byte[] b = System.IO.File.ReadAllBytes(@$"./{Result.image}");
                string imageBase64 = Convert.ToBase64String(b);
                var resultViewModel = new ResultViewModelWithImage
                {
                    isSuccess = true,
                    message = string.Empty,
                    Result = Result,
                    ImageBase64 = imageBase64
                };
                return Ok(resultViewModel);
                /*return Ok(new ResultViewModel<KidViewModel>
                {
                    isSuccess = true,
                    message = string.Empty,
                    Result = Result
                });*/
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
        #region 新增小孩
        [HttpPost]
        public IActionResult Insert([FromBody] KidInsertImportModel model)
        {
            try
            {
                {
                    _service.Insert(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "新增成功",
                        Result = null,
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
        #region 修改
        [HttpPut]
        [Route("UpdateKid")]
        //名字 圖片
        public IActionResult Update([FromForm] KidEditModel model)
        {
            try
            {
                if (_service.GetDataByKid_Id(model.kid_id) != null)
                {
                    _service.Update(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "修改成功",
                        Result = null
                    });
                }
                else
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "查無此筆資料，修改失敗",
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

        //判斷圖片
        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(fileExtension.ToLower());
        }
    }
}
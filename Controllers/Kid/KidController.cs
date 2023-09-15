

using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;
using System.Linq;

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

        #region 顯示小孩資料
        [HttpGet]
        public IActionResult Kid()
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
        #region 新增小孩
        [HttpPost]
        public IActionResult Insert([FromBody] KidInsertImportModel model)
        {
            try
            {
                _service.Insert(model);
                return Ok(new ResultViewModel<string>
                {
                    isSuccess = false,
                    message = "新增成功",
                    Result = null
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









        #region 修改
        [HttpPut]
        public IActionResult Update([FromBody] GuestbooksUpdateModel model)
        {
            try
            {
                if (_service.GetDataById(model.Id) == null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "查無此筆資料，修改失敗",
                        Result = null
                    });
                }
                else
                {
                    _service.Update(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "修改成功",
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

        /*#region 刪除
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                if(_service.GetDataById(id)==null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="查無此筆資料，刪除失敗",
                        Result=null
                    });
                }
                else
                {
                    _service.Delete(id);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="刪除成功",
                        Result=null
                    });
                }
            }
            catch(Exception e)
            {
                return NotFound(new ResultViewModel<string>
                {
                    isSuccess=false,
                    message=e.Message.ToString(),
                    Result=null
                });
            }
        }
        #endregion*/
    }
}
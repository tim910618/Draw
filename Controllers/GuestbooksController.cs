

using System;
using System.Collections.Generic;
using backend.Services;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.ImportModels;

namespace backend.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestbooksController : ControllerBase
    {
        private readonly ILogger<GuestbooksController> _logger;
        private readonly GuestbooksService _service;
        public GuestbooksController(ILogger<GuestbooksController> logger,GuestbooksService service)
        {
            this._logger=logger;
            this._service=service;
        }
        
        #region 全部
        [HttpGet]
        [Route("List")]
        public IActionResult Get(){
            try
            {
                return Ok(new ResultViewModel<List<GuestbooksViewModel>>
                {
                    isSuccess=false,
                    message=string.Empty,
                    Result=_service.GetDataList()
                });
            }
            catch(Exception e)
            {
                return NotFound(new ResultViewModel<List<GuestbooksViewModel>>
                {
                    isSuccess=false,
                    message=e.Message.ToString(),
                    Result=null
                });
            }
        }
        #endregion
        
        #region 單筆
        [HttpGet]
        public ActionResult Get(string id){
            try
            {
                return Ok(new ResultViewModel<GuestbooksViewModel>
                {
                    isSuccess=false,
                    message=string.Empty,
                    Result=_service.GetDataById(id)
                });
            }
            catch(Exception e)
            {
                return NotFound(new ResultViewModel<GuestbooksViewModel>
                {
                    isSuccess=false,
                    message=e.Message.ToString(),
                    Result=null
                });
            }
        }
        #endregion
        
        #region 新增
        [HttpPost]
        public IActionResult Insert([FromBody] GuestbooksImportModel model)
        {
            try
            {
                _service.Insert(model);
                return Ok(new ResultViewModel<GuestbooksViewModel>
                {
                    isSuccess=false,
                    message="新增成功",
                    Result=null
                });
            }
            catch(Exception e)
            {
                return NotFound(new ResultViewModel<GuestbooksViewModel>
                {
                    isSuccess=false,
                    message=e.Message.ToString(),
                    Result=null
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
                if(_service.GetDataById(model.Id)==null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="查無此筆資料，修改失敗",
                        Result=null
                    });
                }
                else
                {
                    _service.Update(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="修改成功",
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
        #endregion
        #region 回覆
        [HttpPut]
        [Route("Reply")]
        public IActionResult Reply([FromBody] GuestbooksReplyModel model)
        {
            try
            {
                if(_service.GetDataById(model.Id)==null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="查無此筆資料，回覆失敗",
                        Result=null
                    });
                }
                else
                {
                    _service.Reply(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess=false,
                        message="回覆成功",
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
        #endregion
        #region 刪除
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
        #endregion
    }
}
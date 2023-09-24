

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
        private readonly ScaleService _scaleservice;
        private readonly PaintingService _paintingservice;
        public ScaleController(ILogger<GuestbooksController> logger, ScaleService scaleservice, PaintingService paintingservice)
        {
            this._logger = logger;
            this._scaleservice = scaleservice;
            this._paintingservice = paintingservice;
        }

        #region 填寫量表資料
        [HttpPost]
        [Route("WriteScale")]
        public IActionResult WriteScale([FromBody] ScaleImportModel model)
        {
            try
            {
                bool checkScale = _paintingservice.GetDataById(model.kid_id);
                if (checkScale)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "已填寫過該階段量表",
                        Result = null
                    });
                }
                else
                {
                    _scaleservice.Insert(model);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "填寫完畢",
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
        #region 量表紀錄
        [HttpGet]
        [Route("Scale")]
        public IActionResult Scale()
        {
            try
            {
                return Ok(new ResultViewModel<string>
                {
                    isSuccess = true,
                    message = "填寫完畢",
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
    }
}
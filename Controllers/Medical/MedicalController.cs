

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
        private readonly MedicalService _service;
        public MedicalController(ILogger<MedicalController> logger, MedicalService service)
        {
            this._logger = logger;
            this._service = service;
        }

        #region 全部
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(new ResultViewModel<List<MedicalViewModel>>
                {
                    isSuccess = false,
                    message = string.Empty,
                    Result = _service.GetDataList()
                });
            }
            catch (Exception e)
            {
                return NotFound(new ResultViewModel<List<MedicalViewModel>>
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
        public ActionResult Only(MedicalImportModel Model)
        {
            try
            {
                return Ok(new ResultViewModel<MedicalViewModel>
                {
                    isSuccess = false,
                    message = string.Empty,
                    Result = _service.GetDataById(Model)
                });
            }
            catch (Exception e)
            {
                return NotFound(new ResultViewModel<MedicalViewModel>
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



using backend.ViewModels;
using System.IO;
using backend.Services;
using backend.ImportModels;

namespace backend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SampleController : ControllerBase
  {
    //日誌記錄器介面
    private readonly ILogger<SampleController> _logger;
    private readonly sampleService _service;
    private readonly string _programname = "教學範例";

    public SampleController(ILogger<SampleController> logger, sampleService sampleService)
    {
        _logger = logger;
        _service = sampleService;
    }

    // GET api/Sample/List
    [HttpGet]
    [Route("List")]
    public IActionResult Get()
    {
      try
      {
        return Ok(new ResultViewModel<List<SampleViewModel>>
        {
          isSuccess = false,
          message = string.Empty,
          Result = _service.GetDataList(),
        });
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<List<SampleViewModel>>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }

    // GET api/Sample?id=2ac27d6d-9b98-4ba1-b77a-eb5549c264dd
    [HttpGet]
    public IActionResult Get(string id)
    {
      try
      {
        return Ok(new ResultViewModel<SampleViewModel>
        {
          isSuccess = false,
          message = string.Empty,
          Result = _service.GetDataById(id),
        });
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<SampleViewModel>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }

    // POST api/Sample
    [HttpPost]
    public IActionResult Post([FromBody] SampleImportModel model)
    {
      try
      {
        _service.Insert(model);
        return Ok(new ResultViewModel<SampleViewModel>
        {
            isSuccess = false,
            message = $"新增{_programname}成功",
            Result = null,
        });
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<SampleViewModel>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }

    // PUT api/Sample
    [HttpPut]
    public IActionResult Put([FromBody] SampleUpdateModel model)
    {
      try
      {
        if (_service.GetDataById(model.id) == null)
        {
          //<string>?? 上面 SampleViewModel
            return BadRequest(new ResultViewModel<string>
            {
                isSuccess = false,
                message = "查無此筆資料，修改失敗",
                Result = null,
            });
        }
        else
        {
            _service.Update(model);
            return Ok(new ResultViewModel<string>
            {
                isSuccess = true,
                message = $"修改{_programname}成功",
                Result = null,
            });
        }
      }
      catch (Exception e)
      {
        return NotFound(new ResultViewModel<SampleViewModel>
        {
          isSuccess = false,
          message = e.Message.ToString(),
          Result = null,
        });
      }
    }

        [HttpDelete]
        // Delete: api/Sample?id=2ac27d6d-9b98-4ba1-b77a-eb5549c264dd
        public IActionResult Delete(string id)
        {
            try
            {
                if (_service.GetDataById(id) == null)
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "查無此筆資料，刪除失敗",
                        Result = null,
                    });
                }
                else
                {
                    _service.Delete(id); 
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = $"刪除{_programname}成功",
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
                    Result = null,
                });
            }
        }

  }
}

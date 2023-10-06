
using System;
using System.IO;
using System.Linq;
using backend.ImportModels.Register;
using backend.Middleware.jwt;
using backend.Middleware.jwt_t;
using backend.Models.auth_t;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers.Ch09
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsController : ControllerBase
    {
        private readonly ILogger<ParentsController> _logger;
        private readonly IUserService_T _service;
        public ParentsController(ILogger<ParentsController> logger, IUserService_T service)
        {
            _logger = logger;
            _service = service;
        }

        #region 註冊
        [HttpPost]
        [Route("Regist")]
        public IActionResult Regist([FromForm] RegisterImportModel newRegist)
        {
            try
            {
                if (_service.checkMember(newRegist.email))
                {
                    //string fileExtension = Path.GetExtension(newRegist.image.FileName);
                    _service.Register(newRegist);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "註冊成功，請去收信來驗證",
                        Result = null,
                    });
                }
                else
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "此帳號已被註冊",
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

        [HttpGet("emailValidate")]
        public IActionResult EmailValidate([FromForm] EmailValidate Data)
        {
            try
            {
                Members ValidateMember = _service.GetDataById(Data.email);
                if (ValidateMember != null)
                {
                    if (ValidateMember.authcode == Data.authcode)
                    {
                        _service.EmailValidate(Data);
                        return Ok(new ResultViewModel<string>
                        {
                            isSuccess = true,
                            message = "驗證成功，現在可以登入",
                            Result = null,
                        });
                    }
                    else
                    {
                        return BadRequest(new ResultViewModel<string>
                        {
                            isSuccess = false,
                            message = "驗證碼錯誤，請重新確認或在註冊",
                            Result = null,
                        });
                    }
                }
                else
                {
                    return Ok(new ResultViewModel<AuthenticateResponse>
                    {
                        isSuccess = false,
                        message = "查無此帳號，請再重新註冊",
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
        #endregion

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] loginViewModel model)
        {
            try
            {
                Members ValidateMember = _service.GetDataById(model.Email);
                if (string.IsNullOrWhiteSpace(ValidateMember.authcode))
                {
                    AuthenticateResponse Result = _service.Authenticate(model);
                    if (Result == null)
                    {
                        return BadRequest(new ResultViewModel<string>
                        {
                            isSuccess = false,
                            message = "帳號密碼輸入錯誤",
                            Result = null,
                        });
                    }
                    else
                    {
                        return Ok(new ResultViewModel<AuthenticateResponse>
                        {
                            isSuccess = true,
                            message = "登入成功",
                            Result = Result,
                        });
                    }
                }
                else
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "尚未驗證通過，請去EMAIL收信進行驗證",
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

        //查看自己資料
        [HttpGet]
        [Route("ParentData")]
        public IActionResult ParentData([FromBody] ParentDataModel Date)
        {
            try
            {
                return Ok(new ResultViewModel<Members>
                {
                    isSuccess = true,
                    message = string.Empty,
                    Result = _service.GetDataById(Date.email),
                });
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

        //編輯自己資料
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromForm] EditModel EditData)
        {
            try
            {
                _service.Edit(EditData);
                return Ok(new ResultViewModel<string>
                {
                    isSuccess = true,
                    message = "修改成功",
                    Result = null,
                });
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

        //忘記密碼
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword([FromBody] ForgetPasswordImportModel Data)
        {
            try
            {
                if (_service.GetDataById(Data.email) != null)
                {
                    _service.ForgetPassword(Data);
                    return Ok(new ResultViewModel<string>
                    {
                        isSuccess = true,
                        message = "已重新寄密碼",
                        Result = null,
                    });
                }
                else
                {
                    return BadRequest(new ResultViewModel<string>
                    {
                        isSuccess = false,
                        message = "查無此帳號",
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

        //判斷圖片
        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(fileExtension.ToLower());
            //string fileExtension = Path.GetExtension(EditData.image.FileName);
            //    if (IsImageFile(fileExtension))
        }
    }
}
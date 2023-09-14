using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

using backend.Middleware.jwt;
using backend.util;
using backend.ViewModels;
using System.Text.Json;
using backend.Middleware.jwt_t;

namespace backend.Middleware
{
    public class jwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly appSettings _appSettings;

        public jwtMiddleware(RequestDelegate next, IOptions<appSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService_T userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            await attachUserToContext(context, userService, token);
            if(context.Response.StatusCode != 401)
                await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService_T userService,string token)
        {

            //設定HttpRequest與HttpResponse
            var Request = context.Request;
            var Response = context.Response;

            //抓取路徑
            string Path = Request.Path.Value;

            //抓取傳送方式，ex：GET、POST、DELETE、PUT
            string URLMethod = Request.Method;

            //加密金鑰
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(this._appSettings.jwt_secret));


            //判斷路徑是否需要做Token驗證
            if (RouteChecked(Path, URLMethod))
            {
                //判斷Token是否存在
                if (string.IsNullOrWhiteSpace(token) || token == "null")
                {
                    await BadResponse(Response);
                }
                else
                {
                    try
                    {
                        //讀取Token的Header與PayLoad
                        tokenEnCode _TokenEnCode = new tokenEnCode(context);
                        var jwtArr = token.Split('.');
                        var Header = _TokenEnCode.GetHeader();
                        var PayLoad = _TokenEnCode.GetPayLoad();

                        Boolean success = true;
                        success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.ASCII.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
                        //驗證Token安全金鑰的值是否正確
                        if (!success)
                        {
                            await BadResponse(Response);
                        }
                        else
                        {
                            //驗證Token是否過期
                            if (!TimeChecked(PayLoad["iat"].ToString(), PayLoad["exp"].ToString()))
                            {
                                await BadResponse(Response);
                            }
                            else
                            {
                                // 驗證使用者是否存在
                                if (!UserChecked(PayLoad["account"].ToString(), userService))
                                {
                                    await BadResponse(Response);
                                }
                                else
                                {
                                    //驗證角色權限
                                    await BadResponse(Response);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        context.Response.ContentType = "application/json; charset = utf-8";
                        context.Response.StatusCode = 401;
                        var res = new ResultViewModel<object>
                        {
                            isSuccess = false,
                            message = "Unauthorized",
                            Result = null,
                            Status = "401",
                        };
                        await context.Response.WriteAsync(JsonSerializer.Serialize<ResultViewModel<object>>(res));
                        await Task.CompletedTask;
                        return;
                    }
                }
            }
        }

        public async Task BadResponse(HttpResponse Response)
        {
            Response.ContentType = "application/json; charset = utf-8";
            Response.StatusCode = 401;
            var res = new ResultViewModel<object>
            {
                isSuccess = false,
                message = "Unauthorized",
                Result = null,
                Status = "401",
            };
            await Response.WriteAsync(JsonSerializer.Serialize<ResultViewModel<object>>(res));
            await Task.CompletedTask;
        }

        public Boolean RouteChecked(string Path, string URLMethod)
        {
            // 路由不須驗證權限
            //判斷Request路徑是不是登入，如果是登入，就執行Token驗證
            if ((Path == "/api/auth/login" && URLMethod == "POST"))
                return false;
            if ((Path.ToLower().Contains("/api/Image".ToLower()) && URLMethod == "GET"))
                return false;
            // if (Path.ToLower().Contains("/api/Sample".ToLower()))
            //     return false;
            return true;
        }

        public Boolean TimeChecked(string StartTime, string EndTime)
        {
            //將現在時間轉換成Unix時間戳
            Int32 DateNowUnixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            // && DateNowUnixTimestamp <= Int32.Parse(EndTime)
            if (DateNowUnixTimestamp >= Int32.Parse(StartTime))
                return true;

            return false;
        }

        public Boolean UserChecked(string UserId, IUserService_T userService)
        {
            //判斷該帳號是否存在
            var Users = userService.GetDataById(UserId);
            if (Users == null)
                return false;

            return true;
        }

        /*public Boolean RolePermissionsChecked(string Role, string Path, string URLMethod)
        {
            List<Dictionary<string, string>> RoleList = GetRoleList(@"{ ""Role"":" + Role + "}");

            string[] PathDetails = Path.Split("/");
            bool PermissionChecked = false;

            List<roleProcessModel> processList;
            foreach (var RoleListItem in RoleList)
            {
                processList = roleProcessService.GetRoleProcessList(RoleListItem["role"], $@"/{PathDetails[3]}", RoleListItem["sys_type"]);

                if (processList != null && processList.Count() != 0)
                {
                    PermissionChecked = true;
                    break;
                }
            }

            return PermissionChecked;
        }*/

        /*public List<Dictionary<string, string>> GetRoleList(string Role)
        {
            List<Dictionary<string, string>> RoleList = new List<Dictionary<string, string>>();

            JObject RoleJson = JObject.Parse(Role);
            foreach (var RoleItem in RoleJson["Role"])
            {
                Dictionary<string, string> RoleDictionary = new Dictionary<string, string>();
                RoleDictionary.Add("role", RoleItem["role"].ToString());
                RoleDictionary.Add("sys_type", RoleItem["sys_type"].ToString());

                RoleList.Add(RoleDictionary);
            }

            return RoleList;
        }*/
    }
}
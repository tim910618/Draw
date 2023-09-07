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

using backend.util;
using backend.ViewModels;
using System.Text.Json;

namespace backend.Middleware
{
    public class sampleMiddleware
    {
        // 完全啟動前執行
        // 須在 Startup.cs 做設定
        private readonly RequestDelegate _next;

        public sampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var Request = context.Request;
            var Response = context.Response;

            await Task.CompletedTask;
            return;
        }

    }
}
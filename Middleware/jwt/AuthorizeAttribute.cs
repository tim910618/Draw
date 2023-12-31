using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using backend.ViewModels;
using backend.Models.auth_t;

namespace backend.Middleware.jwt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (Members)context.HttpContext.Items["email"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new ResultViewModel<object>{
                        isSuccess = false,
                        message = "Unauthorized",
                        Result = null,
                        }) 
                        { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
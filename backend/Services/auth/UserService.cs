using backend.util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.Utils;
using backend.Models.user;
using backend.dao.user;
using backend.ViewModels.auth.User;

namespace backend.Services.user
{
    public class UserService
    {
        private readonly UserDao _userDao;

        public UserService(UserDao userDao)
        {
            this._userDao = userDao;
        }

        public UserInfoViewModel GetUserInfo(string account)
        {
            userinfoModel Data = this._userDao.GetUserInfo(account);
            UserInfoViewModel Result = new UserInfoViewModel();
            Result.info = new InfoViewModel
            {
                account = Data.userinfo.account,
                name = Data.userinfo.name,
                email = Data.userinfo.email,
                createat = Data.userinfo.createat.ToString("yyyy-MM-dd HH:mm:ss"),
                updateat = Data.userinfo.updateat.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            return Result;
        }
    }
}
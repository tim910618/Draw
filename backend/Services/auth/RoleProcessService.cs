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
using backend.Models.auth;
using backend.dao.auth;

namespace backend.Services.auth
{
    public interface IRoleProcessService
    {
        List<roleProcessModel> GetRoleProcessList(string role, string purl, string sysType);
        List<roleProcessModel> GetRoleProcessList(string role, string sysType);
    }
    public class RoleProcessService : IRoleProcessService
    {
        private readonly appSettings _appSettings;
        private readonly RoleProcessDao _roleDao;

        public RoleProcessService(IOptions<appSettings> appSettings, RoleProcessDao roleDao)
        {
            this._appSettings = appSettings.Value;
            this._roleDao = roleDao;
        }

        public List<roleProcessModel> GetRoleProcessList(string role, string purl, string sysType)
        {
            string type = (sysType == "疏運") ? "new_tbroc" : "new_tbroc_travel";
            return this._roleDao.GetRoleProcessList(role, purl, type);
        }

        public List<roleProcessModel> GetRoleProcessList(string role, string sysType)
        {
            string type = (sysType == "疏運") ? "new_tbroc" : "new_tbroc_travel";
            return this._roleDao.GetRoleProcessList(role, type);
        }
    }
}
using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;
using System.IO;
using backend.util;
using Microsoft.Extensions.Options;
using backend.Sqls.mssql;
using Microsoft.AspNetCore.Http;

namespace backend.Services
{
    public class PaintingService
    {
        private readonly MssqlConnect _MssqlConnect;
        private readonly appSettings _appSettings;
        private readonly PaintingDao _PaintingDao;
        public PaintingService(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor,PaintingDao PaintingDao)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);
            this._PaintingDao=PaintingDao;
        }

        #region 新增
        public void Insert(PaintingInsertImportModel model)
        {
            var FileName=Guid.NewGuid().ToString()+Path.GetExtension(model.picture.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);
            
            //string DataName=model.pic.FileName;
            _PaintingDao.Insert(model,FileName);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.picture.CopyTo(stream);
            }
        }
        #endregion
    }
}
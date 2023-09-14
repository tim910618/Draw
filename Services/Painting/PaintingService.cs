using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;
using System.IO;
using backend.util;

namespace backend.Services
{
    public class PaintingService
    {
        private readonly appSettings _appSettings;
        private readonly PaintingDao _PaintingDao;
        public PaintingService(PaintingDao PaintingDao)
        {
            this._PaintingDao=PaintingDao;
        }

        #region 新增
        public void Insert(PaintingInsertImportModel model)
        {
            var FileName=Guid.NewGuid().ToString()+Path.GetExtension(model.pic.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath, "Facility");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);
            
            //string DataName=model.pic.FileName;
            _PaintingDao.Insert(model,path);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.pic.CopyTo(stream);
            }
        }
        #endregion
    }
}
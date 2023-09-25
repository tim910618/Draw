using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;
using System.IO;
using Microsoft.Extensions.Options;
using backend.util;

namespace backend.Services
{
    public class ScaleService
    {
        private readonly appSettings _appSettings;
        private readonly scaleDao _scaleDao;
        private readonly kidDao _kidDao;
        public ScaleService(IOptions<appSettings> appSettings, scaleDao scaleDao, kidDao kidDao)
        {
            _appSettings = appSettings.Value;
            this._scaleDao = scaleDao;
            this._kidDao = kidDao;
        }

        #region 新增
        public void Insert(ScaleImportModel model)
        {
            Kids kid = _kidDao.GetDataByKid_Id(model.kid_id);
            string age_stage = Distinguish_age.Distinguish(kid.birth.ToString());
            //沒問題就 UPDATE kid資料表
            if (model.scale_trouble == "0" && model.disease == "0" && model.disease_other == "0")
            { 
                _scaleDao.Update_stage(model,age_stage,1);
            }
            //有問題就存進 scale_troubles資料表 + UPDATE kid資料表
            else
            {
                _scaleDao.Insert(model,age_stage);
                _scaleDao.Update_stage(model,age_stage,2);
            }
        }
        #endregion
        #region 查詢Scale
        public void GetScale(GetScaleImportModel model)
        {
            _scaleDao.GetScale(model);
        }
        #endregion
    }
}
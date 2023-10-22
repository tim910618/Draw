using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;
using backend.Models.auth_t;
using System.Numerics;
using Org.BouncyCastle.Utilities;

namespace backend.Services
{
    public class MedicalService
    {
        private readonly medicalDao _medicalDao;
        public MedicalService(medicalDao medicalDao)
        {
            this._medicalDao = medicalDao;
        }
        #region 全部
        public List<MedicalViewModel> GetDataList()
        {
            List<MedicalViewModel> Result = new List<MedicalViewModel>();
            List<Medical> DataList = _medicalDao.GetDataList();
            Result = ModelExtension.MatchAndMap<Medical, MedicalViewModel>(DataList, Result);
            return Result;
        }
        #endregion
        #region 單筆
        public MedicalViewModel GetDataById(MedicalImportModel model)
        {
            MedicalViewModel Result = new MedicalViewModel();
            Medical Data = _medicalDao.GetDataById(model);
            if (Data == null) return null;
            Result = ModelExtension.OneMatchAndMap<Medical, MedicalViewModel>(Data, Result);
            return Result;
        }
        #endregion
    }
}
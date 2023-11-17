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
    public class KidService
    {
        private readonly appSettings _appSettings;
        private readonly kidDao _kidDao;
        public KidService(IOptions<appSettings> appSettings, kidDao kidDao)
        {
            _appSettings = appSettings.Value;
            this._kidDao = kidDao;
        }

        #region 新增
        public void Insert(KidInsertImportModel model)
        {
            var FileName = string.Empty;
            if (model.image != null && model.image.FileName != null)
            {
                FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.image.FileName);
            }
            else
            {
                FileName = "default.jpg";
            }
            var folderPath = Path.Combine(this._appSettings.UploadPath, "KidHead");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);

            _kidDao.Insert(model, FileName);
            //存到路徑裡面
            if (model.image != null && model.image.FileName != null)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.image.CopyTo(stream);
                }
            }
        }
        #endregion

        #region 全部
        public List<KidViewModel> GetDataList()
        {
            List<KidViewModel> Result = new List<KidViewModel>();
            List<Kids> DataList = _kidDao.GetDataList();

            foreach (var item in DataList)
            {
                //age 幾年幾個月
                DateTime currentDate = DateTime.Now;
                int years = currentDate.Year - item.birth.Year;
                int months = currentDate.Month - item.birth.Month;
                if (currentDate.Day < item.birth.Day)
                {
                    months--;
                }
                if (months < 0)
                {
                    years--;
                    months += 12;
                }

                Result.Add(new KidViewModel
                {
                    kid_id = item.kid_id.ToString(),
                    name = item.name,
                    birth = item.birth.ToString("yyyy-MM-dd"),
                    gender = item.gender,
                    image = "C:\\IMAGE" + item.image,
                    age = years + "年" + months + "月",
                });
            }
            return Result;
        }
        #endregion
        #region 單筆
        public KidViewModel GetDataByKid_Id(string kid_id)
        {
            KidViewModel Result = new KidViewModel();
            Kids OnlyKid = _kidDao.GetDataByKid_Id(kid_id);
            if (OnlyKid == null) return null;

            //age 幾年幾個月
            DateTime currentDate = DateTime.Now;
            int years = currentDate.Year - OnlyKid.birth.Year;
            int months = currentDate.Month - OnlyKid.birth.Month;
            if (currentDate.Day < OnlyKid.birth.Day)
            {
                months--;
            }
            if (months < 0)
            {
                years--;
                months += 12;
            }

            Result = new KidViewModel
            {
                kid_id = OnlyKid.kid_id.ToString(),
                name = OnlyKid.name,
                birth = OnlyKid.birth.ToString("yyyy-MM-dd"),
                gender = OnlyKid.gender,
                image = "C:\\IMAGE" + OnlyKid.image,
                age = years + "年" + months + "月",
            };
            return Result;
        }
        #endregion

        #region 修改
        public void Update(KidEditModel model)
        {
            var FileName = string.Empty;
            if (model.image != null && model.image.FileName != null)
            {
                FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.image.FileName);
            }
            else
            {
                FileName = "default.jpg";
            }
            var folderPath = Path.Combine(this._appSettings.UploadPath, "KidHead");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);

            _kidDao.Update(model, FileName);

            //存到路徑裡面
            if (model.image != null && model.image.FileName != null)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.image.CopyTo(stream);
                }
            }
        }
        #endregion
    }
}
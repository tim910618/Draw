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
        private readonly paintingDao _PaintingDao;
        private readonly kidDao _kidDao;
        public PaintingService(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor, paintingDao PaintingDao, kidDao kidDao)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);
            this._PaintingDao = PaintingDao;
            this._kidDao = kidDao;
        }

        #region 新增
        public KidViewModelID Insert(PaintingInsertImportModel model)
        {
            var FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.picture.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);

            Kids OnlyKid = _kidDao.GetDataByKid_Id(model.kid_id);
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
            Random random = new Random();

            // 產生介於 1 和 years 之間的隨機數
            int randomValue = random.Next(1, years + 1);
            string result = randomValue.ToString();


            Guid painting_id = Guid.NewGuid();
            _PaintingDao.Insert(painting_id, model, FileName, result);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.picture.CopyTo(stream);
            }

            KidViewModelID Result = new KidViewModelID();
            Result = new KidViewModelID
            {
                painting_id = OnlyKid.kid_id.ToString(),
            };
            return Result;
        }
        #endregion
        #region 搜尋
        public bool GetDataById(string kid_id)
        {
            Kids kid = _PaintingDao.GetDataById(kid_id);
            string age_stage = Distinguish_age.Distinguish(kid.birth.ToString());
            Kids kid_check = _PaintingDao.kid_check(kid_id, age_stage);
            if (kid_check == null)
            {
                return false;
            }
            else return true;
        }
        #endregion
        #region 歷史紀錄
        public List<KidHistoryViewModel> History(PaintingHistoryImportModel model)
        {
            List<KidHistoryViewModel> Result = new List<KidHistoryViewModel>();
            List<Painting> DataList = _PaintingDao.History(model);

            foreach (var item in DataList)
            {
                string imagePath = Path.Combine("C:\\IMAGE", item.picture);
                byte[] imageData = File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageData);
                string dataUrl = "data:image/png;base64," + base64String;

                Result.Add(new KidHistoryViewModel
                {
                    painting_id = item.painting_id.ToString(),
                    result = item.result,
                    create_time = item.create_time.ToString(),
                    image = dataUrl,
                });
            }
            return Result;
        }
        #endregion
        #region 歷史紀錄單筆
        public KidHistoryOneViewModels HistoryById(PaintingHistoryByIdImportModel model)
        {
            KidHistoryOneViewModels Result = new KidHistoryOneViewModels();
            Painting Data = _PaintingDao.GetHistoryById(model);

            string imagePath = Path.Combine("C:\\IMAGE", Data.picture);
            byte[] imageData = File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageData);
            string dataUrl = "data:image/png;base64," + base64String;

            Kids OnlyKid = _kidDao.GetDataByKid_Id(model.kid_id);
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
            Random random = new Random();

            Result = new KidHistoryOneViewModels
            {
                age = years.ToString(),
                result = Data.result,
                create_time = Data.create_time.ToString(),
                image = dataUrl,
            };

            return Result;
        }
        #endregion
    }
}
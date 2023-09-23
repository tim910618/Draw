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

            //圖片
            var FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.image.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath, "KidHead");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);

            _kidDao.Insert(model, FileName);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.image.CopyTo(stream);
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
                    birth = item.birth.ToString(),
                    gender = item.gender,
                    image = item.image,
                    age_0004 = item.age_0004.ToString(),
                    age_0006 = item.age_0006.ToString(),
                    age_0009 = item.age_0009.ToString(),
                    age_0100 = item.age_0100.ToString(),
                    age_0103 = item.age_0103.ToString(),
                    age_0106 = item.age_0106.ToString(),
                    age_0200 = item.age_0200.ToString(),
                    age_0206 = item.age_0206.ToString(),
                    age_0300 = item.age_0300.ToString(),
                    age_0306 = item.age_0306.ToString(),
                    age_0400 = item.age_0400.ToString(),
                    age_0500 = item.age_0500.ToString(),
                    age_0600 = item.age_0600.ToString(),
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

            if (OnlyKid == null) return null;
            Result = new KidViewModel
            {
                kid_id = OnlyKid.kid_id.ToString(),
                name = OnlyKid.name,
                birth = OnlyKid.birth.ToString(),
                gender = OnlyKid.gender,
                image = OnlyKid.image,
                age_0004 = OnlyKid.age_0004.ToString(),
                age_0006 = OnlyKid.age_0006.ToString(),
                age_0009 = OnlyKid.age_0009.ToString(),
                age_0100 = OnlyKid.age_0100.ToString(),
                age_0103 = OnlyKid.age_0103.ToString(),
                age_0106 = OnlyKid.age_0106.ToString(),
                age_0200 = OnlyKid.age_0200.ToString(),
                age_0206 = OnlyKid.age_0206.ToString(),
                age_0300 = OnlyKid.age_0300.ToString(),
                age_0306 = OnlyKid.age_0306.ToString(),
                age_0400 = OnlyKid.age_0400.ToString(),
                age_0500 = OnlyKid.age_0500.ToString(),
                age_0600 = OnlyKid.age_0600.ToString(),
                age = years + "年" + months + "月",
            };
            return Result;
        }
        #endregion

        #region 修改
        public void Update(KidEditModel model)
        {
            var FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.image.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath, "KidHead");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);
            Kids Data = new Kids
            {
                name = model.name,
                image = FileName,
            };
            _kidDao.Update(model, FileName);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.image.CopyTo(stream);
            }
        }
        #endregion




        #region 回覆
        public void Reply(GuestbooksReplyModel model)
        {
            Guestbooks Data = new Guestbooks
            {
                Id = Convert.ToInt32(model.Id),
                Reply = model.Reply,
            };
            _kidDao.Reply(Data);
        }
        #endregion
        #region 刪除
        public void Delete(string Id)
        {
            _kidDao.Delete(Id);
        }
        #endregion
    }
}
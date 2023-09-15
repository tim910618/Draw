using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;

namespace backend.Services
{
    public class KidService
    {
        private readonly kidDao _kidDao;
        public KidService(kidDao kidDao)
        {
            this._kidDao=kidDao;
        }

        #region 新增
        public void Insert(KidInsertImportModel model)
        {
            _kidDao.Insert(model);
        }
        #endregion

        #region 全部
        public List<KidViewModel> GetDataList()
        {
            List<KidViewModel> Result=new List<KidViewModel>();
            List<Kids> DataList=_kidDao.GetDataList();
            foreach(var item in DataList)
            {
                Result.Add(new KidViewModel
                {
                    kid_id=item.kid_id.ToString(),
                    name = item.name,
                    birth = item.birth.ToString(),
                    gender = item.gender,
                    age_0004=item.age_0004.ToString(),
                    age_0006=item.age_0006.ToString(),
                    age_0009=item.age_0009.ToString(),
                    age_0100=item.age_0100.ToString(),
                    age_0103=item.age_0103.ToString(),
                    age_0106=item.age_0106.ToString(),
                    age_0200=item.age_0200.ToString(),
                    age_0206=item.age_0206.ToString(),
                    age_0300=item.age_0300.ToString(),
                    age_0306=item.age_0306.ToString(),
                    age_0400=item.age_0400.ToString(),
                    age_0500=item.age_0500.ToString(),
                    age_0600=item.age_0600.ToString(),
                });
            }
            return Result;
        }
        #endregion
        #region 單筆
        public GuestbooksViewModel GetDataById(string id)
        {
            GuestbooksViewModel Result=new GuestbooksViewModel();
            Guestbooks Data=_kidDao.GetDataById(id);
            if(Data==null) return null;
            Result=ModelExtension.OneMatchAndMap<Guestbooks,GuestbooksViewModel>(Data,Result);
            return Result;
        }
        #endregion
        
        #region 修改
        public void Update(GuestbooksUpdateModel model)
        {
            Guestbooks Data=new Guestbooks
            {
                Id=Convert.ToInt32(model.Id),
                Name=model.Name,
                Content=model.Content
            };
            _kidDao.Update(Data);
        }
        #endregion
        #region 回覆
        public void Reply(GuestbooksReplyModel model)
        {
            Guestbooks Data=new Guestbooks
            {
                Id=Convert.ToInt32(model.Id),
                Reply=model.Reply,
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
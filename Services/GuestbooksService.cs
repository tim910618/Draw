using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;

namespace backend.Services
{
    public class GuestbooksService
    {
        private readonly guestbooksDao _guestbooksDao;
        public GuestbooksService(guestbooksDao guestbooksDao)
        {
            this._guestbooksDao=guestbooksDao;
        }
        #region 全部
        public List<GuestbooksViewModel> GetDataList()
        {
            List<GuestbooksViewModel> Result=new List<GuestbooksViewModel>();
            List<Guestbooks> DataList=_guestbooksDao.GetDataList();
            Result = ModelExtension.MatchAndMap<Guestbooks,GuestbooksViewModel>(DataList,Result);
            return Result;
        }
        #endregion
        #region 單筆
        public GuestbooksViewModel GetDataById(string id)
        {
            GuestbooksViewModel Result=new GuestbooksViewModel();
            Guestbooks Data=_guestbooksDao.GetDataById(id);
            if(Data==null) return null;
            Result=ModelExtension.OneMatchAndMap<Guestbooks,GuestbooksViewModel>(Data,Result);
            return Result;
        }
        #endregion
        #region 新增
        public void Insert(GuestbooksImportModel model)
        {
            Guestbooks Data=new Guestbooks
            {
                Name=model.Name,
                Content=model.Content
            };
            _guestbooksDao.Insert(Data);
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
            _guestbooksDao.Update(Data);
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
            _guestbooksDao.Reply(Data);
        }
        #endregion
        #region 刪除
        public void Delete(string Id)
        {
            _guestbooksDao.Delete(Id);
        }
        #endregion
    }
}
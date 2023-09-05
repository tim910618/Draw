using System;
using System.Collections.Generic;
using backend.dao;
using backend.Extensions;
using backend.ImportModels;
using backend.Models;
using backend.ViewModels;

namespace backend.Services
{
    public class sampleService
    {
        // business logic
        private readonly sampleDao _sampleDao;
        public sampleService(sampleDao sampleDao)
        {
            _sampleDao = sampleDao;
        }

        public List<SampleViewModel> GetDataList()
        {
            List<SampleViewModel> Result = new List<SampleViewModel>();
            List<sys_sample> DataList = _sampleDao.GetDataList();
            Result = ModelExtension.MatchAndMap<sys_sample, SampleViewModel>(DataList, Result);
            return Result;
        }

        public SampleViewModel GetDataById(string id)
        {
            SampleViewModel Result = new SampleViewModel();
            sys_sample Data = _sampleDao.GetDataById(Guid.Parse(id)); // 主鍵為guid 盡量讓dao 保持乾淨不要有太多邏輯
            if(Data == null) return null; // 無Data直接回傳
            //看不懂
            Result = ModelExtension.OneMatchAndMap<sys_sample, SampleViewModel>(Data, Result);
            return Result;
        }

        public void Insert(SampleImportModel model)
        {
            Guid id = Guid.NewGuid();
            sys_sample Data = new sys_sample
            {
                id = id,
                title = model.title,
                content = model.content,
                email = model.email,
                num = Convert.ToInt32(model.num),
            };
            _sampleDao.Insert(Data);
        }

        public void Update(SampleUpdateModel model)
        {
            sys_sample Data = new sys_sample
            {
                id = Guid.Parse(model.id),
                title = model.title,
                content = model.content,
                email = model.email,
                num = Convert.ToInt32(model.num),
            };
            _sampleDao.Update(Data);
        }

        public void Delete(string id)
        {
            _sampleDao.Delete(Guid.Parse(id));
        }
    }
}
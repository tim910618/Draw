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
    public class GuestbooksService
    {
        private readonly guestbooksDao _guestbooksDao;
        public GuestbooksService(guestbooksDao guestbooksDao)
        {
            this._guestbooksDao = guestbooksDao;
        }
        #region 全部
        public List<GuestbooksViewModel> GetDataList()
        {
            List<GuestbooksViewModel> Result = new List<GuestbooksViewModel>();
            List<Guestbooks> DataList = _guestbooksDao.GetDataList();
            Result = ModelExtension.MatchAndMap<Guestbooks, GuestbooksViewModel>(DataList, Result);
            return Result;
        }
        #endregion
        #region 單筆
        public GuestbooksViewModel GetDataById(string id)
        {
            GuestbooksViewModel Result = new GuestbooksViewModel();
            Guestbooks Data = _guestbooksDao.GetDataById(id);
            if (Data == null) return null;
            Result = ModelExtension.OneMatchAndMap<Guestbooks, GuestbooksViewModel>(Data, Result);
            return Result;
        }
        #endregion
        #region 新增
        public void Insert(GuestbooksImportModel model)
        {
            Guestbooks Data = new Guestbooks
            {
                Name = model.Name,
                Content = model.Content
            };
            _guestbooksDao.Insert(Data);
        }
        #endregion
        #region 修改
        public void Update(GuestbooksUpdateModel model)
        {
            Guestbooks Data = new Guestbooks
            {
                Id = Convert.ToInt32(model.Id),
                Name = model.Name,
                Content = model.Content
            };
            _guestbooksDao.Update(Data);
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
            _guestbooksDao.Reply(Data);
        }
        #endregion
        #region 刪除
        public void Delete(string Id)
        {
            _guestbooksDao.Delete(Id);
        }
        #endregion

        #region TEST
        public void TEST()
        {
            //Id
            
            //Type
            int a = 1; int b = 2; int c = 3;
            //Choose
            int Choose = 0;
            //Item
            int Item = 1;
            //Speed
            int Speed = 40;
            //Time
            int Time = 0;
            //時間次數
            int times = 0;
            for (int total = 31; total <= 359; total++)
            {
                Test_All Result = _guestbooksDao.TEST_S(total);
                if (Result != null)
                {
                    for (int i = 1; i <= 26; i++)
                    {
                        //Plan
                        int Plan = 1;
                        if (i == 1)
                        {
                            Choose = Result.SA2A;
                            Item = 1;
                            Speed = 40;
                            times = 0;
                        }
                        if (i == 9) Choose = Result.SA3A;
                        if (i == 17) Choose = Result.SA4A;
                        if (i <= 8)
                        {
                            if (i % 2 == 0)
                            {
                                Plan = 2;
                            }
                            Test_ans test_Ans = new Test_ans
                            {
                                Id = Result.Id,
                                Questionnaire = Result.Questionnaire,
                                Type = a,
                                Plan = Plan,
                                Choose = Choose,
                                Judge = Plan == Choose ? 1 : 0,
                                Item = Item,
                                Speed = 0,
                                Time = 0,
                            };
                            _guestbooksDao.TEST_insert(test_Ans);
                            if (i % 2 == 0)
                            {
                                Item++;
                                if (i == 2) Choose = Result.SA2B;
                                if (i == 4) Choose = Result.SA2C;
                                if (i == 6) Choose = Result.SA2D;
                            }
                        }
                        else if (i > 8 && i <= 16)
                        {
                            if (i == 9) Item = 1;
                            if (i % 2 == 0)
                            {
                                Plan = 2;
                                Speed = 80;
                            }
                            Test_ans test_Ans = new Test_ans
                            {
                                Id = Result.Id,
                                Questionnaire = Result.Questionnaire,
                                Type = b,
                                Plan = Plan,
                                Choose = Choose,
                                Judge = Plan == Choose ? 1 : 0,
                                Item = Item,
                                Speed = Speed,
                                Time = 0,
                            };
                            _guestbooksDao.TEST_insert(test_Ans);
                            if (i % 2 == 0)
                            {
                                Item++;
                                if (i == 10)
                                {
                                    Choose = Result.SA3B;
                                    Speed = 30;
                                }
                                if (i == 12)
                                {
                                    Choose = Result.SA3C;
                                    Speed = 20;
                                }
                                if (i == 14)
                                {
                                    Choose = Result.SA3D;
                                    Speed = 10;
                                }
                            }
                        }
                        else
                        {
                            //判斷時間
                            if(i >= 17) times++;
                            if (Result.Questionnaire == 2 || Result.Questionnaire == 5)
                            {
                                Time = Test_time_oneM(times);
                            }
                            if (Result.Questionnaire == 3 || Result.Questionnaire == 6)
                            {
                                Time = Test_time_oneL(times);
                            }
                            if (Result.Questionnaire == 8 || Result.Questionnaire == 11)
                            {
                                Time = Test_time_threeM(times);
                            }
                            if (Result.Questionnaire == 9 || Result.Questionnaire == 12)
                            {
                                Time = Test_time_threeL(times);
                            }

                            if (i == 17) Item = 1;
                            if (i % 2 == 0)
                            {
                                Plan = 2;
                            }
                            Test_ans test_Ans = new Test_ans
                            {
                                Id = Result.Id,
                                Questionnaire = Result.Questionnaire,
                                Type = c,
                                Plan = Plan,
                                Choose = Choose,
                                Judge = Plan == Choose ? 1 : 0,
                                Item = Item,
                                Speed = 0,
                                Time = Time,
                            };
                            _guestbooksDao.TEST_insert(test_Ans);
                            if (i % 2 == 0)
                            {
                                Item++;
                                if (i == 18) Choose = Result.SA4B;
                                if (i == 20) Choose = Result.SA4C;
                                if (i == 22) Choose = Result.SA4D;
                                if (i == 24) Choose = Result.SA4E;
                            }
                        }
                    }
                }
                else continue;
            }
        }
        public int Test_time_oneM(int times)
        {
            switch (times)
            {
                case 1:
                    return 35;
                case 2:
                    return 23;
                case 3:
                    return 50;
                case 4:
                    return 23;
                case 5:
                    return 50;
                case 6:
                    return 40;
                case 7:
                    return 80;
                case 8:
                    return 23;
                case 9:
                    return 80;
                case 10:
                    return 50;
                default:
                    return 0;
            }
        }
        public int Test_time_oneL(int times)
        {
            switch (times)
            {
                case 1:
                    return 40;
                case 2:
                    return 23;
                case 3:
                    return 60;
                case 4:
                    return 23;
                case 5:
                    return 60;
                case 6:
                    return 40;
                case 7:
                    return 100;
                case 8:
                    return 23;
                case 9:
                    return 100;
                case 10:
                    return 70;
                default:
                    return 0;
            }
        }
        public int Test_time_threeM(int times)
        {
            switch (times)
            {
                case 1:
                    return 35;
                case 2:
                    return 29;
                case 3:
                    return 50;
                case 4:
                    return 29;
                case 5:
                    return 50;
                case 6:
                    return 40;
                case 7:
                    return 80;
                case 8:
                    return 29;
                case 9:
                    return 80;
                case 10:
                    return 50;
                default:
                    return 0;
            }
        }
        public int Test_time_threeL(int times)
        {
            switch (times)
            {
                case 1:
                    return 40;
                case 2:
                    return 29;
                case 3:
                    return 60;
                case 4:
                    return 29;
                case 5:
                    return 60;
                case 6:
                    return 40;
                case 7:
                    return 100;
                case 8:
                    return 29;
                case 9:
                    return 100;
                case 10:
                    return 70;
                default:
                    return 0;
            }
        }
        #endregion
    }
}
using Blog.Core.IRepository;
using Blog.Core.Model;
using Blog.Core.Repository.Base;
using Blog.Core.Repository.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Core.Repository
{
    public class AdvertisementRepository : BaseRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(ISqlSugarClient db)
            : base(db)
        {

        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}

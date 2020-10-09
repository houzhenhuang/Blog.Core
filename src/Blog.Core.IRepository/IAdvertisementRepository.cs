using Blog.Core.IRepository.Base;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Core.IRepository
{
    public interface IAdvertisementRepository : IBaseRepository<Advertisement>
    {
        int Sum(int x, int y);
    }
}

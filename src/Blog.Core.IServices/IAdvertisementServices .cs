using Blog.Core.IServices.Base;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Core.IServices
{
    public interface IAdvertisementServices : IBaseServices<Advertisement>
    {
        int Sum(int i, int j);
    }
}

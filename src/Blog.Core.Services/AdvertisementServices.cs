using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blog.Core.Services
{
    public class AdvertisementServices : BaseServices<Advertisement>, IAdvertisementServices
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        public AdvertisementServices(IAdvertisementRepository advertisementRepository, IBaseRepository<Advertisement> baseRepository)
            : base(baseRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}

using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {
        private readonly IBlogArticleRepository _blogArticleRepository;
        private readonly IMapper _mapper;
        public BlogArticleServices(IBlogArticleRepository blogArticleRepository, IBaseRepository<BlogArticle> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _blogArticleRepository = blogArticleRepository;
            _mapper = mapper;
        }

        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<BlogArticle>> GetBlogs()
        {
            return await base.Query(b => b.Id > 0);
        }
        public async Task<BlogViewModel> GetBlogDetail(int id)
        {
            var blogArticle = (await base.Query(b => b.Id == id && b.IsDeleted == false)).SingleOrDefault();

            var viewModel = _mapper.Map<BlogViewModel>(blogArticle);

            if (blogArticle != null)
            {
                var nextBlog = (await base.Query(a => a.Id > id && a.IsDeleted == false, 1, "id")).SingleOrDefault();
                if (nextBlog != null)
                {
                    viewModel.NextId = nextBlog.Id;
                    viewModel.Next = nextBlog.Title;
                }
                var prevBlog = (await base.Query(a => a.Id < id && a.IsDeleted == false, 1, "id desc")).SingleOrDefault();
                if (prevBlog != null)
                {
                    viewModel.PreviousId = prevBlog.Id;
                    viewModel.Previous = prevBlog.Title;
                }
                blogArticle.Traffic += 1;
                await base.Update(blogArticle, new List<string> { "traffic" });
            }
            return viewModel;
        }
    }
}

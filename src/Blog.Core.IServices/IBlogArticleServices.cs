using Blog.Core.IServices.Base;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IBlogArticleServices : IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetBlogs();
        Task<BlogViewModel> GetBlogDetail(int id);
    }
}

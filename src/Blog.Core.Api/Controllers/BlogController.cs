using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Blog.Core.Api.Models;
using Blog.Core.Api.SwaggerHelper;
using Blog.Core.Common.Redis;
using Blog.Core.IServices;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.Api.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogArticleServices _blogArticleServices;
        //private readonly IOptionsSnapshot<AppSettings> _settings;
        public BlogController(IBlogArticleServices blogArticleServices)
        {
            _blogArticleServices = blogArticleServices;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBlogs")]
        public async Task<IActionResult> GetBlogs()
        {

            //var claims = User.Claims; //存的是token中的payload data

            //HttpContext.RequestServices  //请求服务,生命周期随着请求的结束而结束

            //var blogs = new List<BlogArticle>();
            //if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            //{
            //    blogs = _redisCacheManager.Get<List<BlogArticle>>("Redis.Blog");
            //}
            //else
            //{
            //    blogs = await _blogArticleServices.GetBlogs();
            //    _redisCacheManager.Set<List<BlogArticle>>("Redis.Blog", blogs, TimeSpan.FromHours(2));
            //}
            var blogs = await _blogArticleServices.GetBlogs();
            return Ok(blogs);
        }
        [HttpGet("GetBlogDetail")]
        public async Task<IActionResult> GetBlogDetail(int id)
        {
            var blogDetail = await _blogArticleServices.GetBlogDetail(id);
            return Ok(blogDetail);
        }




        /// <summary>
        /// 获取博客测试信息 v2版本
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        ////MVC自带特性 对 api 进行组管理
        //[ApiExplorerSettings(GroupName = "v2")]
        ////路径 如果以 / 开头，表示绝对路径，反之相对 controller 的想u地路径
        //[Route("v2/Blog/BlogTest")]
        ////和上边的版本控制以及路由地址都是一样的

        [CustomRoute(ApiVersions.v2, "BlogTest")]
        [AllowAnonymous]
        public IActionResult V2_BlogTest()
        {
            return Ok(new
            {
                msg = "获取成功",
                success = true,
                response = "我是第二版的博客信息"
            });
        }
    }
}
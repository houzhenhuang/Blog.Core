using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blog.Core.Api.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Api.SwaggerHelper
{
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        public string GroupName { get; set; }

        /// <summary>
        /// 自定义路由构造函数，继承基类路由
        /// </summary>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(string actionName = "[action]")
            : base("/api/{version}/[controller]/" + actionName)
        {

        }
        /// <summary>
        /// 自定义版本+路由构造函数，继承基类路由
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="version"></param>
        public CustomRouteAttribute(ApiVersions version, string actionName = "")
            : base($"/api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Core.Api.Services.Requirements.PermissionPolicy
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly HttpContext _httpContext;

        public PermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissions = requirement.Permissions.Where(p => p.Url != null).ToList();
            //请求Url
            var questUrl = _httpContext.Request.Path.Value.ToLower();
            //是否经过验证
            //var isAuthenticated = _httpContext.User.Identity.IsAuthenticated;
            if (_httpContext.User.Identity != null)
            {
                //某用户权限列表中是否有请求的url
                if (permissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    var userRoles = _httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    if (!permissions.Where(p => userRoles.Contains(p.Role)).Any())
                    {
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }
                else
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                //过期判断
                var expire = _httpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;

                if (!(expire != null && int.TryParse(expire, out int iExpire) && iExpire >= DateTimeOffset.Now.ToUnixTimeSeconds()))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
            else
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Core.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private UserIdentity _userIdentity;
        protected UserIdentity UserIdentity
        {
            get
            {
                if (_userIdentity == null)
                {
                    var claims = HttpContext.User.Claims;
                    _userIdentity = new UserIdentity();
                    if (int.TryParse(claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)?.Value, out int userId))
                    {
                        _userIdentity.UserId = userId;
                    }
                    _userIdentity.UserName = claims.SingleOrDefault(c => c.Type.Equals(nameof(_userIdentity.UserName)))?.Value;
                    _userIdentity.RealName = claims.SingleOrDefault(c => c.Type.Equals(nameof(_userIdentity.RealName)))?.Value;
                    _userIdentity.Avatar = claims.SingleOrDefault(c => c.Type.Equals(nameof(_userIdentity.Avatar)))?.Value;
                }
                return _userIdentity;
            }
        }
    }
}
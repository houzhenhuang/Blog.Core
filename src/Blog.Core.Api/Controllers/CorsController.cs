using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Api.AuthHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorsController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        public CorsController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }
        [HttpGet]
        [Route("Jsonp")]
        public void GetJsonp(string callBack, int id = 1, string role = "Admin")
        {
            var token = _jwtHelper.IssueJwt(new TokenModelJwt { UserId = id, Roles = new List<string> { role } });

            string response = string.Format("\"value\":\"{0}\"", token);
            var call = callBack + "({" + response + "})";
            Response.WriteAsync(call);
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public async Task<ActionResult> GetToken()
        {
            var token = _jwtHelper.IssueJwt(new TokenModelJwt { UserId = 1, Roles = new List<string>() { "Admin" } });
            return await Task.Run(() =>
            {
                return Ok(new
                {
                    token
                });
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Api.SwaggerHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Blog.Core.Api.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Api.Controllers.v1
{
    [CustomRoute(ApiVersions.v1)]
    //[Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "第一版" };
        }
    }
}
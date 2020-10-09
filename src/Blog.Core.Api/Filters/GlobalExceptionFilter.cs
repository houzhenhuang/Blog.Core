using Blog.Core.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blog.Core.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var errorResponse = new JsonErrorResponse();
            if (context.Exception.GetType() == typeof(UserOperationException))
            {
                errorResponse.Message = context.Exception.Message;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
            else
            {
                errorResponse.Message = "发生了未知的内部错误";
                if (_env.IsDevelopment())
                {
                    errorResponse.DeveloperMessage = context.Exception?.StackTrace;//堆栈信息
                }
                context.Result = new InternalServerErrorObjectResult(errorResponse);
            }
            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object message)
            : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}

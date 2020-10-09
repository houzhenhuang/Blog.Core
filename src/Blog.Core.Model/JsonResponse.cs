using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class JsonResponse
    {
        public JsonResponse() { }
        public JsonResponse(bool success)
        {
            this.Success = success;
        }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "操作成功";
    }
    public class JsonResponse<T> : JsonResponse
    {
        public T Data { get; set; }
    }
}

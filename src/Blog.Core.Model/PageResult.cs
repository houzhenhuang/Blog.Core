using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class PageResult<T> : JsonResponse<List<T>>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.SwaggerHelper
{
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// v1 版本
            /// </summary>
            v1 = 1,
            /// <summary>
            /// v2 版本
            /// </summary>
            v2 = 2,
        }
    }
}
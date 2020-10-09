using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Account
{
    public class JwtTokenDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.AuthHelper
{
    public class TokenModelJwt
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Work { get; set; }
    }
}

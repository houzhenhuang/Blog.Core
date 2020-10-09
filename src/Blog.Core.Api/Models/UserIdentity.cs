using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.Models
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Avatar { get; set; }
    }
}

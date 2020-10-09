using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.User
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public int Sex { get; set; }
        public string Avatar { get; set; }
    }
}

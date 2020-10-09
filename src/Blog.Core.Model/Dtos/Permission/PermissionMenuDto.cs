using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Permission
{
    public class PermissionMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
        public string Path { get; set; }
        public bool IsMenuItem { get; set; }
        public PermissionMenuMeta Meta { get; set; }
        public List<PermissionMenuDto> Children { get; set; } = new List<PermissionMenuDto>();

        [JsonIgnore]
        public string Icon { get; set; }
    }
    public class PermissionMenuMeta
    {
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}

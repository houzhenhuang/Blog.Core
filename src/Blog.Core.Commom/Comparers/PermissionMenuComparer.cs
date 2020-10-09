using Blog.Core.Model.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Common.Comparers
{
    public class PermissionMenuComparer : IEqualityComparer<PermissionMenuDto>
    {
        public bool Equals(PermissionMenuDto x, PermissionMenuDto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(PermissionMenuDto obj)
        {
            return obj.Id;
        }
    }
}

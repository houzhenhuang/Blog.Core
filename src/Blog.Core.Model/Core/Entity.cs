using SqlSugar;

namespace Blog.Core.Model.Core
{
    public class Entity
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Common.Helper
{
    public class AppSettings
    {
        /// <summary>
        /// redis缓存
        /// </summary>
        public RedisCaching RedisCaching { get; set; }
        /// <summary>
        /// memory缓存
        /// </summary>
        public MemoryCaching MemoryCaching { get; set; }
        /// <summary>
        /// sqlserver
        /// </summary>
        public SqlServer SqlServer { get; set; }
        /// <summary>
        /// 跨域信息
        /// </summary>
        public Cors Cors { get; set; }
    }
    public class RedisCaching
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
    public class MemoryCaching
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
    public class SqlServer
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string SqlServerConnection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderName { get; set; }
    }
    /// <summary>
    /// 跨域信息
    /// </summary>
    public class Cors
    {
        public List<string> Ips { get; set; }
    }
}

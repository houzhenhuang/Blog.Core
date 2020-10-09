using SqlSugar;
using System;
using System.Collections.Generic;

namespace ConsoleApp01
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlSugarClient db = new SqlSugarClient(new List<ConnectionConfig>()
            {
                new ConnectionConfig()
                {
                    DbType = DbType.SqlServer,
                    ConnectionString = "server=.;uid=sa;pwd=123456;database=BlogDB",
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true
                }
            });
            try
            {
                db.BeginTran();
                db.Insertable<SysUserRole>(new
                {
                    UserId = 5,
                    RoleId = 1
                }).ExecuteCommand();
                db.RollbackTran();
                //throw new Exception();
                //db.CommitTran();
            }
            catch
            {
                db.RollbackTran();
            }

            Console.WriteLine("Hello World!");
        }
    }
    public class SysUserRole
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
    }
}

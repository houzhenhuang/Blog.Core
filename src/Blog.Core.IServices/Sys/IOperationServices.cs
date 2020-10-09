using Blog.Core.IServices.Base;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Operation;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IOperationServices : IBaseServices<SysOperation>
    {
        /// <summary>
        /// 获取菜单操作功能
        /// </summary>
        /// <returns></returns>
        Task<List<OperationDto>> GetOperationsByMenuId(int menuId);
    }
}

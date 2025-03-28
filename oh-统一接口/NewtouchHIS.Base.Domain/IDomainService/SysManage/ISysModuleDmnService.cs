using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// 系统菜单 （增删改接口）
    /// </summary>
    public interface ISysModuleDmnService : IScopedDependency
    {
        /// <summary>
        /// 查询菜单by id
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysModuleEntity?> GetEntity(string id);
        /// <summary>
        /// 查询菜单by id(仅有效)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        Task<SysModuleEntity?> GetEntity(string id, bool valid = true);
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> AddEntity(SysModuleVO vo, string user, string orgId);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> AddRange(List<SysModuleVO> vo, string user, string orgId, string defaultParentId);
        /// <summary>
        /// 菜单信息更新
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> UpdateEntity(SysModuleVO vo, string user, string orgId);
        /// <summary>
        /// 作废菜单
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<BusResult<string>> DelEntity(string keyValue, string user, string orgId);
        /// <summary>
        /// 根据组织机构获取有效菜单列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysModuleEntity>> GetEntitybyorgId(string orgId);
    }
}

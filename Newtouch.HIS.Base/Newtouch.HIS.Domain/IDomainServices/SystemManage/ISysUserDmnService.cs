using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// 提交新建、更新 系统用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="roleList"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue);

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserVO> GetPagintionList(Pagination pagination, string OrganizeId, string keyword = null);

        /// <summary>
        /// 根据系统（登录）用户Id 获取 系统人员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysStaffEntity GetStaffByUserId(string userId);

        /// <summary>
        /// 获取系统人员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysUserVO GetSysUserByUserId(string userId);

        /// <summary>
        /// 获取隶属于角色 的 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        IList<RoleUserVO> GetPagintionUserListByRoleId(Pagination pagination, string gh, string name, string roleId, string topOrganizeId);

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        void CreateUser(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity);

        /// <summary>
        /// 根据UserId获取关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId);

        /// <summary>
        /// 获取用户的语言类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// 根据员工Id获取所有岗位
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="StaffId"></param>
        /// <returns></returns>
        IList<string> GetDutyListByStaffId(string orgId, string StaffId);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string userId);

        string GetYbdmByGh(string rygh);
    }
}

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public interface ISysUserExDmnService
    {
        /// <summary>
        /// 获取用户可操作的药房（药库）列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetUserPharmacyDepartmentList(string orgId, string userId);

        /// <summary>
        /// 验证用户名密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserVEntity CheckLogin(string username, string password);

        /// <summary>
        /// 根据UserId获取关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        void UpdateUserRole(string keyValue, string[] roleList);

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId);

        /// <summary>
        /// 获取组织机构的人员列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId);

        /// <summary>
        /// 获取用户的语言类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// 根据父级OrgId（递归树）获取 所有 指定 职位 的 StaffId
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        List<string> GetStaffIdListByParentOrg(string parentOrgId, string dutyCode);

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

        /// <summary>
        /// 根据UserId获取系统用户可操作的药房部门Code List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId, string orgId);

        /// <summary>
        /// 根据UserId获取系统用户可操作的药房部门 List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<LoginUserCurrentYfbmModel> GetYfbmListByUserId(string userId, string orgId);

        IList<SysMSGQueryVO> MSGQuery(string yfbmcode,string orgid,int gqyj,string kcyj);
    }
}

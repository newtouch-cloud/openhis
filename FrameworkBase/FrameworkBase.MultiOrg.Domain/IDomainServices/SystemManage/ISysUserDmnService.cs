using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// 验证用户登录，成功登录返回用户实体，否则会抛提示异常
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserVEntity CheckLogin(string username, string password);

        /// <summary>
        /// 获取UserId关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId, string keyword = null);

        /// <summary>
        /// 人员检索
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<StaffSearchReusltDTO> GetSatffList(string orgId, string keyword = null);

        /// <summary>
        /// 获取组织机构的有效人员列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrg(string orgId);

        /// <summary>
        /// 获取用户的语言类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetLanguageTypeByUserId(string userId);

        /// <summary>
        /// 根据DutyCode（职位）查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string keyword = null);

        /// <summary>
        /// 人员岗位关联关系 List
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffDutyComplexVEntity> GetStaffDutyListByOrganizeId(string orgId, string staffId = null);

        /// <summary>
        /// 根据父级OrgId（递归树）获取 所有 指定 岗位 的 StaffId
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        List<string> GetStaffIdListByDutyAndParentOrg(string parentOrgId, string dutyCode);

        /// <summary>
        /// Check员工是否属于某岗位
        /// </summary>
        /// <param name="staffId">员工Id</param>
        /// <param name="dutyCode">岗位Code</param>
        /// <returns></returns>
        bool CheckStaffIsBelongDuty(string staffId, string dutyCode);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="userId"></param>
        void RevisePassword(string userPassword, string userId);

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserVO> GetPagintionUserList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// 根据人员登录账号 获取 人员姓名
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByAccount(string account, string orgId);

        /// <summary>
        /// 查询用户所绑定科室
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysDepartmentVEntity> SelectUserDepartment(string userCode, string organizeId);

        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="msEmaile">用户微软邮箱</param>
        /// <param name="organizeId">机构ID</param>
        /// <returns></returns>
        SysUserVEntity MsSsoLogin(string msEmaile, string organizeId);

        /// <summary>
        /// 微软SSO登录
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        SysUserVEntity MsSso(string account, string pwd);
    }
}

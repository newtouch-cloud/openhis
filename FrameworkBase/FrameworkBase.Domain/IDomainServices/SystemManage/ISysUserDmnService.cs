using System.Collections.Generic;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public interface ISysUserDmnService
    {
        /// <summary>
        /// 验证用户登录，成功登录返回用户实体，否则会抛提示异常
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserEntity CheckLogin(string username, string password);

        /// <summary>
        /// 根据UserId获取关联的系统人员列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetStaffListByUserId(string userId);

        /// <summary>
        /// 获取系统（登录）用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysUserVO> GetPagintionList(Pagination pagination, string keyword = null);

        /// <summary>
        /// 提交新建、更新 系统用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue);

        /// <summary>
        /// 获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <returns></returns>
        IList<SysUserStaffVO> GetSatffVOList();

    }
}

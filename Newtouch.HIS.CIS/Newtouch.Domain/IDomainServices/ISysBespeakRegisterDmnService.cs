using Newtouch.Core.Common;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 预约管理
    /// </summary>
    public interface ISysBespeakRegisterDmnService
    {
        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(Pagination pagination, string keyword, string OrganizeId);

        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string gh, string OrganizeId, DateTime? regDate, string regTime = "");

        /// <summary>
        /// 根据科室和排班日期获取系统预约排班
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, DateTime regDate, string organizeId);

        /// <summary>
        /// 根据科室获取系统预约排班
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string organizeId);
    }
}

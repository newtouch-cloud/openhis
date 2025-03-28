using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 预约挂号
    /// </summary>
    public interface ISysBespeakRegisterRepo : IRepositoryBase<SysBespeakRegisterEntity>
    {

        /// <summary>
        /// 获取时段
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="gh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RegTimeVO> GetRegTime(string deptCode, DateTime regDate, string gh, string organizeId);
    }
}

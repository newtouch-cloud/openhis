using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGuianRybl21OutInfoRepo : IRepositoryBase<GuianRybl21OutInfoEntity>
    {
        /// <summary>
        /// 根据住院号获取贵安医保住院反馈信息
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="OrganizeId">组织机构ID</param>
        /// <returns></returns>
        GuianRybl21OutInfoEntity GetInfoByZyh(string zyh, string OrganizeId);
    }
}

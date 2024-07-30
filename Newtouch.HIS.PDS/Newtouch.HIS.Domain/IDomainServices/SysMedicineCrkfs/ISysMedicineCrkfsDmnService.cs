using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public interface ISysMedicineCrkfsDmnService
    {
        /// <summary>
        /// 获取出入库方式
        /// </summary>
        /// <param name="crkbz">出入库标志 0：入库  1：出库</param>
        /// <returns></returns>
        List<SysMedicineCrkfsVEntity> GetList(string crkbz);
    }
}
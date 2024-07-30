using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院药品医嘱执行批号
    /// </summary>
    public interface IZyYpyzzxphRepo : IRepositoryBase<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// 获取住院有效未归架的排药信息  此方法不加日志
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">执行Id</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// 获取住院有效未归架的排药信息  此方法不加日志
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string pc, string ph, string organizeId);
    }
}
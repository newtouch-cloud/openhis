using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院药品医嘱执行
    /// </summary>
    public interface IZyYpyzxxRepo : IRepositoryBase<ZyYpyzxxEntity>
    {

        /// <summary>
        /// 获取需要发药的病区
        /// </summary>
        /// <returns></returns>
        List<string> GetFyBq();

        /// <summary>
        /// 获取需要发药的病人
        /// </summary>
        /// <returns></returns>
        List<string> GetFyPatients();

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string organizeId);

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">医嘱执行ID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string organizeId);

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">医嘱执行ID</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// 修改发药标志位已退药
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        int UpdateFybzTy(string yzId, string zxId, string userCode, string organizeId, EFDbTransaction db);

        /// <summary>
        /// 根据执行ID，物理删除此次执行的所有明细
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        int DeleteByZxId(string zxId);
    }
}
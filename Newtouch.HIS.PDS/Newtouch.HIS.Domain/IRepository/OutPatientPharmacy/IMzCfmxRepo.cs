using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 门诊处方
    /// </summary>
    public interface IMzCfmxRepo : IRepositoryBase<MzCfmxEntity>
    {
        /// <summary>
        /// 根据处方号获取处方明细
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        List<MzCfmxEntity> GetCfmxByCfh(string cfh);

        /// <summary>
        /// 判断是否已存在
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gg">规格</param>
        /// <returns></returns>
        bool IsExist(string cfh, string ypCode, string organizeId, string gg = "");

        /// <summary>
        /// 删除老的处方明细
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <returns></returns>
        int DeleteCfmx(string cfh);

        /// <summary>
        /// 删除老的处方明细
        /// </summary>
        /// <param name="cfhs">处方号列表</param>
        /// <returns></returns>
        int DeleteCfmx(List<string> cfhs);
    }
}
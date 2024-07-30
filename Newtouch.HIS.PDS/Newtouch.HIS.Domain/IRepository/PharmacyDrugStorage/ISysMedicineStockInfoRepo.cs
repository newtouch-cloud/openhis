using System;
using System.Collections.Generic;
using System.Data.Common;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_kcxx
    /// </summary>
    public interface ISysMedicineStockInfoRepo : IRepositoryBase<SysMedicineStockInfoEntity>
    {

        /// <summary>
        /// 外部出库变更库存冻结数量
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="djsl"></param>
        void UpdateDjsl(string kcId, int djsl);

        /// <summary>
        /// get xt_yp_kcxx by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlParameter"></param>
        /// <returns></returns>
        object FindEntity<T>(string sql, DbParameter[] sqlParameter);

        /// <summary>
        /// get xt_yp_kcxx by yfbmcode,organizeId and pc
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        SysMedicineStockInfoEntity FindEntity(string yfbmCode, string organizeId, string pc);

        /// <summary>
        /// 修改库存状态
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateZt(string ypdm, string ph, string pc, string zt, string yfbmCode, string organizeId);

        /// <summary>
        /// 计算可用库存 当前方法无日志
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineStockInfoEntity> SelectData(string ypCode, string yfbmCode, string organizeId);

        /// <summary>
        /// 计算可用库存 当前方法无日志
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        List<SysMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode,
           string organizeId);

        /// <summary>
        /// 减少冻结库存
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractForzenKc(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId,
           string userCode);

        /// <summary>
        /// 减少djsl和kcsl
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractDjslAndKcsl(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId,
           string userCode);

        /// <summary>
        /// 减少冻结库存
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="kcId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int SubtractForzenKc(int sl, string kcId, string organizeId, string userCode);

        int UpdateExpired(string kcId, string yxq);
    }
}

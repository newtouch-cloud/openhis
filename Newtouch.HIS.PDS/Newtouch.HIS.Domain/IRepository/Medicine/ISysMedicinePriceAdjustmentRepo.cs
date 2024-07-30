using Newtouch.HIS.Domain.Entity;
using System.Collections;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinePriceAdjustmentRepo : IRepositoryBase<SysMedicinePriceAdjustmentEntity>
    {
        /// <summary>
        /// 获取一个调价的entity 未审核 未执行
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotApproveEntity(string ypCode);

        /// <summary>
        /// 获取一个调价的entity 已审核 未执行
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotExecteEntity(string ypCode);

        /// <summary>
        /// 添加药品调价记录(申请)
        /// </summary>
        /// <param name="entity"></param>
        void AddPriceAdjustmentRecord(SysMedicinePriceAdjustmentEntity entity);

        /// <summary>
        /// 检查是否有未处理的数据
        /// </summary>
        /// <param name="ypCodeList"></param>
        string CheckStatus(ArrayList ypCodeList);

        ///  <summary>
        ///  药品调价
        ///  </summary>
        ///  --shzt:0:未审核 1:已审核 2:已拒绝 3.已撤销
        /// 	--zxbz:0:未执行 1:已执行
        /// <param name="ypCodeList"></param>
        /// <param name="operationType"></param>
        string MedicinePriceAdjustment(ArrayList ypCodeList, int operationType);
    }
}

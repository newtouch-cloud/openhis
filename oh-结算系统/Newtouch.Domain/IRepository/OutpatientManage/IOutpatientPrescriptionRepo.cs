
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 处方主表
    /// </summary>
    public interface IOutpatientPrescriptionRepo : IRepositoryBase<OutpatientPrescriptionEntity>
    {
        /// <summary>
        /// 根据cfh获取 处方实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <returns></returns>
        OutpatientPrescriptionEntity GetValidEntityByCfh(string orgId, string cfh);

        /// <summary>
        /// 根据cfnm获取 处方实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        OutpatientPrescriptionEntity GetValidEntityByCfnm(string orgId, int cfnm);

        /// <summary>
        /// 生成新处方号
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type">处方类型1药品 2非药品</param>
        /// <returns></returns>
        string GeneratePresNo(string orgId, int type);

    }
}

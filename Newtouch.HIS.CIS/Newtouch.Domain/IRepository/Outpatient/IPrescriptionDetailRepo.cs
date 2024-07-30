using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 处方明细
    /// </summary>
    public interface IPrescriptionDetailRepo : IRepositoryBase<PrescriptionDetailEntity>
    {

        /// <summary>
        /// 获取处方明细
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz);
        /// <summary>
        /// 获取代煎项目
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionDetailEntity> SelectDataDjxm(string cfId);
    }
}

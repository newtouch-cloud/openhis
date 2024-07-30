using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 门诊处方明细批号信息 mz_cfmxph
    /// </summary>
    public interface IOutpatientPrescriptionDetailBatchNumberRepo : IRepositoryBase<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// 获取所有门诊处方明细批号信息
        /// </summary>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList();

        /// <summary>
        /// 根据处方号和组织机构获取未归架的门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string gjzt = "0");

        /// <summary>
        /// 根据处方号和组织机构获取门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string yfbmCode, string organizeId, string gjzt = "0");

        /// <summary>
        /// 归架药品
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int GJDrug(string ypCode, string pc, string ph, string cfh, string yfbmCode, string organizeId,
           string userCode);
    }
}

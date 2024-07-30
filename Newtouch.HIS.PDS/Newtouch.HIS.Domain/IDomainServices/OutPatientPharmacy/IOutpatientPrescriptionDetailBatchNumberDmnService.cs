using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// mz_cfmxph IDmnService
    /// </summary>
    public interface IOutpatientPrescriptionDetailBatchNumberDmnService
    {
        /// <summary>
        /// 修改归架标志
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateGjztbyCfh(string cfh, string organizeId);

        /// <summary>
        /// 根据处方号和组织机构获取未归架的门诊处方明细批号信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="ypCode"></param>
        /// <param name="gjzt">归架状态：0-未归架  1-已归架</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string ypCode = "", string gjzt = "0");
    }
}

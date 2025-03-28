using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 门诊发药
    /// </summary>
    public interface IOutPatientDispensingApp
    {
        /// <summary>
        /// 执行发药
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="cfhs">发药处方号</param>
        /// <returns></returns>
        string ExecAllDeliveryDrug(patientInfoVO[] patients, string yfbmCode, string userCode, string organizeId, out List<string> cfhs);

        /// <summary>
        /// 执行发药
        /// </summary>
        /// <param name="deliveryInfo"></param>
        /// <param name="cfhList">发药处方号</param>
        /// <returns></returns>
        string ExecAllDeliveryDrugV2(OutpatienDrugDeliveryInfo deliveryInfo, out List<string> cfhList);
    }
}
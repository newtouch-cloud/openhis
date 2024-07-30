using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 退药
    /// </summary>
    public interface ItyDmnService
    {
        /// <summary>
        /// 查询可退药品信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// 查询全退可退药品信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectCompleteRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// 查询药品可退数量
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>最小单位可退数量</returns>
        int SelectRefundedSl(string ypCode, string pc, string ph, string yfbmCode, string organizeId);

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrug(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrugSingleThread(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// 根据处方号获取有效的处方
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// 门诊部分退药
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutpatientPartReturn(ReturnItemData item, string organizeId);
    }
}
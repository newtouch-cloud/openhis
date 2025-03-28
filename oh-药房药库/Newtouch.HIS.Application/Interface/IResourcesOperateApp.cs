using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 取消预定
    /// </summary>
    public interface IResourcesOperateApp
    {
        /// <summary>
        /// 门诊预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult OutpatientBook(OutpatientBookRequestDTO request);

        /// <summary>
        /// 门诊预定修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult OutpatientBookModify(OutpatientBookModifyRequestDTO request);

        /// <summary>
        /// 门诊取消预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelBook(OutpatientCancelPartBookRequestDTO request);

        /// <summary>
        /// 门诊取消预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelBook(OutpatientCancelAllBookRequestDTO request);

        /// <summary>
        /// 门诊退费 冻结库存返还
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelDjYpReturn(OutpatientCancelAllBookRequestDTO request);

        /// <summary>
        /// 门诊确定资源（commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCommit(OutpatientCommitRequestDTO request);

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyParam"></param>
        /// <returns></returns>
        string OutpatientReturnDrugs(tyInfo tyParam, out List<string> returnDrugBillNoList);

        /// <summary>
        /// (未发药前)部分退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request);

        /// <summary>
        /// 住院保存医嘱  落地医嘱信息、排药、冻结库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ActResult HospitalizatiionBook(ZyypyzzxRequest request);

        /// <summary>
        /// 住院退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request);

        /// <summary>
        /// 住院退药
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">退药单号</param>
        /// <returns></returns>
        string HospitalizatiionReturnMedicine(List<tyParam> tyDetail, string yfbmCode, string organizeId,
            string userCode, out string returnDrugBillNo);

        /// <summary>
        /// 门诊取消排药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request);

        string HospitalizatiionReturnMedicineV2(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo);
    }
}
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Inpatient
{
    public interface IWardMaintenanceDmnService
    {
        /// <summary>
        /// 获取当天有效医嘱
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        List<BedItemsVO> GetBedItems(WardMaintenanceRequestDto req);
        IList<Dispensing> GetDispensings(Pagination pagination, DispensingMXRequestDto req);
        IList<DispensingMX> GetDispensingMXs(Pagination pagination, string bysj);
        /// <summary>
        /// 保存床位绑定费用信息
        /// </summary>
        /// <param name="req"></param>
        void SaveBedItems(List<BedItemsVO> mxList, WardMaintenanceRequestDto req, string zyh);
        /// <summary>
        /// 获取床位绑定医生信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedDocsVO> GetBedDocs(patRequestDto req);

        /// <summary>
        /// 保存床位绑定医生信息
        /// </summary>
        /// <param name="req"></param>
        void SaveBedDocs(List<BedDocsVO> mxList, patRequestDto req);

        /// <summary>
        /// 获取患者诊断信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatDiagnosisVO> GetPatDiagnosis(patRequestDto req);
        /// <summary>
        /// 验证患者能否出区
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsOutArea(patBedFeeRequestDto req);
        /// <summary>
        /// 保存患者诊断信息
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        string SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO, patRequestDto req);
        /// <summary>
        /// 保存出院诊断患者信息
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        /// <returns></returns>
       string SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO, patRequestDto req);
		/// <summary>
		/// 保存出区诊断List
		/// </summary>
		/// <param name="PatDxList"></param>
		/// <returns></returns>
		string SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList,string OrganizeId);
		/// <summary>
		/// 保存出院诊断Entity
		/// </summary>
		/// <param name="PatDxEntity"></param>
		/// <returns></returns>
		//string SavaPatDxEntity(InpatientPatientDiagnosisEntity PatDxEntity);
		/// <summary>
		/// 获取住院床卡
		/// </summary>
		/// <param></param>
		List<InpatientBedCardVo> GetBedCard(string zyh, string OrganizeId);

        InpatientVo GetZyPatInfo(string zyh,string orgId);

        InpatiContinuePrintVo GetZyPatContinuePrint(string zyh, string orgId, string yzlx);
        InpatiContinuePrintPageVo GetZyPatContinuePrintPage(string zyh, string orgId, string yzlx, string xh, string page);

    }
}

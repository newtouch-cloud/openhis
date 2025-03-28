using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.HospitalizationManage;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.Infrastructure;
using System;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public class InpatientRefundApp: AppBase, IInpatientRefundApp
    {
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;

        /// <summary>
        /// 获取(计费和已退合计的)计费明细（包括治疗项目和非治疗项目）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatStatusDetailDto InpatientRefundQuery(string zyh, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null)
        {
            // 获取病人信息
            var settpatInfo = GetInpatientPatInfo(zyh);

            // 获取计费明细（包括：项目和药品）
            var settleItemsBo = GetInpatientRefundItemsDetailList(zyh, kssj, jssj, ks, xmlb, xmmc);

            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                InpatientSettleItemBO = settleItemsBo
            };
            return resultDto;

        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetInpatientPatInfo(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settPatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settPatinfoList.First();
            if (settpatinfo.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_WRY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (settpatinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            //else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            //{
            //    throw new FailedCodeException("HOSP_ERROR_PATIENT_NATURE_IS_NO_FOUND");
            //}
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            return settpatinfo;
        }

        /// <summary>
        /// 获取(计费和已退合计的)计费明细（包括治疗项目和药品）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private InpatientSettleItemBO GetInpatientRefundItemsDetailList(string zyh, DateTime? kssj, DateTime? jssj,string ks=null)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            //zy_xmjfb
            var treatmentItemsList = _dischargeSettleDmnService.SelectWsfItemList(zyh,this.OrganizeId, kssj, jssj,ks);
            //zy_ypjfb
            var drugList = _dischargeSettleDmnService.SelectWsfDrugList(zyh, this.OrganizeId, kssj, jssj,ks);
            //非治疗项目
            //var non_treatmentItemsList = _dischargeSettleDmnService.SelectNonTreatmentItemList(zyh, this.OrganizeId);
            var resultBo = new InpatientSettleItemBO()
            {
                TreatmentItemList = treatmentItemsList,
                DrugList = drugList
                //Non_treatmentItemList = non_treatmentItemsList
            };
            return resultBo;
        }
        /// <summary>
        /// 获取(计费和已退合计的)计费明细（包括治疗项目和药品）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private InpatientSettleItemBO GetInpatientRefundItemsDetailList(string zyh, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            //zy_xmjfb
            var treatmentItemsList = _dischargeSettleDmnService.SelectWsfItemList(zyh, this.OrganizeId, kssj, jssj, ks, xmlb, xmmc);
            //zy_ypjfb
            var drugList = _dischargeSettleDmnService.SelectWsfDrugList(zyh, this.OrganizeId, kssj, jssj, ks, xmlb, xmmc);
            //非治疗项目
            //var non_treatmentItemsList = _dischargeSettleDmnService.SelectNonTreatmentItemList(zyh, this.OrganizeId);
            var resultBo = new InpatientSettleItemBO()
            {
                TreatmentItemList = treatmentItemsList,
                DrugList = drugList
                //Non_treatmentItemList = non_treatmentItemsList
            };
            return resultBo;
        }
    }
}

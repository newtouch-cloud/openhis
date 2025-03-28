using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.NonTreatmentItemManage
{
    public interface INonTreatmentItemDmnService
    {
        /// <summary>
        /// 根据卡号查patid和xm
        /// </summary>
        IList<PatientInfoVO> SelectPatientInfoByblhOrzyh(string orgId, string blh, string zyh, string xm);

        /// <summary>
        /// 获取收费项目分类List
        /// </summary>
        IList<SysChargeCategoryVEntity> GetChargeCategoryTreeList(string orgId);

        /// <summary>
        /// 非治疗项目记账查询
        /// </summary>
        IList<NonTreatmentItemBillingInfoVO> SelectNonTreatmentItemList(Pagination pagination, string sfdl, string keyword, string smry, string smks, DateTime? kssj, DateTime? jssj, string orgId, string kehu, string zyh, string blh);

        /// <summary>
        /// 记账退费查询
        /// </summary>
        IList<NonTreatmentItemBillingInfoVO> SelectRefundItemList(Pagination pagination, string zyh, string blh, string keyword, string smry, string kehu, DateTime? kssj, DateTime? jssj, string orgId);
    }
}

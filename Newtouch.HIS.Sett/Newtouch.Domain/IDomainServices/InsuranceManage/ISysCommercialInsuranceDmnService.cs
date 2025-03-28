using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.InsuranceManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysCommercialInsuranceDmnService
    {
        /// <summary>
        /// 获取医保备案列表
        /// </summary>
        List<SysCommercialInsuranceFilingVO> SelectCommercialInsuranceFilingList(string keyword, string orgId, string sbbabId = null);

        /// <summary>
        /// 提交商保
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="KeyValue"></param>
        void SubmitForm(SysCommercialInsuranceVO entity, string KeyValue, string orgId);
    }
}

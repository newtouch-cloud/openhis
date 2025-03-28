using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.KnowledgeBaseManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.KnowledgeBaseManage
{
    public interface IMedicalInsuranceDmnService
    {
        /// <summary>
        /// 获取医保备案列表
        /// </summary>
        List<SysMedicalInsuranceFilingVO> SelectMedicalInsuranceFilingList(Pagination pagination, string keyword, string orgId, string ybbabId = null);

    }
}

using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface ITreatmentPrintDmnService
    {
        /// <summary>
        /// 获取病区发药患者树
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
         IList<InpWardPatTreeVO> GetPatTree(string orgId);

        IList<TreatmentPrintVO> GetDetailGridJson(Pagination pagination, string orgId,string zyh, DateTime? kssj, DateTime? jssj, int? zyxz);
    }
}
using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface ILevelChargeDmnService
    {

        /// <summary>
        /// 获取等级费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
         IList<SysLevelChargeVO> GetLevelCharge(Pagination pagination, string orgId, string LevelCode);

         SysLevelChargeVO GetFormJson(string orgId, string keyValue);
    }
}
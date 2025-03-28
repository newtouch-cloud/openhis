using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.EMR
{
    public interface IMedicalApplyHandleDmnService : IScopedDependency
    {
        /// <summary>
        /// 申请审批
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        Task<bool> ApplyApprove(MedicalApplyApproveVo vo, string orgId, DBEnum db = DBEnum.EmrDb);
    }
}

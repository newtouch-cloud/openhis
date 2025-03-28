using Newtouch.Domain.Api;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Inpatient
{
    public interface IDeptMedicineApplyNoDmnService
    {
        /// <summary>
        /// 查询备药申请单状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>

        List<DeptApplyNoResp> SelectApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// 更新科室备药申请单状态
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="SqdArray"></param>
        /// <returns></returns>
        int UpdateApplyNoStatus(string OrganizeId, string SqdArray,string UserCode);

        /// <summary>
        /// 库存退回申请单回退验证
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        string SelectKcthApplyNoStatus(string orgId, string sqdArr, string UserCode);
        /// <summary>
        /// 更新科室退库存申请状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        int UpdateReturnApplyNoStatus(string orgId, string sqdArr, string UserCode);
    }
}

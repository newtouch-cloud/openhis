using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 申领单管理
    /// </summary>
    public interface IApplyBillApp
    {
        /// <summary>
        /// 提交科室申领单
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId);

        /// <summary>
        /// 提交申领发货
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode);
    }
}
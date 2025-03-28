using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 部门 药品
    /// </summary>
    public interface IDepartmentMedicineInfoDmnService
    {
        /// <summary>
        /// 获取药房的药品 库存数量和单位
        /// </summary>
        /// <param name="yfbmCode">当前药房部门</param>
        /// <param name="ypCode">药品代码</param>
        /// <param name="orgId">组织机构</param>
        /// <param name="fybm">发药部门</param>
        /// <returns></returns>
        DepartmentMedicineStockUnitVO GetYpKcslAndYpdw(string yfbmCode, string ypCode, string orgId, string fybm);

        /// <summary>
        /// 获取 内部申领 药品数据源
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fybm"></param>
        /// <param name="orgId"></param>
        IList<RequisitionDepartmentMedicineSeleVO> GetRequisitionDepartmentMedicineSeleVOList(string keyword, string fybm, string orgId);
    }
}

using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 内部申领
    /// </summary>
    public interface IRequisitionDmnService
    {
        /// <summary>
        /// 新申领 提交保存
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="fybmCode"></param>
        /// <param name="slbmCode"></param>
        /// <param name="mxList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool SubmitRequisition(string sldh, string fybmCode, string slbmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> mxList, string orgId);

        /// <summary>
        /// 内部申领单 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm">申领部门</param>
        /// <param name="ckbm">出库部门</param>
        /// <param name="ffzt">发放状态</param>
        /// <param name="allUseableFfzt"></param>
        /// <returns></returns>
        IList<RequisitionSelectVO> RequisitionSearch(Pagination pagination, string orgId, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string allUseableFfzt);

        /// <summary>
        /// 内部申领单 明细查询
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<RequisitionSelectDetailVO> RequisitionDetailListBySlId(string sldId, string orgId);

    }
}

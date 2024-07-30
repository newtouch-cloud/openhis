using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    public interface IOperationDmnService
    {
        /// <summary>
        /// 手术安排列表查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ArrangeQueryGridVO> ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req);

        /// <summary>
        /// 手术安排内容获取
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        ArrangementVO ArrangementForID(Pagination pagination, ArrangementRequestVO req);

        /// <summary>
        /// 获取手术医嘱病人
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="brzybzType"></param>
        /// <returns></returns>
        List<OperatPatVO> GetOperationPatSearchList(string psOrgId);

        /// <summary>
        /// 提交补录费用
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="FeeDetails"></param>
        void InpatientFeeDetailSubmit(string orgId, string zyh, List<InpatientFeeDetailEntity> FeeDetails);

        /// <summary>
        /// 通过住院号,医嘱性质获取费用信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<OperatFeeVO> GetOperatFee(string OrganizeId, string zyh, string yzxz);
    }
}
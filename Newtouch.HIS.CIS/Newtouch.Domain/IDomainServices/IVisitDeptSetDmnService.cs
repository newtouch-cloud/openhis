using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 医生出诊
    /// </summary>
    public interface IVisitDeptSetDmnService
    {

        /// <summary>
        /// 获取出诊医生信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetVO> SelectVisitDeptSet(Pagination pagination, string keyword, string organizeId);

        /// <summary>
        /// 获取出诊医生明细
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptDetail> SelectVisitDeptSetDetail(string ysgh, string organizeId);

        /// <summary>
        /// 获取出诊医生明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VisitDeptDetail SelectVisitDeptSetInfo(string id);

        /// <summary>
        /// 获取出诊医生信息
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <param name="id">visit_deptSet.id</param>
        /// <returns></returns>
        VisitDeptDetail SelectDoctorInfo(string ysgh, string organizeId, string id = "");
    }
}
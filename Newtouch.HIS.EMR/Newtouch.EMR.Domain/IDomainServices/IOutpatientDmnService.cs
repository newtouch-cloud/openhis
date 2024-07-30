using Newtouch.Core.Common;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IOutpatientDmnService
    {
        /// <summary>
        /// 门诊已就诊列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="jzzt"></param>
        /// <param name="mjzbz"></param>
        /// <param name="rygh"></param>
        /// <param name="kzrq"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatEntityVO> GetTreatingOrTreatedList(Pagination pagination, string orgId, int mjzbz, string rygh, DateTime? kzrq, string keyword);
        /// <summary>
        /// 根据门诊号获取患者就诊信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        TreatEntityVO GetPatMzbymzh(string orgId, string mzh,string rygh);
        /// <summary>
        /// 门诊病历树
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="mzh"></param>
        /// <param name="rygh"></param>
        /// <returns></returns>
        IList<PatMedRecordTreeVO> GetOutPatMedRecordTree(string OrgId, string mzh, string rygh);
    }
}

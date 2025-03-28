using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院入院多诊断
    /// </summary>
    public interface IHospMultiDiagnosisRepo : IRepositoryBase<HospMultiDiagnosisEntity>
    {
        /// <summary>
        /// 用过住院号获取诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<HospMultiDiagnosisEntity> GetDiagnoseByZYH(string zyh);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId);
    }
}

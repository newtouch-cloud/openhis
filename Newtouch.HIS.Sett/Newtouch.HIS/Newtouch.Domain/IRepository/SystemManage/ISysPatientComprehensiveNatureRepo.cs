using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatientComprehensiveNatureRepo : IRepositoryBase<SysPatientComprehensiveNatureEntity>
    {
        /// <summary>
        /// 根据主病人性质 获取 所有
        /// </summary>
        /// <param name="zbrxz"></param>
        /// <returns></returns>
        IList<SysPatientComprehensiveNatureEntity> GetListByZbrxz(string zbrxz, string orgId);

    }
}

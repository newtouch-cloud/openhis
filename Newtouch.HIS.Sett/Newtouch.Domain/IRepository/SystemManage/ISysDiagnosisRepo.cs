using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDiagnosisRepo : IRepositoryBase<SysDiagnosisVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SysDiagnosisVEntity> GetList(string orgId);

        /// <summary>
        /// 根据条件获取诊断下拉框
        /// </summary>
        /// <param name="zd"></param>
        /// <returns></returns>
        IList<ZDSelect> GetzdSelect(string zd, string orgId);
    }
}

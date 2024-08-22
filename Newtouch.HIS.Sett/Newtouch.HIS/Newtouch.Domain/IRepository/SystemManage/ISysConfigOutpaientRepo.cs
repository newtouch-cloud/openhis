using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysConfigOutpaientRepo : IRepositoryBase<SysConfigOutpaientEntity>
    {
        SysConfigOutpaientEntity SelectSysConfigOutPatient(string dm);
        string GetSysConfig(string dm);
    }
}

using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysConfigHospRepo : IRepositoryBase<SysConfigHospEntity>
    {
        /// <summary>
        /// 根据代码获取住院配置
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        SysConfigHospEntity GetByDM(string dm);

        /// <summary>
        /// 根据dm获取配置
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        string GetPZByDM(string dm);

    }
}

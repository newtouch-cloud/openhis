using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineDosageRepo: IRepositoryBase<SysMedicineDosageEntity>
    {
        /// <summary>
        /// 提交药品剂型对应关系
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
         void submitUsage(string jxCode, string yfCode);
    }
}

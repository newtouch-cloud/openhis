using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardBedRepo : IRepositoryBase<SysWardBedEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(SysWardBedEntity entity, int? cwId);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteForm(int cwId,string orgId);

        /// <summary>
        /// 根据床位占用情况
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="sfzy"></param>
        void UpdateOccupyByCode(string code, string orgId, bool sfzy);
    }
}

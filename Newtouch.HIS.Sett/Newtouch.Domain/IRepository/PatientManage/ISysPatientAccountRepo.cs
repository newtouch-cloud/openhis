using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientAccountRepo : IRepositoryBase<SysPatientAccountEntity>
    {
        /// <summary>
        /// 根据住院号获取账户信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        SysPatientAccountEntity GetAccInfoByZHY(string zyh, string orgId);

        /// <summary>
        /// 添加账户信息 xt_brzh
        /// </summary>
        /// <param name="entity"></param>
        bool AddAcc(SysPatientAccountEntity entity, string orgId, string curUserCode);

        /// <summary>
        /// 更新账户信息 xt_brzh
        /// </summary>
        /// <param name="entity"></param>
        bool ModifyAcc(SysPatientAccountEntity entity);
    }
}

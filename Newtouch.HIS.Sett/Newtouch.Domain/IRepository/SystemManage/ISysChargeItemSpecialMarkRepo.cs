using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysChargeItemSpecialMarkRepo : IRepositoryBase<SysChargeItemSpecialMarkEntity>
    {
        void submitform(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue);
    }
}

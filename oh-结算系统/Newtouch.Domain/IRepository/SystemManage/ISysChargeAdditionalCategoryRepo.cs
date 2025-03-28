using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysChargeAdditionalCategoryRepo : IRepositoryBase<SysChargeAdditionalCategoryEntity>
    {
        List<SysChargeAdditionalCategoryEntity> GetEffectiveList(int keyValue, string orgId);
        SysChargeAdditionalCategoryEntity GetForm(int keyValue, string orgId);
        void DeleteForm(int keyValue, string orgId);
        void SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue, string orgId);
        List<SysChargeAdditionalCategoryEntity> SelectALLEffectiveList(string orgId);
    }
}

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeTemplateItemMappRepo : IRepositoryBase<SysChargeTemplateItemMappEntity>
    {

        IList<MbmxsfxmVO> getmbmx(string sfmb, string orgId);
    }
}

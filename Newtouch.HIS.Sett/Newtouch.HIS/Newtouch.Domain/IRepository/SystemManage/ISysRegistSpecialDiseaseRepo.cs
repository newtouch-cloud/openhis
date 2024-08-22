using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{

    /// <summary>
    /// 
    /// </summary>
    public interface ISysRegistSpecialDiseaseRepo : IRepositoryBase<SysRegistSpecialDiseaseEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        SysRegistSpecialDiseaseEntity SelectSysChargeItemByghzb(string ghzb, string orgId);

        /// <summary>
        /// 获取所有挂号专病
        /// </summary>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        List<SysRegistSpecialDiseaseEntity> SelectSysChargeItemByghzbList(string orgId);
    }
}

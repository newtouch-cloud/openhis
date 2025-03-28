using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ICommonLibraryDmnService
    {
        void SyncCommonDrug(SysMedicineBaseEntity sysMedicineBaseEntity,string organizeId);

        IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode,
            string keyword = null,string ypgg = null,string ycmc = null,string dlCode = null);
        
        void SyncCommonSfdl(SysChargeCategoryBaseEntity sysChargeCategoryBaseEntity,string organizeId);
        
        void SyncCommonSfxm(SysChargeItemBaseEntity sysChargeItemBaseEntity,string organizeId);
        /// <summary>
        /// 修改信息时,绑定修改值
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineVO GetFormJson(int keyValue);

        void SyncYbyp(string organizeId);


    }
}
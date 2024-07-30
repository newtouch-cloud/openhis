using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class InventoryInfoDTO
    {
        /// <summary>
        /// 盘点时间下拉框
        /// </summary>
         public InventoryDateDropDownVO DateDropDownVO { get; set; }
        /// <summary>
        /// 返回的盘点信息
        /// </summary>
         public IList<InventoryInfoVO> InventoryInfoList { get; set; }
    }
}

using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 出入库方式 查询
    /// </summary>
    public class MedicineStorageIOModeQueryRequest : RequestBase
    {
        /// <summary>
        /// 0：入库；1：出库
        /// </summary>
        [Required]
        public string crkbz { get; set; }

    }
}

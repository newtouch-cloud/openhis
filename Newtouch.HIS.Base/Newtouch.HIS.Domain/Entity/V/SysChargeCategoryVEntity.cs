using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_sfdl")]
    public class SysChargeCategoryVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int dlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 报表 统计 收费大类
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// 大类类别，对应枚举 EnumSfdlDllb 1药品 2治疗项目 3非治疗项目
        /// </summary>
        public int? dllb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}

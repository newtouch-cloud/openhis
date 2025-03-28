using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfdl_base")]
    public class SysChargeCategoryBaseEntity : IEntity<SysChargeCategoryBaseEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
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
        /// 0 通用 1 门诊 2住院
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 门诊打印报表 费用所属（比饮食费归‘其他’）
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// 门诊结算 费用所属（比饮食费归‘其他’）
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 通用报表大类
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// 大类类别，对应枚举 EnumSfdlDllb 1药品 2治疗项目 3非治疗项目
        /// </summary>
        public int? dllb { get; set; }

        public string fjCode { get; set; }
        public string fjmc { get; set; }

		public decimal? sn { get; set; }
	}
}

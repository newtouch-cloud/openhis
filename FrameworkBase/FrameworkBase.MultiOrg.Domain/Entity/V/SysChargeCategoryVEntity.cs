using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 收费大类
    /// </summary>
    [Table("V_S_xt_sfdl")]
    public class SysChargeCategoryVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int dlId { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 门诊报表大类 大类Code
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// 门诊账单大类 大类Code
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 通用报表大类 大类Code
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

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

    }
}

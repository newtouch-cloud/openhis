using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    [Table("V_S_xt_zd")]
    public class SysDiagnosisVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int zdId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// icd10fjm
        /// </summary>
        public string icd10fjm { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 五笔
        /// </summary>
        public string wb { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdlx { get; set; }
        /// <summary>
        /// 医保农合代码
        /// </summary>
        public string ybnhlx { get; set; }

    }
}

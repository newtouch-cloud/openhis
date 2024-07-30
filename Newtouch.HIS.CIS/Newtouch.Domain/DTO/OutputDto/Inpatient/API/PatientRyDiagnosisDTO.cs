using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// 患者入院诊断
    /// </summary>
    public class PatientRyDiagnosisDto
    {
        /// <summary>
        /// 入院多诊断ID
        /// </summary>
        public string rydzdId { get; set; }
        
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// ICD10
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 诊断排序   1：诊断1 2：诊断2  3：诊断3
        /// </summary>
        public string zdpx { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态 0:无效  1：有效
        /// </summary>
        public string zt { get; set; }
    }
}
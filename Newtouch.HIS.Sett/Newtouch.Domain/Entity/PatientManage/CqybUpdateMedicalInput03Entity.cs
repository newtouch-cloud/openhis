using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    /// <summary>
    /// 重庆医保_就诊更新03入参落地
    /// </summary>
    [Table("cqyb_OutPutInPar03")]
    public class CqybUpdateMedicalInput03Entity: IEntity<CqybUpdateMedicalInput03Entity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 住院门诊号
        /// </summary>
        public string zymzh { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string yllb { get; set; }
        /// <summary>
        /// 居民特殊就诊标记
        /// </summary>
        public string jmtsjzbj { get; set; }
        /// <summary>
        /// 交易类型 1:门诊 2：住院
        /// </summary>
        public string jytype { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int px { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 入院(就诊)科室编码
        /// </summary>
        public string ryksbm { get; set; }
        /// <summary>
        /// 入院(就诊)医生编码(身份证)
        /// </summary>
        public string ryysbm { get; set; }
        /// <summary>
        /// 入院国家医师编码
        /// </summary>
        public string rygjysbm { get; set; }
        /// <summary>
        /// 出院诊断国家医师编码
        /// </summary>
        public string cyzdgjysbm { get; set; }
    }
}

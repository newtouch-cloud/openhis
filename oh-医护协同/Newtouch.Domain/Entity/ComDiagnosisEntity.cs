using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    [Table("Com_Diagnosis")]
    public class ComDiagnosisEntity : IEntity<ComDiagnosisEntity>
    {

        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 常用诊断名称
        /// </summary>
        public string cyzdmc { get; set; }
        /// <summary>
        /// 常用诊断编码
        /// </summary>
        public string cyzdbm { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 常用诊断类型(1 西医 2 中医)
        /// </summary>
        public string cyzdtype { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 状态(0 无效 1 有效)
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 权限控制 1个人 2科室
        /// </summary>
        public string qxkz { get; set; }
    }
}

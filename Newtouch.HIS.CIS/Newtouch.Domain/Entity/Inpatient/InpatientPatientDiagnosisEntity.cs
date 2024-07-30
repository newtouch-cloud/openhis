using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院病人诊断信息
    /// </summary>
    [Table("zy_PatDxInfo")]
    public class InpatientPatientDiagnosisEntity : IEntity<InpatientPatientDiagnosisEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 1入院诊断2出院诊断
        /// </summary>
        /// <returns></returns>
        public string zdlb { get; set; }
        /// <summary>
        /// 0主要诊断1第一辅助诊断2第二辅助诊断3第三辅助诊断9自定义诊断
        /// </summary>
        /// <returns></returns>
        public string zdlx { get; set; }
        /// <summary>
        /// zddm
        /// </summary>
        /// <returns></returns>
        public string zddm { get; set; }
        /// <summary>
        /// zdmc
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// zdyzdmc
        /// </summary>
        /// <returns></returns>
        public string zdyzdmc { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0无效1有效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        public int? cyqk { get; set; }
    }
}
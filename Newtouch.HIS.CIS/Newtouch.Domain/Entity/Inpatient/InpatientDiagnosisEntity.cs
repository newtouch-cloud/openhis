using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：毛浩泽
    /// 日 期：2021-07-02 15:37
    /// 描 述：住院医嘱诊断信息
    /// </summary>
    [Table("zy_clyzzd")]
    public class InpatientDiagnosisEntity : IEntity<InpatientDiagnosisEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// yzId
        /// </summary>
        /// <returns></returns>
        public string yzId { get; set; }
        /// <summary>
        /// yzh
        /// </summary>
        /// <returns></returns>
        public string yzh { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 诊断时间
        /// </summary>
        /// <returns></returns>
        public DateTime? zdsj { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        /// <returns></returns>
        public string icd10 { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        /// <returns></returns>
        public string zdmc { get; set; }
        /// <summary>
        /// 是否精麻类医嘱
        /// </summary>
        /// <returns></returns>
        public string yztag { get; set; }
        /// <summary>
        /// 医嘱排序
        /// </summary>
        /// <returns></returns>
        public string yzpx { get; set; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        /// <returns></returns>
        public int? yzlx { get; set; }
        /// <summary>
        /// 医嘱性质
        /// </summary>
        /// <returns></returns>
        public string yzxz { get; set; }
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
        /// <summary>
        /// 医院唯一id
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
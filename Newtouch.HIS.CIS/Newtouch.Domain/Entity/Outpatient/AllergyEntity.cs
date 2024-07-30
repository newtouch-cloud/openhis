using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 过敏信息
    /// </summary>
    [Table("xt_gmxx")]
    public class AllergyEntity : IEntity<AllergyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 过敏项目Code
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 过敏项目名称
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorName { get; set; }

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
        public string LastModifierName { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 医嘱id
        /// </summary>
        public string yzid { get; set; }

        /// <summary>
        /// 处方明细id
        /// </summary>
        public string cfmxid { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 过敏类型
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// 过敏药品Code
        /// </summary>
        public string ypCode { get; set; }
    }
}

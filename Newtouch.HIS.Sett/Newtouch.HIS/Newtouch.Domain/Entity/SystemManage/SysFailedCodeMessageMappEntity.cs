using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-09-29 19:53
    /// 描 述：错误提示配置
    /// </summary>
    [Table("Sys_FailedCodeMessageMapp")]
    public class SysFailedCodeMessageMappEntity : IEntity<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 顶级组织机构Id
        /// </summary>
        /// <returns></returns>
        public string TopOrganizeId { get; set; }
        /// <summary>
        /// 长测试超长长测试超长长测试超长长测试超长长测试超长长测试超长长测试超长长测试超长长测试超长长测试超长...
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string code { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        /// <returns></returns>
        public string msg { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
    }
}
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-20 14:48
    /// 描 述：模板权限控制表
    /// </summary>
    [Table("bl_mbqxkz")]
    public class BlmbqxkzEntity : IEntity<BlmbqxkzEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// mbId
        /// </summary>
        /// <returns></returns>
        public string mbId { get; set; }
        /// <summary>
        /// dutyId
        /// </summary>
        /// <returns></returns>
        public string dutyCode { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        /// <returns></returns>
        public string dutyName { get; set; }
        /// <summary>
        /// 0 无权限 1 只读权限 2 读写权限 3 完全控制
        /// </summary>
        /// <returns></returns>
        public int ctrlLevel { get; set; }
        /// <summary>
        /// 权限控制等级描述
        /// </summary>
        /// <returns></returns>
        public string ctrlLevelDesc { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2022-06-28 21:50
    /// 描 述：接口注入
    /// </summary>
    [Table("xt_accesscenter")]
    public class AccesscenterEntity : IEntity<AccesscenterEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// AccessType
        /// </summary>
        /// <returns></returns>
        public string AccessType { get; set; }

        /// <summary>
        /// AccessRoute
        /// </summary>
        /// <returns></returns>
        public string AccessRoute { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }

        /// <summary>
        /// AccessName
        /// </summary>
        /// <returns></returns>
        public string AccessName { get; set; }

        /// <summary>
        /// AccessDesc
        /// </summary>
        /// <returns></returns>
        public string AccessDesc { get; set; }

        /// <summary>
        /// AccessMode
        /// </summary>
        /// <returns></returns>
        public string AccessMode { get; set; }

        /// <summary>
        /// enablelog
        /// </summary>
        /// <returns></returns>
        public int? enablelog { get; set; }

        /// <summary>
        /// tags
        /// </summary>
        /// <returns></returns>
        public string tags { get; set; }

        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }

        /// <summary>
        /// parentId
        /// </summary>
        /// <returns></returns>
        public string parentId { get; set; }

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
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }

    }
}
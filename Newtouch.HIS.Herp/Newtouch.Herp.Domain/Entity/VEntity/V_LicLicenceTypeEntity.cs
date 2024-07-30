using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 证照类型
    /// </summary>
    public class VLicLicenceTypeEntity
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 所属名称
        /// </summary>
        public string belonged { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人	
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}

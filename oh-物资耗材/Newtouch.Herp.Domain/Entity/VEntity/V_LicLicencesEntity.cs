using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    public class VLicLicencesEntity
    {
        /// <summary>
        /// 证照主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string belonged { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string objectName { get; set; }

        /// <summary>
        /// 证照号
        /// </summary>
        public string licenceNo { get; set; }

        /// <summary>
        /// 起效日期
        /// </summary>
        public DateTime qxrq { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime sxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fileUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}

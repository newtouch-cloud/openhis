using System;

namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// 物资收费项目对照
    /// </summary>
    public class RelProductAndsfxmVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 物资ID(必填)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 物资类别ID
        /// </summary>
        public string productTypeId { get; set; }

        /// <summary>
        /// 物资类别名称
        /// </summary>
        public string productTypeName { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 收费项目代码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 状态  0:作废；1.有效
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

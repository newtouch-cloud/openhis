using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 库房
    /// </summary>
    [Table("gys_supplier")]
    public class GysSupplierEntity : IEntity<GysSupplierEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 0：其他     1：生产商      2：经销商
        /// </summary>
        public int? supplierType { get; set; }

        /// <summary>
        /// 库房名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string zipCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string fax { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string khh { get; set; }

        /// <summary>
        /// 开户行账号
        /// </summary>
        public string khhzh { get; set; }

        /// <summary>
        /// 税号
        /// </summary>
        public string sh { get; set; }

        /// <summary>
        /// 结算周期
        /// </summary>
        public string jszq { get; set; }

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

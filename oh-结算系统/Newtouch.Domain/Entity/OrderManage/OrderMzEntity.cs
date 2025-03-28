using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊订单详情--2023-9-8 chl
    /// </summary>
    [Table("order_mz")]
    public class OrderMzEntity : IEntity<OrderMzEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 处方金额
        /// </summary>
        public decimal? cfje { get; set; }
        /// <summary>
        /// 处方收费项目
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 处方收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 处方明细
        /// </summary>
        public string cfxmmx { get; set; }
        /// <summary>
        /// 处方类型 EnumCflx
        /// </summary>
        public string cflx { get; set; }
        /// <summary>
        /// 是否药品处方
        /// </summary>
        public string isyp { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}

using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院订单详情--2023-9-19 chl
    /// </summary>
    [Table("order_zy")]
    public class OrderZyEntity : IEntity<OrderZyEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 收费大类
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 处方收费项目
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 计费单位
        /// </summary>
        public string jfdw { get; set; }
        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 转自费标识
        /// </summary>
        public string zzfbz { get; set; }
        /// <summary>
        /// 项目明细 暂不用
        /// </summary>
        public string sfxmmx { get; set; }
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

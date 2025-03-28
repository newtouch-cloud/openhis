using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.Purchase
{
    /// <summary>
    /// 采购
    /// </summary>
    [Table("xt_wz_cg")]
    public class PurchaseEntity : IEntity<PurchaseEntity>
    {
        [Key]


        public string cgId { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// 配送点编码
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// 医院计划单号
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// 最晚到货日期
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// 采购方式
        /// </summary>
        public string cgfs { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        public string xtbm { get; set; }
        /// <summary>
        /// 是否含伴随服务
        /// </summary>
        public string sfhbsfw { get; set; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int ddzt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

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
        public string zt { get; set; }
    }
}

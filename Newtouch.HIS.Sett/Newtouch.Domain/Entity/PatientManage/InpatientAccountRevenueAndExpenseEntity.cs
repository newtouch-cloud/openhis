using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    [Table("zy_zhszjl")]
    public class InpatientAccountRevenueAndExpenseEntity : IEntity<InpatientAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// 帐户收支记录编号
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public int zhCode { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 收支金额
        /// </summary>
        public decimal szje { get; set; }

        /// <summary>
        /// 帐户余额
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        ///  收支性质
        /// </summary>
        public int szxz { get; set; }

        /// <summary>
        /// 现金支付/退款方式 编码
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public int? jsnm { get; set; }

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
        public string memo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string outTradeNo { get; set; }
    }
}

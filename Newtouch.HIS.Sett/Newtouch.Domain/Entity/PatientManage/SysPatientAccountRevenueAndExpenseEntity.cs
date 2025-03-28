using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病人账户收支记录
    /// </summary>
    [Table("xt_brzhszjl")]
    public class SysPatientAccountRevenueAndExpenseEntity : IEntity<SysPatientAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// 帐户收支记录编号
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zhszjlbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 病人内码
        /// </summary>
        public int patid { get; set; }

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
        /// 收支性质
        /// 0 门诊充值、取款(账户存取)      1 家床记帐、退回(家床记帐)   2 门诊结算、退回(门诊结??，包括家床结帐)      3 病区记帐、退回(病区记帐)   4 住院结算、退回(住院结算）住院结算不会产生费用，所以这里的收支是为了填平病区记帐说造成的负账户（账户余额为正，则不需要操作）      5 住院充值、取款(账户存取)   6 住院补缴款   
        /// </summary>
        public string szxz { get; set; }

        /// <summary>
        /// 现金支付方式 编码
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public int? jsnm { get; set; }

        /// <summary>
        /// 关联住院号
        /// 家床则为家床号   住院则为住院号
        /// </summary>
        public string zyh { get; set; }

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
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 创建用户姓名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}

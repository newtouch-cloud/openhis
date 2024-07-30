using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病人账户
    /// 用于：个人（包括慈善病人）、单位帐户等，预交金的表
    /// </summary>
    [Table("xt_brzh")]
    public class SysPatientAccountEntity : IEntity<SysPatientAccountEntity>
    {
        /// <summary>
        /// 帐户编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zhbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 帐户
        /// </summary>
        public int zh { get; set; }

        /// <summary>
        /// 病人内码
        /// 单位帐户 brnm = 0
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 帐户所有人
        /// </summary>
        public string zhsyr { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime qyrq { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime zzrq { get; set; }

        /// <summary>
        /// 帐户余额
        /// 该字段不存余额，如果要查询余额，则到表xt_brzhszjl查询最后一条记录   --------------------------------------      暂时用于处理慈善病人   -1 表示无需校验余额      
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// 帐户性质
        /// 0 单位帐户   1 个人帐户，   2 家床账户，   3 住院预缴款账户，   4 帮困账户      --2007.4.10 账户对应多个性质，分别处理，互不影响      -- 2 家床帐户 2006.11.19  不特别设立家床账户，从个人账户中扣除，通过“账户收支记录”中的“收支性质”区分   
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 帐户备注
        /// 家床病人诊断
        /// </summary>
        public string zhbz { get; set; }

        /// <summary>
        /// 状态
        /// 0 无效 1 有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 关联住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

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
        /// 创建用户名称
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}

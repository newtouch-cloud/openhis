using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    /// <summary>
    /// 住院病人账户
    /// </summary>
    [Table("zy_zh")]
    public class InpatientAccountEntity : IEntity<InpatientAccountEntity>
    {

        /// <summary>
        /// 帐户编号
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 帐户
        /// </summary>
        public int zhCode { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 帐户余额
        /// 该字段不存余额，如果要查询余额，则到表xt_brzhszjl查询最后一条记录   --------------------------------------      暂时用于处理慈善病人   -1 表示无需校验余额      
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// enumzhxz
        public int? zhxz { get; set; }

        /// <summary>
        /// 状态
        /// 0 无效 1 有效
        /// </summary>
        public string zt { get; set; }

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
        /// 病人内码
        /// </summary>
        public int? patid { get; set; }
        public string memo { get; set; }
    }
}

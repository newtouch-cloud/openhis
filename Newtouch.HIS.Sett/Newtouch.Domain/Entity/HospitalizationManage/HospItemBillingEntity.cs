using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_xmjfb")]
    public class HospItemBillingEntity : IEntity<HospItemBillingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jfbbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 医嘱唯一码
        /// </summary>
        public string yzwym { get; set; }

        /// <summary>
        /// 医嘱执行Id
        /// </summary>
        public int? zxid { get; set; }

        /// <summary>
        /// 医嘱明细Id
        /// </summary>
        public int? mxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime tdrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 医生Code
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室Code
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jfdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 待实施 1 已实施（已结束实施/不再实施）    2 无需实施 (在程序中也显示为已实施)         //住院记帐，无需实施的项目，实施标志默认为1（已实施）
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 对应同步翻云医嘱的医嘱执行时间
        /// </summary>
        public DateTime? ssrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 1 临时医嘱   2 长期医嘱   3 出院医嘱
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 1 未撤销   2 已撤销
        /// </summary>
        public string yzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cxtdrq { get; set; }

        /// <summary>
        /// 默认0   表示未撤销结算
        /// </summary>
        public int cxzyjfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? lb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zltime { get; set; }

        /// <summary>
        /// 绑定执行Id
        /// </summary>
        public string bdzxid { get; set; }

        /// <summary>
        /// from  xt_sfclxmzh  对应综合材料类别编号
        /// </summary>
        public int? clzhxmbh { get; set; }

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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ttbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 总治疗量
        /// </summary>
        public int? zzll { get; set; }

        /// <summary>
        /// 收费组套ID xt_sfmb
        /// </summary>
        public string ztbh { get; set; }

        /// <summary>
        /// 组套数量
        /// </summary>
        public decimal? ztsl { get; set; }
        /// <summary>
        /// 同一组套标识
        /// </summary>
        public string dcztbs { get; set; }
        public int? zzfbz { get; set; }
    }
}

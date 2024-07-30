using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfxm")]
    public class SysChargeItemEntity : IEntity<SysChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int sfxmId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string badlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string flCode { get; set; }

        /// <summary>
        /// 确定自负现金比例，无关这部分的性质（分类自负、自理）   见字段zfxz      注意：当自负比例为负数时，表示定额自负   (例：某材料12300，可报10000,那么自负比例＝-2300)
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 确定自负部分（费用×自负比例）的性质   0  可报 1 自理 2 分类自负 3 绝对自理（处理离休专家、特需挂号费）      （费用×（1－自负比例） 为可记帐部分）
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0：通用，1：门诊，2：住院
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 无需实施，（住院直接入帐）    1 需要实施，（住院实施后方可入帐）   from xt_yjxmdz
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 普通项目 1 特殊项目 refer xt_sfxmtsbz   特殊项目说明是特定的病人性质才可使用的。 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 0 非收费项目（例：挂号费..） 1 收费项目
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wjdm { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 持续时长（单位：分）
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 单位计量数
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// 计价策略
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// 默认执行科室
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// 医保最后同步时间
        /// </summary>
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// 手术等级
        /// </summary>
        public string ssdj { get; set; }

        public string sqlx { get; set; }
        /// <summary>
        /// 医保标志1：医保；0：自费
        /// </summary>
        public string ybbz { get; set; }
        /// <summary>
        /// 新农合医保代码
        /// </summary>
        public string xnhybdm { get; set; }
        /// <summary>
        /// 国家医保代码
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public decimal? cxjje { get; set; }
        public string pzwh { get; set; }
        public string sccj { get; set; }
        public string gjybmc { get; set; }
		public string iswzsame { get; set; }

	}
}

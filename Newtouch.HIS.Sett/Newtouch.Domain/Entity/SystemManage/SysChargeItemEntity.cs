using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 收费项目
    /// </summary>
    [Table("xt_sfxm")]
    [Obsolete("please use the view")]
    public class SysChargeItemEntity : IEntity<SysChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sfxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

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
        public string fl { get; set; }

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
        /// 
        /// </summary>
        public string jsbz { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlffbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wjdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string badl { get; set; }

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
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

    }
}

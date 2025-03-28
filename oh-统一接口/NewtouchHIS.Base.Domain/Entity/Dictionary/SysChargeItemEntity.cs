using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///包括挂号（挂号费00、诊疗费01、磁卡费02、工本费03 等）、收费项目、检验项目、
    /// 注意：当注销收费项目时，需要校验其实效性（例：是否正被用在床位费设置上）
    ///费用分类见“系统病人收费算法”
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfxm", "收费项目")]
    public partial class SysChargeItemEntity : IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int sfxmId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfxmCode不可为空")]
        [StringLength(20, ErrorMessage = "sfxmCode长度限制为20")]
        public string sfxmCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfxmmc不可为空")]
        [StringLength(256, ErrorMessage = "sfxmmc长度限制为256")]
        public string sfxmmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfdlCode不可为空")]
        [StringLength(200, ErrorMessage = "sfdlCode长度限制为200")]
        public string sfdlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "badlCode长度限制为20")]
        public string? badlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdlCode长度限制为20")]
        public string? nbdlCode { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? duration { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py不可为空")]
        [StringLength(250, ErrorMessage = "py长度限制为250")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dw不可为空")]
        [StringLength(220, ErrorMessage = "dw长度限制为220")]
        public string dw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dj不可为空")]
        [StringLength(9, ErrorMessage = "dj长度限制为9")]
        public decimal dj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "flCode长度限制为20")]
        public string? flCode { get; set; }

        /// <summary>
        /// Desc:确定自负现金比例，无关这部分的性质（分类自负、自理）	   见字段zfxz	   	   注意：当自负比例为负数时，表示定额自负	   (例：某材料12300，可报10000,那么自负比例＝-2300)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl不可为空")]
        [StringLength(9, ErrorMessage = "zfbl长度限制为9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:确定自负部分（费用×自负比例）的性质	   0  可报 1 自理 2 分类自负 3 绝对自理（处理离休专家、特需挂号费）	   	   （费用×（1－自负比例） 为可记帐部分）
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz不可为空")]
        [StringLength(1, ErrorMessage = "zfxz长度限制为1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:0：通用，1：门诊，2：住院
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz不可为空")]
        [StringLength(1, ErrorMessage = "mzzybz长度限制为1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:0 无需实施，（住院直接入帐） 	   1 需要实施，（住院实施后方可入帐）	   from xt_yjxmdz
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ssbz不可为空")]
        [StringLength(1, ErrorMessage = "ssbz长度限制为1")]
        public string ssbz { get; set; }

        /// <summary>
        /// Desc:0 普通项目 1 特殊项目 refer xt_sfxmtsbz	   特殊项目说明是特定的病人性质才可使用的。 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "tsbz不可为空")]
        [StringLength(1, ErrorMessage = "tsbz长度限制为1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:0 非收费项目（例：挂号费..） 1 收费项目
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfbz不可为空")]
        [StringLength(1, ErrorMessage = "sfbz长度限制为1")]
        public string sfbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ybdm长度限制为20")]
        public string? ybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(40, ErrorMessage = "wjdm长度限制为40")]
        public string? wjdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1000, ErrorMessage = "bz长度限制为1000")]
        public string? bz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }


        /// <summary>
        /// Desc:单位计数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? dwjls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? jjcl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zxks长度限制为20")]
        public string? zxks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gg长度限制为100")]
        public string? gg { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastYBUploadTime长度限制为23")]
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "ssdj长度限制为2")]
        public string? ssdj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "sqlx长度限制为20")]
        public string? sqlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ybbz不可为空")]
        [StringLength(1, ErrorMessage = "ybbz长度限制为1")]
        public string ybbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xnhybdm长度限制为20")]
        public string? xnhybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjybdm长度限制为50")]
        public string? gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "cxjje长度限制为11")]
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "pzwh长度限制为100")]
        public string? pzwh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "sccj长度限制为100")]
        public string? sccj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gjybmc长度限制为100")]
        public string? gjybmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Code长度限制为50")]
        public string? wz_Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_ConsumablesID长度限制为50")]
        public string? wz_ConsumablesID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_SerialNumber长度限制为50")]
        public string? wz_SerialNumber { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Packing长度限制为50")]
        public string? wz_Packing { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "wz_IsHight长度限制为1")]
        public bool? wz_IsHight { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_MaterialCode长度限制为50")]
        public string? wz_MaterialCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(1, ErrorMessage = "isYnss长度限制为1")]
        //public string? isYnss { get; set; }

    }

}

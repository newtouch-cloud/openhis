using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///开方时供选择的药品列表——
    ///规格：(剂量+剂量单位)*(包装数+包装单位)/定价单位
    ///零售价：零售价/定价单位
    ///单位：拆零单位
    ///金额：零售价*数量/拆零数（这里先乘数量再除拆零数 计算金额，而不是先除拆零数计算出单价 再乘数量计算金额，这样可以减小误差。）
    ///批发价(pfj)：在系统中，将名称定义为“进价”，取消批发价的含义，因为系统开发中都使用批发价字   ，故此字段延用，但是变更其含义。
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_yp", "药品")]
    public partial class SysMedicineEntity:IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ypId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypCode不可为空")]
        [StringLength(20, ErrorMessage = "ypCode长度限制为20")]
        public string ypCode { get; set; }

        /// <summary>
        /// Desc:通用名
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypmc不可为空")]
        [StringLength(256, ErrorMessage = "ypmc长度限制为256")]
        public string ypmc { get; set; }

        /// <summary>
        /// Desc:前最例如：(甲) (乙10%)……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "ypqz长度限制为10")]
        public string? ypqz { get; set; }

        /// <summary>
        /// Desc:后最例如：……
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yphz长度限制为10")]
        public string? yphz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "spm长度限制为50")]
        public string? spm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py不可为空")]
        [StringLength(70, ErrorMessage = "py长度限制为70")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "cfl长度限制为9")]
        public decimal? cfl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cfdw长度限制为20")]
        public string? cfdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "jl长度限制为9")]
        public decimal? jl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "jldw长度限制为10")]
        public string? jldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzs不可为空")]
        [StringLength(4, ErrorMessage = "bzs长度限制为4")]
        public decimal bzs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzdw不可为空")]
        [StringLength(20, ErrorMessage = "bzdw长度限制为20")]
        public string bzdw { get; set; }

        /// <summary>
        /// Desc:准备废止
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "mzcls长度限制为9")]
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Desc:准备废止，以“拆零单位”代替
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mzcldw长度限制为20")]
        public string? mzcldw { get; set; }

        /// <summary>
        /// Desc:准备废止
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "zycls长度限制为9")]
        public decimal? zycls { get; set; }

        /// <summary>
        /// Desc:准备废止
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zycldw长度限制为20")]
        public string? zycldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zxdw不可为空")]
        [StringLength(20, ErrorMessage = "zxdw长度限制为20")]
        public string zxdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "djdw不可为空")]
        [StringLength(20, ErrorMessage = "djdw长度限制为20")]
        public string djdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "lsj不可为空")]
        [StringLength(11, ErrorMessage = "lsj长度限制为11")]
        public decimal lsj { get; set; }

        /// <summary>
        /// Desc:默认进价
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "pfj长度限制为11")]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Desc:详见系统项目表“自负比例”说明
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl不可为空")]
        [StringLength(9, ErrorMessage = "zfbl长度限制为9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:1 自理 2 分类自负	   详见系统项目表“自负性质”说明	   	   门诊可用，急诊不可用，或反之，以不同药剂部门有无药来控制
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz不可为空")]
        [StringLength(1, ErrorMessage = "zfxz长度限制为1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dlCode不可为空")]
        [StringLength(20, ErrorMessage = "dlCode长度限制为20")]
        public string dlCode { get; set; }

        /// <summary>
        /// Desc:xt_ypjx
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jx长度限制为20")]
        public string? jx { get; set; }

        /// <summary>
        /// Desc:xt_yc  修改直接保存名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ycmc不可为空")]
        [StringLength(100, ErrorMessage = "ycmc长度限制为100")]
        public string ycmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? medid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? medextid { get; set; }

        /// <summary>
        /// Desc:药品在各药房消耗时的“包装级别”或“拆零数信息”
        /// Default:3
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ypbzdm长度限制为50")]
        public string? ypbzdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdl长度限制为20")]
        public string? nbdl { get; set; }

        /// <summary>
        /// Desc:----0：停用  1：启用	   ---0：通用，1：门诊，2：住院  （作废）	   	   
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz不可为空")]
        [StringLength(1, ErrorMessage = "mzzybz长度限制为1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "lsbz长度限制为1")]
        public bool? lsbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? mjzbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "yfCode长度限制为20")]
        public string? yfCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "isKss不可为空")]
        [StringLength(1, ErrorMessage = "isKss长度限制为1")]
        public string isKss { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "kssId长度限制为50")]
        public string? kssId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jybz不可为空")]
        [StringLength(20, ErrorMessage = "jybz长度限制为20")]
        public string jybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "bz长度限制为500")]
        public string? bz { get; set; }

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
        [StringLength(50, ErrorMessage = "tsypbz长度限制为50")]
        public string? tsypbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "isYnss长度限制为1")]
        public string? isYnss { get; set; }

    }

}

using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.PdsDb)]
    [SugarTable("V_C_xt_yp", "药品字典视图")]
    public partial class SysMedicineVPDSEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypId不可为空")]
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
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypmc不可为空")]
        [StringLength(256, ErrorMessage = "ypmc长度限制为256")]
        public string ypmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "spm长度限制为50")]
        public string spm { get; set; }

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
        public string cfdw { get; set; }

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
        public string jldw { get; set; }

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
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "mzcls长度限制为9")]
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mzcldw长度限制为20")]
        public string mzcldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "zycls长度限制为9")]
        public decimal? zycls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zycldw长度限制为20")]
        public string zycldw { get; set; }

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
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "pfj长度限制为11")]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl不可为空")]
        [StringLength(9, ErrorMessage = "zfbl长度限制为9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:
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
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jx长度限制为20")]
        public string jx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "yfCode长度限制为20")]
        public string yfCode { get; set; }

        /// <summary>
        /// Desc:
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
        [StringLength(50, ErrorMessage = "ypbzdm长度限制为50")]
        public string ypbzdm { get; set; }

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
        [StringLength(1, ErrorMessage = "tsbz长度限制为1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "gzy长度限制为1")]
        public string gzy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "mzy长度限制为1")]
        public string mzy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "yljsy长度限制为1")]
        public string yljsy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "zlff长度限制为2")]
        public string zlff { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "sjap长度限制为2")]
        public string sjap { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(6, ErrorMessage = "yl长度限制为6")]
        public decimal? yl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yldw长度限制为10")]
        public string yldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ypgg长度限制为100")]
        public string ypgg { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ybdm长度限制为20")]
        public string ybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? syts { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "dczdjl长度限制为9")]
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "dczdsl长度限制为9")]
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "ljzdjl长度限制为9")]
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "ljzdsl长度限制为9")]
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "pzwh长度限制为50")]
        public string pzwh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yptssx长度限制为10")]
        public string yptssx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ypflCode长度限制为20")]
        public string ypflCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "jzlx长度限制为1")]
        public string jzlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? mrbzq { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ghdw长度限制为50")]
        public string ghdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ypcd { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "xzyy长度限制为1")]
        public bool? xzyy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "xzyysm长度限制为256")]
        public string xzyysm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt不可为空")]
        [StringLength(1, ErrorMessage = "zt长度限制为1")]
        public string zt { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jybz不可为空")]
        [StringLength(20, ErrorMessage = "jybz长度限制为20")]
        public string jybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "isKss不可为空")]
        [StringLength(1, ErrorMessage = "isKss长度限制为1")]
        public string isKss { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
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
        public string xnhybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjybdm长度限制为50")]
        public string gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "tsypbz长度限制为50")]
        public string tsypbz { get; set; }

    }
}

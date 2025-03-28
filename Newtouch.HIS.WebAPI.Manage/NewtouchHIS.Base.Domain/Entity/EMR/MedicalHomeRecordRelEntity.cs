using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant("MrmsDb")]
    [SugarTable("mr_basy_rel_code", "")]
    public partial class MedicalHomeRecordRelEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "SYId不可为空")]
        [StringLength(50, ErrorMessage = "SYId长度限制为50")]
        public string SYId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "BAH不可为空")]
        [StringLength(100, ErrorMessage = "BAH长度限制为100")]
        public string BAH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "YLFKFS长度限制为100")]
        public string YLFKFS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "XB长度限制为100")]
        public string XB { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "GJ长度限制为100")]
        public string GJ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZ长度限制为100")]
        public string MZ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZY长度限制为100")]
        public string ZY { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "HY长度限制为100")]
        public string HY { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "GX长度限制为100")]
        public string GX { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RYTJ长度限制为100")]
        public string RYTJ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RYKB长度限制为100")]
        public string RYKB { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RYBF长度限制为100")]
        public string RYBF { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZKKB长度限制为100")]
        public string ZKKB { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "CYBF长度限制为100")]
        public string CYBF { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "CYKB长度限制为100")]
        public string CYKB { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "BLZDDM长度限制为100")]
        public string BLZDDM { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "BRLY长度限制为50")]
        public string BRLY { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "YWGM长度限制为100")]
        public string YWGM { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "XX长度限制为100")]
        public string XX { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RH长度限制为100")]
        public string RH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "KZR长度限制为100")]
        public string KZR { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZRYS长度限制为100")]
        public string ZRYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZZYS长度限制为100")]
        public string ZZYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZYYS长度限制为100")]
        public string ZYYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZRHS长度限制为100")]
        public string ZRHS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "JXYS长度限制为100")]
        public string JXYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SXYS长度限制为100")]
        public string SXYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "BMY长度限制为100")]
        public string BMY { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "BAZL长度限制为100")]
        public string BAZL { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZKYS长度限制为100")]
        public string ZKYS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZKHS长度限制为100")]
        public string ZKHS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "LYFS长度限制为100")]
        public string LYFS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SFZZYJH长度限制为100")]
        public string SFZZYJH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "BQFX长度限制为10")]
        public string BQFX { get; set; }


        public string ZZYS1 { get; set; }


        /// <summary>
        /// CSD_SN
        /// </summary>
        /// <returns></returns>
        public string CSD_SN { get; set; }

        /// <summary>
        /// CSD_SI
        /// </summary>
        /// <returns></returns>
        public string CSD_SI { get; set; }

        /// <summary>
        /// CSD_QX
        /// </summary>
        /// <returns></returns>
        public string CSD_QX { get; set; }

        /// <summary>
        /// CSD_JD
        /// </summary>
        /// <returns></returns>
        public string CSD_JD { get; set; }

        /// <summary>
        /// XZZ_SN
        /// </summary>
        /// <returns></returns>
        public string XZZ_SN { get; set; }

        /// <summary>
        /// XZZ_SI
        /// </summary>
        /// <returns></returns>
        public string XZZ_SI { get; set; }

        /// <summary>
        /// XZZ_QX
        /// </summary>
        /// <returns></returns>
        public string XZZ_QX { get; set; }

        /// <summary>
        /// XZZ_JD
        /// </summary>
        /// <returns></returns>
        public string XZZ_JD { get; set; }

        /// <summary>
        /// HKDZ_SN
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SN { get; set; }

        /// <summary>
        /// HKDZ_SI
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SI { get; set; }

        /// <summary>
        /// HKDZ_QX
        /// </summary>
        /// <returns></returns>
        public string HKDZ_QX { get; set; }

        /// <summary>
        /// HKDZ_JD
        /// </summary>
        /// <returns></returns>
        public string HKDZ_JD { get; set; }

        /// <summary>
        /// LXRDZ_SN
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SN { get; set; }

        /// <summary>
        /// LXRDZ_SI
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SI { get; set; }

        /// <summary>
        /// LXRDZ_QX
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_QX { get; set; }

        /// <summary>
        /// LXRDZ_JD
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_JD { get; set; }

        /// <summary>
        /// 输血反应
        /// </summary>
        /// <returns></returns>
        public string SXFY { get; set; }

        /// <summary>
        /// 临床路径变异原因
        /// </summary>
        /// <returns></returns>
        public string BYYY { get; set; }
    }
}

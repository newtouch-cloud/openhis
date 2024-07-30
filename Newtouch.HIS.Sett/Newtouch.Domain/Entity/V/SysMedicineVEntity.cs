using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_yp")]
    public class SysMedicineVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? cfl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzzybz { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? lsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// 药品用法
        /// </summary>
        public string yfCode { get; set; }

    }
}

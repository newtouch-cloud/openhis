using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品
    /// </summary>
    [Table("xt_yp")]
    [Obsolete("please use the view")]
    public class SysMedicineEntity : IEntity<SysMedicineEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 通用名
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 前最例如：(甲) (乙10%)……
        /// </summary>
        public string ypqz { get; set; }

        /// <summary>
        /// 后最例如：……
        /// </summary>
        public string yphz { get; set; }

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
        /// 准备废止
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// 准备废止，以“拆零单位”代替
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// 准备废止
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// 准备废止
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 最小单位
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
        /// 批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 详见系统项目表“自负比例”说明
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 1 自理 2 分类自负   详见系统项目表“自负性质”说明      门诊可用，急诊不可用，或反之，以不同药剂部门有无药来控制
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// xt_ypjx
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// xt_yc  修改直接保存名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? medextid { get; set; }

        /// <summary>
        /// 药品在各药房消耗时的“包装级别”或“拆零数信息”
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 药房代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// ----0：停用  1：启用   ---0：通用，1：门诊，2：住院  （作废）      
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
        /// 1：有效 0：无效
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


    }
}

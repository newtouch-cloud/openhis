using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统药品
    /// </summary>
    [Table("V_S_xt_yp")]
    public class SysMedicineVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string spm { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 成分量
        /// </summary>
        public decimal? cfl { get; set; }

        /// <summary>
        /// 成分单位
        /// </summary>
        public string cfdw { get; set; }

        /// <summary>
        /// 剂量单位转换系数。最小单位片，1片2mg，jl：2，jldw：mg
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 门诊拆零数
        /// </summary>
        public decimal? mzcls { get; set; }

        /// <summary>
        /// 门诊拆零单位
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// 住院拆零数
        /// </summary>
        public decimal? zycls { get; set; }

        /// <summary>
        /// 住院拆零单位
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 单价单位
        /// </summary>
        public string djdw { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 批发价（默认进价）
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 收费大类Code
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 药品在各药房消耗时的“包装级别”或“拆零数信息”
        /// </summary>
        public string ypbzdm { get; set; }

        /// <summary>
        /// 农保大类
        /// </summary>
        public string nbdl { get; set; }

        /// <summary>
        /// 门诊住院标志----0：停用  1：启用   ---0：通用，1：门诊，2：住院  （作废）      
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
        /// 零售标志
        /// </summary>
        public bool? lsbz { get; set; }

        /// <summary>
        /// 门急诊标志
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// 药品用法 编码
        /// </summary>
        public string yfCode { get; set; }

    }
}

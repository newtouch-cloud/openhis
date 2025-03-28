using NewtouchHIS.Base.Domain.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets
{
    #region Entity 转 VO 结构一致
    public class SysPatientBasicInfoEVO : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public int patid { get; set; }


        /// <summary>
        /// Desc:本院磁卡号（包括医保离休病人），或者医保卡号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "blh不可为空")]
        [StringLength(30, ErrorMessage = "blh长度限制为30")]
        public string blh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "xm长度限制为50")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "py长度限制为50")]
        public string? py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb不可为空")]
        [StringLength(1, ErrorMessage = "xb长度限制为1")]
        public string xb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "csny长度限制为10")]
        public DateTime? csny { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "zjlx长度限制为2")]
        public string? zjlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(18, ErrorMessage = "zjh长度限制为18")]
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_sheng长度限制为100")]
        public string? cs_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_shi长度限制为100")]
        public string? cs_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_xian长度限制为100")]
        public string? cs_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_dz长度限制为100")]
        public string? cs_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cs_yb长度限制为20")]
        public string? cs_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_sheng长度限制为100")]
        public string? hu_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_shi长度限制为100")]
        public string? hu_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_xian长度限制为100")]
        public string? hu_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_dz长度限制为100")]
        public string hu_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_yb长度限制为100")]
        public string? hu_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_sheng长度限制为100")]
        public string? xian_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_shi长度限制为100")]
        public string? xian_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_xian长度限制为100")]
        public string? xian_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_dz长度限制为100")]
        public string? xian_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_yb长度限制为100")]
        public string? xian_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_sheng长度限制为200")]
        public string? jjlxr_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_shi长度限制为200")]
        public string? jjlxr_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_xian长度限制为200")]
        public string? jjlxr_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_dz长度限制为200")]
        public string? jjlxr_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jjllrgx长度限制为20")]
        public string? jjllrgx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "jjllr长度限制为50")]
        public string? jjllr { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "jjlldh长度限制为50")]
        public string? jjlldh { get; set; }

        /// <summary>
        /// Desc:from xtdy
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? dybh { get; set; }

        /// <summary>
        /// Desc:单位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "dwmc长度限制为30")]
        public string? dwmc { get; set; }

        /// <summary>
        /// Desc:单位地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "dwdz长度限制为10")]
        public string? dwdz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xl长度限制为20")]
        public string? xl { get; set; }

        /// <summary>
        /// Desc:xt_gj.gj
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "gj长度限制为20")]
        public string? gj { get; set; }

        /// <summary>
        /// Desc:0 未婚 1 已婚
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(3, ErrorMessage = "hf长度限制为3")]
        public byte? hf { get; set; }

        /// <summary>
        /// Desc:民族
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mz长度限制为20")]
        public string? mz { get; set; }

        /// <summary>
        /// Desc:职业
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zy长度限制为20")]
        public string? zy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zjlxfs长度限制为20")]
        public string? zjlxfs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "dh长度限制为30")]
        public string? dh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "phone长度限制为30")]
        public string? phone { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wechat长度限制为50")]
        public string? wechat { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "email长度限制为50")]
        public string? email { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "brly长度限制为20")]
        public string? brly { get; set; }

        /// <summary>
        /// Desc:过敏史
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "gms长度限制为200")]
        public string? gms { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jbs长度限制为200")]
        public string jbs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "bz长度限制为200")]
        public string? bz { get; set; }



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
        [StringLength(12, ErrorMessage = "dwdh长度限制为12")]
        public string? dwdh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(12, ErrorMessage = "dwyb长度限制为12")]
        public string? dwyb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(12, ErrorMessage = "jg长度限制为12")]
        public string? jg { get; set; }
    }

    #endregion
    /// <summary>
    /// 患者基本信息视图
    /// </summary>
    public class SysPatientBasicVO : SysPatInfoVO
    {

        /// <summary>
        /// 婚姻
        /// </summary>
        public string hy { get; set; }
    }
    /// <summary>
    /// 患者基本信息精简视图
    /// </summary>
    public class SysPatInfoVO
    {
        public int? patid { get; set; }
        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// 病人最近的性质xt_brxz.brxz
        /// </summary>
        public string? brxz { get; set; }

        /// <summary>
        /// 本院磁卡号（包括医保离休病人），或者医保卡号
        /// </summary>
        public string? blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? xm { get; set; }
        public string? xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? dh { get; set; }
        public string? kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string klx { get; set; }
    }
    /// <summary>
    /// 患者查询索引
    /// </summary>
    public class SysPatIndexVO
    {
        public string? OrganizeId { get; set; }
        public int? patid { get; set; }
        public string? brxz { get; set; }
        public string? xm { get; set; }
        public string? kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string klx { get; set; }
        public string? zjh { get; set; }
        public string? dh { get; set; }
    }
}

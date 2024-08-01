using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 收费项目
    /// </summary>
    [Table("V_S_xt_sfxm")]
    public class SysChargeItemVEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int sfxmId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 收费大类Code
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 病案大类Code
        /// </summary>
        public string badlCode { get; set; }

        /// <summary>
        /// 农保大类Code
        /// </summary>
        public string nbdlCode { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 是否需要实施 0 无需实施，（直接入帐）    1 需要实施，（实施后方可入帐）
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 0 普通项目 1 特殊项目   特殊项目说明是特定的病人性质才可使用的。 
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 收费标志    0 非收费项目（例：挂号费..） 1 收费项目
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 医保代码
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 物价代码
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
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 服务持续时长（单位：分）
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
        /// 计价策略 对应枚举EnumKfxmJjcl 1按时长 2按数量 3按面积
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// 执行科室（默认执行科室）
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

    }
}

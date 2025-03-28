using NewtouchHIS.Base.Domain.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    public class TreatedApplyVO : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Id { get; set; }

        /// <summary>
        /// Desc:申请时间
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "申请时间不可为空")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:就诊时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:申请科室
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "科室不可为空")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:申请医生
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "医生工号不可为空")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:申请状态
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:患者标识
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "患者id不可为空")]
        public string patid { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:患者姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "患者姓名不可为空")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb不可为空")]
        public string xb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? nl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? birth { get; set; }

        /// <summary>
        /// Desc:病人性质
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        public string brxz { get; set; } = ((int)EnumBrxz.zf).ToString();

        /// <summary>
        /// Desc:会议号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:申请人信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqr { get; set; }

        /// <summary>
        /// Desc:申请流水号（由第三方系统生成，不可重复）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqlsh { get; set; }
        /// <summary>
        /// 申请AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 申请机构
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// 申请人工号
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
    }

    public class TreatedApplyBaseVO
    {
        /// <summary>
        /// 申请AppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// 申请机构
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// 申请人工号
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// Desc:申请时间
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "申请会诊时间不可为空")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:就诊时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:申请科室
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "科室不可为空")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:申请医生
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "医生工号不可为空")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:患者标识
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "患者id不可为空")]
        public string patid { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:患者姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "患者姓名不可为空")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:会议号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:申请人信息(可用于申请会议室)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqr { get; set; }

        /// <summary>
        /// Desc:申请流水号（由第三方系统生成，不可重复）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqlsh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }

    }
    /// <summary>
    /// 诊疗申请冗余信息
    /// </summary>
    public class TreatedApplyExtendVO : TreatedApplyVO
    {
        public string? ksmc { get; set; }
        public string? ysxm { get; set; }
    }
}

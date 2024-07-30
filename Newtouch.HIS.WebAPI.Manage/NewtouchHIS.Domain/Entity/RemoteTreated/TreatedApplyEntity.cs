using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Domain.Entity.RemoteTreated
{
    ///<summary>
    ///系统科室
    ///</summary>
    [Tenant(DBEnum.CisDb)]
    [SugarTable("zl_zlsq", "TreatedApplyEntity")]
    public class TreatedApplyEntity : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:申请时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "sqsj长度限制为23")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:就诊时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "jzsj长度限制为23")]
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:申请科室
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "deptCode长度限制为20")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:申请医生
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ysgh长度限制为20")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:申请状态
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sqzt不可为空")]
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:患者标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "patid长度限制为50")]
        public string patid { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Required(ErrorMessage = "kh不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xm不可为空")]
        [StringLength(50, ErrorMessage = "xm长度限制为50")]
        public string xm { get; set; }

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
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "nl不可为空")]
        [StringLength(50, ErrorMessage = "nl长度限制为50")]
        public string? nl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "zjh长度限制为50")]
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "birth长度限制为10")]
        public DateTime? birth { get; set; }

        /// <summary>
        /// Desc:病人性质
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "brxz长度限制为20")]
        public string? brxz { get; set; } 

        /// <summary>
        /// Desc:会议号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "mettingId长度限制为50")]
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:申请人信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqr长度限制为50")]
        public string sqr { get; set; }

        /// <summary>
        /// Desc:申请流水号（由第三方系统生成，不可重复）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqlsh长度限制为50")]
        public string sqlsh { get; set; }
        /// <summary>
        /// 申请AppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// 申请机构
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// 申请人工号
        /// </summary>
        public string? sqrgh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get;set; }
    }
}

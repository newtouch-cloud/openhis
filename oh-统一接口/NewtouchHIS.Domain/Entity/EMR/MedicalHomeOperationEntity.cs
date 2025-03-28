using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.MrmsDb)]
    [SugarTable("mr_basy_ss", "")]
    public partial class MedicalHomeOperationEntity
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
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "BAH不可为空")]
        [StringLength(100, ErrorMessage = "BAH长度限制为100")]
        public string BAH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ZYH不可为空")]
        [StringLength(50, ErrorMessage = "ZYH长度限制为50")]
        public string ZYH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "SSOrder不可为空")]
        public int SSOrder { get; set; }

        /// <summary>
        /// Desc:手术及操作编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SSJCZBM长度限制为100")]
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Desc:手术及操作日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "SSJCZRQ长度限制为23")]
        public DateTime? SSJCZRQ { get; set; }

        /// <summary>
        /// Desc:手术级别
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SSJB长度限制为100")]
        public string SSJB { get; set; }

        /// <summary>
        /// Desc:手术及操作名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "SSJCZMC长度限制为200")]
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Desc:术者
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SZ长度限制为100")]
        public string SZ { get; set; }

        /// <summary>
        /// Desc:I助
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "YZ长度限制为100")]
        public string YZ { get; set; }

        /// <summary>
        /// Desc:II助
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "EZ长度限制为100")]
        public string EZ { get; set; }

        /// <summary>
        /// Desc:切口等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "QKDJ长度限制为100")]
        public string QKDJ { get; set; }

        /// <summary>
        /// Desc:切口愈合类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "QKYHLB长度限制为100")]
        public string QKYHLB { get; set; }

        /// <summary>
        /// Desc:麻醉方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZFS长度限制为100")]
        public string MZFS { get; set; }

        /// <summary>
        /// Desc:麻醉医师
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZYS长度限制为100")]
        public string MZYS { get; set; }

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
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode长度限制为50")]
        public string LastModifierCode { get; set; }

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
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "QKYHDJ长度限制为50")]
        public string QKYHDJ { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.MrmsDb)]
    [SugarTable("mr_basy_zd", "")]
    public partial class MedicalHomeDiagEntity
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
        [Required(ErrorMessage = "ZDOrder不可为空")]
        public int ZDOrder { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "JBDM长度限制为100")]
        public string JBDM { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "JBMC长度限制为100")]
        public string JBMC { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RYBQ长度限制为100")]
        public string RYBQ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "RYBQMS长度限制为200")]
        public string RYBQMS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "CYQK长度限制为100")]
        public string CYQK { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "CYQKMS长度限制为200")]
        public string CYQKMS { get; set; }

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
    }
}

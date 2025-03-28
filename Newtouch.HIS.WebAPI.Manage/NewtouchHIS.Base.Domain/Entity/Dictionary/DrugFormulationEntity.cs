using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypjx", "药品剂型")]
    public partial class DrugFormulationEntity : ISysEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int jxId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jxCode不可为空")]
        [StringLength(20, ErrorMessage = "jxCode长度限制为20")]
        public string jxCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jxmc不可为空")]
        [StringLength(50, ErrorMessage = "jxmc长度限制为50")]
        public string jxmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py不可为空")]
        [StringLength(50, ErrorMessage = "py长度限制为50")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }
    }    
}

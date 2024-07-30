using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_yp_jxyfdy", "药品剂型用法对照")]
    public partial class DrugFormulationUsageEntity : ISysEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }

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
        [Required(ErrorMessage = "yfCode不可为空")]
        [StringLength(50, ErrorMessage = "yfCode长度限制为50")]
        public string yfCode { get; set; }        
    }    
}

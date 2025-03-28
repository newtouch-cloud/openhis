using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///收费大类 
    ///一般按上级主管部门要求分类
    ///    （挂号费00、诊疗费01、磁卡费02、工本费03 等
    ///    门诊医保编码 varchar(2)
    ///    住院医保编码 varchar(2) 
    ///    重新建立表做医保代码对应
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfdl_lx", "收费大类类型")]
    public partial class SysCategoryTypesEntity : IEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public String Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrganizeId { get; set; }

        /// <summary>
        /// 关联ChargeCateType的字典项
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String zt { get; set; }

    }
}

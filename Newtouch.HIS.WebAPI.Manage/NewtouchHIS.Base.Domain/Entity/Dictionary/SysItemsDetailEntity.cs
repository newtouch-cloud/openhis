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
    [SugarTable("V_C_Sys_ItemsDetail", "字典表")]
    public partial class SysItemsDetailEntity : IEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public System.String CateCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String Id { get; set; }

    }
}

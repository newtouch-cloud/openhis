
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///系统科室
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Department", "系统科室")]
    public class SysDepartmentEntity:IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public System.String Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean yjbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ybksbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String iscz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ybksbm2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zlks { get; set; }
    }
}

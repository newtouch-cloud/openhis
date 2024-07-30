using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// 系统人员
    /// </summary>
    [Table("V_S_Sys_Staff")]
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Staff")]
    [Description("系统人员")]
    public class SysStaffVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 科室Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 默认关联康复类别
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 模板权限
        /// </summary>
        public string mbqx { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string zc { get; set; }
    }
}

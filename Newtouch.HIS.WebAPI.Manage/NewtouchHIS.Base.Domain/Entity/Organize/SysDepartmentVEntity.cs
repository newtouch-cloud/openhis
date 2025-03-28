using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///系统科室
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_Sys_Department", "系统科室视图")]
    public class SysDepartmentVEntity
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
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 医技标志（是否是医技部门，如放射科）
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }
    }
}

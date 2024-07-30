using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///用户关系表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_C_Sys_StaffDuty", "SysStaffDutyComplexVEntity")]
    public partial class SysStaffDutyComplexVEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffPY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffGh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DutyCode { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string DutyName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

    }
}

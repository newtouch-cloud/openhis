using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// 民族
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_mz", "民族")]
    public partial class SysNationVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string mzmc { get; set; }

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

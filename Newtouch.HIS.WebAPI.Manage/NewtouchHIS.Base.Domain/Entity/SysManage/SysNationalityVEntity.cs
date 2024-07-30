using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// 系统岗位
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_gj", "国籍")]
    public partial class SysNationalityVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int gjId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string gjmc { get; set; }

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

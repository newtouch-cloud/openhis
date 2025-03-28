using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///现金支付方式表
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_xjzffs", "现金支付方式")]
    public partial class SysCashPaymentModelEntity : ISysEntity
    {
        /// <summary>
        /// 现金支付方式编号
        /// </summary>
        public int xjzffsbh { get; set; }

        /// <summary>
        /// 现金支付方式
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 现金支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// 账户性质
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }


    }
}

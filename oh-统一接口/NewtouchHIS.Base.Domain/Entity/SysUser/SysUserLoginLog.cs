using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///系统登录日志
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Log")]
    public class SysUserLoginLog
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 顶级组织机构Id（可以确定唯一Account）
        /// </summary>
        public string TopOrganizeId { get; set; }
        /// <summary>
        /// 医疗机构
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        /// <returns></returns>
        public DateTime? Date { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        /// <returns></returns>
        public string? Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string? NickName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        public string? Type { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        public string? IPAddress { get; set; }
        /// <summary>
        /// IP所在城市
        /// </summary>
        /// <returns></returns>
        public string? IPAddressName { get; set; }
        /// <summary>
        /// 系统模块Id
        /// </summary>
        /// <returns></returns>
        public string? ModuleId { get; set; }
        /// <summary>
        /// 系统模块
        /// </summary>
        /// <returns></returns>
        public string? ModuleName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        /// <returns></returns>
        public bool? Result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string? Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
    }
}

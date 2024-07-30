using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Log")]
    public class SysLogEntity : IEntity<SysLogEntity>
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// IP所在城市
        /// </summary>
        public string IPAddressName { get; set; }

        /// <summary>
        /// 系统模块Id
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 系统模块
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}

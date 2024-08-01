using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统日志
    /// </summary>
    [Table("Sys_Log")]
    public class SysLogEntity : IEntity<SysLogEntity>
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        /// <returns></returns>
        public DateTime? Date { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        public string IPAddress { get; set; }
        /// <summary>
        /// IP所在城市
        /// </summary>
        /// <returns></returns>
        public string IPAddressName { get; set; }
        /// <summary>
        /// 系统模块Id
        /// </summary>
        /// <returns></returns>
        public string ModuleId { get; set; }
        /// <summary>
        /// 系统模块
        /// </summary>
        /// <returns></returns>
        public string ModuleName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        /// <returns></returns>
        public bool? Result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：系统人员
    /// </summary>
    [Table("Sys_Staff")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// gh
        /// </summary>
        /// <returns></returns>
        public string gh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// py
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// DepartmentCode
        /// </summary>
        /// <returns></returns>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// zc
        /// </summary>
        /// <returns></returns>
        public string zc { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        public string HeadIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public bool? Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <returns></returns>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        /// <returns></returns>
        public string WeChat { get; set; }
        /// <summary>
        /// 主管主键
        /// </summary>
        /// <returns></returns>
        public string ManagerCode { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
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
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}
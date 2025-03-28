using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统人员
    /// </summary>
    [Table("Sys_Staff")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（科室所对应的OrganizeId）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        public string zjlx { get; set; }

        public string zjh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 部门Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string zc { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIcon { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 主管主键
        /// </summary>
        public string ManagerCode { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }


        /// <summary>
        /// 套餐权限（根据枚举）
        /// </summary>
        public string mbqx { get; set; }
        /// <summary>
        /// 国家医师代码
        /// </summary>
        public string gjybdm { get; set; }

        /// <summary>
        /// 微软邮箱
        /// </summary>

        public string MsEmail { get; set; }

		public string yszydm { get; set; }

	}
}

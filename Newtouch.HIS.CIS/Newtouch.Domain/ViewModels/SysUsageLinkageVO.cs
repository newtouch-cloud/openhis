using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// 用法联动绑定
    /// </summary>
    public class SysUsageLinkageVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 项目大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }
    }
}

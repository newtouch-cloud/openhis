using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 住院计费项目Dto
    /// </summary>
    public class HosFeeItemInfoDto
    {
        /// <summary>
        /// 项目计费表编号
        /// </summary>
        public int? xmjfbbh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 创建日期（执行日期）
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssrymc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ssrq { get; set; }

        public decimal je { get; set; }
    }
}

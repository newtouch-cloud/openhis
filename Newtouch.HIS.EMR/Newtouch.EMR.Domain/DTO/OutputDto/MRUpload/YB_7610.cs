using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// 住院病案首页诊断信息
    /// </summary>
    public class YB_7610
    {
        /// <summary>
        /// 就诊流水号
        /// 1
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 病案流水号
        /// 1
        /// </summary>
        public string BKF303 { get; set; }
        /// <summary>
        /// 住院流水号
        /// 1
        /// </summary>
        public string BKC191 { get; set; }
        /// <summary>
        /// 主从诊断标识 1 主要诊断；2 其他诊断
        /// 1
        /// </summary>
        public string BKF718 { get; set; }
        /// <summary>
        /// 诊断顺位
        /// 1
        /// </summary>
        public string BKF512 { get; set; }
        /// <summary>
        /// 西医诊断编码
        /// </summary>
        public string BKF857 { get; set; }
        /// <summary>
        /// 西医诊断名称
        /// </summary>
        public string BKF856 { get; set; }
        /// <summary>
        /// 中医病名代码
        /// </summary>
        public string BKF460 { get; set; }
        /// <summary>
        /// 中医病名
        /// </summary>
        public string BKF459 { get; set; }
        /// <summary>
        /// 中医证候代码
        /// </summary>
        public string BKF475 { get; set; }
        /// <summary>
        /// 中医证候
        /// </summary>
        public string BKF474 { get; set; }
        /// <summary>
        /// 入院病情代码
        /// </summary>
        public string BKF262 { get; set; }
    }
}

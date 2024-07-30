using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class YB_7110
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        /// <returns></returns>
        public string AKC190 { get; set; }

        /// <summary>
        /// 入院记录流水号
        /// </summary>
        /// <returns></returns>
        public string BKF263 { get; set; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        public string BKC191 { get; set; }

        /// <summary>
        /// 入院诊断分类
        /// </summary>
        /// <returns></returns>
        public string BKF260 { get; set; }

        /// <summary>
        /// 主从诊断标识1 主要诊断；2 其他诊断；3 病理诊断
        /// </summary>
        /// <returns></returns>
        public string BKF718 { get; set; }

        /// <summary>
        /// 诊断顺位
        /// </summary>
        /// <returns></returns>
        public string BKF512 { get; set; }

        /// <summary>
        /// 西医诊断编码
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// 西医诊断名称
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// 中医病名代码
        /// </summary>
        /// <returns></returns>
        public string BKF460 { get; set; }

        /// <summary>
        /// 中医病名
        /// </summary>
        /// <returns></returns>
        public string BKF459 { get; set; }

        /// <summary>
        /// 中医证候代码
        /// </summary>
        /// <returns></returns>
        public string BKF475 { get; set; }

        /// <summary>
        /// BKF474
        /// </summary>
        /// <returns></returns>
        public string BKF474 { get; set; }

        /// <summary>
        /// 诊断日期时间
        /// </summary>
        /// <returns></returns>
        public string BKF510 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 住院患者信息
    /// </summary>
    public class InHosPatientVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织结构id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime birth { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedCode { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime ryrq { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        public string cardno { get; set; }
    }
}

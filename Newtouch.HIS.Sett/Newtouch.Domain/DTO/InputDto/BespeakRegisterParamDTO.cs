using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public class BespeakRegisterParamDTO
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 门诊挂号科室
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 门诊挂号类型 1:普通门诊  2:急诊  3:专家门诊
        /// </summary>
        public int mzlx { get; set; }

        /// <summary>
        /// 专家挂号 医生工号
        /// </summary>
        public string ysgh { get; set; }
    }
}

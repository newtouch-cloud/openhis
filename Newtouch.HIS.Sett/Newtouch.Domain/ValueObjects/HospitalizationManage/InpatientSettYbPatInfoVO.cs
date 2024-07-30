using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院医保结算 患者信息
    /// </summary>
    public class InpatientSettYbPatInfoVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string jzbh { get; set; }
        /// <summary>
        /// 社保编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 分中心编号
        /// </summary>
        public string fzxbh { get; set; }
        /// <summary>
        /// 支付类别
        /// </summary>
        public string zhifuleibie { get; set; }
        /// <summary>
        /// 社保办法
        /// </summary>
        public string sbbf { get; set; }
    }
}

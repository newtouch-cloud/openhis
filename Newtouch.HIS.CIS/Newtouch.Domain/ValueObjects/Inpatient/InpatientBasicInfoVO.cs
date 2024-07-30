using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InpatientBasicInfoVO
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string zhcode { get; set; }
        public string ryrq { get; set; }
        public string cyrq { get; set; }
        public int? zybz { get; set; }
        /// <summary>
        /// 账户性质
        /// </summary>
        public int? zhxz { get; set; }
        /// <summary>
        /// 预交金余额
        /// </summary>
        public string zhye { get; set; }
        /// <summary>
        /// 发生的总费用
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// 已结费用
        /// </summary>
        public decimal? yjfy { get; set; }
        /// <summary>
        /// 预交金报警额
        /// </summary>
        public decimal? bje { get; set; }
        /// <summary>
        /// 医保现金支付
        /// </summary>
        public decimal? ybxjzf { get; set; }
        public List<InpatientBedUseInfoVO> patTransList { get; set; }

    }
}

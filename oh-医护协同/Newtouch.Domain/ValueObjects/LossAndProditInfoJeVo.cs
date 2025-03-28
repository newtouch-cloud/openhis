using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class LossAndProditInfoJeVo
    {
        /// <summary>
        /// 零售价总金额
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// 批发价总金额
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// 进价总金额
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}

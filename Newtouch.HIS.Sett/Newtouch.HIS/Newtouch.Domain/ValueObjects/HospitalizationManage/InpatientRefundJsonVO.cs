using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientRefundJsonVO
    {
        /// <summary>
        /// 是否是药品 0：非 1：是
        /// </summary>
        public string isYp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jfbbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? tsl { get; set; }
        /// <summary>
        /// 组套的计费编号
        /// </summary>
        public string jfbbhs { get; set; }
    }
}

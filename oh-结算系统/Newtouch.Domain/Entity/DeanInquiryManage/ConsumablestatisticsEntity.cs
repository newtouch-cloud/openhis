using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class ConsumablestatisticsEntity
    {
        public string sfxmmc { get; set; }
        public string sfxmCode { get; set; }
        public string gg { get; set; }
        public string dw { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal sl { get; set; }
        [DecimalPrecision(11, 4)]
        public decimal zje { get; set; }
    }


}

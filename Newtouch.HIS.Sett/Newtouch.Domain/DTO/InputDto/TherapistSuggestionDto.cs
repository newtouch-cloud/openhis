using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// 门诊住院患者信息
    /// </summary>
    public class MzZyPatInfoDto
    {
        public string mzzyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public int nl { get; set; }
        public string ryzd { get; set; }
        public string zjh { get; set; }
        public string phone { get; set; }
        public DateTime csny { get; set; }
        public DateTime? ryrq { get; set; }
    }

    public class TherapistAdviceDto
    {
        public string jyId { get; set; }
        public string blh { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public decimal? mczll { get; set; }
        public decimal? sl { get; set; }
        public string pc { get; set; }
        public string pcmc { get; set; }
        public string zxksdm { get; set; }
        public string zxksmc { get; set; }
        public string bz { get; set; }
        public string zhbz { get; set; }
        public string bw { get; set; }
    }

    public class TherapistSuggestionDto
    {
        public MzZyPatInfoDto MzZyPatInfoDto { get; set; }
        public IList<TherapistAdviceDto> TherapistAdviceDto { get; set; }
    }
}

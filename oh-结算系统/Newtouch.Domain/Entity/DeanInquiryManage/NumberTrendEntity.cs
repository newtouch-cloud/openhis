using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class NumberTrendEntity
    {
        public string mount { get; set; }
        public int sl { get; set; }
    }
    public class NumberTrendList
    {
        public List<NumberTrendEntity> ryqs { get; set; }
        public List<NumberTrendEntity> cyqs { get; set; }
        public List<NumberTrendEntity> zzqs { get; set; }
    }
    public class NumberPersonEntity
    {
        public string mount { get; set; }
        public int? rysl { get; set; }
        public int? cysl { get; set; }
        public int? zysl { get; set; }
    }

    public class DeparNumberEntity
    {
        public string bqCode { get; set; }
        public string bqmc { get; set; }
        public int? rysl { get; set; }
        public int? cysl { get; set; }
        public int? zysl { get; set; }
        public int? ssrs { get; set; }
        public decimal? pjzyr { get; set; }
    }
    public class StaffNumberEntity
    {
        public string Name { get; set; }
        public int? rysl { get; set; }
        public int? cysl { get; set; }
        public int? zysl { get; set; }
        public int? ssrs { get; set; }
        public decimal? pjzyr { get; set; }
    }

}

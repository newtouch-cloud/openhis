using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects
{
    public class ScoreItemVo
    {
        public string Zyh { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? qxs { get; set; }
        public int? ysc { get; set; }
        public decimal? Score { get; set; }
        public string BlmbId { get; set; }
    }
    public class ScoreEntity
    {
        public string Zyh { get; set; }
        public decimal? Score { get; set; }
        public string BlmbId { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? qxs { get; set; }
        public int? ysc { get; set; }
        public string Code { get; set; }
    }
}
